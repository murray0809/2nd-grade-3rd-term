using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カギのスクリプト
/// </summary>
public class Key : MonoBehaviour
{
    private GameObject m_stage;
    StageManager m_stageManager;

    void Start()
    {
        m_stage = GameObject.FindGameObjectWithTag("StageManager");
        m_stageManager = m_stage.GetComponent<StageManager>();
    }

    void Update()
    {
        
    }

    /// <summary>
    /// カギをゲットした際の処理
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_stageManager.KeyGet();
            Destroy(this.gameObject);
        }
    }
}
