using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Airborne() function is controlling the bool isJumping from nested class InfoBools using a raycast oriented downside, with length groundDistance
/// GetDirection() function is returning the Vector3 moveDirection (where the 2 movement variables are combined together and normalized) 
///  
/// Variables from nested Class CameraWorks are controlling how the player can rotate. A script is on the Camera tagged "MainCamera" and is making camera follow the player
/// 
/// CanRun() function is controlling the bool isRunning and controls if the player is using moveSpeed with or without runMultiplier. If bool isWalking is true and runKey is pressed, return true. If it's not walking or runKey is not pressed, return false.
/// 
/// SpeedDecision() function uses value of bool isRunning to decide if the speed used for moving is altered with runMultiplier or not
/// </summary>

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class Player_ControlBoard : MonoBehaviour
{
    #region Own Classes
    [Serializable] public class PlayerStats
    {
        [Range(0, 200)]
        public float moveSpeed = 0;

        [Space]

        [Range(1, 3)]
        public float runMultiplier = 1;
        public KeyCode runKey = KeyCode.LeftShift;

        [Space]

        [Range(0, 350)]
        public float jumpPower = 0;
        public KeyCode jumpKey = KeyCode.Space;
        public float groundDistance = 1.1f; //used by raycast that is starting from player and going down.
    }

    [Serializable] public class InfoBools
    {
        [HideInInspector] public enum PlayerMood {idle, walking, running, jumping, dead};
        public PlayerMood player_mood;
        [Space]
        public bool isIdle; //true if isWalking, isJumping and isDead are false
        public bool isJumping; //true if jump key is pressed. this bool is always equal with Airborne() function. Function Airborne() controls this bool by returning true or false
        public bool isWalking; //true if horizontal or vertical axis are not equal with Vector3.zero. Function Movement() controls this bool
        public bool isRunning; //true if run key is pressed while isWalking = true. Function CanRun() controls this bool
        public bool isDead; //true if player have no health left
    }

    [Serializable] public class Animate
    {
        public string parameterName;
        [Space]
        public int number_anim_idle;
        public int number_anim_walk;
        public int number_anim_run;
        public int number_anim_jump;
        public int number_anim_dead;
    }

    [Serializable] public class CameraWorks
    {
        public float min_X = -60;
        public float max_X = 60;
        public float min_Y = -360;
        public float max_Y = 360;

        public float sensitivity_X = 2f;
        public float sensitivity_Y = 2f;

        public Camera cam;
    }
    #endregion


    #region Public Variables
    public PlayerStats playerStats = new PlayerStats();
    [Space]
    [Space]
    public InfoBools infoBools = new InfoBools();
    [Space]
    [Space]
    public Animate animate = new Animate();
    [Space]
    [Space]
    public CameraWorks cameraWorks = new CameraWorks();
    #endregion


    #region Private Variables
    private Rigidbody rb;
    private Animator anim;

    private float horizontal_Movement = 0;
    private float vertical_Movement = 0;
    private Vector3 moveDirection;

    private Vector3 directionDown; //used for raycast to check if player is airborne or not

    private float rotation_X = 0; //used by nested class CameraWorks
    private float rotation_Y = 0; //used by nested class CameraWorks
    #endregion



    #region Pre-defined functions
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        cameraWorks.cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

    }//Start


    private void Update()
    {
        CaseMonitor();
        MouseRotation();
    }


    void FixedUpdate()
    {
        Movement();

    }//Update
    #endregion



    #region Own Functions

    private void Movement()
    {
        if(Input.GetKey(playerStats.jumpKey) && infoBools.isJumping == false)// if jump key is pressed and is not airborne
        {
            rb.velocity = new Vector3(rb.velocity.x, playerStats.jumpPower * Time.deltaTime, rb.velocity.z);
        }


        if (Airborne() != infoBools.isJumping) infoBools.isJumping = Airborne();// make sure isJumping is equals with what is returned by the Airborne function
        if (CanRun() != infoBools.isRunning) infoBools.isRunning = CanRun();// make sure isRunning is equals with what is returned by the CanRun function
        

        if (GetDirection() != Vector3.zero)// if input is received
        {
            infoBools.isWalking = true;// input detected, is walking            

            Vector3 yVelFixx = new Vector3(0, rb.velocity.y, 0); //temp Vector3 variable with x and z 0 and y controlled by rigidbody
            rb.velocity = GetDirection() * SpeedDecision() * Time.deltaTime;
            rb.velocity += yVelFixx; //add the temp Vector3 to the rb.velocity to allow rigidbody to control y
        }


        if (GetDirection() == Vector3.zero)// if no input is received
        {
            infoBools.isWalking = false;// no input, so it's no longer walking

            if (infoBools.isJumping == false) rb.velocity = new Vector3(0 * Time.fixedDeltaTime, rb.velocity.y, 0 * Time.fixedDeltaTime); //if not airborne, let rigidbody control Y axis and set X and Z to 0
            else rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);// if airborne, let rigidbody control everything
        }

    }//Movement


    private void MouseRotation()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Cursor.lockState != CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        rotation_X += Input.GetAxis("Mouse Y") * cameraWorks.sensitivity_X;
        rotation_Y += Input.GetAxis("Mouse X") * cameraWorks.sensitivity_Y;

        rotation_X = Mathf.Clamp(rotation_X, cameraWorks.min_X, cameraWorks.max_X);

        transform.localEulerAngles = new Vector3(0, rotation_Y, 0);
        cameraWorks.cam.transform.localEulerAngles = new Vector3(-rotation_X, rotation_Y, 0);

    }//MouseRotation


    private void CaseMonitor()// this function updates enum player_mood according to the bools provided.
    {   
        if (infoBools.isWalking == true && infoBools.isRunning == false && infoBools.isJumping == false && infoBools.isDead == false) infoBools.player_mood = InfoBools.PlayerMood.walking;

        if (infoBools.isWalking == true && infoBools.isRunning == true && infoBools.isJumping == false && infoBools.isDead == false) infoBools.player_mood = InfoBools.PlayerMood.running;

        if (infoBools.isJumping == true && infoBools.isDead == false) infoBools.player_mood = InfoBools.PlayerMood.jumping;

        if (infoBools.isWalking == false && infoBools.isJumping == false && infoBools.isDead == false) infoBools.player_mood = InfoBools.PlayerMood.idle;

        if (infoBools.isDead == true) infoBools.player_mood = InfoBools.PlayerMood.dead;

    }//CaseMonitor


    private void AnimMonitor()// this function decides which animation to run according to the state provided by CaseMonitor()
    {
        switch (infoBools.player_mood)
        {
            case InfoBools.PlayerMood.dead:
                //TODO animation for dead
                break;

            case InfoBools.PlayerMood.jumping:
                //TODO animation for jump
                break;

            case InfoBools.PlayerMood.running:
                //TODO animation for run
                break;

            case InfoBools.PlayerMood.walking:
                //TODO animation for walk
                break;

            case InfoBools.PlayerMood.idle:
                //TODO animation for idle
                break;

            default:
                return;
        }

    }//AnimMonitor

    #endregion



    #region Functions that return something

    private Vector3 GetDirection()// function that simply returns the normalized Vector3 that is made from the sum of horizontal and vertical movements
    {
        horizontal_Movement = Input.GetAxisRaw("Horizontal"); //getting the horizontal movement
        vertical_Movement = Input.GetAxisRaw("Vertical"); //getting the vertical movement
        moveDirection = (horizontal_Movement * transform.right + vertical_Movement * transform.forward).normalized; //assembly the 2 movement variables together in this Vector3 and normalize it

        
        return moveDirection;

    }//GetDirection


    private Boolean Airborne()// function that return false if player is not at distance "groundDistance" from ground and true if it is
    {
        directionDown = transform.TransformDirection(Vector3.up);

        if (Physics.Raycast(transform.position, -directionDown, playerStats.groundDistance)) return false;
        else return true;

    }//Airborne


    private Boolean CanRun()// function that returns true or false fro bool isRunning
    {
        if (infoBools.isWalking)// if it's already walking
        {
            if (Input.GetKey(playerStats.runKey)) return true;// if runKey is pressed it can run
            else return false;// if runKey is no longer pressed, it cannot run
        }
        else// if it's no longer walking
        {
            return false;// it cannot run even if runKey is pressed
        }

    }//CanRun


    private float SpeedDecision()// return the speed to be used to move according to isRunning bool
    {
        if (infoBools.isRunning) return playerStats.moveSpeed * playerStats.runMultiplier;
        else return playerStats.moveSpeed * (playerStats.runMultiplier / playerStats.runMultiplier);

    }//SpeedDecision

    #endregion


}//END
