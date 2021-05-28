using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MoveObject
{
    [SerializeField] GameObject m_muzzle;
    [SerializeField] GameObject m_bullet;

    private bool m_canShot = false;

    [SerializeField] GameObject m_rightWeel;
    [SerializeField] GameObject m_leftWeel;

    [SerializeField] float m_weelSpeed;

    new void Start()
    {
        base.Start();
        m_muzzle = GameObject.FindGameObjectWithTag("Muzzle");
        m_rightWeel = GameObject.FindGameObjectWithTag("RightWeel");
        m_leftWeel = GameObject.FindGameObjectWithTag("LeftWeel");

        m_weelSpeed = 1;
    }

    new void Update()
    {
        base.Update();

        if (m_canShot && Input.GetButtonDown("Enter"))
        {
            m_bullet = (GameObject)Resources.Load("Bullet");
            Instantiate(m_bullet, m_muzzle.transform.position,Quaternion.identity); 
        }

        if (playerController.Moving && playerController.Catching)
        {
            Weel();
        }
    }

    new void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody>().constraints
            = RigidbodyConstraints.FreezePositionY
            | RigidbodyConstraints.FreezePositionZ
            | RigidbodyConstraints.FreezeRotation;

            m_canShot = true;
        }
    }

    new void OnCollisionExit(Collision collision)
    {
        base.OnCollisionExit(collision);

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody>().constraints
            = RigidbodyConstraints.FreezePositionZ
            | RigidbodyConstraints.FreezeRotation;

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

        if (playerController.RightDirection)
        {
            rightLocalAngle.y -= m_weelSpeed;
        }
        else
        {
            rightLocalAngle.y += m_weelSpeed;
        }
        
        rightWeelTransform.localEulerAngles = rightLocalAngle;

        Transform leftWeelTransform = m_leftWeel.transform;

        Vector3 leftLocalAngle = leftWeelTransform.localEulerAngles;

        if (playerController.RightDirection)
        {
            rightLocalAngle.y -= m_weelSpeed;
        }
        else
        {
            rightLocalAngle.y += m_weelSpeed;
        }
        leftWeelTransform.localEulerAngles = leftLocalAngle;
    }
}
