using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpStand : MonoBehaviour
{
    [SerializeField] float m_jumpForce = 0;
    [SerializeField] Vector3 m_directionForce = new Vector3(0,0,0);

    GameObject m_player;
    Rigidbody m_rb;
    CharacterController m_characterController;

    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_rb = m_player.GetComponent<Rigidbody>();
        m_characterController = m_player.GetComponent<CharacterController>();
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
            m_rb.AddForce(new Vector3(0, m_jumpForce, 0), ForceMode.Impulse);
        }
    }
}
