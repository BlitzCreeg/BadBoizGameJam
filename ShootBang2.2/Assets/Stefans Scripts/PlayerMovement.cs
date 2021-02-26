using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    public GameObject player;

    public GameObject crouchCamPos;
    public GameObject standCamPos;

    public Camera playerCam;

    public float speed = 12f;
    public float gravity = -9.81f;

    public float maxStamina = 200f;
    public float staminaIncrease = 20f;
    public float stamina = 200f;
    public float staminaUsing = 0f;
    public float staminaJump = 40f;
    public float staminaPullUp = 60f;

    public float jumpHeight = 3f;
    public float pullUpHeight = 2f;
    public float crouchHeight;

    private float height;

    public float fov = 60;
    public float sprintFov = 80;

    public float x;
    public float z;

    public float crouchSpeed = 2f;
    public float sprintSpeed = 10f;
    public float walkSpeed = 6f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;

    public bool isGrounded;
    public bool canClimb;

    // DANNNNNNNNNNNYYYYYYYYYYYYYSSSSSSSSSSSSSSSS BOOOOOOLLLLLLLLSSSSSSSSSSS
    public bool isClimbing;
    public bool isCrouching;
    public bool isCrouchWalking;
    public bool isSprinting;
    public bool isWalking;
    public bool isJumping;

    //YYYYYYYYYYYYYYYYYYYYYYYYY VVVVVVVVVVVEEEEEEELLLLOOCCCCCCCIIIIIITTTTTTTYYYYYYYYY for impact
    public float currentYVelocity;

    private void Awake()
    {
        playerCam.fieldOfView = fov;
        height = controller.height;
        //standCamPos.transform.position = new Vector3(controller.transform.position.x, controller.transform.position.y + height / 2.5f, controller.transform.position.z);
        //crouchCamPos.transform.position = new Vector3(controller.transform.position.x, controller.transform.position.y + 0.2f, controller.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfGrounded();
        Jump();
        MovePlayer();
        ApplyGravity();
        CheckVelocity();
        Crouch();
        Sprint();
        StaminaUpdate();
        if (canClimb)
            Climbing();
        CheckState();
    }

    //Checks to see if player is grounded
    public void CheckIfGrounded()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGrounded)
        {
            isJumping = false;
            isClimbing = false;
        }
    }

    //Player Jump function
    public void Jump()
    {
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    //Checks player input and moves charactor controller accordingly
    public void MovePlayer()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded && stamina >= 30f && !isCrouching)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            stamina -= staminaJump;
            isJumping = true;

        }
    }

    public void ApplyGravity()
    {
            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
    }

    //Crouches the player by adjusting the height of the Player asset
    public void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if(isGrounded)
                isCrouching = true;

            controller.height = height / 4;
            playerCam.transform.position = crouchCamPos.transform.position;
            speed = crouchSpeed;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl) && x > 0 || Input.GetKeyUp(KeyCode.LeftControl) && z > 0)
        {
            controller.height = height;
            playerCam.transform.position = standCamPos.transform.position;

            speed = walkSpeed;
        }
    }

    public void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && stamina >= 10f)
        {
            speed = sprintSpeed;
            staminaUsing = 20f;

            playerCam.fieldOfView = sprintFov;
        }

        if (stamina < 10f)
        {
            speed = walkSpeed;
            playerCam.fieldOfView = fov;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = walkSpeed;
            staminaUsing = 0f;
            playerCam.fieldOfView = fov;
        }
    }

    public void StaminaUpdate()
    {
        if (staminaUsing == 0f && stamina < maxStamina)
            stamina += staminaIncrease * Time.deltaTime;
        else if (stamina > maxStamina)
            stamina = maxStamina;

        if (staminaUsing != 0f)
            stamina -= staminaUsing * Time.deltaTime;

        if (stamina < 0f)
            stamina = 0f;
    }

    public void Climbing()
    {
        if (Input.GetButton("Jump"))
        {
            if (stamina >= 60)
            {
                isClimbing = true;

                velocity.y = Mathf.Sqrt(pullUpHeight * -2f * gravity);

                stamina -= staminaPullUp;
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ClimbableEdge"))
        {
            canClimb = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ClimbableEdge"))
        {
            canClimb = false;
        }
    }

    public void CheckVelocity()
    {
        currentYVelocity = velocity.y;
    }

    public void CheckState()
    {
        if (x == 0 && z == 0 || !isGrounded)
        {
            isSprinting = false;
            isWalking = false;
            isCrouchWalking = false;
        }
        else if (speed == crouchSpeed && isGrounded && x != 0f || speed == crouchSpeed && isGrounded && z != 0f)
        {
            isCrouching = true;
            isCrouchWalking = true;

            isSprinting = false;
            isWalking = false;
        }
        
        else if (speed == walkSpeed && isGrounded && x != 0f || speed == walkSpeed && isGrounded && z != 0f)
        {
            isWalking = true;

            isSprinting = false;
            isCrouching = false;
        }

        else if (speed == sprintSpeed && isGrounded && x != 0f || speed == sprintSpeed && isGrounded && z != 0f)
        {
            isSprinting = true;

            isCrouching = false;
            isWalking = false;
        }
        
        else if (speed == crouchSpeed && isGrounded && x == 0f || speed == crouchSpeed && isGrounded && z == 0f)
        {
            isCrouching = true;

            isCrouchWalking = false;
            isSprinting = false;
            isWalking = false;
        }
    }

    IEnumerator staminaCoolOff()
    {
        yield return new WaitForSeconds(2f);
    }
}