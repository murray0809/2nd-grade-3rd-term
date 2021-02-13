﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] GameObject m_player;
    [SerializeField] GameObject m_rope;

    private bool m_touch = false;
    public bool Touch { get { return m_touch; } set { m_touch = value; } }

    bool touching = false;

    Rigidbody m_playerRb;
    Rigidbody m_rb;

    ConfigurableJoint configurableJoint;

    CharacterController characterController;

    GetAngle getAngle;
    [SerializeField] GameObject m_angle;

    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_playerRb = m_player.GetComponent<Rigidbody>();
        configurableJoint = GetComponent<ConfigurableJoint>();
        m_rb = GetComponent<Rigidbody>();
        getAngle = m_angle.GetComponent<GetAngle>();
    }

    void Update()
    {
        Debug.Log(getAngle.Angle);
        if (m_touch && Input.GetMouseButton(0))
        {
            //m_playerRb.mass = 0;

            configurableJoint.connectedBody = m_playerRb;
            configurableJoint.xMotion = ConfigurableJointMotion.Limited;
            configurableJoint.yMotion = ConfigurableJointMotion.Limited;

            //if (getAngle.Angle < -120f)
            //{ 
            //    Vector3 force = new Vector3(2f, 0.0f, 0f);
            //    m_rb.AddForce(force, ForceMode.Impulse);
            //}
            //else if (getAngle.Angle > -60f)
            //{
            //    Vector3 force = new Vector3(-2f, 0.0f, 0f);
            //    m_rb.AddForce(force, ForceMode.Impulse);
            //}


            if (Input.GetKeyDown(KeyCode.D))
            {
                Vector3 force = new Vector3(0.1f, 0.0f, 0f);
                m_playerRb.AddForce(force, ForceMode.Impulse);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                Vector3 force = new Vector3(-0.1f, 0.0f, 0f);
                m_playerRb.AddForce(force, ForceMode.Impulse);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            configurableJoint.connectedBody = null;
            configurableJoint.xMotion = ConfigurableJointMotion.Free;
            configurableJoint.yMotion = ConfigurableJointMotion.Free;

            //m_playerRb.mass = 1;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_touch = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_touch = false;
        }
    }
}
