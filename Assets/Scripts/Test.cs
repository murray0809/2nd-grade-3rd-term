using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] GameObject target;

    [SerializeField] GameObject player;

    Vector3 targetPos;

    RaycastController raycastController;

    Rigidbody rb;
    void Start()
    {
        raycastController = GetComponent<RaycastController>();

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        target = raycastController.test;
        if (target == null)
        {
            
        }
        else
        {
            targetPos = target.transform.position;
        }
        

        if (Input.GetKey(KeyCode.Z))
        {
            rb.AddForce(targetPos,ForceMode.Impulse);
            Debug.Log("aaa");
        }
        else
        {
                
        }
    }
}
