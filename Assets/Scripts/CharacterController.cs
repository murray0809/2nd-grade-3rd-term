using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// キャラクター操作のスクリプト
/// </summary>
public class CharacterController : MonoBehaviour
{
    private bool m_canJump;
    public bool CanJump { get { return m_canJump; } }

    [SerializeField] float m_jumpPower = 5f;
    [SerializeField] float m_moveSpeed = 5f;

    Rigidbody m_rb;

    [SerializeField] Animator m_anim;

    private Vector3 m_nowPos;
    public Vector3 NowPos { get { return m_nowPos; } set { m_nowPos = value; } }

    /// <summary>
    /// オブジェクトを動かせる状態かどうか
    /// </summary>
    private bool m_catch = false;
    public bool Catch { get { return m_catch; } set { m_catch = value; } }

    /// <summary>
    /// 動かすオブジェクトを取得する
    /// </summary>
    [SerializeField] GameObject m_catchObject;
    public GameObject CatchObject { get { return m_catchObject; } set { m_catchObject = value; } }

    private bool m_canMove = true;
    public bool CanMove { get { return m_canMove; } set { m_canMove = value; } }

    private bool m_moving = false;
    public bool Moving { get { return m_moving; } }

    private bool m_canJumpMove = false;

    [SerializeField] Lane m_mode = Lane.Lane2;
    [SerializeField] float m_lanePos;

    bool m_canMoveLane = false;
    bool m_moveLaneFlag = true;

    [SerializeField] GameObject m_wireTarget;
    TargetManager targetManager;

    [SerializeField] GameObject m_model;

    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        m_wireTarget = GameObject.Find("Wire");
        targetManager = m_wireTarget.GetComponent<TargetManager>();
    }

    void Update()
    {
        if (m_canMoveLane)
        {
            MoveLane(m_moveLaneFlag);
        }

        float h = Input.GetAxisRaw("Horizontal");

        if (h > 0)
        {
            Transform myTransform = m_model.transform;

            // ワールド座標を基準に、回転を取得
            Vector3 worldAngle = myTransform.eulerAngles;
            worldAngle.y = 90f; // ワールド座標を基準に、y軸を軸にした回転を10度に変更
            myTransform.eulerAngles = worldAngle; // 回転角度を設定
        }
        else if(h < 0)
        {
            Transform myTransform = m_model.transform;

            // ワールド座標を基準に、回転を取得
            Vector3 worldAngle = myTransform.eulerAngles;
            worldAngle.y = -90f; // ワールド座標を基準に、y軸を軸にした回転を10度に変更
            myTransform.eulerAngles = worldAngle; // 回転角度を設定
        }

        m_nowPos = transform.position;

        //左右への移動
        if (m_anim && m_canJump)
        {
            Vector3 vel = m_rb.velocity;

            vel.x = h * m_moveSpeed;

            m_rb.velocity = vel;

            m_anim.SetFloat("Run", h);

            if (m_catch && vel.x > 0 || m_catch && vel.x < 0)
            {
                m_moving = true;
            }
            else
            {
                m_moving = false;
            }
        }

        //手前と奥への移動
        if (Input.GetButtonDown("Down") && m_canMove && (m_mode == Lane.Lane1 || m_mode == Lane.Lane2))
        {
            m_rb.constraints = RigidbodyConstraints.FreezeRotation;
            m_rb.AddForce(new Vector3(0, 0, -10f), ForceMode.Impulse);
            m_canMoveLane = true;
            m_moveLaneFlag = false;
        }
        else if (Input.GetButtonDown("Up") && m_canMove && (m_mode == Lane.Lane2 || m_mode == Lane.Lane3))
        {
            m_rb.constraints = RigidbodyConstraints.FreezeRotation;
            m_rb.AddForce(new Vector3(0, 0, 10f), ForceMode.Impulse);
            m_canMoveLane = true;
            m_moveLaneFlag = true;
        }

        //ジャンプ処理
        if (m_canJump && Input.GetButtonDown("Jump"))
        {
            m_rb.AddForce(new Vector3(0, m_jumpPower, 0), ForceMode.Impulse);
        }

        //ジャンプ中の移動
        if (!m_canJump && !targetManager.Connecting)
        {
            if (Input.GetButtonDown("Right") && m_canJumpMove)
            {
                m_rb.AddForce(new Vector3(3, 0, 0), ForceMode.Impulse);
                m_canJumpMove = false;
            }
            else if (Input.GetButtonDown("Left") && m_canJumpMove)
            {
                m_rb.AddForce(new Vector3(-3, 0, 0), ForceMode.Impulse);
                m_canJumpMove = false;
            }
        }
        else if (targetManager.Connecting)
        {
            if (Input.GetButtonDown("Right"))
            {
                m_rb.AddForce(new Vector3(1, 0, 0), ForceMode.Impulse);
            }
            else if (Input.GetButtonDown("Left"))
            {
                m_rb.AddForce(new Vector3(-1, 0, 0), ForceMode.Impulse);
            }
        }

        //オブジェクトを動かす処理
        if (m_catch)
        {
            if ((Input.GetButton("RightCommand") || Input.GetButton("RightCtrl")))
            {
                m_catchObject.transform.SetParent(transform);
            }
            else
            {
                m_catchObject.transform.SetParent(null);
            }
        }

    }

    /// <summary>
    /// レーンの移動処理
    /// </summary>
    /// <param name="flag"></param>
    void MoveLane(bool flag)
    {
        if (flag)
        {
            switch (m_mode)
            {
                case Lane.Lane2:
                    if (transform.position.z > m_lanePos)
                    {
                        m_nowPos.z = m_lanePos;
                        transform.position = m_nowPos;
                        m_rb.isKinematic = false;
                        m_canMoveLane = false;
                        m_rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
                        m_mode = Lane.Lane1;
                    }
                    break;
                case Lane.Lane3:
                    if (transform.position.z > 0)
                    {
                        m_nowPos.z = 0;
                        transform.position = m_nowPos;
                        m_rb.isKinematic = false;
                        m_canMoveLane = false;
                        m_rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
                        m_mode = Lane.Lane2;
                    }
                    break;
            }
        }
        else
        {
            switch (m_mode)
            {
                case Lane.Lane1:
                    if (transform.position.z < 0)
                    {
                        m_nowPos.z = 0;
                        transform.position = m_nowPos;
                        m_rb.isKinematic = false;
                        m_canMoveLane = false;
                        m_rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
                        m_mode = Lane.Lane2;
                    }
                    break;
                case Lane.Lane2:
                    if (transform.position.z < -1 * m_lanePos)
                    {
                        m_nowPos.z = -1 * m_lanePos;
                        transform.position = m_nowPos;
                        m_rb.isKinematic = false;
                        m_canMoveLane = false;
                        m_rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
                        m_mode = Lane.Lane3;
                    }
                    break;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        m_canJump = true;
        m_canJumpMove = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        m_canJump = false;
        m_canJumpMove = true;
    }
}

public enum Lane
{
    Lane1,
    Lane2,
    Lane3,
}