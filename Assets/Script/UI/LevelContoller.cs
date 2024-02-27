using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class LevelContoller : MonoBehaviour
{

    [SerializeField] private int lvlId;//����� ������
    [SerializeField] private TextMeshProUGUI recordText; //����� �������
    [SerializeField] private GameObject objectRecord;
    [SerializeField] private Button button;


    private int record; //����� ������������ ��������� �� ������ �� ���� ������ ����������
    private int maxProgressLevel;//������� �� ������� ������������

    public int MaxProgressLevel
    {
        get { return maxProgressLevel; }
        set { maxProgressLevel = Math.Max(1, value); }//������ ��������� �������� ������ 1
    }

    private void Start()
    {
        button = GetComponent<Button>();
        record = PlayerPrefs.GetInt("Record" + lvlId);//����� ������������ ��������� �� ������ �� ���� ������ ����������
        LevelInactiveCheck();
        CheckRecord();
    }

    private void CheckRecord()
    {
        if (record <= 0)
        {
            objectRecord.SetActive(false);
        }
        else if (record > 12)
        {
            objectRecord.SetActive(true);
            recordText.text = "12";
        }
        else if (record > 0)
        {
            objectRecord.SetActive(true);
            recordText.text = ($"x{record.ToString()}");
        }
    }

    private void LevelInactiveCheck() //��������� ����� �� �� �� ����� ������
    {
        MaxProgressLevel = PlayerPrefs.GetInt("LevelProgress");// +1 ��������� ����� �� ���� ������� ���� ������ �������

        if (lvlId <= MaxProgressLevel && button != null)
                button.interactable = true;
        else if (button != null)
             button.interactable = false;

    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(lvlId);
    }
}
