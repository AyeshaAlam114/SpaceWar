using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        groundCheck = transform.GetChild(1).transform.GetChild(0).transform;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (GetComponent<PhotonView>().IsMine)
        {
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            Move();
            Jump();

        }


    }

    void Move()
    {
        float xPosition = Input.GetAxis("Horizontal");
        float zPosition = Input.GetAxis("Vertical");

        Vector3 move = transform.right * xPosition + transform.forward * zPosition;
        controller.Move(move * speed * Time.deltaTime);
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}