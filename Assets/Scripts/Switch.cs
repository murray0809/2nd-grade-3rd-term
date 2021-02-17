using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] GameObject m_door;
    [SerializeField] GameObject m_particle;

    [SerializeField]GameObject mainCamera;
    [SerializeField]GameObject otherCamera;

    void Start()
    {
        otherCamera.SetActive(false);
    }

    IEnumerator BreakObject()
    {
        yield return new WaitForSeconds(1f);
        Destroy(m_door.gameObject);
    }

    IEnumerator CameraChange()
    {
        yield return new WaitForSeconds(1.5f);
        otherCamera.SetActive(false);
        mainCamera.SetActive(true);
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(m_particle, m_door.transform.position, Quaternion.identity);
        StartCoroutine("BreakObject");
        otherCamera.SetActive(true);
        mainCamera.SetActive(false);
        StartCoroutine("CameraChange");
    }
}
