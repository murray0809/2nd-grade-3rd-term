using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 大砲の弾のスクリプト
/// </summary>
public class Bullet : MonoBehaviour
{
    /// <summary>
    /// 弾を飛ばす方向
    /// </summary>
    [SerializeField] Vector3 m_direction;

    Rigidbody m_rb;

    private GameObject m_mainCamera;

    [SerializeField] GameObject m_wallCamera;

    void Start()
    {
        //弾が発射された時の処理
        m_mainCamera = GameObject.Find("Main Camera");
        m_mainCamera.SetActive(false);

        m_wallCamera = GameObject.FindGameObjectWithTag("WallCamera");

        m_direction = new Vector3(5f, 15f, 0);
        //弾を飛ばす処理
        m_rb = GetComponent<Rigidbody>();
        m_rb.AddForce(m_direction, ForceMode.Impulse);
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BreakWall"))
        {
            m_wallCamera.SetActive(true);
        }
        else
        {
            m_mainCamera.SetActive(true);
        }

        Destroy(this.gameObject);
    }
}
