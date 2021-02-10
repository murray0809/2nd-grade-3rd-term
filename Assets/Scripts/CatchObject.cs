﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchObject : MonoBehaviour
{
    CharacterController characterController;

    void Start()
    {
        characterController = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            characterController.Catch = true;
            characterController.CatchObject = this.gameObject;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            characterController.Catch = false;
            characterController.CatchObject = null;
        }
    }
}
