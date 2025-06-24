using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Troop", menuName = "Troops")]
public class Troop : ScriptableObject
{
    public string team;
    public GameObject Prefab;
    public Material Material;
}
