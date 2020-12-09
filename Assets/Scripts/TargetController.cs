using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]

public class TargetController : MonoBehaviour
{
    TargetController myObject;

    bool m_isTargetable = false;

    bool targeting = false;

    TargetManager targetManager;

    GameObject manager;
   
    /// <summary>
    /// オブジェクトが画面内にあるかどうかを返す
    /// </summary>
    public bool IsHookable
    {
        get { return m_isTargetable; }
    }
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("TargetManager");
        targetManager = manager.GetComponent<TargetManager>();
    }

    void Update()
    {
        myObject = targetManager.NowTarget;

        if (myObject == this)
        {
            this.GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            this.GetComponent<Renderer>().material.color = Color.white;
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
