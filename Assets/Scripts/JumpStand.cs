using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpStand : MonoBehaviour
{
    [SerializeField] float m_jumpForce;
    [SerializeField] Vector3 m_directionForce;

    GameObject m_player;
    Rigidbody rb;
    CharacterController characterController;

    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        rb = m_player.GetComponent<Rigidbody>();
        characterController = m_player.GetComponent<CharacterController>();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (characterController.Gimmick == false)
            {
                characterController.Gimmick = true;
            }
            
            characterController.JumpStand = true;
            collision.rigidbody.AddForce(m_directionForce * m_jumpForce,ForceMode.Impulse);
        }
    }
}
