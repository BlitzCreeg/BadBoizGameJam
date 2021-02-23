using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public GameObject player;

    public float speed = 12f;
    public float gravity = -9.81f;

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
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfGrounded();
        CheckIfCanJump();
        MovePlayer();
        Crouch();
    }

    public void CheckIfGrounded()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    public void CheckIfCanJump()
    {
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    public void MovePlayer()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    public void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
            player.transform.localScale = crouchHeight;

        if (Input.GetKeyUp(KeyCode.LeftControl))
            player.transform.localScale = playerHeight;
    }
}
