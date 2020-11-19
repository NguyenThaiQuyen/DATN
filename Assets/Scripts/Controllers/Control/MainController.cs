using System.Collections;
using System.Collections.Generic;
using GoogleARCore;
using GoogleARCore.Examples.Common;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#if UNITY_EDITOR
// Set up touch input propagation while using Instant Preview in the editor.
using Input = GoogleARCore.InstantPreviewInput;
#endif
public class MainController : MonoBehaviour
{
    private PathController pathController;
    private QuarryController quarryController;
    private PredatorController predatorController;
    public static bool havePlane;

    float initialFingersDistance;
    Vector3 initialScale;
    
    public enum State { 
        DetectedPlane,
        RecognizationImage,
        Idle,
        GenerationPath,
        Playing,
        Reset
    }

    public State state;
    public int count = 0;
    public GameObject animalFirst = null;
    public GameObject animalSecond = null;
    
    private static MainController instance;

    public static MainController Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<MainController>();
            }
            return instance;
        }
    }

    public void Start()
    {
        state = State.DetectedPlane;
    }

    public void Update()
    {
        if (state == State.Idle) {
            Touch touch;
            if (Input.touchCount > 0) {
                touch = Input.GetTouch(0);
                TrackableHit hit;
                TrackableHitFlags raycastFilter = 
                    TrackableHitFlags.PlaneWithinBounds |
                    TrackableHitFlags.PlaneWithinPolygon;

                if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit)) {
                    Anchor anchor = hit.Trackable.CreateAnchor(hit.Pose);
                    if (Input.touchCount == 1) {
                        if (touch.phase == TouchPhase.Began) {
                            // Animal animal = new Animal(0, ManagerUIPredator.currentObject.animalName , ManagerUIPredator.currentObject.animalPrefab);
                            // PredatorController.createPredator(hit, anchor, animal, ManagerUIPredator.currentObject.animalPrefab);
                            // switch(state) {
                            //     case State.GenerationPredator: {
                            //         if (PredatorController.currentPredatorUnity == null) {
                            //             Animal animal = new Animal(0, ManagerUIPredator.currentObject.animalName , ManagerUIPredator.currentObject.animalPrefab);
                            //             PredatorController.createPredator(hit, anchor, animal, ManagerUIPredator.currentObject.animalPrefab);
                            //             state = State.GenerationQuarry;
                            //         } 
                            //         break;
                            //     }
                            //     case State.GenerationQuarry: {
                            //         if (QuarryController.currentQuarryUnity == null) {
                            //             Animal animal = new Animal(1, ManagerUIQuarry.currentObject.animalName , ManagerUIQuarry.currentObject.animalPrefab);
                            //             QuarryController.createQuarry(hit, anchor, animal, ManagerUIQuarry.currentObject.animalPrefab);
                            //         }
                            //         break;   
                            //     }
                            //     case State.GenerationPath: {
                            //         pathController.CreatePath();
                            //         break;
                            //     }
                            //     case State.Playing: {
                            //         quarryController.Moving();
                            //         //predatorController.Moving();
                            //         break;
                            //     }
                            // }
                        }
                    } 
                    else if (Input.touchCount == 2)
                        {
                            // Scale object
                            Touch firstFinger = Input.touches[0];
                            Touch secondFinger = Input.touches[1];
                            if (firstFinger.phase == TouchPhase.Began || secondFinger.phase == TouchPhase.Began)
                            {
                                initialFingersDistance = Vector2.Distance(firstFinger.position, secondFinger.position);
                                initialScale = instance.transform.localScale;
                            }
                            else if(firstFinger.phase == TouchPhase.Moved || secondFinger.phase == TouchPhase.Moved)
                            {
                                var currentFingersDistance = Vector2.Distance(firstFinger.position, secondFinger.position);
                                var scaleFactor = currentFingersDistance / initialFingersDistance;
                                instance.transform.localScale = initialScale * scaleFactor;
                            }
                        }
                }
            }
        }
    }
}