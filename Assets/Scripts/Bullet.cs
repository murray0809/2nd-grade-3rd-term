using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Vector3 m_direction;

    Rigidbody m_rb;

    private GameObject m_mainCamera;

    void Start()
    {
        m_mainCamera = GameObject.Find("Main Camera");
        m_mainCamera.SetActive(false);

        m_rb = GetComponent<Rigidbody>();
        m_rb.AddForce(m_direction, ForceMode.Impulse);
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        m_mainCamera.SetActive(true);
        Destroy(this.gameObject);
    }
}
