using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    /// <summary>ワイヤーの射程距離</summary>
    [SerializeField] float m_targetRange = 6f;

    [SerializeField] List<TargetController> myList = new List<TargetController>();

    TargetController target;

    [SerializeField] GameObject player;
    void Start()
    {
      
    }

    void Update()
    {
        myList.Clear();

        TargetController[] targets = transform.GetComponentsInChildren<TargetController>();
        foreach (TargetController t in targets)
        {
            if (t.IsHookable /*&& Vector3.Distance(player.transform.position, t.transform.position) < m_targetRange*/)
            {
                myList.Add(t);
            }
        }
    }
}
