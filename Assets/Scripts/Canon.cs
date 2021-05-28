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

    [SerializeField] GameObject m_rightWeel;
    [SerializeField] GameObject m_leftWeel;

    [SerializeField] float m_weelSpeed;
    /// <summary>
    /// 連射できないようにする為に設定
    /// </summary>
    private GameObject m_nowBulllet = null;

    private bool m_canShot = false;

    private GameObject m_player;

    CharacterController m_characterController;

    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_characterController = m_player.GetComponent<CharacterController>();
        
    }

    void Update()
    {
        if (m_characterController.Moving)
        {
            Weel();
        }

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

    /// <summary>
    /// 車輪を回転させる
    /// </summary>
    private void Weel()
    {
        Transform rightWeelTransform = m_rightWeel.transform;

        Vector3 rightLocalAngle = rightWeelTransform.localEulerAngles;
        rightLocalAngle.y += m_weelSpeed;
        rightWeelTransform.localEulerAngles = rightLocalAngle;

        Transform leftWeelTransform = m_leftWeel.transform;

        Vector3 leftLocalAngle = leftWeelTransform.localEulerAngles;
        leftLocalAngle.y += m_weelSpeed;
        leftWeelTransform.localEulerAngles = leftLocalAngle;
    }
}
