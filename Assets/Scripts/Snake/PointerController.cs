using System.Collections;
using System.Collections.Generic;
using GoogleARCore;
using UnityEngine;

public class PointerController : MonoBehaviour
{
    private DetectedPlane detectedPlane;
    public GameObject pointerPrefab;
    public GameObject pointerStartObject;
    private GameObject pointerInstance;
    private const float k_PrefabRotation = 180.0f;

    // public GameObject pointer;
    // public Camera firstPersonCamera;
    // // Speed to move.
    // public float speed = 20f;
    public void Update()
    {
        // if (snakeInstance == null || snakeInstance.activeSelf == false) 
        // {
        //     pointer.SetActive(false);
        //     return;
        // }
        // else
        // {
        //     pointer.SetActive(true);
        // }

        // TrackableHit hit;
        // TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinBounds;    

        // if (Frame.Raycast (Screen.width/2, Screen.height/2, raycastFilter, out hit))
        // {
        // Vector3 pt = hit.Pose.position;
        // //Set the Y to the Y of the snakeInstance
        // pt.y = snakeInstance.transform.position.y;
        // // Set the y position relative to the plane and attach the pointer to the plane
        // Vector3 pos = pointer.transform.position;
        // pos.y = pt.y;
        // pointer.transform.position = pos; 

        // // Now lerp to the position                                         
        // pointer.transform.position = Vector3.Lerp (pointer.transform.position, pt,
        //     Time.smoothDeltaTime * speed);
        // }

        // float dist = Vector3.Distance (pointer.transform.position,
        // snakeInstance.transform.position) - 0.05f;
        // if (dist < 0)
        // {
        //     dist = 0;
        // }

        // Rigidbody rb = snakeInstance.GetComponent<Rigidbody> ();
        // rb.transform.LookAt (pointer.transform.position);
        // rb.velocity = snakeInstance.transform.localScale.x *
        //     snakeInstance.transform.forward * dist / .01f;
    } 

    public void SpawnPointer (TrackableHit hit, Anchor anchor, bool isPointStart)
    {
        // Not anchored, it is rigidbody that is influenced by the physics engine.
        if (isPointStart) {
            pointerInstance = Instantiate (pointerStartObject, hit.Pose.position, hit.Pose.rotation);
        } else {
            pointerInstance = Instantiate (pointerPrefab, hit.Pose.position, hit.Pose.rotation);
        }
        pointerInstance.transform.Rotate(0, k_PrefabRotation, 0, Space.Self);
        pointerInstance.transform.parent = anchor.transform;      
        SceneController.pathMarkers.Add(pointerInstance);
        // Pass the head to the slithering component to make movement work.
        //snakeInstance.AddComponent<FoodConsumer>();
    }
}
