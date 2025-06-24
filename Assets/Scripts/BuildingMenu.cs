using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildingMenu : MonoBehaviour
{

    public string Team;
    public bool isSelected;
    public int MaxHealth;
    public int Health;
    public bool isGivingMoney = false;
    public int GivingMoneyAmmount = 0;
    public float MoneyCooldownSec = 0;
    public bool canyoubuildhere = false;

    private float m_cooldown;
    private GameObject SelectObj;
    private GameObject myPlayer;
    private GameObject moneyCanvas;
    void Start()
    {
        Health = MaxHealth;
        isSelected = false;
        #region Select Object Finder
        SelectObj = transform.Find("Select").gameObject;
        if (SelectObj != null)
        {
            Debug.Log("Please check your Select object");
        }
        #endregion
        var birse = FindObjectsByType<Selection>(FindObjectsSortMode.None);
        foreach (var obj in birse)
        {
            if (obj.GetComponent<Selection>() != null && obj.GetComponent<Selection>().Playerteam == Team)
            {
                myPlayer = obj.gameObject;
                myPlayer.GetComponent<PlayerData>().buildingcount += 1;
            }
        }

    }
    private void Awake()
    {
        //var birse = FindObjectsByType<Selection>(FindObjectsSortMode.None);
        //foreach (var obj in birse)
        //{
        //    if(obj.GetComponent<Selection>() != null && obj.GetComponent<Selection>().Playerteam == Team)
        //    {
        //        myPlayer = obj.gameObject;
        //        myPlayer.GetComponent<PlayerData>().buildingcount += 1;
        //    }
        //}
    }
    int sended = 0; //health
    void FixedUpdate()
    {
        #region Select Object
        if (SelectObj != null)
        {
            if (!isSelected)
            {
                SelectObj.SetActive(false);
            }
            else if (isSelected)
            {
                SelectObj.SetActive(true);
            }
        }
        #endregion

        #region Health
        
        if(Health <= 0 && sended == 0)
        {
            sended = 1;
            myPlayer.GetComponent<PlayerData>().buildingcount = myPlayer.GetComponent<PlayerData>().buildingcount - 1;
            Selection.instance.OnDestroyed(transform.gameObject);
            StartCoroutine(destroyobject(transform.gameObject));
        }
        #endregion
        #region Money Giving
        if (isGivingMoney && m_cooldown <= 0)
        {
            myPlayer.GetComponent<PlayerData>().money += GivingMoneyAmmount;


            m_cooldown = MoneyCooldownSec;
        }
        if (m_cooldown > 0) m_cooldown -= Time.deltaTime;
        #endregion
    }
    IEnumerator destroyobject(GameObject gameObject)
    {
        
        yield return new WaitForSeconds (0.1f);
        Destroy(transform.gameObject);

    }
}
