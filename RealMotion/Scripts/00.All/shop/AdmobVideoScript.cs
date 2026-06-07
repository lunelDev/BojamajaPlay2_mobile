using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdmobVideoScript : MonoBehaviour
{
  //  static bool isAdVideoLoaded = false;
 /*   private RewardedAd rewardedAd;
    private RewardedAd videoAd;
    public ShopUIManager _shopUI;
    public bool ShowAd = false;
    string videoID;
    public GameObject Uicanvas;
    public GameObject ShopClose;
    public GameObject PopUpPanel;

    [Header("광고 UI")]
    public GameObject NoAdPanel;
    public bool noAdPanelAble = false;
    public static AdmobVideoScript Instance { get; private set; }
    public void Awake()
    {
        if (Instance != null) Destroy(this);
        else Instance = this;
    }
    public void Start()
    {
        //Test ID : "ca-app-pub-3940256099942544/5224354917"
        //광고 ID
     //   videoID = "ca-app-pub-9800709682711040/4206703704";
      //  videoAd = new RewardedAd(videoID);

        // Handle(videoAd);
        // Load();
        // MobileAds.Initialize(appId);
        string adUnitId;
#if UNITY_ANDROID
        adUnitId = "ca-app-pub-9800709682711040/4206703704";
#else
            adUnitId = "unexpected_platform";
#endif

        this.rewardedAd = new RewardedAd(adUnitId);

        this.RequestRewardedAd();
    }
    // 보상형 광고
    private void RequestRewardedAd()
    {
        string adUnitId;
#if UNITY_ANDROID
        adUnitId = "ca-app-pub-9800709682711040/4206703704";
#else
            adUnitId = "unexpected_platform";
#endif

        this.rewardedAd = new RewardedAd(adUnitId);

        // Called when an ad request has successfully loaded.
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdClosed event received");
        RequestRewardedAd();
    }
    //보상설정
    public void HandleUserEarnedReward(object sender, Reward args)
    {
        _EndAD();
    }

    public void ShowRewardedAd()
    {
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
        }
        else
        {
            Debug.Log("NOT Loaded Interstitial");
            CanShowAd();
            RequestRewardedAd();
        }
    }
    void _EndAD()
    {
        TimeManager.instance.diamondSu += 2; //다이아 제공
        PlayerPrefs.SetString("PlayStartTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        TimeManager.instance.DiamondSu_Check();
        PlayerPrefs.SetInt("Diamond", TimeManager.instance.diamondSu);
        PlayerPrefs.Save();
        //
        Uicanvas.SetActive(true); //UI캔버스는 활성화
        PopUpPanel.SetActive(false); //팝업창 비활성화
        ShopClose.SetActive(false); //스토어창 비활성화
        GameManager.ShopPanel = false;    //스토어창이 닫혀있다고 체크(ShopUIManager) 
        PopUpSystem.PopUpState = false;
        Time.timeScale = 1;
    }
    void CanShowAd()
    {
        NoAdPanel.SetActive(true);
        noAdPanelAble = true; // 판넬 활성화됨 
    }*/
}
 
