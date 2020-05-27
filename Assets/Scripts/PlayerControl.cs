using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl _instance { get; protected set; }

    public float moveSpeed = 1.5f;
    public float changeViewSpeed = 3.0f;
    private float maxViewAngle = 60.0f;
    private float gravity = 50.0f;
    public float upHeight = 1.0f;
    private Vector3 xyzMove = Vector3.zero;

    public bool isRunning = false;

    public GameObject Model;
    public Transform weaponPlace;
    public Transform cameraPosition;
    public CharacterController controller;

    public Camera playerCamera;

    float xRotate = 0, yRotate = 0;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        DoMoveAndJump();
        DoChangeView();
    }

    private void DoMoveAndJump()
    {
        if (IsOnGround())
        {
            float xMove = Input.GetAxisRaw("Horizontal");
            float zMove = Input.GetAxisRaw("Vertical");

            isRunning = Input.GetKey(KeyCode.LeftShift);
            // 左Shift加速
            if (isRunning)
            {
                xyzMove = new Vector3(xMove, 0, zMove) * moveSpeed * 2;
            }
            else
            {
                xyzMove = new Vector3(xMove, 0, zMove) * moveSpeed;   
            }

            // Run_Forward,Forward,Backward,Run_Backward在Animator对应的condition分别为3、2、1、-1
            if(xMove == 0 && zMove == 0)
            {
                //animator.SetInteger("condition", 0);
            }
            else if(zMove < 0)
            {
                if (isRunning)
                {
                    //animator.SetInteger("condition", -1);
                }
                else
                {
                    //animator.SetInteger("condition", 1);
                }
            }
            else
            {
                if (isRunning)
                {
                    //animator.SetInteger("condition", 3);
                }
                else
                {
                    //animator.SetInteger("condition", 2);
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                xyzMove.y = upHeight;
            }
        }

        xyzMove.y -= gravity * Time.deltaTime;

        controller.Move(controller.transform.TransformDirection(xyzMove * Time.deltaTime));
    }

    private bool IsOnGround()
    {
        int layerMask = 1 << 9;
        //Debug.DrawRay(Model.transform.position, Vector3.down * 1.2f);
        return Physics.Raycast(Model.transform.position, Vector3.down, 1.2f, layerMask);
    }

    private void DoChangeView()
    {
        xRotate += Input.GetAxis("Mouse X");
        yRotate -= Input.GetAxis("Mouse Y");

        Model.transform.eulerAngles = new Vector3(0, xRotate * changeViewSpeed, 0);

        cameraPosition.eulerAngles = new Vector3(Mathf.Clamp(yRotate * changeViewSpeed, -maxViewAngle, maxViewAngle), xRotate * changeViewSpeed, 0);
    }


}
