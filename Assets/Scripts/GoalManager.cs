using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ゴール管理のスクリプト
/// </summary>
public class GoalManager : MonoBehaviour
{
    StageManager m_stageManager;

    void Start()
    {
        m_stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
    }

    void Update()
    {
        
    }

    /// <summary>
    /// ゴールした時の処理
    /// </summary>
    void Goal()
    {
        if (m_stageManager.KeyCount >= 3)
        {
            SceneManager.LoadScene("Result");
        }
    }

    /// <summary>
    /// ゴールしたかどうかを判定する
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Goal();
        }
    }
}
