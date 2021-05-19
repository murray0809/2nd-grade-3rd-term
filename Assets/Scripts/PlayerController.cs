using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody m_rb;

    [SerializeField] float m_jumpPower = 5f;

    [SerializeField] float m_moveSpeed = 5f;

    [SerializeField] Animator m_anim;

    [SerializeField] GameObject m_model;

    [SerializeField] GameObject m_movingObject;
    public GameObject MovingObject { get { return m_movingObject; } set { m_movingObject = value; } }

    [SerializeField] List<TargetObject> m_trgetList = new List<TargetObject>();
    public List<TargetObject> TargetList { get { return m_trgetList; } }

    [SerializeField] TargetObject m_targetObject;
    public TargetObject TargetObject { get { return m_targetObject; } }

    /// <summary>
    /// ワイヤーターゲットがまとまっているオブジェクト
    /// </summary>
    [SerializeField] GameObject m_wireSet;

    [SerializeField] GameObject m_wirePos;
    public GameObject WirePos { get { return m_wirePos; } }

    /// <summary>
    /// 右を向いているかどうか
    /// </summary>
    private bool m_rightDirection = true;

    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        m_anim = GetComponentInChildren<Animator>();
        m_targetObject = GetComponent<TargetObject>();
        m_wireSet = GameObject.FindGameObjectWithTag("Wire");
        m_wirePos = GameObject.FindGameObjectWithTag("WirePos");
    }

    void Update()
    {
        GetTarget();

        if (m_trgetList.Count > 0)
        {
            GetNearTarget();
        }
        else
        {
            m_targetObject = null;
        }

        float h = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Right") && !m_rightDirection)
        {
            m_rightDirection = true;

            CHangeDirection(m_rightDirection);

            WirePosChange(m_rightDirection);
        }
        else if (Input.GetButtonDown("Left") && m_rightDirection)
        {
            m_rightDirection = false;

            CHangeDirection(m_rightDirection);

            WirePosChange(m_rightDirection);
        }

        //左右への移動
        if (m_anim)
        {
            Vector3 vel = m_rb.velocity;

            vel.x = h * m_moveSpeed;

            m_rb.velocity = vel;

            m_anim.SetFloat("Run", h);
        }

        //ジャンプ処理
        if (Input.GetButtonDown("Jump"))
        {
            m_rb.AddForce(new Vector3(0, m_jumpPower, 0), ForceMode.Impulse);

            m_anim.SetBool("Jump",true);
        }

        if (m_movingObject)
        {
            if ((Input.GetButton("RightCommand")))
            {
                m_movingObject.transform.SetParent(transform);
            }
            else
            {
                m_movingObject.transform.SetParent(null);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        m_anim.SetBool("Jump", false);
    }

    //private void OnCollisionExit(Collision collision)
    //{
    //    m_jumping = true;

    //    m_anim.SetBool("Jump", true);

    //    m_rb.constraints = RigidbodyConstraints.FreezeRotation;
    //}

    /// <summary>
    /// キャラクターの向きを変える
    /// </summary>
    /// <param name="rightDirection"></param>
    void CHangeDirection(bool rightDirection)
    {
        Transform myTransform = m_model.transform;

        Vector3 worldAngle = myTransform.eulerAngles;

        if (rightDirection)
        {
            worldAngle.y = 90f;
        }
        else
        {
            worldAngle.y = -90f;
        }

        myTransform.eulerAngles = worldAngle;
    }

    /// <summary>
    /// ターゲットを取得
    /// </summary>
    private void GetTarget()
    {
        m_trgetList.Clear();

        //子オブジェクトを取得
        TargetObject[] targets = m_wireSet.GetComponentsInChildren<TargetObject>();

        //画面内にあるものだけをリストに追加
        foreach (TargetObject t in targets)
        {
            if (t.IsTargetable)
            {
                m_trgetList.Add(t);
            }
        }
    }

    /// <summary>
    /// プレイヤーから最も近いターゲットを取得
    /// </summary>
    private void GetNearTarget()
    {
        float minDistance = 0;

        int index = 0;

        for (int i = 0; i < m_trgetList.Count; i++)
        {
            float distance = Vector3.Distance(transform.position, m_trgetList[i].transform.position);

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

            m_targetObject = m_trgetList[index];
        }
    }

    /// <summary>
    /// ワイヤーポジションの向きを変える
    /// </summary>
    /// <param name="rightDirection"></param>
    void WirePosChange(bool rightDirection)
    {
        Transform wirePos = m_wirePos.transform;

        Vector3 pos = wirePos.localPosition;

        if (rightDirection)
        {
            pos.x = 0.3f;

            wirePos.localPosition = pos;
        }
        else
        {
            pos.x = -0.3f;

            wirePos.localPosition = pos;
        }
    }
}
