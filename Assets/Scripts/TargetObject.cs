using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObject : MonoBehaviour
{
    private bool m_isTargetable = false;
    public bool IsTargetable { get { return m_isTargetable; } }

    void Start()
    {
        
    }

    void Update()
    {
        
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
}
