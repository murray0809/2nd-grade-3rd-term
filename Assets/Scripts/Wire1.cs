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

    private bool m_onCamera = false;

    CharacterController characterController;

    LineRenderer lineRenderer;

    float distanse;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        m_rb = GetComponent<Rigidbody>();
        joint = GetComponent<ConfigurableJoint>();
        characterController = player.GetComponent<CharacterController>();

        limit2.limit = 1f;

        lineRenderer = GetComponent<LineRenderer>();
        m_onCamera = false;
    }

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        distanse = Vector3.Distance(player.transform.position, this.transform.position);
        
        if (distanse < 7)
        {
            if ((Input.GetButtonDown("RightCommand") || Input.GetButtonDown("RightCtrl")) && !characterController.CanJump)
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

                lineRenderer.SetPosition(0, player.transform.position);
                lineRenderer.SetPosition(1, transform.position);

                lineRenderer.startWidth = 0.1f;
                lineRenderer.endWidth = 0.1f;
            }

            if (limit2.limit < 0.5f)
            {
                joint.connectedBody = null;
                joint.xMotion = ConfigurableJointMotion.Free;
                joint.yMotion = ConfigurableJointMotion.Free;
                flag = false;

                lineRenderer.startWidth = 0;
                lineRenderer.endWidth = 0;
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
