using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using GoogleARCore;
using System.IO;

public class ManagerHandleUI : MonoBehaviour
{
    private GameObject btnCompleted;
    private GameObject btnBack;
    private GameObject btnGotIt;
    private GameObject sliderPredator;
    private GameObject sliderQuarry;
    private GameObject panelInform;
    private GameObject btnCreatePath;
    private GameObject btnPlay;
    private GameObject btnHidden;
    private GameObject btnRecognize;
    private GameObject panelFrame;
    private GameObject txtPopUp;
    private GameObject btnStart;
    private GameObject btnNext;

    private bool isCompleted = false;
    private bool success = false;
    private string nameAnimal = "";
    
    void Start()
    {
        initUI();
    }

    void Update()
    {
        if (DetectedPlanes.havePlane) {
            displayObject(btnHidden);
        }

        if (MainController.Instance.state == MainController.State.RecognizationImage &&
             MainController.Instance.animalFirst == null) {
            displayObject(panelFrame);
        }


        // if (MainController.state == MainController.State.Playing) {
        //     if (QuarryController.currentQuarryUnity != null && PredatorController.currentPredatorUnity != null) {
        //         sliderOfQua.SetActive(true);
        //         sliderOfQua.GetComponent<Slider>().value = QuarryController.animalCurrentQuarry.Speed * 100;
        //         sliderOfPre.SetActive(true);
        //         sliderOfPre.GetComponent<Slider>().value = PredatorController.animalCurrentPredator.Speed * 100;
        //     }
        // }
    }

    public void handleBtnCompleted() {
        hiddenObject(btnCompleted);
        MainController.Instance.state = MainController.State.Idle;
        displayObject(btnStart);
    }

    public void handeBtnCreatePath() {
        MainController.Instance.state = MainController.State.GenerationPath;
        hiddenObject(btnCreatePath);
        displayObject(btnCompleted);
    }

    public void handeBtnGotIt() {
        MainController.Instance.state = MainController.State.SetUpObject;
        hiddenObject(panelInform);
    }

    public void handleBtnStart() {
        MainController.Instance.state = MainController.State.Playing;
        hiddenObject(btnStart);
    }

    public void handleBtnHidden() {
        // MainController.Instance.state = MainController.Instance.State.Playing;
        // hiddenObject(btnStart);
    }

    public void handleBtnNext() {
        displayObject(panelFrame);
    }

    public void handleBtnRecognize() {
        Data data = new Data(GetFrame.Instance.GetImage());
        string dataSend = data.SaveToString();
        StartCoroutine(Upload(dataSend));
        if (MainController.Instance.state == MainController.State.RecognizationImage 
        && count == 1) {
            displayObject(btnNext);
        } else {
            MainController.Instance.state = MainController.State.Idle;
        }
    }

    IEnumerator Upload(string data)
    {       
        WWWForm formData = new WWWForm();
        formData.AddField("body", data);
        UnityWebRequest request = UnityWebRequest.Post(ServerConfig.SERVER_API_URL_FORMAT + ServerConfig.API_PREDICT, formData);
        yield return request.SendWebRequest();
      
        if (request.isNetworkError || request.isHttpError) {
            Debug.Log("Error" + request.error);
            txtPopUp.GetComponent<UnityEngine.UI.Text>().text = request.error.ToString();
            displayObject(panelInform);
        } else {
            Debug.Log("Result: " + request.downloadHandler.text);
            if (MainController.Instance.count == 0) {
                MainController.Instance.animalFrist = HandleCommon.newAnimal(request.downloadHandler.text);
                MainController.Instance.count++;
            } else {
                MainController.Instance.animalSecond = HandleCommon.newAnimal(request.downloadHandler.text);
                MainController.Instance.count--;
            }
        }            
    }

    // public void changeValueSliderOfQuarry(float value) {
    //     QuarryController.changeSpeedOfQuarry(value/100);
    // }

    // public void changeValueSliderOfPredator(float value) {
    //     PredatorController.changeSpeedOfPredator(value/100);
    // }

    public void initUI() {
        btnCompleted = GameObject.Find("btnCompleted");
        sliderPredator = GameObject.Find("sliderPredator");
        sliderQuarry = GameObject.Find("sliderQuarry");
        panelInform = GameObject.Find("panelInform");
        btnCreatePath = GameObject.Find("btnCreatePath");
        btnHidden = GameObject.Find("btnHidden");
        btnPlay = GameObject.Find("btnPlay");
        btnRecognize = GameObject.Find("btnRecognize");
        panelFrame = GameObject.Find("panelFrame");
        txtPopUp = GameObject.Find("txtPopUp");
        btnStart = GameObject.Find("btnStart");
        btnNext = GameObject.Find("btnNext");

        hiddenObject(btnCompleted);
        hiddenObject(sliderPredator);
        hiddenObject(sliderQuarry);
        hiddenObject(panelInform);
        hiddenObject(btnCreatePath);
        hiddenObject(btnPlay);
        hiddenObject(btnHidden);
        hiddenObject(btnRecognize);
        hiddenObject(panelFrame);
        hiddenObject(btnStart);
        hiddenObject(btnNext);
    }

    public void hiddenObject(GameObject gameObject) {
        gameObject.SetActive(false);
    }

    public void displayObject(GameObject gameObject) {
        gameObject.SetActive(true);
    }
}