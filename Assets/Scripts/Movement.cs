using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform floor;
    public float moveSpeed;
    private bool canJump;
    private bool movingDown;
    private bool movingUp;
    private float yPos;
    private float zPos;

    Rigidbody rb;

    bool flag = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //transform.position = new Vector3(-12f,1f,5f);
    }

    void Update()
    {
        if (Input.GetButton("A"))
        {
            transform.position = transform.position + transform.TransformDirection(Vector3.left) * moveSpeed;
        }

        if (Input.GetButton("D"))
        {
            transform.position = transform.position + transform.TransformDirection(-Vector3.left) * moveSpeed;
        }

        if (!canJump)
        {
            rb.AddForce(0, -10f, 0);
        }
        else if (Input.GetButton("space") && canJump)
        {
            canJump = false;

            rb.AddForce(0, 250f, 0);
        }

        //zPos = transform.position.z;

        if (Input.GetButton("S") && flag)
        {
            //if (zPos > 4.9 && zPos < 5.1f)
            //{
            //    movingDown = true;
            //    yPos = 2.0f;
            //}
            //else if (zPos > 7.9f && zPos < 8.1f)
            //{
            //    movingDown = true;
            //    yPos = 5.0f;
            //}

            transform.position = transform.position + transform.TransformDirection(-Vector3.forward) * moveSpeed;
        }

        if (Input.GetButton("W") && flag)
        {
            //if (zPos > 1.9 && zPos < 2.1f)
            //{
            //    movingUp = true;
            //    yPos = 5.0f;
            //}
            //else if (zPos > 4.9f && zPos < 5.1f)
            //{
            //    movingUp = true;
            //    yPos = 8.0f;
            //}

            transform.position = transform.position + transform.TransformDirection(Vector3.forward) * moveSpeed;
        }

        MoveUporDown(yPos);

        flag = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform == floor)
        {
            canJump = true;
        }

        if (collision.gameObject.tag == "Target")
        {
            flag = true;
        }
    }

    void MoveUporDown(float target)
    {
        if (!movingDown)
        {
            if (zPos <= target)
            {
                movingDown = false;

                //zPos = target;
            }
            else
            {
                transform.position = transform.position + transform.TransformDirection(-Vector3.forward) * moveSpeed;
            }
        }

        if (movingUp)
        {
            if (zPos >= target)
            {
                movingUp = false;
            }
            else
            {
                transform.position = transform.position + transform.TransformDirection(Vector3.forward) * moveSpeed;
            }
        }
    }
}
