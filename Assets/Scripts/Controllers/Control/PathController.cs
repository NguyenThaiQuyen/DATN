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
[RequireComponent(typeof(LineRenderer))]
public class PathController : MonoBehaviour
{
    public GameObject firstPointPrefab;
    public GameObject pointPrefab;
    private GameObject m_prefab;
    private static List<Vector3> pathPoints = new List<Vector3>();

    void Start() {
    }

    void Update() {
        if (MainController.Instance.state == MainController.State.GenerationPath) {
            CreatePath();
        }  
    }

    public void CreatePath() {
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
                        if (pathPoints.Count == 0) {
                            m_prefab = firstPointPrefab;
                        } else {
                            m_prefab = pointPrefab;
                        }
                        HandleCommon.InstanceNewObject(hit, anchor, m_prefab);
                        pathPoints.Add(hit.Pose.position);
                        UpdateLineRenderer();
                    }
                }
            }
        }
    }

    public static List<Vector3> getPoints() {
        return pathPoints;
    }

    private void UpdateLineRenderer()
    {
        LineRenderer renderer = GetComponent<LineRenderer> ();
        renderer.SetWidth(0.05f, 0.05f);
        renderer.positionCount = pathPoints.Count;
        renderer.SetPositions(pathPoints.ToArray());  
    }

}
