using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour
{
    [SerializeField] Button[] m_stageSelect = new Button[12];

    private int m_selectNumber = 0;

    void Start()
    {
        m_stageSelect[0] = GameObject.Find("Tutorial").GetComponent<Button>();

        for (int i = 1; i < 12; i++)
        {
            m_stageSelect[i] = GameObject.Find("Stage" + i).GetComponent<Button>();
        }

        for (int i = 2; i < 12; i++)
        {
            m_stageSelect[i].interactable = false;
        }

        m_stageSelect[0].image.color = Color.cyan;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) && m_selectNumber < 11)
        {
            m_selectNumber++;
            Select(m_selectNumber);
        }
        else if (Input.GetKeyDown(KeyCode.A) && m_selectNumber > 0)
        {
            m_selectNumber--;
            Select(m_selectNumber);
        }
        if (Input.GetKeyDown(KeyCode.S) && m_selectNumber < 8)
        {
            m_selectNumber += 4;
            Select(m_selectNumber);
        }
        else if (Input.GetKeyDown(KeyCode.W) && m_selectNumber > 3)
        {
            m_selectNumber -= 4;
            Select(m_selectNumber);
        }

        if (Input.GetKeyDown(KeyCode.RightCommand))
        {
            Click(m_selectNumber);
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
