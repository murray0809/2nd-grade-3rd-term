using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    [SerializeField] GameObject m_player;

    [SerializeField] Transform m_spwan;

    [SerializeField] int m_key;

    private int m_keyCount = 0;
    public int KeyCount { get { return m_keyCount; } }

    [SerializeField] Image[] m_keyImage;

    void Awake()
    {
        Instantiate(m_player, m_spwan.position, Quaternion.identity);
    }

    void Start()
    {
        m_keyImage = new Image[m_key];

        for (int i = 0; i < m_key; i++)
        {
            m_keyImage[i] = GameObject.Find("Key" + (i + 1)).GetComponent<Image>();
            m_keyImage[i].enabled = false;
        }
    }

    void Update()
    {
        
    }

    public void KeyGet()
    {
        m_keyImage[m_keyCount].enabled = true;
        m_keyCount++;
    }
}
