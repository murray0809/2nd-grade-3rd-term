using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]

public class TargetController : MonoBehaviour
{
    TargetController myObject;

    bool m_isTargetable = false;

    bool m_targeting = false;

    TargetManager m_targetManager;

    GameObject m_manager;

    GameObject m_player;

    float m_distance;

    private bool m_canHook = false;
    public bool CanHook { get { return m_canHook; } }
   
    /// <summary>
    /// オブジェクトが画面内にあるかどうかを返す
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
        myObject = m_targetManager.NowTarget;

        m_distance = Vector3.Distance(transform.position, m_player.transform.position);

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

    private void OnBecameVisible()
    {
        m_isTargetable = true;
    }

    private void OnBecameInvisible()
    {
        m_isTargetable = false;
    }
}
