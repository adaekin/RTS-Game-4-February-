using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopMenu : MonoBehaviour
{

    public string Team;
    public bool isSelected;
    public int MaxHealth;
    public int Health;
    public int speed;
    public int bulletspeed;
    public int bulletdamage;

    private GameObject SelectObj;

    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;
        isSelected = false;
        #region Select Object Finder
        SelectObj = transform.Find("Select").gameObject;
        if (SelectObj == null)
        {
            Debug.Log("Please check your Select object");
        }
        #endregion
    }

    // Update is called once per frame
    void Update()
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
        if(Health <= 0)
        {
            Selection.instance.OnDestroyed(transform.gameObject);
            StartCoroutine(destroyobject(transform.gameObject));
        }
        #endregion



    }
    IEnumerator destroyobject(GameObject gameObject)
    {
        yield return new WaitForSeconds (0.1f);
        Destroy(transform.gameObject);

    }
}
