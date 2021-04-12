using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    GameObject m_stage;
    StageManager m_stageManager;

    void Start()
    {
        m_stage = GameObject.Find("StageManager");
        m_stageManager = m_stage.GetComponent<StageManager>();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_stageManager.KeyGet();
            Destroy(this.gameObject);
        }
    }
}
