using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform m_floor;
    public float m_moveSpeed;
    private bool m_canJump;
    
    Rigidbody m_rb;

    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetButton("A"))
        {
            transform.position = transform.position + transform.TransformDirection(Vector3.left) * m_moveSpeed;
        }

        if (Input.GetButton("D"))
        {
            transform.position = transform.position + transform.TransformDirection(-Vector3.left) * m_moveSpeed;
        }

        if (!m_canJump)
        {
            m_rb.AddForce(0, -10f, 0);
        }
        else if (Input.GetButton("space") && m_canJump)
        {
            m_canJump = false;

            m_rb.AddForce(0, 250f, 0);
        }

        Debug.Log(m_canJump);
    }

    private void OnCollisionStay(Collision collision)
    {
        m_canJump = true;
    }
}
