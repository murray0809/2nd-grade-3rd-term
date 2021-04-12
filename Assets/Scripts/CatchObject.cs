using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchObject : MonoBehaviour
{
    CharacterController m_characterController;

    void Start()
    {
        m_characterController = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_characterController.Catch = true;
            m_characterController.CatchObject = this.gameObject;
            this.GetComponent<Renderer>().material.color = Color.blue;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_characterController.Catch = false;
            m_characterController.CatchObject = null;
            this.GetComponent<Renderer>().material.color = Color.white;
        }
    }
}
