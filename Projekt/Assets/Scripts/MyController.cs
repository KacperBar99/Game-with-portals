using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MyController : MonoBehaviour
{
    [Header("Movement data")]
    public Camera playerCamera;
    [Range(0f, 50f)]
    public float walkspeed = 6f;
    [Range(0f, 100f)]
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;

    public float lookSpeed = 2f;
    [Tooltip("Maximum elevation and depression")]
    public float lookXlimit = 45f;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;

    CharacterController characterController;

    private float step;
    private float height;
    private float radius;
    private float defaultSpeed;
    private float defaultRun;
    private float defaultJump;
    
    void Start()
    {
        
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        this.defaultJump = this.jumpPower;
        this.defaultRun = this.runSpeed;
        this.defaultSpeed = this.walkspeed;
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkspeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkspeed) * Input.GetAxis("Horizontal") : 0;

        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        

      
        if(Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
       

       
        characterController.Move(moveDirection * Time.deltaTime);
        if (canMove)
        {
            rotationX -= Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXlimit, lookXlimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation*=Quaternion.Euler(0,Input.GetAxis("Mouse X")*lookSpeed,0);
        }
    }
    public void setNewScale(float scale)
    {
        this.transform.localScale = scale*new Vector3(1,1,1);
        
        this.walkspeed = scale*this.defaultSpeed;
        this.runSpeed = scale*this.defaultRun;
        this.jumpPower = scale*this.defaultJump;
    }
}
