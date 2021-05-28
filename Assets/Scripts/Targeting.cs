using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeting : MonoBehaviour
{
    [SerializeField] List<TargetObject> m_trgetList = new List<TargetObject>();

    [SerializeField] TargetObject m_targetObject;

    [SerializeField] GameObject m_player;

    void Start()
    {
        m_targetObject = GetComponent<TargetObject>();

        m_player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        GetTarget();

        if (m_trgetList.Count > 0)
        {
            GetNearTarget();
        }
        else
        {
            m_targetObject = null;
        }
    }

    /// <summary>
    /// ターゲットを取得
    /// </summary>
    private void GetTarget()
    {
        m_trgetList.Clear();

        //子オブジェクトを取得
        TargetObject[] targets = transform.GetComponentsInChildren<TargetObject>();

        //画面内にあるものだけをリストに追加
        foreach (TargetObject t in targets)
        {
            if (t.IsTargetable)
            {
                m_trgetList.Add(t);
            }
        }
    }

    /// <summary>
    /// プレイヤーから最も近いターゲットを取得
    /// </summary>
    private void GetNearTarget()
    {
        float minDistance = 0;

        int index = 0;

        for (int i = 0; i < m_trgetList.Count; i++)
        {
            float distance = Vector3.Distance(m_player.transform.position, m_trgetList[i].transform.position);

            if (i == 0)
            {
                minDistance = distance;
                index = i;
            }
            else
            {
                if (minDistance > distance)
                {
                    minDistance = distance;
                    index = i;
                }
            }

            m_targetObject = m_trgetList[index];
        }

        //m_target = m_nowTarget;
    }
}
