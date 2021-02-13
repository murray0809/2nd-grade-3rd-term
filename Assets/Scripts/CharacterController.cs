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
    public Vector3 NowPos { get { return m_nowPos; } set { m_nowPos = value; } }

    private bool m_catch = false;
    public bool Catch { get { return m_catch; } set { m_catch = value; } }

    [SerializeField] GameObject m_catchObject;
    public GameObject CatchObject { get { return m_catchObject; } set { m_catchObject = value; } }

    private bool m_canMove = true;
    public bool CanMove { get { return m_canMove; } set { m_canMove = value; } }

    [SerializeField] Lane m_mode = Lane.Lane2;

    bool m_moveLane = false;
    bool a = true;

    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (transform.position.z == 1.2f)
        {
            m_mode = Lane.Lane1;
        }
        else if (transform.position.z == 0)
        {
            m_mode = Lane.Lane2;
        }
        else if (transform.position.z == -1.2f)
        {
            m_mode = Lane.Lane3;
        }

        if (m_moveLane)
        {
            MoveLane(a);
        }

        float h = Input.GetAxisRaw("Horizontal");

        m_nowPos = transform.position;

        if (!m_anim && m_canJump)
        {
            Vector3 vel = m_rb.velocity;
            vel.x = h * moveSpeed;
            m_rb.velocity = vel;
        }

        if (Input.GetKeyDown(KeyCode.S) && m_canMove && (m_mode == Lane.Lane1 || m_mode == Lane.Lane2))
        {
            m_rb.constraints = RigidbodyConstraints.FreezeRotation;
            m_rb.AddForce(new Vector3(0, 0, -10f), ForceMode.Impulse);
            m_moveLane = true;
            a = false;
        }
        else if (Input.GetKeyDown(KeyCode.W) && m_canMove && (m_mode == Lane.Lane2 || m_mode == Lane.Lane3))
        {
            m_rb.constraints = RigidbodyConstraints.FreezeRotation;
            m_rb.AddForce(new Vector3(0, 0, 10f), ForceMode.Impulse);
            m_moveLane = true;
            a = true;
        }

        if (m_canJump && Input.GetButtonDown("Jump"))
        {
            m_rb.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
        }

        if (!m_canJump)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                m_rb.AddForce(new Vector3(3, 0, 0), ForceMode.Impulse);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                m_rb.AddForce(new Vector3(-3, 0, 0), ForceMode.Impulse);
            }
        }

        if (m_catch)
        {
            if (Input.GetButton("Fire1"))
            {
                m_catchObject.transform.SetParent(transform);
            }
            else
            {
                m_catchObject.transform.SetParent(null);
            }
        }
    }

    void MoveLane(bool flag)
    {
        if (flag)
        {
            switch (m_mode)
            {
                case Lane.Lane2:
                    if (transform.position.z > 1.2f)
                    {
                        m_nowPos.z = 1.2f;
                        transform.position = m_nowPos;
                        m_rb.isKinematic = false;
                        m_moveLane = false;
                        m_rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
                    }
                    break;
                case Lane.Lane3:
                    if (transform.position.z > 0)
                    {
                        m_nowPos.z = 0;
                        transform.position = m_nowPos;
                        m_rb.isKinematic = false;
                        m_moveLane = false;
                        m_rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
                    }
                    break;
            }
        }
        else
        {
            switch (m_mode)
            {
                case Lane.Lane1:
                    if (transform.position.z < 0)
                    {
                        m_nowPos.z = 0;
                        transform.position = m_nowPos;
                        m_rb.isKinematic = false;
                        m_moveLane = false;
                        m_rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
                    }
                    break;
                case Lane.Lane2:
                    if (transform.position.z < -1.2f)
                    {
                        m_nowPos.z = -1.2f;
                        transform.position = m_nowPos;
                        m_rb.isKinematic = false;
                        m_moveLane = false;
                        m_rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
                    }
                    break;
            }
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
            m_rb.AddForce(new Vector3(0, jumpPower, 2f), ForceMode.Impulse);
        }
        else
        {
            m_rb.AddForce(new Vector3(0, jumpPower, -2f), ForceMode.Impulse);
        }
    }
}

public enum Lane
{
    Lane1,
    Lane2,
    Lane3,
}