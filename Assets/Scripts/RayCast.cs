using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    float m_distance = 10f;
    CharacterController m_characterController;

    void Start()
    {
        m_characterController = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
    }

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(this.transform.position + new Vector3(0,-1f,0), this.transform.forward, out hit, 10f))
        {
            m_characterController.CanMove = false;
            Debug.Log(hit.collider.gameObject.name);
        }
        else
        {
            m_characterController.CanMove = true;
        }
        
        Debug.DrawRay(this.transform.position + new Vector3(0, -1f, 0), this.transform.forward, Color.red);
    }
}
