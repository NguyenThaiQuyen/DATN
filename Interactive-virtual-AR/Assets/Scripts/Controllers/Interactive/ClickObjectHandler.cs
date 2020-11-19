using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickObjectHandler : MonoBehaviour
{
    public Camera firstPersonCamera;
    public GameObject instance;

    public static GameObject prefab = null;
    void Update() {
        // Touch touch;
        // if (Input.touchCount > 0) {
        //     touch = Input.GetTouch(0);

        //     var ray = firstPersonCamera.ScreenPointToRay(Input.mousePosition);
        //     RaycastHit hit;
        //     if (Physics.Raycast(ray, out hit)) {
        //         var selection = hit.transform;
        //         if (selection.CompareTag("Animal")) {
        //             Debug.Log("hehe " + hit.collider.gameObject.name);
        //         }
        //     }
        // }


        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Ray raycast = firstPersonCamera.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit))
            {
                if (raycastHit.collider.CompareTag("Quarry") || raycastHit.collider.CompareTag("Predator")) {
                    Vector3 centerPosition = firstPersonCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, firstPersonCamera.nearClipPlane));
                    GameObject currentObject = raycastHit.collider.gameObject;
                    currentObject.transform.position = centerPosition;
                    Vector3 tmp = currentObject.transform.localScale;
                    tmp.x += 0.01f;
                    tmp.y += 0.01f;
                    tmp.z += 0.01f;
                    currentObject.transform.localScale = tmp;
                    currentObject.transform.Rotate(Vector3.up, Time.deltaTime * 0.5f);
                }
                // if (raycastHit.collider.CompareTag("Quarry"))
                // {
                //     prefab = QuarryController.getAnimalQuarry().Prefab;
                //     //Debug.Log("hehe " + raycastHit.collider.gameObject.name);
                // } else if (raycastHit.collider.CompareTag("Predator")) {
                //     prefab = PredatorController.getAnimalPredator().Prefab;
                // }
                // Vector3 centerPosition = firstPersonCamera.ScreenToWorldPoint(new Vector3(Screen.width / 4, Screen.height / 4, firstPersonCamera.nearClipPlane));
                // instance = Instantiate(prefab, centerPosition, Quaternion.identity);
                // instance.transform.Rotate(0, 180.0f, 0, Space.Self);

            }
        }
    }
}
