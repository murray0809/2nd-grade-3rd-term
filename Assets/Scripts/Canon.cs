using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 大砲のスクリプト
/// </summary>
public class Canon : MonoBehaviour
{
    [SerializeField] GameObject m_bullet = null;
    [SerializeField] Transform m_muzzle = null;

    /// <summary>
    /// 連射できないようにする為に設定
    /// </summary>
    private GameObject m_nowBulllet = null;

    private bool m_canShot = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetButtonDown("Enter") && !m_nowBulllet && m_canShot)
        {
            m_nowBulllet = Instantiate(m_bullet, m_muzzle.position, Quaternion.identity);
        }
    }

    /// <summary>
    /// プレイヤーが大砲に触れている時に発射できるようにする
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_canShot = true;
        }
    }

    /// <summary>
    /// プレイヤーが大砲から離れた時に発射できないようにする
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_canShot = false;
        }
    }
}
