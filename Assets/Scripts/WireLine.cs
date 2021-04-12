using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireLine : MonoBehaviour
{
    [SerializeField] GameObject m_player;
    [SerializeField] GameObject m_target;

    Vector3 m_playerPos;
    Vector3 m_targetPos;

    LineRenderer m_lineRenderer;

    TargetManager m_targetManager;

    void Start()
    {
        m_targetManager = GetComponent<TargetManager>();

        m_lineRenderer = GetComponent<LineRenderer>();
    }

    
    void Update()
    {
        if (m_targetManager.Connecting)
        {
            m_lineRenderer.SetPosition(0, m_targetManager.PlayerPos);
            m_lineRenderer.SetPosition(1, m_targetManager.TargetPos);

            m_lineRenderer.startWidth = 0.1f;
            m_lineRenderer.endWidth = 0.1f;
        }
        else
        {
            m_lineRenderer.startWidth = 0;
            m_lineRenderer.endWidth = 0;
        }
    }
}
