using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// リスポーンのスクリプト
/// </summary>
public class Respwan : MonoBehaviour
{
    [SerializeField] Transform m_respwan;
    private GameObject m_player;
    [SerializeField] GameObject m_respawnPlayer;

    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        
    }

    void Respawn()
    {
        m_player.transform.position = m_respwan.position;
    }

    /// <summary>
    /// キルゾーンに当たった時の処理
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Respawn();
        }
    }
}
