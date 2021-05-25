using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObject : MonoBehaviour
{
    private bool m_isTargetable = false;
    public bool IsTargetable { get { return m_isTargetable; } }

    PlayerController playerController;

    [SerializeField] GameObject m_catchig;

    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        m_catchig = transform.Find("Icon").gameObject;
        m_catchig.SetActive(false);
    }

    void Update()
    {
        if (playerController.TargetObject == this)
        {
            m_catchig.SetActive(true);
        }
        else
        {
            m_catchig.SetActive(false);
        }
    }

    /// <summary>
    /// 画面内にある場合
    /// </summary>
    private void OnBecameVisible()
    {
        m_isTargetable = true;
    }

    /// <summary>
    /// 画面内にない場合
    /// </summary>
    private void OnBecameInvisible()
    {
        m_isTargetable = false;
    }
}
