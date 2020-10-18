using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastController : MonoBehaviour
{
    [SerializeField] Camera cam = default;
    Vector3 pos = new Vector3(200, 400, 0);

    public LayerMask mask;

    [SerializeField] Image aim = default; // 照準のImageをインスペクターから設定

    public GameObject test;
    void Start()
    {
        
    }

    void Update()
    {
        Ray ray = cam.ScreenPointToRay(pos);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            //hit.collider.GetComponent<MeshRenderer>().material.color = Color.red;
            
            string hitTag = hit.collider.tag; // Rayがhitしたオブジェクトのタグ名を取得
           
            if ((hitTag.Equals("Target")))
            {
                aim.color = new Color(1.0f, 0.0f, 0.0f, 1.0f); //照準を赤に変える
                //Debug.Log(hit.collider.gameObject.transform.position);

                test = hit.collider.gameObject;
            }
            else
            {
                aim.color = new Color(0.0f, 1.0f, 1.0f, 1.0f); //照準を青に変える

                test = null;
            }
        }
        else
        {
            aim.color = new Color(0.0f, 1.0f, 1.0f, 1.0f); // Rayがヒットしていない場合は水色に

            test = null;
        }
    }
}