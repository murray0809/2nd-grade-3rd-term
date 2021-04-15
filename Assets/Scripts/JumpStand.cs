using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ジャンプ台のスクリプト
/// </summary>
public class JumpStand : MonoBehaviour
{
    [SerializeField] float m_jumpForce = 0;

    private GameObject m_player;
    Rigidbody m_rb;
    CharacterController m_characterController;

    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_rb = m_player.GetComponent<Rigidbody>();
        m_characterController = m_player.GetComponent<CharacterController>();
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
            m_rb.AddForce(new Vector3(0, m_jumpForce, 0), ForceMode.Impulse);
        }
    }
}
