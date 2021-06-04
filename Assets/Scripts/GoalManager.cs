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

    Singleton singleton;

    void Start()
    {
        singleton = Singleton.Instance;
        m_stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
    }

    /// <summary>
    /// ゴールした時の処理
    /// </summary>
    void Goal()
    {
        switch (singleton.NowStageMode)
        {
            case Singleton.StageMode.Tutorial:
                SceneManager.LoadScene("Result");
                break;

            case Singleton.StageMode.Key:
                if (m_stageManager.KeyCount >= 3)
                {
                    SceneManager.LoadScene("Result");
                }
                break;

            case Singleton.StageMode.TimeAttack:
                break;
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
