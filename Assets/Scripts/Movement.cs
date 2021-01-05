using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform floor;
    public float moveSpeed;
    private bool canJump;
    
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
    }

    private void OnCollisionStay(Collision collision)
    {
        canJump = true;
    }
}
