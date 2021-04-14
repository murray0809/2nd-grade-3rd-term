using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    /// <summary>
    /// ワイヤーの射程距離
    /// </summary>
    [SerializeField] float m_targetRange;

    [SerializeField] List<TargetController> m_myList = new List<TargetController>();

    [SerializeField] TargetController m_target;
    [SerializeField] GameObject m_player;

    Vector3 m_playerPos;
    public Vector3 PlayerPos { get { return m_playerPos; } }
    Vector3 m_targetPos;
    public Vector3 TargetPos { get { return m_targetPos; } }

    /// <summary>
    /// 現在のターゲット
    /// </summary>
    public TargetController NowTarget { get { return m_nowTarget; } }
    [SerializeField] TargetController m_nowTarget;

    [SerializeField] float m_minDistance;
    int m_index = 0;

    /// <summary>
    /// ワイヤーが引っ掛かっているかどうか
    /// </summary>
    bool m_connecting = false;
    public bool Connecting { get { return m_connecting; } }

    TargetController m_targetController;

    private float m_distance;

    Rigidbody m_rb;
    ConfigurableJoint m_joint;
    SoftJointLimit m_limit;

    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_rb = GetComponent<Rigidbody>();
        m_joint = m_player.GetComponent<ConfigurableJoint>();
        m_targetController = GetComponentInChildren<TargetController>();
    }

    void Update()
    {
        GetTarget();

        if (!m_connecting)
        {
            GetNearTarget();
        }

        //ワイヤーの射程距離の決定
        if (Input.GetButtonDown("RightCommand") || Input.GetButtonDown("RightCtrl"))
        {
            m_limit.limit = m_targetRange;
            m_joint.linearLimit = m_limit;
        }

        //ワイヤーとターゲットを繋げる
        if ((Input.GetButton("RightCommand") || Input.GetButton("RightCtrl")) && m_distance <= 4f && m_target)
        {
            m_connecting = true;

            Rigidbody rb = m_target.GetComponent<Rigidbody>();

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

            m_connecting = false;
        }

        m_playerPos = m_player.transform.position;

        if (m_target)
        {
            m_targetPos = m_target.transform.position;
            m_distance = Vector3.Distance(m_target.transform.position, m_player.transform.position);
        }
    }

    /// <summary>
    /// ターゲットを取得
    /// </summary>
    private void GetTarget()
    {
        m_myList.Clear();

        //子オブジェクトを取得
        TargetController[] targets = transform.GetComponentsInChildren<TargetController>();

        //画面内にあるものだけをリストに追加
        foreach (TargetController t in targets)
        {
            if (t.IsHookable)
            {
                m_myList.Add(t);
            }
        }
    }

    /// <summary>
    /// プレイヤーから最も近いターゲットを取得
    /// </summary>
    private void GetNearTarget()
    {
        for (int i = 0; i < m_myList.Count; i++)
        {
            float distance = Vector3.Distance(m_player.transform.position, m_myList[i].transform.position);

            if (i == 0)
            {
                m_minDistance = distance;
                m_index = i;
            }
            else
            {
                if (m_minDistance > distance)
                {
                    m_minDistance = distance;
                    m_index = i;
                }
            }

            m_nowTarget = m_myList[m_index];
        }

        m_target = m_nowTarget;
    }
}
