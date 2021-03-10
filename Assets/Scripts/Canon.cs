using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    [SerializeField] GameObject m_bullet = null;
    [SerializeField] Transform m_muzzle = null;

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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_canShot = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_canShot = false;
        }
    }
}
