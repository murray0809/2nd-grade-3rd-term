using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    GameObject m_stage;
    StageManager stageManager;

    void Start()
    {
        m_stage = GameObject.Find("StageManager");
        stageManager = m_stage.GetComponent<StageManager>();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            stageManager.KeyGet();
            Destroy(this.gameObject);
        }
    }
}
