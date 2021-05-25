using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ゴール管理のスクリプト
/// </summary>
public class GoalManager : MonoBehaviour
{
    //StageManager m_stageManager;

    //Clear m_clearMode;

    //Singleton singleton;

    void Start()
    {
    //    singleton = Singleton.Instance;
    //    m_clearMode = singleton.m_clearMode;
    //    Debug.Log(singleton.m_stageClearCount);
    //    m_stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
    }

    void Update()
    {
        
    }

    /// <summary>
    /// ゴールした時の処理
    /// </summary>
    void Goal()
    {
    //    switch (m_clearMode)
    //    {
    //        case Clear.Tutorial:
    //            SceneManager.LoadScene("Result");
    //            break;

    //        case Clear.Key:
    //            if (m_stageManager.KeyCount >= 3)
    //            {
    //                SceneManager.LoadScene("Result");
    //            }
    //            break;

    //        case Clear.Time:
    //            break;
    //    }
    }

    /// <summary>
    /// ゴールしたかどうかを判定する
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Result");
        }
    }
}
