// namespace GoogleARCore.Examples.HelloAR
// {
//     using System.Collections;
//     using System.Collections.Generic;
//     using GoogleARCore;
//     using GoogleARCore.Examples.Common;
//     using UnityEngine;
//     using UnityEngine.EventSystems;
//     using UnityEngine.UI;

// #if UNITY_EDITOR
//     // Set up touch input propagation while using Instant Preview in the editor.
//     using Input = InstantPreviewInput;
// #endif
// [RequireComponent(typeof(LineRenderer))]
//     public class HelloARController : MonoBehaviour
//     {
//         public Camera FirstPersonCamera;
//         private int numberOfAnimalsCurrent = 0;
//         // public GameObject gameObjectPrefabFirst;
//         // public GameObject gameObjectPrefabSecond;
//         private const float k_PrefabRotation = 180.0f;
//         // private GameObject firstObject;
//         // private GameObject secondObject;
//         private GameObject m_prefab;
//         private GameObject m_instance;
//         public GameObject pointerStartPrefab;
//         public GameObject demoPredatorPrefab;
//         public GameObject demoQuarryPrefab;
//         public GameObject pointerPrefab;
//         private GameObject quarryInstance;
//         private GameObject predatorInstance;
//         private GameObject btnCompleted;
//         private List<GameObject> pathMarkers = new List<GameObject>();
//         private static List<Vector3> pathPoints = new List<Vector3>();
//         private QuarryController quarryController;

//         private GameObject sliderOfPre;
//         private GameObject sliderOfQua;
//         // private GameObject btnQuarry;

//         private int index = 0;

//         public enum State
//         {
//             GenerationOfPath,
//             GenerationOfPredator,
//             GenerationOfQuarry
//         };
//         public static State state;

//         static public int objectIndexToMove = 0;
//         public void Start()
//         {
//             // QuitOnConnectionErrors();
//             // btnCompleted = GameObject.Find("btnCompleted");
//             // btnCompleted.SetActive(false);
//             // sliderOfPre = GameObject.Find("sliderPre");
//             // sliderOfPre.SetActive(false);
//             // sliderOfQua = GameObject.Find("sliderQua");
//             // sliderOfQua.SetActive(false);
//             // state = State.GenerationOfPath;
//             // btnPredator = GameObject.Find("btnPredator");
//             // btnQuarry = GameObject.Find("btnQuarry");
//         }
//         public void Update()
//         {
//             _UpdateApplicationLifecycle();
//             ProcessTouches();
//         }

//         void ProcessTouches ()
//         {
           
//         // if (touch.phase == TouchPhase.Began && touch.phase != TouchPhase.Moved) {
//         //     if (firstObject == null ) {
//         //         firstObject = Instantiate(gameObjectPrefabFirst, hit.Pose.position, hit.Pose.rotation);
//         //         firstObject.transform.Rotate(0, k_PrefabRotation, 0, Space.Self);
//         //         firstObject.transform.parent = anchor.transform;      
//         //     } else if (secondObject == null) {
//         //         secondObject = Instantiate(gameObjectPrefabSecond, hit.Pose.position, hit.Pose.rotation);
//         //         secondObject.transform.Rotate(0, k_PrefabRotation, 0, Space.Self);
//         //         secondObject.transform.parent = anchor.transform;      
//         //     }
//         // } else if (touch.phase == TouchPhase.Moved) {  
//         //     firstObject.transform.position = Vector3.Lerp(firstObject.transform.position, hit.Pose.position, Time.time);        

//             Touch touch, secondTouch;
//             if (Input.touchCount > 0) {
//                 touch  = Input.GetTouch(0);
//                 TrackableHit hit;
//                 TrackableHitFlags raycastFilter = 
//                     TrackableHitFlags.PlaneWithinBounds |
//                     TrackableHitFlags.PlaneWithinPolygon;

//                 if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit)) {
//                     Anchor anchor = hit.Trackable.CreateAnchor(hit.Pose);
//                     // if (Input.touchCount == 1) {
//                     //     if (touch.phase == TouchPhase.Began) {
//                     //         switch (state)
//                     //         {
//                     //             case State.GenerationOfPath: {
//                     //                 if (pathMarkers.Count == 0) {
//                     //                     m_prefab = pointerStartPrefab;
//                     //                 } else {
//                     //                     m_prefab = pointerPrefab;
//                     //                 }

//                     //                 m_instance = instanceObject(hit, anchor, m_prefab);

