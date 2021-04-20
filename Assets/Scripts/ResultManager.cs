using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// リザルトのスクリプト
/// </summary>
public class ResultManager : MonoBehaviour
{
    [SerializeField] Text m_message;

    Singleton singleton;

    void Start()
    {
        singleton = Singleton.Instance;
        Debug.Log(singleton.m_stageClearCount);

        m_message = GameObject.Find("Message").GetComponent<Text>();
        m_message.text = "ステージ2は製作中です";
    }

    void Update()
    {
        if (Input.GetButtonDown("Enter"))
        {
            SceneManager.LoadScene("StageSelect");
        }
    }
}
