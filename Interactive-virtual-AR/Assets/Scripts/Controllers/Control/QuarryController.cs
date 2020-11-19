using System.Collections;
using System.Collections.Generic;
using GoogleARCore;
using UnityEngine;

public class QuarryController : MonoBehaviour
{
    public static GameObject currentQuarryUnity = null;
    public static Animal animalCurrentQuarry;
    public static Vector3 endPoint;
    private static float startTime;
    private static float journeyLength;
    private static int indexPoint = 0;
    private static Animator animalAnimator;
    public static Vector3 currentPosition;

    public static bool isRunning = false;

    void Start() {
      journeyLength=0;
      animalAnimator = GetComponent<Animator>();
      currentPosition = transform.position;
    } 
    void Update() {
      if (MainController.Instance.state == MainController.State.Playing) {
        Moving();
        isRunning = true;
      }
    }

    public GameObject getCurrentOfQuarry() {
        return currentQuarryUnity;
    }

    public static Animal getAnimalQuarry() {
        return animalCurrentQuarry;
    }

    public void Moving() {
      animalAnimator.SetBool("IsWalking",true);
      endPoint = PathController.getPoints()[indexPoint];
      startTime = Time.time;
      journeyLength = Vector3.Distance(transform.position, endPoint);

      if(journeyLength > 0) {
        float distCovered = (Time.time - startTime) * animalCurrentQuarry.Speed;
        float fracJourney = distCovered / journeyLength;
        // transform.position = Vector3.Lerp(startPoint.position, endPoint, fracJourney);
        // Debug.Log("hehe" + this.transform.position);
        float step =  animalCurrentQuarry.Speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, endPoint, step);

        if(fracJourney < 0.1){
          var lookPos = endPoint - transform.position;
          lookPos.y = 0;
          var rotation = Quaternion.LookRotation(lookPos);
          transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10f); 
        }

        if (this.transform.position == endPoint) {
            indexPoint++;
        }
        if (indexPoint ==  PathController.getPoints().Count) {
          animalAnimator.Play("Base Layer.idle");
        }
      }
    }

    public static void changeSpeedOfQuarry(float value) {
      Debug.Log("hehe" + value);
      animalCurrentQuarry.Speed = value;
    }
    public static void createQuarry(TrackableHit hit, Anchor anchor, Animal newAnimal, GameObject prefab) {
      animalCurrentQuarry = newAnimal;
      currentQuarryUnity = HandleCommon.InstanceNewObject(hit, anchor, prefab);
      currentPosition = currentQuarryUnity.transform.position;
    }
}
