using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Building", menuName = "Building")]
public class Building : ScriptableObject
{
    public int Cost;
    public GameObject Prefab;
}
