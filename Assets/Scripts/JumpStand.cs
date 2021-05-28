using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ジャンプ台のスクリプト
/// </summary>
public class JumpStand : MonoBehaviour
{
    [SerializeField] float m_jumpForce = 0;

    PlayerController playerController;

    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();  
    }

    void Update()
    {
        
    }

    /// <summary>
    /// ジャンプ台に乗った時の処理
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(0, m_jumpForce, 0, ForceMode.Impulse);
            playerController.Jumping = true;
            playerController.Anim.SetBool("Jump", true);
        }
    }
}
