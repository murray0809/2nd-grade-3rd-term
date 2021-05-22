using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ワイヤーのラインスクリプト
/// </summary>
public class WireLine : MonoBehaviour
{
    /// <summary>
    /// プレイヤーのオブジェクト
    /// </summary>
    [SerializeField] GameObject m_player;
    /// <summary>
    /// ワイヤーを繋げるオブジェクト
    /// </summary>
    [SerializeField] GameObject m_target;

    LineRenderer m_lineRenderer;

    /// <summary>
    /// 線の最初の太さ
    /// </summary>
    [SerializeField] float m_lineStartWidth = 0.05f;

    /// <summary>
    /// 線の終わりの太さ
    /// </summary>
    [SerializeField] float m_lineEndWidth = 0.05f;

    PlayerController playerController;

    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");

        playerController = m_player.GetComponent<PlayerController>();

        m_lineRenderer = GetComponent<LineRenderer>();
    }

    
    void Update()
    {
        //ワイヤーが繋がっている間はプレイヤーとターゲットの間に線を引く
        if (playerController.Connecting)
        {
            m_lineRenderer.SetPosition(0, playerController.WirePos.transform.position);
            m_lineRenderer.SetPosition(1, playerController.ConnectingObject.transform.position);

            m_lineRenderer.startWidth = m_lineStartWidth;
            m_lineRenderer.endWidth = m_lineEndWidth;
        }
        else
        {
            m_lineRenderer.startWidth = 0;
            m_lineRenderer.endWidth = 0;
        }
        
    }
}
