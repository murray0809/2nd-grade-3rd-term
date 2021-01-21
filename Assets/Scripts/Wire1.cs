using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire1 : MonoBehaviour
{
    [SerializeField] GameObject player;

    Rigidbody m_rb;

    ConfigurableJoint joint;
    SoftJointLimit limit;
    SoftJointLimit limit2;

    [SerializeField] float a;

    bool flag = false;

    CharacterController characterController;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        m_rb = GetComponent<Rigidbody>();
        joint = GetComponent<ConfigurableJoint>();
        characterController = player.GetComponent<CharacterController>();

        limit2.limit = 1f;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && !characterController.CanJump)
        {
            limit.limit = Vector3.Distance(transform.position, player.transform.position);
            joint.linearLimit = limit;

            limit2.limit = Vector3.Distance(transform.position, player.transform.position);

            Rigidbody rb = player.GetComponent<Rigidbody>();

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

        if (limit2.limit < 1f)
        {
            joint.connectedBody = null;
            joint.xMotion = ConfigurableJointMotion.Free;
            joint.yMotion = ConfigurableJointMotion.Free;
            flag = false;
        }
    }
}
