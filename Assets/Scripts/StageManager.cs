﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ステージ管理のスクリプト
/// </summary>
public class StageManager : MonoBehaviour
{
    [SerializeField] GameObject m_player;

    [SerializeField] Transform m_spwan;

    [SerializeField] int m_key;

    private int m_keyCount = 0;
    public int KeyCount { get { return m_keyCount; } }

    [SerializeField] Image[] m_keyImage;

    Singleton singleton;

    void Awake()
    {
        Instantiate(m_player, m_spwan.position, Quaternion.identity);
    }

    void Start()
    {
        singleton = Singleton.Instance;
        Debug.Log(singleton.NowStageMode);

        if (singleton.NowStageMode == Singleton.StageMode.Key)
        {
            m_keyImage = new Image[m_key];

            for (int i = 0; i < m_key; i++)
            {
                m_keyImage[i] = GameObject.Find("Key" + (i + 1)).GetComponent<Image>();
                m_keyImage[i].enabled = false;
            }
        }
    }

    /// <summary>
    /// カギをゲットした時の処理
    /// </summary>
    public void KeyGet()
    {
        m_keyImage[m_keyCount].enabled = true;
        m_keyCount++;
    }
}
