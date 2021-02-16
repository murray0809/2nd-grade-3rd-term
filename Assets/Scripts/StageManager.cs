using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    [SerializeField] GameObject m_player;

    [SerializeField] Transform m_spwan;

    [SerializeField] bool[] m_key;

    private int m_keyCount = 0;
    public int KeyCount { get { return m_keyCount; } set { m_keyCount = value; } }

    [SerializeField] Image[] m_keyImage;

    void Awake()
    {
        Instantiate(m_player, m_spwan.position, Quaternion.identity);
    }

    void Start()
    {
        m_keyImage = new Image[m_key.Length];

        for (int i = 0; i < m_key.Length; i++)
        {
            m_keyImage[i] = GameObject.Find("Key" + (i + 1)).GetComponent<Image>();
        }
    }

    void Update()
    {
        
    }

    public void KeyGet()
    {
        m_key[m_keyCount] = true;
        m_keyImage[m_keyCount].color = Color.green;
        m_keyCount++;
    }
}
