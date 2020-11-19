using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal
{
    private float speed;
    private string name;
    private int type;
    private GameObject prefab;

    public Animal(int _type, string _name, GameObject _prefab, float _speed = 0.01f) {
        speed = _speed;
        type = _type;
        name = _name;
        prefab = _prefab;
    }

    public float Speed 
    {
        get { return speed; } 
        set { speed = value; }
    }
    public string Name 
    {
        get { return name; } 
        set { name = value; }
    }

    public int Type 
    {
        get { return type; } 
        set { type = value; }
    }

    public GameObject Prefab 
    {
        get { return prefab; } 
        set { prefab = value; }
    }
    
    public void Move() {

    }

    public void Delete() {

    }
}
