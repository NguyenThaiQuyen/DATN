using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using GoogleARCore;
using System.IO;
public class ServerCommunication : MonoBehaviour
{
    public string response;
    public void SendRequest(string url, WWWForm data)
    {
        StartCoroutine(RequestCoroutine(url, data));
    }

    private IEnumerator RequestCoroutine(string url, WWWForm data)
    {
        UnityWebRequest request = UnityWebRequest.Post(url, data);
        yield return request.SendWebRequest();
      
        if (request.isNetworkError || request.isHttpError) {
            Debug.Log("Error " + request.error);
            response = "";
        }
        Debug.Log("Result: " + request.downloadHandler.text);
        response = request.downloadHandler.text;
    }
}
