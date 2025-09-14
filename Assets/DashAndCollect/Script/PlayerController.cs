using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rgd2D;
    public Vector2 moveInput;

    public float activeMoveSpeed;
    public float dashSpeed;

    public float dashLength = .5f, dashCoolDown = 1f;

    public float dashCounter = 1f;
    public float dashCoolCounter = 1f;



    private void Start()
    {
        activeMoveSpeed = moveSpeed;
    }

    private void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();
        rgd2D.linearVelocity = moveInput * activeMoveSpeed;

        if (Input.GetKeyDown(KeyCode.Space)) {
            if (dashCoolCounter <= 0 && dashCounter <= 0) {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
            }
        }

        if (dashCounter > 0) { 
            dashCounter -= Time.deltaTime;

            if (dashCounter <= 0) { 
                activeMoveSpeed = moveSpeed;
                dashCoolCounter = dashCoolDown;
            }
        }

        if (dashCoolCounter > 0) { 
            dashCoolCounter -= Time.deltaTime;
        }
    }
}