//                     //                 pathPoints.Add(hit.Pose.position);
//                     //                 pathMarkers.Add (m_instance);
//                     //                 if (pathPoints.Count > 1) {
//                     //                     btnCompleted.SetActive(true);
//                     //                     btnCompleted.GetComponent<Button>().interactable = true;
//                     //                 }
//                     //                 UpdateLineRenderer();
//                     //                 break;
//                     //             }
//                     //             case State.GenerationOfPredator: {
//                     //                 m_prefab = demoPredatorPrefab;
//                     //                 predatorInstance = instanceObject(hit, anchor, m_prefab);  
//                     //                 //scrollBarPre.SetActive(true);
//                     //                 //Debug.Log("hehe " + scrollBarPre.GetComponent<Scrollbar>().value);
//                     //                 state = State.GenerationOfQuarry;
//                     //                 break;
//                     //             }
                                    
//                     //             case State.GenerationOfQuarry: {
//                     //                 if (quarryInstance != null) {
//                     //                     return;
//                     //                 } else {
//                     //                     m_prefab = demoQuarryPrefab;
//                     //                     quarryInstance = instanceObject(hit, anchor, m_prefab);  
//                     //                     //scrollBarQua.SetActive(true);
//                     //                     break;
//                     //                 }
//                     //             }                         
//                     //         }
//                     //     }
//                     // } else if (Input.touchCount ==2) {
//                     //     secondTouch = Input.GetTouch(1);
//                     //     if ((touch.phase == TouchPhase.Began || secondTouch.phase == TouchPhase.Began) 
//                     //             && quarryInstance != null) {
//                     //         //quarryInstance.transform.position = pathPoints[0];
//                     //         //quarryController.movementOfQuarry(quarryInstance,pathPoints);
//                     //         // for (var i = 0; i < pathPoints.Count; i++) {
//                     //         //     quarryInstance.GetComponent<MovementCat>().StartMove(pathPoints[i]);
//                     //         //     StartCoroutine(ExecuteAfterTime(5));
//                     //         // }
//                     //         quarryInstance.GetComponent<MovementCat>().StartMove();
//                     //         // if (quarryInstance.transform.position == pathPoints[index]) {
//                     //         //     index++;
//                     //         // }
//                     //         // if (index == pathPoints.Count) {
//                     //         //     return;
//                     //         // }

//                     //     }
//                     // } else {
//                     //     //return;
//                     // }
//                 }
//             }
//         }

//         public static List<Vector3> getPoints() {
//             return pathPoints;
//         }

//         private GameObject instanceObject(TrackableHit hit, Anchor anchor, GameObject prefab) {
//             GameObject instance;
//             instance = Instantiate (prefab, hit.Pose.position, hit.Pose.rotation);
//             instance.transform.Rotate(0, k_PrefabRotation, 0, Space.Self);
//             instance.transform.parent = anchor.transform; 
//             return instance;
//         }

//         private void UpdateLineRenderer()
//         {
//             LineRenderer renderer = GetComponent<LineRenderer> ();
//             renderer.SetWidth(0.05f, 0.05f);
//             renderer.positionCount = pathPoints.Count;
//             renderer.SetPositions(pathPoints.ToArray());  
//         }

//         private void _UpdateApplicationLifecycle()
//         {
//             // Exit the app when the 'back' button is pressed.
//             if (Input.GetKey(KeyCode.Escape))
//             {
//                 Application.Quit();
//             }

//             // Only allow the screen to sleep when not tracking.
//             if (Session.Status != SessionStatus.Tracking)
//             {
//                 Screen.sleepTimeout = SleepTimeout.SystemSetting;
//             }
//             else
//             {
//                 Screen.sleepTimeout = SleepTimeout.NeverSleep;
//             }
//         }

//         private void _ShowAndroidToastMessage(string message)
//         {
//             AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
//             AndroidJavaObject unityActivity =
//                 unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

//             if (unityActivity != null)
//             {
//                 AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
//                 unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
//                 {
//                     AndroidJavaObject toastObject =
//                         toastClass.CallStatic<AndroidJavaObject>(
//                             "makeText", unityActivity, message, 0);
//                     toastObject.Call("show");
//                 }));
//             }
//         }

//         private void _DoQuit()
//         {
//             {
//                 Application.Quit();
//             }
//         }
//         void QuitOnConnectionErrors()
//         {
//             if (Session.Status ==  SessionStatus.ErrorPermissionNotGranted)
//             {
//                 _ShowAndroidToastMessage(
//                     "ARCore encountered a problem connecting.  Please start the app again.");
//                 Invoke("_DoQuit", 0.5f);
//             }
//             else if (Session.Status.IsError())
//             {
//                _ShowAndroidToastMessage(
//                     "ARCore encountered a problem connecting.  Please start the app again.");
//                 Invoke("_DoQuit", 0.5f);
//             }
//         }
//     }
    
// }
