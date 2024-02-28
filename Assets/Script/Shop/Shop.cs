using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using AppodealStack.Monetization.Api;
using AppodealStack.Monetization.Common;
using System;

public class Shop : MonoBehaviour
{

    [SerializeField] private GameObject buttonRewarded; //Кнопка рекламы
    [SerializeField] private TextMeshProUGUI snailCurrency;
    [SerializeField] private Animator animPlayer;

    void Start()
    {
        RefreshRewardedVideo();
        SnailUpadate();
        SetInitialAcquisitions();
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OnClickRewarded()
    {
        if (Appodeal.IsLoaded(AppodealAdType.RewardedVideo))
            Appodeal.Show(AppodealShowStyle.RewardedVideo);
        else
            Appodeal.Cache(AppodealAdType.RewardedVideo);
    }

    public void ClickBackDance()
    {
        animPlayer.Play("Idle");
    }

    void OnRewardedVideoFinished(object sender, RewardedVideoFinishedEventArgs e)
    {
        PlayerPrefs.SetInt("SnailCurrency", PlayerPrefs.GetInt("SnailCurrency") + 1000);
        SnailUpadate();
        buttonRewarded.SetActive(false);
    }

    void RefreshRewardedVideo()
    {
        AppodealCallbacks.RewardedVideo.OnFinished += OnRewardedVideoFinished;
        Appodeal.Cache(AppodealAdType.RewardedVideo);
    }

    private void SetInitialAcquisitions() // Устанавливает начальные приобретения в магазине
    {
        PlayerPrefs.SetInt("acquiredKey" + 0, 1);
        PlayerPrefs.SetInt("acquiredDance" + 0, 1);
        PlayerPrefs.SetInt("acquiredSky" + 0, 1);
        DynamicGI.UpdateEnvironment(); // Обновляет динамическое глобальное освещение и отражения в игровой сцене.
    }

    public void SnailUpadate()
    {
        snailCurrency.text = PlayerPrefs.GetInt("SnailCurrency").ToString();
    }

}
