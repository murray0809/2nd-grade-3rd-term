using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// タイトル管理のスクリプト
/// </summary>
public class TitleManager : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("StageSelect");
        }
    }
}
