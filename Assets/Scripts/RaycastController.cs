using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastController : MonoBehaviour
{
    [SerializeField] Camera cam = default;
    Vector3 pos = new Vector3(200, 200, 0);

    //public LayerMask mask;

    [SerializeField] Image aim = default; // 照準のImageをインスペクターから設定

    public GameObject test;

    private SpringJoint joint;

    [SerializeField] GameObject player;

    Rigidbody m_rb;

    RaycastHit hit;
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Ray ray = cam.ScreenPointToRay(pos);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);


        if (Physics.Raycast(ray, out hit,100f))
        {
            Debug.Log(hit.point);
            aim.color = new Color(1.0f, 0.0f, 0.0f, 1.0f); //照準を赤に変える
            if (Input.GetMouseButtonDown(0))
            {
                Joint();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                DestroyJoint();
            }
        }
        else
        {
            aim.color = new Color(0.0f, 1.0f, 1.0f, 1.0f); //照準を青に変える
        }




        //if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        //{
        //    string hitTag = hit.collider.tag; // Rayがhitしたオブジェクトのタグ名を取得

        //    if ((hitTag.Equals("Target")))
        //    {
        //        aim.color = new Color(1.0f, 0.0f, 0.0f, 1.0f); //照準を赤に変える
        //        Debug.Log(hit.collider.gameObject.transform.position);

        //        test = hit.collider.gameObject;
        //    }
        //    else
        //    {
        //        aim.color = new Color(0.0f, 1.0f, 1.0f, 1.0f); //照準を青に変える

        //        test = null;
        //    }
        //}
        //else
        //{
        //    aim.color = new Color(0.0f, 1.0f, 1.0f, 1.0f); // Rayがヒットしていない場合は水色に

        //    test = null;
        //}
    }

    void Joint()
    {
        joint = player.gameObject.AddComponent<SpringJoint>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = hit.point;

        float distanceFromPoint = Vector3.Distance(player.transform.position, hit.point);

        joint.maxDistance = 0;
        joint.minDistance = 0;

        joint.spring = 4.5f;
        joint.damper = 7f;
        joint.massScale = 4.5f;
    }

    void DestroyJoint()
    {
        Destroy(joint);
    }
}