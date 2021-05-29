using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーを引き寄せるワイヤーのスクリプト
/// </summary>
public class Wire1 : MonoBehaviour
{
    [SerializeField] GameObject m_player;

    Rigidbody m_rb;

    ConfigurableJoint m_joint;
    SoftJointLimit m_limit;
    SoftJointLimit m_limit2;

    [SerializeField] float a;

    bool m_flag = false;

    private bool m_onCamera = false;

    LineRenderer m_lineRenderer;

    float m_distanse;

    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_rb = GetComponent<Rigidbody>();
        m_joint = GetComponent<ConfigurableJoint>();

        m_limit2.limit = 1f;

        m_lineRenderer = GetComponent<LineRenderer>();
        m_onCamera = false;
    }

    void Update()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_distanse = Vector3.Distance(m_player.transform.position, this.transform.position);
        
        if (m_distanse < 7)
        {
            if ((Input.GetButtonDown("RightCommand") || Input.GetButtonDown("RightCtrl")))
            {
                m_limit.limit = Vector3.Distance(transform.position, m_player.transform.position);
                m_joint.linearLimit = m_limit;

                m_limit2.limit = Vector3.Distance(transform.position, m_player.transform.position);

                Rigidbody rb = m_player.GetComponent<Rigidbody>();

                if (rb)
                {
                    m_joint.connectedBody = rb;
                    m_joint.xMotion = ConfigurableJointMotion.Limited;
                    m_joint.yMotion = ConfigurableJointMotion.Limited;
                }

                m_flag = true;
            }

            if (m_flag)
            {
                m_limit2.limit -= a;
                m_joint.linearLimit = m_limit2;

                m_lineRenderer.SetPosition(0, m_player.transform.position);
                m_lineRenderer.SetPosition(1, transform.position);

                m_lineRenderer.startWidth = 0.1f;
                m_lineRenderer.endWidth = 0.1f;
            }

            if (m_limit2.limit < 0.5f)
            {
                m_joint.connectedBody = null;
                m_joint.xMotion = ConfigurableJointMotion.Free;
                m_joint.yMotion = ConfigurableJointMotion.Free;
                m_flag = false;

                m_lineRenderer.startWidth = 0;
                m_lineRenderer.endWidth = 0;
            }
        }
    }

    private void OnBecameVisible()
    {
        m_onCamera = true;
        this.GetComponent<Renderer>().material.color = Color.red;
    }

    private void OnBecameInvisible()
    {
        m_onCamera = false;
        this.GetComponent<Renderer>().material.color = Color.white;
    }
}
