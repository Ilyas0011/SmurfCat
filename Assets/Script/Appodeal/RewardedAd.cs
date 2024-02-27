using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppodealStack.Monetization.Api;
using AppodealStack.Monetization.Common;
using System;

public class RewardedAd : MonoBehaviour
{
    [SerializeField] private GameObject buttonRewarded; // нопка рекламы
    public void OnClickRewarded()
    {
        if (Appodeal.IsLoaded(AppodealAdType.RewardedVideo))
        {
            Appodeal.Show(AppodealShowStyle.RewardedVideo);
        }
        else
        {
            Appodeal.Cache(AppodealAdType.RewardedVideo);

            if (Appodeal.IsLoaded(AppodealAdType.RewardedVideo))
                Appodeal.Show(AppodealShowStyle.RewardedVideo);
        }
    }

    private void Start()
    {
        AppodealCallbacks.RewardedVideo.OnFinished += OnRewardedVideoFinished;
    }

    private void OnRewardedVideoFinished(object sender, RewardedVideoFinishedEventArgs e)
    {
        PlayerPrefs.SetInt("MainCrystal", PlayerPrefs.GetInt("MainCrystal") + 100);
        Debug.Log($"[APDUnity] [Callback] OnRewardedVideoFinished(double amount:{e.Amount}, string name:{e.Currency})");
        buttonRewarded.SetActive(false);
    }
}
