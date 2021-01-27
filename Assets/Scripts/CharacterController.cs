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

    bool m_flag = false;
    bool m_flag2 = false;

    private Vector3 m_nowPos;

    bool m_zMove = false;

    [SerializeField] Lane m_laneMode = Lane.Lane2;
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");

        m_nowPos = transform.position;

        if (transform.position.z > 1.2f)
        {
            m_nowPos.z = 1.2f;
            transform.position = m_nowPos;
        }
        else if (transform.position.z < -1.2f)
        {
            m_nowPos.z = -1.2f;
            transform.position = m_nowPos;
        }

        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    m_rb.AddForce(new Vector3(0, 0, 5f), ForceMode.Impulse);
           
        //}
        //else if (Input.GetKeyDown(KeyCode.S))
        //{
        //    m_rb.AddForce(new Vector3(0, 0, -5f), ForceMode.Impulse);
            
        //}

        if (!m_anim && m_canJump)
        {
            Vector3 vel = m_rb.velocity;
            vel.x = h * moveSpeed;
            m_rb.velocity = vel;
        }

        if (!m_canJump)
        {
            m_rb.AddForce(0, -9.8f, 0);
        }
        else if (m_canJump && Input.GetButtonDown("Jump") && Input.GetKey(KeyCode.S))
        {
            m_flag2 = false;
            Jump(false);
        }
        else if (m_canJump && Input.GetButtonDown("Jump") && Input.GetKey(KeyCode.W))
        {
            m_flag = false;
            Jump(true);
        }
        else if (m_canJump && Input.GetButtonDown("Jump"))
        {
            m_rb.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
        }

        if (transform.position.z > 0 && m_flag)
        {
            m_nowPos.z = 0;
            transform.position = m_nowPos;
        }
        else if (transform.position.z < 0 && m_flag2)
        {
            m_nowPos.z = 0;
            transform.position = m_nowPos;
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

    private void Jump(bool flag)
    {
        m_canJump = false;

        if (flag)
        {
            if (m_laneMode == Lane.Lane2)
            {
                m_rb.AddForce(new Vector3(0, jumpPower, 2f), ForceMode.Impulse);
                m_laneMode = Lane.Lane1;
            }
            else if (m_laneMode == Lane.Lane3)
            {
                m_rb.AddForce(new Vector3(0, jumpPower, 2f), ForceMode.Impulse);
                m_laneMode = Lane.Lane2;
                m_flag = true;
            }
        }
        else
        {
            if (m_laneMode == Lane.Lane1)
            {
                m_rb.AddForce(new Vector3(0, jumpPower, -2f), ForceMode.Impulse);
                m_laneMode = Lane.Lane2;
                m_flag2 = true;
            }
            else if (m_laneMode == Lane.Lane2)
            {
                m_rb.AddForce(new Vector3(0, jumpPower, -2f), ForceMode.Impulse);
                m_laneMode = Lane.Lane3;
            }
        }
    }
}

public enum Lane
{
    Lane1,
    Lane2,
    Lane3,
}