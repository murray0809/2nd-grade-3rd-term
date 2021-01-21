using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    bool test;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(test);
    }

    private void OnCollisionStay(Collision collision)
    {
        test = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        test = false;
    }
}
