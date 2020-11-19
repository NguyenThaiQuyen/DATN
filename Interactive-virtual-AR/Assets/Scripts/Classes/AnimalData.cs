using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Animal1", menuName="AddAnimal/AnimalData")]
public class AnimalData : ScriptableObject
{
    public GameObject animalPrefab;
    public string animalName;
}
