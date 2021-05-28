using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakWall : MonoBehaviour
{
    private GameObject m_mainCamera;

    [SerializeField] GameObject m_effect1;
    [SerializeField] GameObject m_effect2;

    void Start()
    {
        m_mainCamera = GameObject.Find("Main Camera");

        m_effect1 = (GameObject)Resources.Load("Explosion_A");
        m_effect2 = (GameObject)Resources.Load("BreakEffect");
    }
    
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            StartCoroutine("BreakStart");
        }
    }

    IEnumerator BreakStart()
    {
        Instantiate(m_effect1, this.transform.position, Quaternion.identity,this.transform);
        Instantiate(m_effect2, this.transform.position, Quaternion.identity,this.transform);

        yield return new WaitForSeconds(2f);

        m_mainCamera.SetActive(true);

        Destroy(this.gameObject);

        yield return null;
    }
}
