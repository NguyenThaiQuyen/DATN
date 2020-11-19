using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

#if UNITY_EDITOR
     using Input = GoogleARCore.InstantPreviewInput;
#endif
public class SceneController : MonoBehaviour
{
    public Camera firstPersonCamera;
    //public SnakeController snakeController;
    //public GameObject snakeHeadPrefab;
    public PointerController pointerController;
    public static List<GameObject> pathMarkers;
    public static List<Vector2> listPoint;
    // Start is called before the first frame update
    void Start()
    {
        QuitOnConnectionErrors();
    }

    // Update is called once per frame
    void Update()
    {
        // The session status must be Tracking in order to access the Frame.
        if (Session.Status != SessionStatus.Tracking)
        {
            int lostTrackingSleepTimeout = 15;
            Screen.sleepTimeout = lostTrackingSleepTimeout;
            return;
        }
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        ProcessTouches();
    }

    void QuitOnConnectionErrors() {
        if (Session.Status ==  SessionStatus.ErrorPermissionNotGranted)
        {
            StartCoroutine(CodelabUtils.ToastAndExit(
                "Camera permission is needed to run this application.", 5));
        }
        else if (Session.Status.IsError())
        {
            // This covers a variety of errors.  See reference for details
            // https://developers.google.com/ar/reference/unity/namespace/GoogleARCore
            StartCoroutine(CodelabUtils.ToastAndExit(
                "ARCore encountered a problem connecting. Please restart the app.", 5));
        }
    }

    void ProcessTouches() {
        Touch touch;
        if (Input.touchCount > 0) {
            if (Input.touchCount != 1 || 
                (touch = Input.GetTouch(0)).phase != TouchPhase.Began) {
                return;
            }
            touch  = Input.GetTouch(0);

            TrackableHit hit;
            TrackableHitFlags raycastFilter = 
                TrackableHitFlags.PlaneWithinBounds |
                TrackableHitFlags.PlaneWithinPolygon;

            if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit)) {
                var anchor = hit.Trackable.CreateAnchor(hit.Pose);
                SetSelectedPlane(hit, anchor);
            }
        }
    }

    void SetSelectedPlane(TrackableHit hit, Anchor anchor) {
        //scoreboard.SetSelectedPlane(selectedPlane);
        //Instantiate (snakeHeadPrefab, hit.Pose.position, hit.Pose.rotation);
        // pointerController.SpawnPointer(hit, anchor, true);
        if (pathMarkers.Count > 0) {
            pointerController.SpawnPointer(hit, anchor, true);
        } else {
            pointerController.SpawnPointer(hit, anchor, false);
        }
        //GetComponent<FoodController>().SetSelectedPlane(selectedPlane);
    }
}
