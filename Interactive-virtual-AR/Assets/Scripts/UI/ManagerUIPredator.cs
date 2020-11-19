// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement;
// using UnityEngine.UI;

// public class ManagerUIPredator : MonoBehaviour
// {
//     // Start is called before the first frame update
//     private GameObject btnLeft;
//     private GameObject btnRight;
//     private GameObject txtName;
//     private GameObject plane;

//     private AudioSource[] audioSources;
//     [SerializeField] private static List<AnimalData> listPredators;
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
//         listPredators = new List<AnimalData>();
//         listPredators = HandleCommon.LoadAnimals("Predators");

//         instance = Instantiate(listPredators[index].animalPrefabModel, plane.transform.position + new Vector3(0,2,0) , plane.transform.rotation);
//         txtName.GetComponent<Text>().text = listPredators[index].animalName;
//         currentObject = listPredators[index];
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

//         if (index == listPredators.Count - 1) {
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
//         instance = Instantiate(listPredators[index].animalPrefabModel, plane.transform.position + new Vector3(0,2,0) , plane.transform.rotation);
//         txtName.GetComponent<Text>().text = listPredators[index].animalName;
//     }

//     public void handleBtnLeft() {
//         index--;
//         Destroy(instance);
//         instance = Instantiate(listPredators[index].animalPrefabModel, plane.transform.position + new Vector3(0,2,0) , plane.transform.rotation);
//         txtName.GetComponent<Text>().text = listPredators[index].animalName;
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
//             ani.Play("Base Layer.sound");
//             sound.Play();
//         } else {
//             ani.Play("Base Layer.idle");
//             sound.Stop();
//         }
//     }

//     public void handleBtnAction() {
//         ani.Play("Base Layer.run");
//     }

//     public void handleBack() {
//         SceneManager.LoadScene("SelectQuarry");
//     }

//     public void handleNext() {
//         SceneManager.LoadScene("Virtual-interaction");
//     }

//     public void handleSelect() {
//         currentObject = listPredators[index];
//         SceneManager.LoadScene("Virtual-interaction");
//     }
// }
