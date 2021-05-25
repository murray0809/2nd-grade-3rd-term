using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public PlayerController playerController;

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

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !playerController.MovingObject)
        {
            playerController.MovingObject = this.gameObject;
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !playerController.Catching)
        {
            playerController.MovingObject = null;
        }

        m_catchig.SetActive(false);
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_catchig.SetActive(true);
        }
    }
}
