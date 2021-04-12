using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRope : MonoBehaviour
{
	//　進んでいる方向
	private int m_direction = 1;
	//　Z軸の角度
	private float m_angle = 0f;
	//　動き始める時の時間
	private float m_startTime;
	//　補間間隔
	[SerializeField]
	private float m_duration = 5f;
	//　Z軸で振り子をする角度
	[SerializeField]
	private float m_limitAngle = 90f;

	void Start()
	{
		m_startTime = Time.time;
	}

	// Update is called once per frame
	void Update()
	{

		//　経過時間に合わせた割合を計算
		float t = (Time.time - m_startTime) / m_duration;
		//　スムーズに角度を計算
		m_angle = Mathf.SmoothStep(m_angle, m_direction * m_limitAngle, t);
		//　角度を変更
		transform.localEulerAngles = new Vector3(0f, 0f, m_angle);
		//　角度が指定した角度と1度の差になったら反転
		if (Mathf.Abs(Mathf.DeltaAngle(m_angle, m_direction * m_limitAngle)) < 1f)
		{
			m_direction *= -1;
			m_startTime = Time.time;
		}
	}
	//　進んでいる向きを返す（実際にはint値）
	public int GetDirection()
	{
		return m_direction;
	}
}
