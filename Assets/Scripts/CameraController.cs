using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;              //メインカメラ格納用
    [SerializeField] GameObject playerObject;            //回転の中心となるプレイヤー格納用
    [SerializeField] float rotateSpeed = 2.0f;            //回転の速さ
    void Start()
    {
        
    }

    void Update()
    {
        rotateCamera();
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
