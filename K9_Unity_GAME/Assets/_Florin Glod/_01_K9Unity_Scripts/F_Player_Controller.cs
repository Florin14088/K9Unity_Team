using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class F_Player_Controller : MonoBehaviour
{
    #region Own Classes



    #endregion


    #region Public Variables
    [Range(0, 200)]
    public float moveSpeed = 0;
    public float rot = 0f; //body rotation
    public float rotSpeed = 5f;
    [Space]
    [Range(1, 3)] public float runMultiplier = 1;
    public KeyCode runKey = KeyCode.LeftShift;
    [Space]
    [Range(0, 350)] public float jumpPower = 0;
    public KeyCode jumpKey = KeyCode.Space;
    public float groundDistance = 1.1f; //used by raycast that is starting from player and going down.

    [HideInInspector] public enum PlayerMood { idle, walking, running, jumping};
    public PlayerMood player_mood;
    [Space]
    public bool isIdle; //true if isWalking, isJumping and isDead are false
    public bool isJumping; //true if jump key is pressed. this bool is always equal with Airborne() function. Function Airborne() controls this bool by returning true or false
    public bool isWalking; //true if horizontal or vertical axis are not equal with Vector3.zero. Function Movement() controls this bool
    public bool isRunning; //true if run key is pressed while isWalking = true. Function CanRun() controls this bool

    #endregion


    #region Private Variables
    private Rigidbody rb;
    private Animator anim;
    private float horizontal_Movement = 0;
    private float vertical_Movement = 0;
    private Vector3 moveDirection;

    private Vector3 directionDown; //used for raycast to check if player is airborne or not

    #endregion


    #region Pre-defined functions

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        

    }//Awake


    void Start()
    {


    }//Start


    void Update()
    {
        Movement();

    }//Update

    #endregion


    #region Own Functions

    private void Movement()
    {       
        if (Input.GetKeyDown(jumpKey) && isJumping == false)// if jump key is pressed and is not airborne
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpPower * Time.deltaTime, rb.velocity.z);
        }

        if (Input.GetKey(jumpKey))
        {
            anim.SetInteger("Ana", 2);
        }

        if (Airborne() != isJumping) isJumping = Airborne();// make sure isJumping is equals with what is returned by the Airborne function
        if (CanRun() != isRunning) isRunning = CanRun();// make sure isRunning is equals with what is returned by the CanRun function


        if (GetDirection() != Vector3.zero && !Input.GetKey(jumpKey))// if input is received
        {
            isWalking = true;// input detected, is walking            

            Vector3 yVelFixx = new Vector3(0, rb.velocity.y, 0); //temp Vector3 variable with x and z 0 and y controlled by rigidbody
            rb.velocity = GetDirection() * SpeedDecision() * Time.deltaTime;
            rb.velocity += yVelFixx; //add the temp Vector3 to the rb.velocity to allow rigidbody to control y

            anim.SetInteger("Ana", 1);
        }


        if (GetDirection() == Vector3.zero && !Input.GetKey(jumpKey))// if no input is received
        {
            isWalking = false;// no input, so it's no longer walking

            if (isJumping == false) rb.velocity = new Vector3(0 * Time.fixedDeltaTime, rb.velocity.y, 0 * Time.fixedDeltaTime); //if not airborne, let rigidbody control Y axis and set X and Z to 0
            else rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);// if airborne, let rigidbody control everything

            anim.SetInteger("Ana", 0);
        }

    }//Movement

    #endregion


    #region Functions that return something

    private Vector3 GetDirection()// function that simply returns the normalized Vector3 that is made from the sum of horizontal and vertical movements
    {
        // horizontal_Movement = Input.GetAxisRaw("Horizontal"); //getting the horizontal movement
        // vertical_Movement = Input.GetAxisRaw("Vertical"); //getting the vertical movement
        // moveDirection = (horizontal_Movement * transform.right + vertical_Movement * transform.forward).normalized; //assembly the 2 movement variables together in this Vector3 and normalize it

        if (Input.GetKey(KeyCode.W)) //                 moving FORWARD
        {
            moveDirection = new Vector3(0, 0, 1);
            moveDirection = transform.TransformDirection(moveDirection);

        }//Forward Key press

        if (Input.GetKey(KeyCode.S)) //moving backward
        {
            moveDirection = new Vector3(0, 0, -1);
            moveDirection = transform.TransformDirection(moveDirection);

        }//Backward Key press

        if (Input.GetKey(KeyCode.A))
        {
            rot += Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, rot, 0);
            if (rot >= 360) rot = 0;
            if (rot <= -360) rot = 0;
        }//Turning Left


        if (Input.GetKey(KeyCode.D))
        {
            rot += Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, rot, 0);
            if (rot >= 360) rot = 0;
            if (rot <= -360) rot = 0;
        }//Turning Right

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            moveDirection = new Vector3(0, 0, 0);

        }//Stop Movement

            return moveDirection;

    }//GetDirection

    private bool Airborne()// function that return false if player is not at distance "groundDistance" from ground and true if it is
    {
        directionDown = transform.TransformDirection(Vector3.up);

        if (Physics.Raycast(transform.position, -directionDown, groundDistance)) return false;
        else return true;

    }//Airborne


    private bool CanRun()// function that returns true or false fro bool isRunning
    {
        if (isWalking)// if it's already walking
        {
            if (Input.GetKey(runKey)) return true;// if runKey is pressed it can run
            else return false;// if runKey is no longer pressed, it cannot run
        }
        else// if it's no longer walking
        {
            return false;// it cannot run even if runKey is pressed
        }

    }//CanRun

    private float SpeedDecision()// return the moveSpeed to be used to move according to isRunning bool
    {
        if (isRunning) return moveSpeed * runMultiplier;
        else return moveSpeed * (runMultiplier / runMultiplier);

    }//SpeedDecision

    #endregion


}
