using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAngle : MonoBehaviour
{
    [SerializeField] GameObject _start;

    [SerializeField] GameObject _target;

    private float m_angle;
    public float Angle { get { return m_angle; } }

    void Start()
    {
        m_angle = AngleGet(_start.transform.position, _target.transform.position);
        Debug.Log(m_angle);
    }

    void Update()
    {
        m_angle = AngleGet(_start.transform.position, _target.transform.position);
    }

    float AngleGet(Vector2 start, Vector2 target)
    {
        Vector2 dt = target - start;
        float rad = Mathf.Atan2(dt.y, dt.x);
        float degree = rad * Mathf.Rad2Deg;

        return degree;
    }
}
