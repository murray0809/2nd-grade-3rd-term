using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]

public class TargetController : MonoBehaviour
{
    Renderer targetRenderer; // 判定したいオブジェクトのrendererへの参照

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
        targetRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (targetRenderer.isVisible)
        {
            // 表示されている場合の処理
           
        }
        else
        {
            // 表示されていない場合の処理
          
        }
    }

    private void OnBecameVisible()
    {
        m_isTargetable = true;
    }

    private void OnBecameInvisible()
    {
        m_isTargetable = false;
        Debug.Log("out");
    }
}
