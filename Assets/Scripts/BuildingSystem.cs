using System.Collections;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    bool buildingmenu = true;
    public bool buildingMenuOn = true;
    public bool objectselected = true;
    public GameObject selectedobj;
    private GameObject BuildingMenuObj;
    private float cost;
    private GameObject PressUto;
    private Camera cam;
    private Vector3 raypoint;
    private GameObject tempbuildobj;
    private bool waitforsome = true;
    // Start is called before the first frame update
    void Start()
    {
        BuildingMenuObj = transform.Find("Canvas/BuildingMenu").gameObject;
        PressUto = transform.Find("Canvas/PressU").gameObject;
        cam = transform.Find("Camera").GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U) && !buildingmenu)
        {
            buildingmenu = true;
            PressUto.SetActive(false);
            BuildingMenuObj.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            buildingmenu = false;
            PressUto.SetActive(true);
            BuildingMenuObj.SetActive(false);
        }





        #region Build Object
        if (buildingmenu && objectselected && selectedobj != null && waitforsome)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity))
            {
                if (hitInfo.transform != null)
                {
                    raypoint = hitInfo.point;
                    if (raypoint != null)
                    {
                        if (tempbuildobj == null) tempbuildobj = tempBuildObject(selectedobj, raypoint);
                        tempbuildobj.transform.position = raypoint;
                        //selectedobj.Find("HealthBar").gameObject.SetActive(false);
                        //selectedobj.Find("Select").gameObject.SetActive(false);


                        selectedobj.transform.position = raypoint;
                        if (Input.GetMouseButtonDown(0) && waitforsome && selectedobj != null && tempbuildobj.GetComponent<CanYouBuildHere>().canyoubuildhere)
                        {
                            Debug.Log("ASDASD");
                            StartCoroutine(BuildObj());
                        }
                    }
                }
            }
        }
        else if (tempbuildobj != null)
        {
            Destroy(tempbuildobj);
        }
        #endregion



    }
    GameObject tempBuildObject(GameObject Selectedobj, Vector3 raypoint)
    {
        GameObject tempobj = Instantiate(selectedobj, raypoint, transform.rotation).gameObject;
        tempobj.layer = 2;
        tempobj.GetComponent<BoxCollider>().enabled = false;
        tempobj.GetComponent<BuildingMenu>().enabled=false;
        tempobj.GetComponent<BoxCollider>().enabled=false;
        return tempobj;
    }
    public void SelectBuildingMenuObj(Building Selectedobj)
    {
        selectedobj = null;
        waitforsome = false;
        
        StartCoroutine(DelayTime());
        selectedobj = Selectedobj.Prefab;
        cost = Selectedobj.Cost;
    }
    IEnumerator DelayTime()
    {

        yield return new WaitForSeconds(0.1f);
        waitforsome=true;
    }
    IEnumerator BuildObj()
    {
        yield return new WaitForSeconds(0.1f);
        if (waitforsome)
        {
            Destroy(tempbuildobj);
            GameObject Created = Instantiate(selectedobj, raypoint, transform.rotation).gameObject;
            Created.GetComponent<BuildingMenu>().Team = "Red";
            Created.GetComponent<BuildingMenu>().enabled = true;
            Created.GetComponent<BoxCollider>().enabled = true;
            Created.layer = 6;
            selectedobj = null;
        }
        yield return new WaitForSeconds(0.2f);
    }
}
