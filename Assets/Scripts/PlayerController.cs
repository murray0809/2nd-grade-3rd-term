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

    bool test;

    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if (h > 0)
        {
            Transform myTransform = m_model.transform;

            // ワールド座標を基準に、回転を取得
            Vector3 worldAngle = myTransform.eulerAngles;
            worldAngle.y = 90f; // ワールド座標を基準に、y軸を軸にした回転を10度に変更
            myTransform.eulerAngles = worldAngle; // 回転角度を設定
        }
        else if (h < 0)
        {
            Transform myTransform = m_model.transform;

            // ワールド座標を基準に、回転を取得
            Vector3 worldAngle = myTransform.eulerAngles;
            worldAngle.y = -90f; // ワールド座標を基準に、y軸を軸にした回転を10度に変更
            myTransform.eulerAngles = worldAngle; // 回転角度を設定
        }

        //左右への移動
        if (m_anim)
        {
            Vector3 vel = m_rb.velocity;

            vel.x = h * m_moveSpeed;

            m_rb.velocity = vel;

            m_anim.SetFloat("Run", h);

            //if (vel.x == 0)
            //{
            //    test = false;
            //}
            //else
            //{
            //    test = true;
            //}
            
        }

        //if (test)
        //{
        //    Transform myTransform = this.transform;

        //    Vector3 pos = myTransform.transform.position;
        //    pos.y = 0.5f;

        //    myTransform.transform.position = pos;
        //}

        //ジャンプ処理
        if (Input.GetButtonDown("Jump"))
        {
            m_rb.AddForce(new Vector3(0, m_jumpPower, 0), ForceMode.Impulse);

            m_anim.SetBool("Jump",true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        test = true;

        m_anim.SetBool("Jump", false);
    }

    //private void OnCollisionExit(Collision collision)
    //{
    //    test = false;
    //}
}
