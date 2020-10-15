using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastController : MonoBehaviour
{
    [SerializeField] Camera cam = default;
    Vector3 pos = new Vector3(200, 400, 0);

    public LayerMask mask;

    // 照準のImageをインスペクターから設定
    [SerializeField] Image aim = default;
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
            hit.collider.GetComponent<MeshRenderer>().material.color = Color.red;
            // Rayがhitしたオブジェクトのタグ名を取得
            string hitTag = hit.collider.tag;
            // タグの名前がEnemyだったら、照準の色が変わる
            if ((hitTag.Equals("Target")))
            {
                //照準を赤に変える
                aim.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            }
            else
            {
                // Enemy以外では水色に
                aim.color = new Color(0.0f, 1.0f, 1.0f, 1.0f);
            }
        }
        else
        {
            // Rayがヒットしていない場合は水色に
            aim.color = new Color(0.0f, 1.0f, 1.0f, 1.0f);
        }
    }
}