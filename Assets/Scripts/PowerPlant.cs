using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlant : MonoBehaviour
{
    [Header("Data")]
    public bool canyoubuildhere = true;
    public GameObject[] crystals;


    void Start()
    {
        crystals = GameObject.FindGameObjectsWithTag("Crystal");
    }

    void FixedUpdate()
    {
        foreach (GameObject crystal in crystals)
        {
            float distance = Vector3.Distance(transform.position, crystal.transform.position);
            if (distance < 10f)
            {
                canyoubuildhere = true;
            }
            else
            {
                canyoubuildhere = false;
            }
        }
    }
}
