using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpStand : MonoBehaviour
{
    [SerializeField] float m_jumpForce = 0;
    [SerializeField] Vector3 m_directionForce = new Vector3(0,0,0);

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
            //if (characterController.Gimmick == false)
            //{
            //    characterController.Gimmick = true;
            //}

            //characterController.JumpStand = true;
            rb.AddForce(new Vector3(0, m_jumpForce, 0), ForceMode.Impulse);
        }
    }
}
