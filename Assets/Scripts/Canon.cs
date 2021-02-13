using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    [SerializeField] GameObject m_bullet = null;
    [SerializeField] Transform m_muzzle = null;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(m_bullet, m_muzzle.position, Quaternion.identity);
        }
    }
}
