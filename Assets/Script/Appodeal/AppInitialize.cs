using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppodealStack.Monetization.Api;
using AppodealStack.Monetization.Common;
using System;

public class AppInitialize : MonoBehaviour
{
    void Start()
    {
        int adTypes = AppodealAdType.Interstitial | AppodealAdType.RewardedVideo;
        string appKey = "b1bdcd03e2e3d27f9e3503ac43f134fbf479e604d0c5d0ae";
        AppodealCallbacks.Sdk.OnInitialized += OnInitializationFinished;
        Appodeal.Initialize(appKey, adTypes);
        Appodeal.SetAutoCache(AppodealAdType.Interstitial, false); // Отключаем ручное кэширование
        Appodeal.SetAutoCache(AppodealAdType.RewardedVideo, false); // Отключаем ручное кэшироваие
    }

    public void OnInitializationFinished(object sender, SdkInitializedEventArgs e) { }
}
