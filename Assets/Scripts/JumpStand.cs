using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpStand : MonoBehaviour
{
    [SerializeField] private float jumpForce;

    Rigidbody rb;

    void Start()
    {
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.rigidbody.AddForce(new Vector3(0,1,0) * jumpForce,ForceMode.Impulse);
        }
    }
}
