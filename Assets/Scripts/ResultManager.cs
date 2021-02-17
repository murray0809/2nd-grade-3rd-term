using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    Singleton singleton;

    void Start()
    {
        singleton = Singleton.Instance;
        Debug.Log(singleton.stageClearCount);
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            singleton.stageClearCount++;
            SceneManager.LoadScene("StageSelect");
        }
    }
}
