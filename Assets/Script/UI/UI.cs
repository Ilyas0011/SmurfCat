using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class UI : MonoBehaviour
{
    [SerializeField] private PlayerController scripPlayerContoller;
    private int lvlId;
    private int nextLvlId;

    private void Start()
    {
        lvlId = SceneManager.GetActiveScene().buildIndex;
        nextLvlId = lvlId + 1;
    }

    public void StartLevel()
    {
        scripPlayerContoller.StartPlayer();
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(nextLvlId);
    }

    public void Restart()
    {
        SceneManager.LoadScene(lvlId);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
