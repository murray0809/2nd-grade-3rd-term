using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーを動かすスクリプト
/// </summary>
public class PlayerController : MonoBehaviour
{
    Rigidbody m_rb;

    /// <summary>
    /// ジャンプ力
    /// </summary>
    [SerializeField] float m_jumpPower = 5f;

    /// <summary>
    /// 動くスピード
    /// </summary>
    [SerializeField] float m_moveSpeed = 5f;

    /// <summary>
    /// プレイヤーが今いるレーン
    /// </summary>
    [SerializeField] PlayerLane m_mode = PlayerLane.Lane2;

    /// <summary>
    /// プレイヤーのアニメーション
    /// </summary>
    [SerializeField] Animator m_anim;
    public Animator Anim { get { return m_anim; } set { m_anim = value; } }

    /// <summary>
    /// プレイヤーのモデル
    /// </summary>
    [SerializeField] GameObject m_model;

    /// <summary>
    /// 動かせるオブジェクト
    /// </summary>
    [SerializeField] GameObject m_movingObject;
    public GameObject MovingObject { get { return m_movingObject; } set { m_movingObject = value; } }

    /// <summary>
    /// シーン内にあるワイヤーを繋げられるオブジェクトを全て格納する配列
    /// </summary>
    TargetObject[] targets;

    /// <summary>
    /// 画面内にあるワイヤーを繋げられるオブジェクトを格納するリスト
    /// </summary>
    [SerializeField] List<TargetObject> m_trgetList = new List<TargetObject>();
    public List<TargetObject> TargetList { get { return m_trgetList; } }

    /// <summary>
    /// プレイヤーから最も近いワイヤーを繋げられるオブジェクト
    /// </summary>
    [SerializeField] TargetObject m_targetObject;
    public TargetObject TargetObject { get { return m_targetObject; } }

    /// <summary>
    /// 今ワイヤーと繋がっているオブジェクト
    /// </summary>
    [SerializeField] TargetObject m_connectingObject;
    public TargetObject ConnectingObject { get { return m_connectingObject; } }

    /// <summary>
    /// ワイヤーターゲットがまとまっているオブジェクト
    /// </summary>
    [SerializeField] GameObject m_wireSet;

    /// <summary>
    /// ワイヤーを飛ばす場所
    /// </summary>
    [SerializeField] GameObject m_wirePos;
    public GameObject WirePos { get { return m_wirePos; } }

    /// <summary>
    /// ワイヤーターゲットとプレイヤーの距離
    /// </summary>
    float m_targetDistance;

    /// <summary>
    /// 右を向いているかどうか
    /// </summary>
    private bool m_rightDirection = true;
    public bool RightDirection { get { return m_rightDirection; } }

    ConfigurableJoint m_joint;

    /// <summary>
    /// ワイヤーに繋がっているかどうか
    /// </summary>
    private bool m_connecting = false;
    public bool Connecting { get { return m_connecting; } }

    /// <summary>
    /// ジャンプしているかどうか
    /// </summary>
    private bool m_jumping = false;
    public bool Jumping { get { return m_jumping; } set { m_jumping = value; } }

    /// <summary>
    /// ものを掴んでいるかどうか
    /// </summary>
    private bool m_catching = false;
    public bool Catching { get { return m_catching; } }

    /// <summary>
    /// 手前に動くかどうか
    /// </summary>
    private bool m_front = false;

    /// <summary>
    /// 今動いているかどうか
    /// </summary>
    private bool m_moving = false;
    public bool Moving { get { return m_moving; } }

    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        m_rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;

        m_anim = GetComponentInChildren<Animator>();
        m_targetObject = GetComponent<TargetObject>();
        m_wireSet = GameObject.FindGameObjectWithTag("Wire");
        m_wirePos = GameObject.FindGameObjectWithTag("WirePos");
        m_joint = GetComponent<ConfigurableJoint>();

        m_mode = PlayerLane.Lane2;

