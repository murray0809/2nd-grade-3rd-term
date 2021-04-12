using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWire : MonoBehaviour
{
    [SerializeField] GameObject m_player;
    [SerializeField] GameObject m_targetPlayer;

    Rigidbody m_rb;

    SpringJoint m_springJoint;

    ConfigurableJoint m_joint;
    SoftJointLimit m_limit;
    SoftJointLimit m_limit2;

    bool m_flag = false;

    [SerializeField] float m_a;

    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_rb = GetComponent<Rigidbody>();
        m_springJoint = m_player.GetComponent<SpringJoint>();
        m_joint = m_player.GetComponent<ConfigurableJoint>();

        m_limit2.limit = 1f;
    }

    void Update()
    {
        float distance = Vector3.Distance(m_player.transform.position, m_targetPlayer.transform.position);

        //Debug.Log(distance);

        if (Input.GetButtonDown("RightCommand"))
        {
            //Rigidbody rb = targetPlayer.GetComponent<Rigidbody>();
            //if (rb)
            //{
            //    springJoint.connectedBody = rb;
            //}

            m_limit.limit = Vector3.Distance(m_player.transform.position, m_targetPlayer.transform.position);
            m_joint.linearLimit = m_limit;
            Debug.Log(m_limit.limit);

            m_limit2.limit = Vector3.Distance(m_player.transform.position, m_targetPlayer.transform.position);

            Rigidbody rb = m_targetPlayer.GetComponent<Rigidbody>();

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
            m_limit2.limit -= m_a;
            m_joint.linearLimit = m_limit2;
        }

        if (m_limit2.limit < 3f)
        {
            //joint.connectedBody = null;
            //joint.xMotion = ConfigurableJointMotion.Free;
            //joint.yMotion = ConfigurableJointMotion.Free;
            //flag = false;
            m_limit2.limit = 3f;
        }

        //if (Input.GetMouseButtonDown(0))
        //{
        //    springJoint.connectedBody = null;
        //}

        //if (distance < 0.1f)
        //{
        //    springJoint.connectedBody = null;
        //}
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "TargetPlayer")
        {
            Debug.Log("接触");
        }
    }
}
