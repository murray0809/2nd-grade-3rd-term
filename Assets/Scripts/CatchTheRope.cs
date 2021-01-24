using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchTheRope : MonoBehaviour
{
	//　キャラクターの到達点
	[SerializeField]
	private Transform arrivalPoint;

	bool m_flag = false;

	[SerializeField] GameObject m_player;

    private void Start()
    {
		m_player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && m_flag)
        {
			//　キャラクターの親をロープにする
			m_player.transform.SetParent(transform);
		}

        if (Input.GetMouseButtonUp(0))
        {
			m_player.transform.SetParent(null);
		}
    }

    void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player")
		{
			m_flag = true;
			

		}

	}

	
}
