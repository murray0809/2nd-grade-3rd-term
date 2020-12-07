using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]

public class TargetController : MonoBehaviour
{
    [SerializeField] Text test;

    bool m_isTargetable = false;

    /// <summary>
    /// オブジェクトが画面内にあるかどうかを返す
    /// </summary>
    public bool IsHookable
    {
        get { return m_isTargetable; }
    }
    void Start()
    {
        
    }

    void Update()
    {
        
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
