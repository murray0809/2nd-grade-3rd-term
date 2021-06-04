using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Singleton;

public class StageData
{
    private static StageData mInstance;

    private StageMode m_tutrial = StageMode.Tutorial;
    public StageMode Tutrial { get { return m_tutrial; } }

    private StageMode m_stage1 = StageMode.Key;
    public StageMode Stage1 { get { return m_stage1; } }

    private StageMode m_stage2 = StageMode.TimeAttack;
    public StageMode Stage2 { get { return m_stage2; } }

    private StageData()
    {
        Debug.Log("Create SampleSingleton instance.");
    }

    public static StageData Instance
    {
        get
        {
            if (mInstance == null) mInstance = new StageData();

            return mInstance;
        }
    }
}
