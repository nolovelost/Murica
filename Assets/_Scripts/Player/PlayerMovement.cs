using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    IInput playerInput;
    Animator animator;
    Rigidbody2D rb;
    int moveDirection = 0;

    public float speed = 5.0f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        SetAnimation();
        //moveDirection = animator.GetInteger("MoveDirection");
        MovePlayer();
    }

    void MovePlayer()
    {
        if (((moveDirection >> 1) & 1) == 1)
            rb.position += (Vector2)transform.up * speed * Time.deltaTime;
        if (((moveDirection >> 2) & 1) == 1)
            rb.position += (Vector2)transform.up * -1.0f * speed * Time.deltaTime;
        if (((moveDirection >> 3) & 1) == 1)
            rb.position += (Vector2)transform.right * -1.0f * speed * Time.deltaTime;
        if (((moveDirection >> 4) & 1) == 1)
            rb.position += (Vector2)transform.right * speed * Time.deltaTime;
    }

    void SetAnimation()
    {
        bool isVertMoving = false;
        bool isSideMoving = false;

        if (Input.GetKey(KeyCode.W))
        {
            animator.SetInteger("MoveDirection", 1);
            isVertMoving = true;
            moveDirection |= 1 << 1;
            moveDirection &= ~(1 << 2);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            animator.SetInteger("MoveDirection", 2);
            isVertMoving = true;
            moveDirection |= 1 << 2;
            moveDirection &= ~(1 << 1);
        }
        else
        {
            isVertMoving = false;
            moveDirection &= ~(1 << 1);
            moveDirection &= ~(1 << 2);
        }

        if (Input.GetKey(KeyCode.A))
        {
            animator.SetInteger("MoveDirection", 3);
            isSideMoving = true;
            moveDirection |= 1 << 3;
            moveDirection &= ~(1 << 4);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            animator.SetInteger("MoveDirection", 4);
            isSideMoving = true;
            moveDirection |= 1 << 4;
            moveDirection &= ~(1 << 3);
        }
        else
        {
            isSideMoving = false;
            moveDirection &= ~(1 << 3);
            moveDirection &= ~(1 << 4);
        }

        if (!isVertMoving && !isSideMoving)
        {
            animator.SetInteger("MoveDirection", 0);
            moveDirection = 0;
        }
    }
}
