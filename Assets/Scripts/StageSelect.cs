using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// ステージセレクト管理のスクリプト
/// </summary>
public class StageSelect : MonoBehaviour
{
    [SerializeField] Button[] m_stageSelect = new Button[12];

    private int m_selectNumber = 0;

    [SerializeField] GameObject m_check;
    [SerializeField] Button[] m_startBack = new Button[2];

    //ステージ決定画面が表示されているかどうか
    private bool m_checked = false;
    private bool m_yes = true;

    [SerializeField] Text m_stageNumber;
    [SerializeField] Text m_clear;

    Singleton singleton;
    StageData stageData;

    void Start()
    {
        singleton = Singleton.Instance;
        Debug.Log(singleton.m_stageClearCount);

        stageData = StageData.Instance;

        for (int i = 0; i < 12; i++)
        {
            m_stageSelect[i] = GameObject.Find("Stage" + i).GetComponent<Button>();
        }

        //全てのステージボタンを選択不可にする
        for (int i = 0; i < 12; i++)
        {
            m_stageSelect[i].interactable = false;
        }

        //ステージ選択可能な箇所を選択できるようにする
        for (int i = 0; i < singleton.m_stageClearCount + 1; i++)
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
        if (Input.GetButtonDown("Right") && m_selectNumber < 11 &&!m_checked)
        {
            m_selectNumber++;
            Select(m_selectNumber);
        }
        else if (Input.GetButtonDown("Left") && m_selectNumber > 0 && !m_checked)
        {
            m_selectNumber--;
            Select(m_selectNumber);
        }
        if (Input.GetButtonDown("Down") && m_selectNumber < 8 && !m_checked)
        {
            m_selectNumber += 4;
            Select(m_selectNumber);
        }
        else if (Input.GetButtonDown("Up") && m_selectNumber > 3 && !m_checked)
        {
            m_selectNumber -= 4;
            Select(m_selectNumber);
        }

        if (Input.GetButtonDown("Enter") && !m_checked && m_selectNumber <= singleton.m_stageClearCount)
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
            if (Input.GetButtonDown("Right"))
            {
                StartButtonColorChange(false);
                m_yes = false;
            }
            else if (Input.GetButtonDown("Left"))
            {
                StartButtonColorChange(true);
                 m_yes = true;
            }
        }
    }

    /// <summary>
    /// 選択されているステージ処理
    /// </summary>
    /// <param name="number"></param>
    private void Select(int number)
    {
        for (int i = 0; i < 12; i++)
        {
            m_stageSelect[i].image.color = Color.white;
        }
        m_stageSelect[number].image.color = Color.cyan;
    }

    /// <summary>
    /// ステージ決定処理
    /// </summary>
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

        switch (singleton.NowStageMode)
        {
            case Singleton.StageMode.Tutorial:
                m_clear.text = "ゴールする";
                break;
            case Singleton.StageMode.Key:
                m_clear.text = "カギを３つ集めてゴール";
                break;
            case Singleton.StageMode.TimeAttack:
                m_clear.text = "制限時間内にゴール";
                break;
        }

        m_startBack[0].image.color = Color.cyan;
        m_yes = true;
    }

    void StageCheck()
    {
        switch (m_selectNumber)
        {
            case 0:
                singleton.NowStageMode = stageData.Tutrial;
                break;
            case 1:
                singleton.NowStageMode = stageData.Stage1;
                break;
        }
    }

    /// <summary>
    ///　選択決定画面で選択肢の色変更処理
    /// </summary>
    /// <param name="start"></param>
    void StartButtonColorChange(bool start)
    {
        if (start)
        {
            m_startBack[0].image.color = Color.cyan;
            m_startBack[1].image.color = Color.white;
        }
        else
        {
            m_startBack[0].image.color = Color.white;
            m_startBack[1].image.color = Color.cyan;
        }
    }
    /// <summary>
    /// 戻るを押した際の処理
    /// </summary>
    void Back()
    {
        m_startBack[1].image.color = Color.white;
        m_check.SetActive(false);
        m_checked = false;
    }

    /// <summary>
    /// ステージの読み込み処理
    /// </summary>
    /// <param name="number"></param>
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