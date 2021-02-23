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
    public float stamina = 100f;
    public float staminaUsing = 0f;
    public float staminaJump = 40f;

    public float jumpHeight = 3f;

    public Vector3 playerHeight;
    public Vector3 crouchHeight;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    private void Awake()
    {
        playerHeight = new Vector3(1f, 1f, 1f);
        crouchHeight = new Vector3(1f, 0.6f, 1f);

        playerCam.fieldOfView = 70;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfGrounded();
        Jump();
        MovePlayer();
        Crouch();
        Sprint();
        StaminaUpdate();
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

        if (Input.GetButtonDown("Jump") && isGrounded && stamina >= 15f && player.transform.localScale == playerHeight)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            stamina -= staminaJump;
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    //Crouches the player by adjusting the height of the Player asset
    public void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
            player.transform.localScale = crouchHeight;

        if (Input.GetKeyUp(KeyCode.LeftControl))
            player.transform.localScale = playerHeight;

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

            playerCam.fieldOfView = 90;
        }

        if (stamina < 10f)
        {
            speed = 12f;
            playerCam.fieldOfView = 70;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 12f;
            staminaUsing = 0f;
            playerCam.fieldOfView = 70;
        }
    }

    public void StaminaUpdate()
    {
        if (staminaUsing == 0f && stamina < 100f)
            stamina += 5 * Time.deltaTime;
        else if (stamina > 100f)
            stamina = 100f;

        if (staminaUsing != 0f)
            stamina -= staminaUsing * Time.deltaTime;

        if (stamina < 0f)
            stamina = 0f;
    }
}
