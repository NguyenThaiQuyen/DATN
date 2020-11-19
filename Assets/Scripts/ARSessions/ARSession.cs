using System.Collections;
using System.Collections.Generic;
using GoogleARCore;
using UnityEngine;

public class ARSession : MonoBehaviour
{
    public static Camera m_firstPersonCamera;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        QuitOnConnectionErrors();
        _UpdateApplicationLifecycle();
    }

    private void _UpdateApplicationLifecycle()
    {
        // Exit the app when the 'back' button is pressed.
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        // Only allow the screen to sleep when not tracking.
        if (Session.Status != SessionStatus.Tracking)
        {
            Screen.sleepTimeout = SleepTimeout.SystemSetting;
        }
        else
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }
    }

    private void _ShowAndroidToastMessage(string message)
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity =
            unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        if (unityActivity != null)
        {
            AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaObject toastObject =
                    toastClass.CallStatic<AndroidJavaObject>(
                        "makeText", unityActivity, message, 0);
                toastObject.Call("show");
            }));
        }
    }

    private void _DoQuit()
    {
        {
            Application.Quit();
        }
    }
    void QuitOnConnectionErrors()
    {
        if (Session.Status ==  SessionStatus.ErrorPermissionNotGranted)
        {
            _ShowAndroidToastMessage(
                "ARCore encountered a problem connecting.  Please start the app again.");
            Invoke("_DoQuit", 0.5f);
        }
        else if (Session.Status.IsError())
        {
            _ShowAndroidToastMessage(
                "ARCore encountered a problem connecting.  Please start the app again.");
            Invoke("_DoQuit", 0.5f);
        }
    }

    public static void resetSession() {
        ARCoreSession session = GameObject.Find ("ARCore Device").GetComponent<ARCoreSession> ();
        ARCoreSessionConfig myConfig = session.SessionConfig;
        DestroyImmediate (session);
    }
}
