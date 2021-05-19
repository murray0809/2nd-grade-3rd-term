﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ワイヤーのラインスクリプト
/// </summary>
public class WireLine : MonoBehaviour
{
    [SerializeField] GameObject m_player;
    [SerializeField] GameObject m_target;

    LineRenderer m_lineRenderer;

    TargetManager m_targetManager;

    PlayerController playerController;

    bool test = false;

    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");

        playerController = m_player.GetComponent<PlayerController>();

        m_targetManager = GetComponent<TargetManager>();

        m_lineRenderer = GetComponent<LineRenderer>();
    }

    
    void Update()
    {
        //if (m_targetManager.Connecting)
        //{
        //    m_lineRenderer.SetPosition(0, m_targetManager.PlayerPos);
        //    m_lineRenderer.SetPosition(1, m_targetManager.TargetPos);

        //    m_lineRenderer.startWidth = 0.1f;
        //    m_lineRenderer.endWidth = 0.1f;
        //}
        //else
        //{
        //    m_lineRenderer.startWidth = 0;
        //    m_lineRenderer.endWidth = 0;
        //}
        if (playerController.TargetList.Count == 0)
        {
            m_lineRenderer.startWidth = 0;
            m_lineRenderer.endWidth = 0;
        }
        else
        {
            m_lineRenderer.SetPosition(0, playerController.WirePos.transform.position);
            m_lineRenderer.SetPosition(1, playerController.TargetObject.transform.position);

            m_lineRenderer.startWidth = 0.1f;
            m_lineRenderer.endWidth = 0.1f;
        }
        
    }
}
