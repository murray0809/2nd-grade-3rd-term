using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    Rigidbody m_rb;
    ConfigurableJoint m_joint;

    [SerializeField] GameObject m_player;

    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        m_joint = GetComponent<ConfigurableJoint>();

        m_player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetButton("RightCommand")))
        {

            Rigidbody rb = m_player.GetComponent<Rigidbody>();

            if (rb)
            {
                m_joint.connectedBody = rb;
                m_joint.xMotion = ConfigurableJointMotion.Limited;
                m_joint.yMotion = ConfigurableJointMotion.Limited;
            }
        }

        //ワイヤーとターゲットを切り離す
        if (Input.GetButtonUp("RightCommand") || Input.GetButtonUp("RightCtrl"))
        {
            m_joint.connectedBody = null;
            m_joint.xMotion = ConfigurableJointMotion.Free;
            m_joint.yMotion = ConfigurableJointMotion.Free;

            if ((Input.GetButton("RightCommand")))
            {


                Rigidbody rb = m_player.GetComponent<Rigidbody>();

                if (rb)
                {
                    m_joint.connectedBody = rb;
                    m_joint.xMotion = ConfigurableJointMotion.Limited;
                    m_joint.yMotion = ConfigurableJointMotion.Limited;
                }
            }

            //ワイヤーとターゲットを切り離す
            if (Input.GetButtonUp("RightCommand") || Input.GetButtonUp("RightCtrl"))
            {
                m_joint.connectedBody = null;
                m_joint.xMotion = ConfigurableJointMotion.Free;
                m_joint.yMotion = ConfigurableJointMotion.Free;


            }
        }
    }
}
