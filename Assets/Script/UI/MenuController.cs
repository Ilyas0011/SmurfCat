using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private int _maxProgressLevel;//������� �� ������� ������������
    public int MaxProgressLevel
    {
        get { return _maxProgressLevel; } 
        set { _maxProgressLevel = Math.Max(1, value); }//������ ��������� �������� ������ 1
    }
        
    private void Start()
    {
        MaxProgressLevel = PlayerPrefs.GetInt("LevelProgress");
    }

    public void OnClickStart()
    {
        SceneManager.LoadScene(MaxProgressLevel);
    }

    public void OnClickShop()
    {
        SceneManager.LoadScene(12);
    }
}
