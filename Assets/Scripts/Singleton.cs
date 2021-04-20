﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton
{
    private static Singleton mInstance;

    public int m_stageClearCount = 1;

    public Clear m_clearMode;
   
    private Singleton()
    { // Private Constructor

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
