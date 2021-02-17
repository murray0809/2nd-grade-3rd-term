using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerate : MonoBehaviour
{
    [SerializeField] TargetController obj;
    [SerializeField] GameObject targetManager;

    void Start()
    {
        StageData stage = new StageData();
        stage.x = 1;
        stage.y = 1;
        string jsonstr = JsonUtility.ToJson(stage);

        Debug.Log(jsonstr);

        StageData stage1 = JsonUtility.FromJson<StageData>(jsonstr);

        Debug.Log(stage1.x);
        Debug.Log(stage1.y);

        for (int i = 0; i < 20; i += 5)
        {
            Instantiate(obj, new Vector3(stage1.x + i, stage1.y, 0), Quaternion.identity, targetManager.transform);
        }

        //Instantiate(obj, new Vector3(stage1.x, stage1.y, 0), Quaternion.identity, targetManager.transform);

    }

    void Update()
    {
        
    }
}
