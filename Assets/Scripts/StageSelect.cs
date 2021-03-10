using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour
{
    [SerializeField] Button[] m_stageSelect = new Button[12];

    private int m_selectNumber = 0;

    [SerializeField] GameObject m_check;
    [SerializeField] Button[] m_startBack = new Button[2];

    private bool m_checked = false;
    private bool m_yes = true;

    [SerializeField] Text m_stageNumber;
    [SerializeField] Text m_clear;

    Clear m_clearMode;

    Singleton singleton;

    void Start()
    {
        singleton = Singleton.Instance;
        Debug.Log(singleton.stageClearCount);

        m_stageSelect[0] = GameObject.Find("Tutorial").GetComponent<Button>();

        for (int i = 1; i < 12; i++)
        {
            m_stageSelect[i] = GameObject.Find("Stage" + i).GetComponent<Button>();
        }

        for (int i = 2; i < 12; i++)
        {
            m_stageSelect[i].interactable = false;
        }

        for (int i = 0; i < singleton.stageClearCount + 1; i++)
        {
            m_stageSelect[i].interactable = true;
        }

        m_stageSelect[0].image.color = Color.cyan;

        m_check = GameObject.Find("Check");

        m_stageNumber = GameObject.Find("StageNumber").GetComponent<Text>();
        m_clear = GameObject.Find("Clear").GetComponent<Text>();

        m_startBack[0] = GameObject.Find("Start").GetComponent<Button>();
        m_startBack[1] = GameObject.Find("Back").GetComponent<Button>();

        m_check.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("D") && m_selectNumber < 11 &&!m_checked)
        {
            m_selectNumber++;
            Select(m_selectNumber);
        }
        else if (Input.GetButtonDown("A") && m_selectNumber > 0 && !m_checked)
        {
            m_selectNumber--;
            Select(m_selectNumber);
        }
        if (Input.GetButtonDown("S") && m_selectNumber < 8 && !m_checked)
        {
            m_selectNumber += 4;
            Select(m_selectNumber);
        }
        else if (Input.GetButtonDown("W") && m_selectNumber > 3 && !m_checked)
        {
            m_selectNumber -= 4;
            Select(m_selectNumber);
        }

        if (Input.GetButtonDown("Enter") && !m_checked)
        {
            Check();
        }
        else if (m_yes && Input.GetButtonDown("Enter") && m_checked)
        {
            Click(m_selectNumber);
        }
        else if (!m_yes && Input.GetButtonDown("Enter") && m_checked)
        {
            Back();
        }

        if (m_checked)
        {
            if (Input.GetButtonDown("D"))
            {
                m_startBack[0].image.color = Color.white;
                m_startBack[1].image.color = Color.cyan;
                m_yes = false;
            }
            else if (Input.GetButtonDown("A"))
            {
                m_startBack[1].image.color = Color.white;
                m_startBack[0].image.color = Color.cyan;
                m_yes = true;
            }
        }
    }

    private void Select(int number)
    {
        for (int i = 0; i < 12; i++)
        {
            m_stageSelect[i].image.color = Color.white;
        }
        m_stageSelect[number].image.color = Color.cyan;
    }

    void Check()
    {
        m_check.SetActive(true);
        m_checked = true;
        if (m_selectNumber == 0)
        {
            m_stageNumber.text = "Tutorial";
        }
        else
        {
            m_stageNumber.text = "Stage" + m_selectNumber;
        }

        StageCheck();

        switch (m_clearMode)
        {
            case Clear.Tutorial:
                m_clear.text = "ゴールする";
                break;
            case Clear.Key:
                m_clear.text = "カギを３つ集めてゴール";
                break;
            case Clear.Time:
                m_clear.text = "制限時間内にゴール";
                break;
        }

        m_startBack[0].image.color = Color.cyan;
        m_yes = true;
    }

    void StageCheck()
    {
        if (m_selectNumber == 0)
        {
            m_clearMode = Clear.Tutorial;
        }
        else if (m_selectNumber == 1)
        {
            m_clearMode = Clear.Key;
        }
    }

    void Back()
    {
        m_startBack[1].image.color = Color.white;
        m_check.SetActive(false);
        m_checked = false;
    }

    public void Click(int number)
    {
        switch (number)
        {
            case 0:
                SceneManager.LoadScene("Tutorial");
                break;
            case 1:
                SceneManager.LoadScene("Stage1");
                break;
            case 2:
                SceneManager.LoadScene("Stage2");
                break;
            case 3:
                SceneManager.LoadScene("Stage3");
                break;
            case 4:
                SceneManager.LoadScene("Stage4");
                break;
            case 5:
                SceneManager.LoadScene("Stage5");
                break;
            case 6:
                SceneManager.LoadScene("Stage6");
                break;
            case 7:
                SceneManager.LoadScene("Stage7");
                break;
            case 8:
                SceneManager.LoadScene("Stage8");
                break;
            case 9:
                SceneManager.LoadScene("Stage9");
                break;
            case 10:
                SceneManager.LoadScene("Stage10");
                break;
            case 11:
                SceneManager.LoadScene("Stage11");
                break;
        }
    }
}

public enum Clear
{
    Tutorial,
    Key,
    Time,
}