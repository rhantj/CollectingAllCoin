using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    PlayerContext plc;
    float moveSpeed = 10f;
    [SerializeField] Vector3 jumpForce;
    Vector3 jumpeForceVelocity;
    float jumpHeight = 8f;
    Vector2 moveInput;
    public Vector2 MoveInput
    {
        get {  return moveInput; }
        set { moveInput = value; }
    }

    [Header("Gravity")]
    [SerializeField] float playerGravity = 0.0f;
    [SerializeField] float gravity = 0.03f;
    [SerializeField] bool isGrounded;
    public bool IsGrounded { get { return isGrounded; } }


    private void Awake()
    {
        plc = GetComponent<PlayerContext>();
    }

    private void Update()
    {
        CalculateMovement();
        CalculateJump();

        isGrounded = plc.Controller.isGrounded;
    }

    void CalculateMovement()
    {
        float moveX = moveInput.x * moveSpeed;
        float moveZ = moveInput.y * moveSpeed;
        var Combine = new Vector3(moveX, 0f, moveZ);
        var moveDir = Combine * Time.deltaTime;

        var newDir = transform.TransformDirection(moveDir);

        if (playerGravity > -1f)
        {
            playerGravity -= gravity * Time.deltaTime;
        }

        if (playerGravity < -0.05f && isGrounded)
        {
            playerGravity = -0.1f;
        }

        newDir.y += playerGravity;
        newDir += jumpForce * Time.deltaTime;

        plc.PlayerAvata.transform.LookAt(plc.PlayerAvata.transform.position + Combine);
        plc.Controller.Move(newDir);
    }

    void CalculateJump()
    {
        jumpForce = Vector3.SmoothDamp(jumpForce, Vector3.zero, ref jumpeForceVelocity, 1);
    }

    public void Jump()
    {
        if (!isGrounded) return;
        jumpForce = Vector3.up * jumpHeight;
        playerGravity = 0;
    }

}
