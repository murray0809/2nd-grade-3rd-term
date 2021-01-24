using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private bool m_canJump;
    public bool CanJump { get { return m_canJump; } }

    [SerializeField] float jumpPower;

    [SerializeField] float moveSpeed;
    Rigidbody m_rb;

    [SerializeField] Animator m_anim;

    bool flag = false;

    private Vector3 m_nowPos;

    [SerializeField] Lane m_mode = Lane.Lane2;

    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 dir = Vector3.right * h;

        m_nowPos = transform.position;

        //if (m_mode == Lane.Lane1)
        //{
        //    m_nowPos.z = 1f;
        //    transform.position = m_nowPos;
        //}
        //else if(m_mode == Lane.Lane2)
        //{
        //    m_nowPos.z = 0;
        //    transform.position = m_nowPos;
        //}
        //else
        //{
        //    m_nowPos.z = -1f;
        //    transform.position = m_nowPos;
        //}

        if (Input.GetButtonDown("W"))
        {
            if (m_mode == Lane.Lane2)
            {
                m_mode = Lane.Lane1;
            }
            else if (m_mode == Lane.Lane3)
            {
                m_mode = Lane.Lane2;
            }
        }

        if (Input.GetButtonDown("S"))
        {
            if (m_mode == Lane.Lane1)
            {
                m_mode = Lane.Lane2;
            }
            else if (m_mode == Lane.Lane2)
            {
                m_mode = Lane.Lane3;
            }
        }

        if (!m_anim && m_canJump)
        {
            Vector3 vel = m_rb.velocity;
            vel.x = h * moveSpeed;
            
                vel.z = v * moveSpeed;
            
            m_rb.velocity = vel;

            if (transform.position.z > 1f)
            {
                m_nowPos.z = 1f;
                transform.position = m_nowPos;
            }
            else if (transform.position.z < -1f)
            {
                m_nowPos.z = -1f;
                transform.position = m_nowPos;
            }
        }

        if (!m_canJump)
        {
            m_rb.AddForce(0, -9.8f, 0);
        }
        else if (m_canJump && Input.GetButtonDown("Jump"))
        {
            m_canJump = false;
            m_rb.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
        }
    }

   
    private void OnCollisionStay(Collision collision)
    {
        m_canJump = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        m_canJump = false;
    }
}

public enum Lane
{
    Lane1,
    Lane2,
    Lane3,
}
