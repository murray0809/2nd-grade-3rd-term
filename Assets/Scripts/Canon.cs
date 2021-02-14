using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    [SerializeField] GameObject m_bullet = null;
    [SerializeField] Transform m_muzzle = null;

    private GameObject m_nowBulllet = null;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && !m_nowBulllet)
        {
            m_nowBulllet = Instantiate(m_bullet, m_muzzle.position, Quaternion.identity);
        }
    }
}
