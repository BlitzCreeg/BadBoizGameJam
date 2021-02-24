using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public GameObject player;
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

    public float fov = 60;
    public float sprintFov = 80;

    public Vector3 playerHeight;
    public Vector3 crouchHeight;
    public Vector3 crouchCamScale;
    public Vector3 standCamScale;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    bool isClimbing;
    bool canClimb;

    private void Awake()
    {
        playerHeight = new Vector3(1f, 1f, 1f);
        crouchHeight = new Vector3(1f, 0.5f, 1f);
        crouchCamScale = new Vector3(1f, 2f, 1f);
        standCamScale = new Vector3(1f, 1f, 1f);

        playerCam.fieldOfView = fov;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfGrounded();
        Jump();
        MovePlayer();
        ApplyGravity();
        Crouch();
        Sprint();
        StaminaUpdate();
        if (canClimb)
            Climbing();
    }

    //Checks to see if player is grounded
    public void CheckIfGrounded()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
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
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded && stamina >= 30f && player.transform.localScale == playerHeight)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            stamina -= staminaJump;
        }
    }

    public void ApplyGravity()
    {
        if (!isClimbing)
        {
            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        }
    }

    //Crouches the player by adjusting the height of the Player asset
    public void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            player.transform.localScale = crouchHeight;
            playerCam.transform.localScale = crouchCamScale;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            player.transform.localScale = playerHeight;
            playerCam.transform.localScale = standCamScale;
        }

        if (player.transform.localScale == crouchHeight)
            speed = 4f;
        else
            speed = 12f;
    }

    public void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && stamina >= 10f)
        {
            speed = 18f;
            staminaUsing = 20f;

            playerCam.fieldOfView = sprintFov;
        }

        if (stamina < 10f)
        {
            speed = 12f;
            playerCam.fieldOfView = fov;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 12f;
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

    IEnumerator staminaCoolOff()
    {
        yield return new WaitForSeconds(2f);
    }
}