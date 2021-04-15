using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 大砲が壁に当たった時のスクリプト
/// </summary>
public class Switch : MonoBehaviour
{
    [SerializeField] GameObject m_door;
    [SerializeField] GameObject m_particle;

    [SerializeField]GameObject m_mainCamera;
    [SerializeField]GameObject m_otherCamera;

    void Start()
    {
        m_otherCamera.SetActive(false);
    }

    /// <summary>
    /// 壁の破壊処理
    /// </summary>
    /// <returns></returns>
    IEnumerator BreakObject()
    {
        yield return new WaitForSeconds(1f);
        Destroy(m_door.gameObject);
    }

    /// <summary>
    /// カメラの切り替え処理
    /// </summary>
    /// <returns></returns>
    IEnumerator CameraChange()
    {
        yield return new WaitForSeconds(1.5f);
        m_otherCamera.SetActive(false);
        m_mainCamera.SetActive(true);
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(m_particle, m_door.transform.position, Quaternion.identity);
        StartCoroutine("BreakObject");
        m_otherCamera.SetActive(true);
        m_mainCamera.SetActive(false);
        StartCoroutine("CameraChange");
    }
}
