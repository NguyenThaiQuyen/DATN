using System.Collections;
using System.Collections.Generic;
using GoogleARCore;
using UnityEngine;

public class HandleCommon : MonoBehaviour
{
    private const float k_PrefabRotation = 180.0f;

    public static GameObject InstanceNewObject(TrackableHit hit, Anchor anchor, GameObject prefab) {
        GameObject instance;
        instance = Instantiate (prefab, hit.Pose.position, hit.Pose.rotation);
        instance.transform.Rotate(0, k_PrefabRotation, 0, Space.Self);
        instance.transform.parent = anchor.transform; 
        return instance;
    }

    public static GameObject newAnimal(string name) {
        AnimalData animalData = Resources.Load(name, typeof(AnimalData));
        GameObject prefab = animalData.animalPrefab;
        GameObject instance;
        instance = Instantiate (prefab, Screen.width/2, Screen.height/2);
        instance.transform.Rotate(0, k_PrefabRotation, 0, Space.Self);
        return instance;
     }
}