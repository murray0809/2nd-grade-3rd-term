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
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");

        Vector3 dir = Vector3.right * h;

        if (!m_anim)
        {
            Vector3 vel = m_rb.velocity;
            vel.x = h * moveSpeed;
            m_rb.velocity = vel;
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
