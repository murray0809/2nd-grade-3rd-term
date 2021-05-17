using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// キャラクターを追いかけるカメラのスクリプト
/// </summary>
public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject m_player;

    PlayerController playerController;

    [SerializeField] float m_addY;

    [SerializeField] float m_addZ;

    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        playerController = m_player.GetComponent<PlayerController>();
    }

    void Update()
    {
        Transform myTransform = this.transform;

        Vector3 myPos = myTransform.transform.position;

        float xPos = m_player.transform.position.x;
        myPos.x = xPos;

        float yPos;
        if (playerController.Jumping)
        {
            yPos = m_player.transform.position.y * 100;
            yPos = Mathf.Floor(yPos);
            yPos = yPos / 100;
            myPos.y = yPos + m_addY;
        }
        else
        {
            yPos = m_player.transform.position.y * 10;
            yPos = Mathf.Floor(yPos);
            yPos = yPos / 10;
            myPos.y = yPos + m_addY;
        }
        
        float zPos = m_player.transform.position.z;
        myPos.z = zPos + m_addZ;

        myTransform.transform.position = myPos;
    }
}
