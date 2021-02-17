using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    float distance = 10f;
    CharacterController characterController;

    void Start()
    {
        characterController = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
    }

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(this.transform.position + new Vector3(0,-1f,0), this.transform.forward, out hit, 10f))
        {
            characterController.CanMove = false;
            Debug.Log(hit.collider.gameObject.name);
        }
        else
        {
            characterController.CanMove = true;
        }
        
        Debug.DrawRay(this.transform.position + new Vector3(0, -1f, 0), this.transform.forward, Color.red);
    }
}
