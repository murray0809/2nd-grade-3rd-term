using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpToRopeChara : MonoBehaviour
{
	private Animator animator;
	private CharacterController characterController;
	//　速度
	private Vector3 velocity;

	public enum State
	{
		normal,
		catchRope,
	}

	private State state;

	void Start()
	{
		animator = GetComponent<Animator>();
		characterController = GetComponent<CharacterController>();
		velocity = Vector3.zero;
		state = State.normal;
	}

	void Update()
	{
		////　通常時だけ移動やジャンプが出来る
		//if (state == State.normal)
		//{
		//	//　キャラクターコライダが接地、またはレイが地面に到達している場合
		//	if (cCon.isGrounded)
		//	{
		//		//キャラクターの移動やジャンプ等の処理
		//	}
		//}
	}
	public void SetState(State sta)
	{
        state = sta;
        if (state == State.catchRope)
        {
            //　現在の角度を保持しておく
            //preRotation = transform.rotation;

            animator.SetFloat("Speed", 0f);

            velocity = Vector3.zero;

            //　移動値等の初期化
            var rot = transform.localEulerAngles.y;

            //　角度を設定し直す
            transform.localRotation = Quaternion.Euler(0f, rot, 0f);
            //　キャラクターを到達点に動かすフラグオン
            //moveFlag = true;
        }
    }
}
