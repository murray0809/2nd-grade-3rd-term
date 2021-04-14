using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class TargetController : MonoBehaviour
{
    TargetController myObject;

    private bool m_isTargetable = false;

    TargetManager m_targetManager;

    GameObject m_manager;

    GameObject m_player;

    float m_distance;

    private bool m_canHook = false;
    public bool CanHook { get { return m_canHook; } }
   
    /// <summary>
    /// オブジェクトが画面内にあるか
    /// </summary>
    public bool IsHookable
    {
        get { return m_isTargetable; }
    }

    void Start()
    {
        m_manager = GameObject.FindGameObjectWithTag("TargetManager");
        m_targetManager = m_manager.GetComponent<TargetManager>();
        m_player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        //現在のターゲットの取得
        myObject = m_targetManager.NowTarget;

        m_distance = Vector3.Distance(transform.position, m_player.transform.position);

        AddColor();
    }

    /// <summary>
    /// 画面内にある場合
    /// </summary>
    private void OnBecameVisible()
    {
        m_isTargetable = true;
    }

    /// <summary>
    /// 画面内にない場合
    /// </summary>
    private void OnBecameInvisible()
    {
        m_isTargetable = false;
    }

    /// <summary>
    /// ワイヤーを繋げられるなら赤にする
    /// </summary>
    private void AddColor()
    {
        if (myObject == this && m_distance <= 4f)
        {
            this.GetComponent<Renderer>().material.color = Color.red;
            m_canHook = true;
        }
        else
        {
            this.GetComponent<Renderer>().material.color = Color.white;
            m_canHook = false;
        }
    }
}
