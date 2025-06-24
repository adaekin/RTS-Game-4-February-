using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class look : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    public Transform cams;
    

    public float xSensitivity;
    public float ySensitivity;
    private Quaternion camCenter;
    public float maxAngle = 90;

    private Camera cam;
    private Vector3 raypoint;
    public Transform selectedobj;
    public Transform insobj;
    void Start()
    {
        cam = transform.Find("Camera").GetComponent<Camera>();
        camCenter = cams.localRotation;
    }

    // Update is called once per frame
    void Update()
    {


        #region Rot and lockstate
        if (Input.GetMouseButton(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            SetX();
            SetY();
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;

        }
        #endregion

    }
    #region camerarot
    void SetY()
    {
        float t_input = Input.GetAxis("Mouse Y") * ySensitivity * Time.deltaTime;
        Quaternion t_adj = Quaternion.AngleAxis(t_input, -Vector3.right);
        Quaternion t_delta = cams.localRotation * t_adj;
        if(Quaternion.Angle(camCenter,t_delta) < maxAngle)
        {
            cams.localRotation = t_delta;
        }
    }
    void SetX()
    {
        float t_input = Input.GetAxis("Mouse X") * xSensitivity * Time.deltaTime;
        Quaternion t_adj = Quaternion.AngleAxis(t_input, Vector3.up);
        Quaternion t_delta = transform.localRotation * t_adj;
        transform.localRotation = t_delta;

    }

    #endregion
}
