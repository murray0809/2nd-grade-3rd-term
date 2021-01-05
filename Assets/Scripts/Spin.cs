using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    bool flag = true;

    GameObject player;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //transform.Rotate(new Vector3(0, 5, 0));

       
        rb.AddTorque(Vector3.forward * 200.0f, ForceMode.Acceleration);


        //if (flag)
        //{
        //    player.transform.parent = transform;
        //}
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            flag = true;
        }
    }
}
