using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    /// <summary>ワイヤーの射程距離</summary>
    [SerializeField] float m_targetRange = 6f;

    [SerializeField] List<TargetController> myList = new List<TargetController>();

    [SerializeField] GameObject player;

    [SerializeField] TargetController m_target;
    [SerializeField] GameObject m_player;

    Rigidbody m_rb;

    ConfigurableJoint joint;
    SoftJointLimit limit;
    public TargetController NowTarget { get { return nowTarget; } }
    [SerializeField] TargetController nowTarget;

    [SerializeField] float minDistance;

    int index = 0;

    bool connecting = false;



    [SerializeField] int targetCount = 0;
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        joint = m_player.GetComponent<ConfigurableJoint>();

        targetCount = this.transform.childCount;
    }

    void Update()
    {
        myList.Clear();

        TargetController[] targets = transform.GetComponentsInChildren<TargetController>();

        foreach (TargetController t in targets)
        {
            if (t.IsHookable)
            {
                myList.Add(t);
            }
        }

        if (!connecting)
        {
            for (int i = 0; i < myList.Count; i++)
            {
                float distance = Vector3.Distance(player.transform.position, myList[i].transform.position);

                if (i == 0)
                {
                    minDistance = distance;
                    index = i;
                }
                else
                {
                    if (minDistance > distance)
                    {
                        minDistance = distance;
                        index = i;
                    }
                }
                Debug.Log("target" + i + distance);
            }
            nowTarget = myList[index];

            m_target = nowTarget;
        }
       

        if (Input.GetButtonDown("Fire1"))
        {
            limit.limit = Vector3.Distance(player.transform.position, m_target.transform.position);
            joint.linearLimit = limit;
            Debug.Log(limit.limit);
        }
        if (Input.GetButton("Fire1"))
        {
            connecting = true;

            Rigidbody rb = m_target.GetComponent<Rigidbody>();

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

            connecting = false;
        }
    }
}
