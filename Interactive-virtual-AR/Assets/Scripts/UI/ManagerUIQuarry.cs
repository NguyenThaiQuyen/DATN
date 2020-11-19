// using System.Collections;
// using System.Collections.Generic;
// using TMPro;
// using UnityEngine;
// using UnityEngine.SceneManagement;
// using UnityEngine.UI;

// public class ManagerUIQuarry : MonoBehaviour
// {
//     private GameObject btnLeft;
//     private GameObject btnRight;
//     private GameObject txtName;
//     private GameObject plane;

//     private AudioSource[] audioSources;
//     [SerializeField] private static List<AnimalData> listQuarries;
//     public static AnimalData currentObject = null;
//     public GameObject instance;
//     public static int index = 0;

//     AudioSource sound;
//     AudioSource spell;
//     Animator ani;
    
//     void Start()
//     {
//         btnLeft = GameObject.Find("btnLeft");
//         btnRight = GameObject.Find("btnRight");
//         plane = GameObject.Find("posModel");
//         txtName = GameObject.Find("txtName");
//         listQuarries = new List<AnimalData>();
//         listQuarries = HandleCommon.LoadAnimals("Quarries");

//         instance = Instantiate(listQuarries[index].animalPrefabModel, plane.transform.position + new Vector3(0,2,0) , plane.transform.rotation);
//         txtName.GetComponent<Text>().text = listQuarries[index].animalName;
//         currentObject = listQuarries[index];
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         instance.transform.rotation = plane.transform.rotation;
//         if (index == 0) {
//             btnLeft.SetActive(false);
//         } else {
//             btnLeft.SetActive(true);
//         }

//         if (index == listQuarries.Count - 1) {
//             btnRight.SetActive(false);
//         } else {
//             btnRight.SetActive(true);
//         }
//         audioSources = instance.GetComponents<AudioSource> ();
//         ani = instance.GetComponent<Animator>();
//         ani.SetBool("IsWalking", false);
//         sound = audioSources [0];
//         spell = audioSources [1];

//     }

//     public void handleBtnRight() {
//         index++;
//         Destroy(instance);
//         instance = Instantiate(listQuarries[index].animalPrefabModel, plane.transform.position + new Vector3(0,2,0) , plane.transform.rotation);
//         txtName.GetComponent<Text>().text = listQuarries[index].animalName;
//     }

//     public void handleBtnLeft() {
//         index--;
//         Destroy(instance);
//         instance = Instantiate(listQuarries[index].animalPrefabModel, plane.transform.position + new Vector3(0,2,0) , plane.transform.rotation);
//         txtName.GetComponent<Text>().text = listQuarries[index].animalName;
//     }

//     public void handleBtnSpell() {
//         if (!spell.isPlaying) {
//             spell.Play();
//         } else {
//             spell.Stop();
//         }
//     }

//     public void handleBtnIdle() {
//        ani.Play("Base Layer.idle");
//     }

//     public void handleBtnSound() {
//         if (!sound.isPlaying) {
//             sound.Play();
//         } else {
//             sound.Stop();
//         }
//     }

//     public void handleBtnAction() {
//         ani.Play("Base Layer.run");
//     }

//     public void handleBack() {
//         Screen.orientation = ScreenOrientation.Portrait;
//         SceneManager.LoadScene("Start");
//     }

//     public void handleNext() {
//         SceneManager.LoadScene("SelectPredator");
//     }

//     public void handleSelect() {
//         currentObject = listQuarries[index];
//         SceneManager.LoadScene("SelectPredator");
//     }

// }
