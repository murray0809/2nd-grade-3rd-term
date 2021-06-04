using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージの土台を作るスクリプト
/// </summary>
public class CreateBlock : MonoBehaviour
{
    /// <summary>
    /// 生成するオブジェクト
    /// </summary>
    [SerializeField] GameObject m_block;

    /// <summary>
    /// 生成を開始するポジション
    /// </summary>
    [SerializeField] Vector3[] m_startPos;

    /// <summary>
    /// 生成するオブジェクトの数
    /// </summary>
    [SerializeField] int[] m_count;

    /// <summary>
    /// 生成するボックスコライダーの配列
    /// </summary>
    private BoxCollider[] m_boxCollider;

    /// <summary>
    /// 生成するボックスコライダーの中心点
    /// </summary>
    private float m_center;

    /// <summary>
    /// センターポジションの調整値
    /// </summary>
    const float m_centerPosAdjustEven = 0.5f;

    /// <summary>
    /// 生成するボックスコライダーのYの大きさ
    /// </summary>
    const float m_boxColliderSizeY = 1f;

    /// <summary>
    /// 生成するボックスコライダーのZの大きさ
    /// </summary>
    const float m_boxColliderSizeZ = 2f;

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
                m_center = m_count[i] / 2 - m_centerPosAdjustEven;
            }
            else
            {
                m_center = m_count[i] / 2;
            }
            
            m_boxCollider[i] = gameObject.AddComponent<BoxCollider>();
            m_boxCollider[i].center = new Vector3(m_startPos[i].x + m_center, m_startPos[i].y, m_startPos[i].z);
            m_boxCollider[i].size = new Vector3(m_count[i], m_boxColliderSizeY, m_boxColliderSizeZ);
        }
    }
}
