using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 動かせるオブジェクトのスクリプト
/// </summary>
public class MoveObject : MonoBehaviour
{
    public PlayerController playerController;

    /// <summary>
    /// プレイヤーが触れている間表示するアイコン
    /// </summary>
    [SerializeField] GameObject m_catchig;

    public void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        m_catchig = GameObject.FindGameObjectWithTag("Target");
        m_catchig.SetActive(false);
    }

    public void Update()
    {
        if (playerController.Catching && playerController.MovingObject == this.gameObject)
        {
            m_catchig.SetActive(true);
        }
    }

    /// <summary>
    /// プレイヤーが動いていない状態で触れたら動かせるようにする
    /// </summary>
    /// <param name="collision"></param>
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !playerController.MovingObject)
        {
            playerController.MovingObject = this.gameObject;
        }
    }

    /// <summary>
    /// プレイヤーが離れたら動かないようにする
    /// </summary>
    /// <param name="collision"></param>
    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !playerController.Catching)
        {
            playerController.MovingObject = null;
        }

        m_catchig.SetActive(false);
    }

    /// <summary>
    /// プレイヤーが触れている間、アイコンを表示する
    /// </summary>
    /// <param name="collision"></param>
    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_catchig.SetActive(true);
        }
    }
}
