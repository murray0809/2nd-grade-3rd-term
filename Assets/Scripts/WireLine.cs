using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireLine : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject target;

    Vector3 playerPos;
    Vector3 targetPos;

    LineRenderer lineRenderer;

    TargetManager targetManager;
    void Start()
    {
        targetManager = GetComponent<TargetManager>();

        lineRenderer = GetComponent<LineRenderer>();

        
    }

    
    void Update()
    {
        if (targetManager.Connecting)
        {
            lineRenderer.SetPosition(0, targetManager.PlayerPos);
            lineRenderer.SetPosition(1, targetManager.TargetPos);

            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
        }
        else
        {
            lineRenderer.startWidth = 0;
            lineRenderer.endWidth = 0;
        }
    }
}
