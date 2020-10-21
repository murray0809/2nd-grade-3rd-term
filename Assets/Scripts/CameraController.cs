using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;              //メインカメラ格納用
    [SerializeField] GameObject playerObject;            //回転の中心となるプレイヤー格納用
    [SerializeField] float rotateSpeed = 2.0f;            //回転の速さ

    public Transform cam;
    private RaycastHit hit;
    private Rigidbody rb;
    private bool attached = false;
    public float momentum;
    public float speed;
    public float step;

    public LayerMask layerMask;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rotateCamera();


        if (Input.GetButton("Fire1"))
        {
            if (Physics.Raycast(cam.position, cam.forward, out hit))
            {
                Debug.DrawRay(cam.position, cam.forward * 100, Color.yellow);
                attached = true;
                rb.isKinematic = true;
            }
        }

        if (Input.GetButtonUp("Fire1"))
        {
            attached = false;
            rb.isKinematic = false;
            rb.velocity = transform.forward * momentum;
        }

        if (attached)
        {
            momentum += Time.deltaTime * speed;
            step = momentum * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, hit.point, step);
        }

        if (!attached && momentum >= 0)
        {
            momentum -= Time.deltaTime * 5;
            step = 0;
        }
    }

    private void rotateCamera()
    {
        //Vector3でX,Y方向の回転の度合いを定義
        Vector3 angle = new Vector3(Input.GetAxis("Mouse X") * rotateSpeed, Input.GetAxis("Mouse Y") * rotateSpeed, 0);

        //transform.RotateAround()をしようしてメインカメラを回転させる
        mainCamera.transform.RotateAround(playerObject.transform.position, Vector3.up, angle.x);
        mainCamera.transform.RotateAround(playerObject.transform.position, transform.right, angle.y);
    }
}
