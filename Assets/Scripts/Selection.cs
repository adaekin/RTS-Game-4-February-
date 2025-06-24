using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Animations;

public class Selection : MonoBehaviour
{

    #region Multiple Selection Variables
    public static Selection instance;
    public Camera cam;
    public LayerMask canselectable;
    public string Playerteam;
    public GameObject Checkpoint;


    private List<GameObject> selectedobjects = new List<GameObject>();
    private List<GameObject> removedobjects = new List<GameObject>();
    private bool clickedisobject = false;
    private Vector3 raypoint;
    #endregion

    private void Awake()
    {
        #region Instance
        if (instance == null)
        {
            instance = this;
        }
        #endregion
    }
    void Update()
    {
        #region selection
        if ((Input.GetMouseButtonDown(0) && !(selectedobjects.Count > 0)) || (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftShift)))
        {
            sendselection();
        }
        #endregion
        #region commanding selection
        if (Input.GetMouseButtonDown(0) && selectedobjects.Count > 0 && !clickedisobject && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            Debug.Log("moving");
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity))
            {
                if(hitInfo.transform != null)
                {
                    raypoint = hitInfo.point;
                    if (raypoint != null) moveobjects();
                }
            }
        }
        if (Input.GetMouseButtonDown(0) && selectedobjects.Count > 0 && !clickedisobject && !(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            Debug.Log("moving");
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity))
            {
                if (hitInfo.transform != null)
                {
                    foreach (var obj in selectedobjects)
                    {
                        obj.GetComponent<MoveObject>().clearcheckpoints = true;
                        raypoint = hitInfo.point;

                        if (raypoint != null) StartCoroutine(moveobj(obj));
                    }
                    
                }
            }
        }
        clickedisobject = false;
        #endregion
        #region deleteallselection
        if (Input.GetKey(KeyCode.C))
        {
            clearallselection();
        }
        #endregion


    }
    IEnumerator moveobj(GameObject Obj)
    {
        yield return new WaitUntil(() => Obj.GetComponent<MoveObject>().clearcheckpoints == true);
        moveobjects();
    }
    private void FixedUpdate()
    {
        foreach (GameObject obj in selectedobjects)
        {
            obj.GetComponent<TroopMenu>().isSelected = true;
        }
        foreach (GameObject obj in removedobjects)
        {
            obj.GetComponent<TroopMenu>().isSelected = false;
            selectedobjects.Remove(obj);
        }
        removedobjects =  new List<GameObject>();
    }
    #region selection
    void sendselection()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, canselectable))
        {
            if (hitInfo.transform != null)
            {
                if (!selectedobjects.Contains(hitInfo.transform.gameObject) && hitInfo.transform.gameObject.layer == 6 && hitInfo.transform.GetComponent<TroopMenu>() != null && hitInfo.transform.GetComponent<TroopMenu>().Team == Playerteam)
                {
                        selectedobjects.Add(hitInfo.transform.gameObject);
                        clickedisobject = true;
                }
                else if (selectedobjects.Contains(hitInfo.transform.gameObject) && hitInfo.transform.gameObject.layer == 6 && hitInfo.transform.GetComponent<TroopMenu>() != null && hitInfo.transform.GetComponent<TroopMenu>().Team == Playerteam)
                {
                    removeselection(hitInfo.transform.gameObject);
                }
            }
        }
    }
    #endregion
    #region deselection
    void clearallselection()
    {
        removedobjects = selectedobjects;
    }
    void removeselection(GameObject obj)
    {
        removedobjects.Add(obj);
    }

    #endregion
    #region moving object
    void moveobjects()
    {
        List<GameObject> temp_taskmoveobjects = selectedobjects;

        if(temp_taskmoveobjects.Count > 0)
        {
            foreach (GameObject obj in temp_taskmoveobjects)
            {
                GameObject checkpoint = Instantiate(Checkpoint, raypoint, transform.rotation);
                obj.GetComponent<MoveObject>().destinationloc.Add(checkpoint);
            }
            
        }
        
    }
    #endregion
    #region Destroy
    public void OnDestroyed(GameObject game)
    {
        if(selectedobjects.Contains(game)) removeselection(game);
        Debug.Log("Destroyed");
    }
    #endregion
}
