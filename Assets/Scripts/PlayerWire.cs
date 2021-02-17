using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWire : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject targetPlayer;

    Rigidbody m_rb;

    SpringJoint springJoint;

    ConfigurableJoint joint;
    SoftJointLimit limit;
    SoftJointLimit limit2;

    bool flag = false;

    [SerializeField] float a;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        m_rb = GetComponent<Rigidbody>();
        springJoint = player.GetComponent<SpringJoint>();
        joint = player.GetComponent<ConfigurableJoint>();

        limit2.limit = 1f;
    }

    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, targetPlayer.transform.position);

        //Debug.Log(distance);

        if (Input.GetMouseButtonDown(1))
        {
            //Rigidbody rb = targetPlayer.GetComponent<Rigidbody>();
            //if (rb)
            //{
            //    springJoint.connectedBody = rb;
            //}

            limit.limit = Vector3.Distance(player.transform.position, targetPlayer.transform.position);
            joint.linearLimit = limit;
            Debug.Log(limit.limit);

            limit2.limit = Vector3.Distance(player.transform.position, targetPlayer.transform.position);

            Rigidbody rb = targetPlayer.GetComponent<Rigidbody>();

            if (rb)
            {
                joint.connectedBody = rb;
                joint.xMotion = ConfigurableJointMotion.Limited;
                joint.yMotion = ConfigurableJointMotion.Limited;
            }

            flag = true;
        }

        if (flag)
        {
            limit2.limit -= a;
            joint.linearLimit = limit2;
        }

        if (limit2.limit < 3f)
        {
            //joint.connectedBody = null;
            //joint.xMotion = ConfigurableJointMotion.Free;
            //joint.yMotion = ConfigurableJointMotion.Free;
            //flag = false;
            limit2.limit = 3f;
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
