using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    private GameObject m_player;
    private CinemachineVirtualCamera m_cinemachine;

    void Start()
    {
        m_cinemachine = GetComponent<CinemachineVirtualCamera>();
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_cinemachine.LookAt = m_player.transform;
        m_cinemachine.Follow = m_player.transform;
    }

    void Update()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_cinemachine.LookAt = m_player.transform;
        m_cinemachine.Follow = m_player.transform;
    }
}
