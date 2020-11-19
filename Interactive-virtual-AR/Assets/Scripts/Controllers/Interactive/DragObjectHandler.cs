using System.Collections;
using System.Collections.Generic;
using GoogleARCore;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject objectBeingDragged;
    public static Animal animalBeingDragged;
    Vector3 startPosition;
    Vector3 mousePosition;

    public void OnBeginDrag(PointerEventData eventData)
    {
        
        objectBeingDragged = gameObject;
        startPosition = transform.position;
        //animalBeingDragged = ManagerHandleUI.getAnimal(objectBeingDragged.name);
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        mousePosition = Input.mousePosition;
        transform.position = mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {    
        TrackableHit hit;
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinBounds |
            TrackableHitFlags.PlaneWithinPolygon;
        if(Frame.Raycast(mousePosition.x, mousePosition.y, raycastFilter, out hit)) {
            Anchor anchor = hit.Trackable.CreateAnchor(hit.Pose);
            if (animalBeingDragged.Type == 0) {
                PredatorController.createPredator(hit, anchor, animalBeingDragged, animalBeingDragged.Prefab);
            } else {
                QuarryController.createQuarry(hit, anchor, animalBeingDragged, animalBeingDragged.Prefab);
            }
        }
        objectBeingDragged = null;
        transform.position = startPosition;
    }

}
