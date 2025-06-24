using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


public class charactermovenment : MonoBehaviour
{
    public float speed;
    public float sprintModifier;    
    public Rigidbody rig;
    public float jumpforce;
    public LayerMask ground;
    public Transform groundDetector;


    #region Monobehaviour CallBacks
    // Start is called before the first frame update
    void Start()
    {
        rig.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        //axes
        float t_hmove = Input.GetAxis("Horizontal");
        float t_vmove = Input.GetAxis("Vertical");


        //controls
        bool sprint = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        bool jump = Input.GetKeyDown(KeyCode.Space);

        //statements
        bool isgrounded = Physics.Raycast(groundDetector.position, Vector3.down, 0.2f, ground);
        bool isJumping = jump && isgrounded;

        bool isSprinting = sprint && t_vmove > 0 && !isJumping && isgrounded;

        //jumping
        if (isJumping)
        {
            rig.AddForce(Vector3.up * jumpforce);
        }
    }
    void FixedUpdate()
    {
        //axes
        float t_hmove = Input.GetAxis("Horizontal");
        float t_vmove = Input.GetAxis("Vertical");


        //controls
        bool sprint = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        bool jump = Input.GetKeyDown(KeyCode.Space);

        //statements
        bool isgrounded = Physics.Raycast(groundDetector.position, Vector3.down, 0.1f, ground);
        bool isJumping = jump && isgrounded;

        bool isSprinting = sprint && t_vmove > 0 && !isJumping && isgrounded;

        //movenments
        Vector3 t_direction = new Vector3(t_hmove, 0 , t_vmove);
        t_direction.Normalize();
        float t_adjustedSpeed = speed;

        if (isSprinting)
        {
            t_adjustedSpeed *= sprintModifier;
        }
        Vector3 t_targetVelocity = transform.TransformDirection(t_direction) * t_adjustedSpeed * 100 * Time.deltaTime;
        t_targetVelocity.y = rig.velocity.y;
        rig.velocity = t_targetVelocity;
    }
    #endregion

}
