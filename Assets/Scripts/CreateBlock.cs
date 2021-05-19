using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBlock : MonoBehaviour
{
    [SerializeField] GameObject m_block;

    [SerializeField] Vector3[] m_startPos;

    [SerializeField] int[] m_count;

    private BoxCollider[] m_boxCollider;

    private float m_center;

    void Start()
    {
        m_boxCollider = new BoxCollider[m_count.Length]; 

        Transform parent = this.transform;

        for (int i = 0; i < m_startPos.Length; i++)
        {
            for (int j = 0; j < m_count[i]; j++)
            {
                Vector3 createPos = new Vector3(m_startPos[i].x + j, m_startPos[i].y, m_startPos[i].z);
                GameObject block = Instantiate(m_block, createPos, Quaternion.identity, parent);
                Destroy(block.GetComponent<BoxCollider>());
                m_center += m_startPos[i].x + j;
            }

            if (m_count[i] % 2 == 0)
            {
                m_center = m_count[i] / 2 - 0.5f;
            }
            else
            {
                m_center = m_count[i] / 2;
            }
            
            m_boxCollider[i] = gameObject.AddComponent<BoxCollider>();
            m_boxCollider[i].center = new Vector3(m_startPos[i].x + m_center, m_startPos[i].y, m_startPos[i].z);
            m_boxCollider[i].size = new Vector3(m_count[i], 1, 2);
        }
    }

    void Update()
    {
        
    }
}
