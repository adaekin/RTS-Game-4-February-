using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private float Health;
    private float MaxHealth;
    private GameObject parent;
    private Slider slidebar;

    void Start()
    {
        parent = transform.parent.gameObject;
        slidebar = transform.Find("Canvas/Border").gameObject.GetComponent<Slider>();
    }
    private void FixedUpdate()
    {
        Vector3 destination = Camera.main.transform.position;
        float distance = Vector3.Distance(transform.position, destination);
        if (distance > 10f)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else if(distance <= 10f)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        transform.LookAt(Camera.main.transform.position);
        if(transform.parent.gameObject.GetComponent<TroopMenu>() != null)
        {
            Health = transform.parent.gameObject.GetComponent<TroopMenu>().Health;
            MaxHealth = transform.parent.gameObject.GetComponent<TroopMenu>().MaxHealth;
            slidebar.value = (float)(Health / MaxHealth);
        }
        else if (transform.parent.gameObject.GetComponent<BuildingMenu>() != null)
        {
            Health = transform.parent.gameObject.GetComponent<BuildingMenu>().Health;
            MaxHealth = transform.parent.gameObject.GetComponent<BuildingMenu>().MaxHealth;
            slidebar.value = (float)(Health / MaxHealth);
        }


    }
}