        //シーン内にある全てのワイヤーのターゲットになるオブジェクトを取得する
        if (m_wireSet)
        {
            targets = m_wireSet.GetComponentsInChildren<TargetObject>();
        }
    }

    void Update()
    {
        //シーン内にある全てのワイヤーのターゲットになるオブジェクトがあればターゲットを取得する
        if (m_wireSet)
        {
            GetTarget();
        }
       
        //画面内にワイヤーのターゲットになるオブジェクトがあれば
        //プレイヤーに一番近いオブジェクトを取得する
        if (m_trgetList.Count > 0)
        {
            GetNearTarget();
        }
        else
        {
            m_targetObject = null;
            m_targetDistance = 0;
        }

        float h = Input.GetAxisRaw("Horizontal");

        if (h > 0 || h < 0)
        {
            m_moving = true;
        }
        else
        {
            m_moving = false;
        }

        //左右の向きの変更
        if (Input.GetButtonDown("Right") )
        {
            m_rightDirection = true;

            CHangeDirection(m_rightDirection);

            WirePosChange(m_rightDirection);
        }
        else if (Input.GetButtonDown("Left") )
        {
            m_rightDirection = false;

            CHangeDirection(m_rightDirection);

            WirePosChange(m_rightDirection);
        }

        //左右への移動
        if (m_anim && !m_connecting && !m_jumping)
        {
            Vector3 vel = m_rb.velocity;

            vel.x = h * m_moveSpeed;

            m_rb.velocity = vel;

            m_anim.SetFloat("Run", h);
        }

        //手前と奥の移動
        if (Input.GetButtonDown("Down") && (m_mode == PlayerLane.Lane1 || m_mode == PlayerLane.Lane2))
        {
            m_front = false;
            m_rb.constraints = RigidbodyConstraints.FreezeRotation;
            m_rb.AddForce(new Vector3(0, 0, -1 * m_moveSpeed), ForceMode.Impulse);

            Transform myTransform = m_model.transform;

            Vector3 worldAngle = myTransform.eulerAngles;

            worldAngle.y = 180f;

            myTransform.eulerAngles = worldAngle;

            m_anim.SetBool("MoveLane", true);

            StartCoroutine(MoveLaneAnim());
        }
        else if (Input.GetButtonDown("Up") && (m_mode == PlayerLane.Lane2 || m_mode == PlayerLane.Lane3))
        {
            m_front = true;
            m_rb.constraints = RigidbodyConstraints.FreezeRotation;
            m_rb.AddForce(new Vector3(0, 0, m_moveSpeed), ForceMode.Impulse);

            Transform myTransform = m_model.transform;

            Vector3 worldAngle = myTransform.eulerAngles;

            worldAngle.y = 0f;

            myTransform.eulerAngles = worldAngle;

            m_anim.SetBool("MoveLane", true);

            StartCoroutine(MoveLaneAnim());
        }

        MoveLane(m_front);

        //ジャンプ処理
        if (Input.GetButtonDown("Jump") && !m_jumping)
        {
            m_jumping = true;

            m_rb.AddForce(new Vector3(0, m_jumpPower, 0), ForceMode.Impulse);

            m_anim.SetBool("Jump",true);
        }

        //動かせるオブジェクトを動かす
        if (m_movingObject)
        {
            if ((Input.GetButton("RightCommand")) || (Input.GetButton("RightControl")))
            {
                m_catching = true;
                m_movingObject.transform.SetParent(transform);
            }
            else if((Input.GetButtonUp("RightCommand")) && m_movingObject)
            {
                m_movingObject.transform.SetParent(null);
                m_movingObject = null;
                m_catching = false;
            }
        }

        //ワイヤーを繋げる
        if (((Input.GetButton("RightCommand")) || (Input.GetButton("RightControl"))) && m_targetObject && m_jumping
            && !m_connecting && m_targetDistance <= 2.5f)
        {
            Rigidbody rb = m_targetObject.GetComponent<Rigidbody>();

            if (rb)
            {
                m_connecting = true;

                m_connectingObject = m_targetObject;

                m_joint.connectedBody = rb;
                m_joint.xMotion = ConfigurableJointMotion.Limited;
                m_joint.yMotion = ConfigurableJointMotion.Limited;
            }
        }

        //ワイヤーとターゲットを切り離す
        if (Input.GetButtonUp("RightCommand") || (Input.GetButton("RightControl")))
        {
            m_joint.connectedBody = null;
            m_joint.xMotion = ConfigurableJointMotion.Free;
            m_joint.yMotion = ConfigurableJointMotion.Free;

            m_connectingObject = null;

            m_connecting = false;
        }

        //ワイヤーアクション中の移動
        if (m_connecting || m_jumping)
        {
            if (Input.GetButtonDown("Right"))
            {
                m_rb.AddForce(new Vector3(0.5f * m_moveSpeed, 0, 0), ForceMode.Impulse);
            }
            else if (Input.GetButtonDown("Left"))
            {
                m_rb.AddForce(new Vector3(-0.5f * m_moveSpeed, 0, 0), ForceMode.Impulse);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        m_anim.SetBool("Jump", false);

        m_jumping = false;
    }

    //private void OnCollisionExit(Collision collision)
    //{
    //    m_anim.SetBool("Jump", true);
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
    /// ワイヤーのターゲットを取得
    /// </summary>
    private void GetTarget()
    {
        m_trgetList.Clear();

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
    /// プレイヤーから最も近いワイヤーのターゲットを取得
    /// </summary>
    private void GetNearTarget()
    {
        float minDistance = 0;

        for (int i = 0; i < m_trgetList.Count; i++)
        {
            float distance = Vector3.Distance(transform.position, m_trgetList[i].transform.position);

            if (i == 0)
            {
                minDistance = distance;
                m_targetObject = m_trgetList[i];
            }
            else
            {
                if (minDistance > distance)
                {
                    minDistance = distance;
                    m_targetObject = m_trgetList[i];
                }
            }

            m_targetDistance = Vector3.Distance(transform.position, m_targetObject.transform.position);
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

    /// <summary>
    /// レーンを変更する
    /// </summary>
    /// <param name="test"></param>
    void MoveLane(bool test)
    {
        if (test)
        {
            switch (m_mode)
            {
                case PlayerLane.Lane2:
                    if (transform.position.z > 0.5f)
                    {
                        Transform myTransform = this.transform;

                        Vector3 myPos = myTransform.transform.position;

                        myPos.z = 0.5f;

                        myTransform.transform.position = myPos;

                        m_rb.constraints =
                            RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;

                        m_mode = PlayerLane.Lane1;
                    }
                    break;
                case PlayerLane.Lane3:
                    if (transform.position.z > 0)
                    {
                        Transform myTransform = this.transform;

                        Vector3 myPos = myTransform.transform.position;

                        myPos.z = 0;

                        myTransform.transform.position = myPos;

                        m_rb.constraints =
                            RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;

                        m_mode = PlayerLane.Lane2;
                    }
                    break;
            }
        }
        else
        {
            switch (m_mode)
            {
                case PlayerLane.Lane1:
                    if (transform.position.z < 0)
                    {
                        Transform myTransform = this.transform;

                        Vector3 myPos = myTransform.transform.position;

                        myPos.z = 0;

                        myTransform.transform.position = myPos;

                        m_rb.constraints =
                            RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;

                        m_mode = PlayerLane.Lane2;
                    }

                    break;
                case PlayerLane.Lane2:
                    if (transform.position.z < -0.5f)
                    {
                        Transform myTransform = this.transform;

                        Vector3 myPos = myTransform.transform.position;

                        myPos.z = -0.5f;

                        myTransform.transform.position = myPos;

                        m_rb.constraints =
                            RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;

                        m_mode = PlayerLane.Lane3;
                    }
                    break;
            }
        }
    }

    /// <summary>
    /// 手前か奥に移動した時にアニメーションを呼ぶ
    /// </summary>
    /// <returns></returns>
    IEnumerator MoveLaneAnim()
    {
        yield return new WaitForSeconds(0.1f);

        m_anim.SetBool("MoveLane", false);

        yield return null;
    }
}

public enum PlayerLane
{
    Lane1,
    Lane2,
    Lane3,
}
