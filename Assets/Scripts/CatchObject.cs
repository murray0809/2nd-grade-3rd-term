using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 動かせるオブジェクトのスクリプト
/// </summary>
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

    /// <summary>
    /// プレイヤーが触れた時に動かせる状態にする
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_characterController.Catch = true;
            m_characterController.CatchObject = this.gameObject;
            this.GetComponent<Renderer>().material.color = Color.blue;
        }
    }

    /// <summary>
    /// プレイヤーが離れた時に動かせないようにする
    /// </summary>
    /// <param name="collision"></param>
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
