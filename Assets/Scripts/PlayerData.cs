using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance;

    
    public bool giveinfo = false;
    public int money;
    public int buildingcount;
    public int troopscount;
    public int navycount;
    public int planecount;
    public int tankcount;

    private GameObject moneyCanvas;
    private int red, green, blue;//teams
    //public delegate void moneychange(object sender, moneychangeargs e);
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        #region UI Objects Finder
        if (transform.Find("Canvas/Money") != null)
        {
            moneyCanvas = transform.Find("Canvas/Money").gameObject;
        }
        #endregion

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        #region UI
        moneyCanvas.GetComponent<TextMeshProUGUI>().text = "Money : " + money.ToString();
        #endregion
    }
    public void changeteamdata(string team, int amount, string type)
    {
        int selectedTeam = 99;
        
    }
}
