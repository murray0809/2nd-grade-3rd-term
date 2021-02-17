using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    [SerializeField] GameObject particleObject;

    void Start()
    {
        Instantiate(particleObject, this.transform.position, Quaternion.identity); //パーティクル用ゲームオブジェクト生成
        Destroy(this.gameObject); //衝突したゲームオブジェクトを削除
    }

    void Update()
    {
        
    }


}
