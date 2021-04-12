using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    void Goal()
    {
        if (m_stageManager.KeyCount >= 3)
        {
            SceneManager.LoadScene("Result");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Goal();
        }
    }
}
