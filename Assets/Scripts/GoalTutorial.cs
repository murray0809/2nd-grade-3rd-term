using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// チュートリアル用のゴールスクリプト/後にゴールのスクリプトは一つにする予定
/// </summary>
public class GoalTutorial : MonoBehaviour
{
    StageManager m_stageManager;

    void Start()
    {
        m_stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
    }

    void Update()
    {

    }

    void Goal()
    {
        SceneManager.LoadScene("Result");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Goal();
        }
    }
}
