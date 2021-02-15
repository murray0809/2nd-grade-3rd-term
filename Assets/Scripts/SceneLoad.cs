using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    [SerializeField] string m_sceneName;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(m_sceneName);
        }
    }
}
