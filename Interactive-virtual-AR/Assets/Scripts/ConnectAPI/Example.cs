using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Example : MonoBehaviour
{
    
    void Start()
    {
        StartCoroutine(Upload());
    }

    IEnumerator Upload()
    {       
        byte[] body = new byte[] { 0x21, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };
        Data data = new Data(body);
        string dataSend = data.SaveToString();
             
        WWWForm formData = new WWWForm();
        formData.AddField("body", dataSend);
        UnityWebRequest request = UnityWebRequest.Post("http://192.168.43.197:5555/predict", formData);

        yield return request.SendWebRequest();
      
        if (request.isNetworkError || request.isHttpError) {
            Debug.Log("hehe" + request.error);
        } else {
            Debug.Log("Result: " + request.downloadHandler.text);
        }            
    }
}


