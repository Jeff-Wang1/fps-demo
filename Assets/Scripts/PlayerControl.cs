using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl _instance { get; protected set; }

    public float moveSpeed = 1.5f;
    public float changeViewSpeed = 3.0f;
    private float maxViewAngle = 60.0f;
    public float gravity = 50.0f;
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
        //Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        Debug.DrawRay(Model.transform.position, Vector3.down * 1.5f, Color.red);
        if (xyzMove.y > 0)
        {
            xyzMove.y -= gravity * Time.deltaTime;
            
        }
        controller.Move(controller.transform.TransformDirection(xyzMove * Time.deltaTime));
    }

    public void DoMove(float xMove,float zMove)
    {
        if (IsOnGround())
        {
 
            // 加速
            if (isRunning)
            {
                xyzMove = new Vector3(xMove, 0, zMove) * moveSpeed * 2;
            }
            else
            {
                xyzMove = new Vector3(xMove, 0, zMove) * moveSpeed;   
            }         

        }

        controller.Move(controller.transform.TransformDirection(xyzMove * Time.deltaTime));

        xyzMove.x = 0f;
        xyzMove.z = 0f;
    }

    public void DoJump()
    {
        if (IsOnGround())
        {
            xyzMove.y = upHeight;
        }
    }

    private bool IsOnGround()
    {
        int layerMask = 1 << 9;
        
        return Physics.Raycast(Model.transform.position, Vector3.down, 1.5f, layerMask);
    }

    public void DoChangeView(float xRotation,float yRotation)
    {
        xRotate += xRotation;
        yRotate -= yRotation;

        Model.transform.eulerAngles = new Vector3(0, xRotate * changeViewSpeed, 0);

        cameraPosition.eulerAngles = new Vector3(Mathf.Clamp(yRotate * changeViewSpeed, -maxViewAngle, maxViewAngle), xRotate * changeViewSpeed, 0);
    }


}
