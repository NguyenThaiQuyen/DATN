using System.Collections;
using System.Collections.Generic;
using GoogleARCore;
using UnityEngine;

public class PredatorController : MonoBehaviour
{
    public static GameObject currentPredatorUnity = null;
    public static Animal animalCurrentPredator;
    public static Vector3 endPoint;
    private static float startTime;
    private static float journeyLength;
    private static int indexPoint = 0;
    private static Animator animalAnimator;
    public static Vector3 currentPosition;
    private bool isRunByQuarry = true;

    void Start() {
      journeyLength=0;
      animalAnimator = GetComponent<Animator>();
      isRunByQuarry = true;
    } 
    void Update() {
      if (MainController.Instance.state == MainController.State.Playing && QuarryController.isRunning) {
          Moving();
        }
    }

    public GameObject getCurrentOfQuarry() {
        return currentPredatorUnity;
    }

    public static Animal getAnimalQuarry() {
        return animalCurrentPredator;
    }

    public void Moving() {
      animalAnimator.SetBool("IsWalking",true);
     
      if (isRunByQuarry) {
          endPoint = QuarryController.currentPosition;
          isRunByQuarry = false;
      } else {
          endPoint = PathController.getPoints()[indexPoint];
      }
      startTime = Time.time;
      journeyLength = Vector3.Distance(transform.position, endPoint);

      if(journeyLength > 0) {
        float distCovered = (Time.time - startTime) * animalCurrentPredator.Speed;
        float fracJourney = distCovered / journeyLength;
        // transform.position = Vector3.Lerp(startPoint.position, endPoint, fracJourney);
        // Debug.Log("hehe" + this.transform.position);
        float step =  animalCurrentPredator.Speed * Time.deltaTime; // calculate distance to move
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

        isRunByQuarry = false;
      }
    }

    public static void changeSpeedOfPredator(float value) {
      Debug.Log("hehe" + value);
      animalCurrentPredator.Speed = value;
    }
    public static void createPredator(TrackableHit hit, Anchor anchor, Animal newAnimal, GameObject prefab) {
        animalCurrentPredator = newAnimal;
        currentPredatorUnity = HandleCommon.InstanceNewObject(hit, anchor, prefab);
        currentPosition = currentPredatorUnity.transform.position;
    }
}
