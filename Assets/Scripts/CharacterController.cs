using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    bool jump = false;

    [SerializeField] float jumpPower;

    [SerializeField] float moveSpeed;
    [SerializeField] float m_movingPowerInTheAir = 5f;
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

        if (!m_anim && !jump)
        {
            Vector3 vel = m_rb.velocity;
            vel.x = h * moveSpeed;
            m_rb.velocity = vel;
        }

        if (!jump && Input.GetButtonDown("Jump"))
        {
            m_rb.AddForce(new Vector3(0f, jumpPower, 0f), ForceMode.Impulse);
            jump = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        jump = false;
    }
}
