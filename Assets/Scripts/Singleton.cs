using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton
{
    private static Singleton mInstance;

    public int m_stageClearCount = 1;

    private StageMode m_stageMode = StageMode.Tutorial;
    public StageMode NowStageMode { get { return m_stageMode; } set { m_stageMode = value; } }

    public enum StageMode
    {
        Tutorial,
        Key,
        TimeAttack,
    }

    private Singleton()
    { 
        Debug.Log("Create SampleSingleton instance.");
    }

    public static Singleton Instance
    {

        get
        {

            if (mInstance == null) mInstance = new Singleton();

            return mInstance;
        }
    }
}
