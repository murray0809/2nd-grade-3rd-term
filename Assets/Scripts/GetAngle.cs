using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAngle : MonoBehaviour
{
    [SerializeField] GameObject m_start;

    [SerializeField] GameObject m_target;

    private float m_angle;
    public float Angle { get { return m_angle; } }

    void Start()
    {
        m_angle = AngleGet(m_start.transform.position, m_target.transform.position);
        Debug.Log(m_angle);
    }

    void Update()
    {
        m_angle = AngleGet(m_start.transform.position, m_target.transform.position);
    }

    float AngleGet(Vector2 start, Vector2 target)
    {
        Vector2 dt = target - start;
        float rad = Mathf.Atan2(dt.y, dt.x);
        float degree = rad * Mathf.Rad2Deg;

        return degree;
    }
}
