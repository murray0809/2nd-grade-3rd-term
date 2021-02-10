using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchTheRope : MonoBehaviour
{
	////　キャラクターの到達点
	//[SerializeField]
	//private Transform arrivalPoint;

	bool m_flag = false;

	[SerializeField] GameObject m_player;
    Rigidbody m_playerRb;

    private void Start()
    {
		m_player = GameObject.FindGameObjectWithTag("Player");
        m_playerRb = m_player.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (m_flag && Input.GetMouseButton(0))
        {
			m_player.transform.SetParent(transform);
            m_playerRb.isKinematic = true;
            m_playerRb.constraints = RigidbodyConstraints.None;
        }

        if (Input.GetMouseButtonUp(0))
        {
            m_playerRb.isKinematic = false;
            m_player.transform.SetParent(null);
            m_player.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0);
            m_playerRb.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_flag = true;
        }
    }
}
