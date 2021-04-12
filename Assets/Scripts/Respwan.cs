using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        //Instantiate(m_respawnPlayer, m_respwan.position, Quaternion.identity);
        m_player.transform.position = m_respwan.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Respawn();
        }
    }
}
