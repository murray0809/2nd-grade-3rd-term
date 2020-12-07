﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] GameObject player;

    Rigidbody m_rb;

    ConfigurableJoint joint;
    SoftJointLimit limit;

    [SerializeField] GameObject targetManage;
    TargetManager targetManager;

    [SerializeField] TargetController m_target;

    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        joint = player.GetComponent<ConfigurableJoint>();
    }

    void Update()
    {
        m_target = targetManager.NowTarget;

        if (Input.GetButtonDown("Fire1"))
        {
            limit.limit = Vector3.Distance(player.transform.position, target.transform.position);
            joint.linearLimit = limit;
            Debug.Log(limit.limit);
        }
        if (Input.GetButton("Fire1"))
        {
            Rigidbody rb = target.GetComponent<Rigidbody>();

            if (rb)
            {
                joint.connectedBody = rb;
                joint.xMotion = ConfigurableJointMotion.Limited;
                joint.yMotion = ConfigurableJointMotion.Limited;
            }
        }
        if (Input.GetButtonUp("Fire1"))
        {
            joint.connectedBody = null;
            joint.xMotion = ConfigurableJointMotion.Free;
            joint.yMotion = ConfigurableJointMotion.Free;
        }
    }

    void Hook(ConfigurableJoint joint, TargetController target)
    {
        Rigidbody rb = target.GetComponent<Rigidbody>();

        if (rb)
        {
            joint.connectedBody = rb;
            joint.xMotion = ConfigurableJointMotion.Limited;
            joint.yMotion = ConfigurableJointMotion.Limited;
        }
    }
}