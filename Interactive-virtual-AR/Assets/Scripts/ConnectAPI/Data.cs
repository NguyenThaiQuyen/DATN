using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class Data : MonoBehaviour
{
    public byte[] data;

    public string SaveToString()
    {
        return JsonUtility.ToJson(this);
    }

    public Data(byte[] _data) {
        data = _data;
    }
}
