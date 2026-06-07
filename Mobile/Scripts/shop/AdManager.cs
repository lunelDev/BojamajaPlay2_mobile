using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.SceneManagement;

public class AdManager : MonoBehaviour
{
  
    /* 안드로이드 테스트 ID
    배너 광고	ca-app-pub-3940256099942544/6300978111
    전면 광고	ca-app-pub-3940256099942544/1033173712
    보상형 동영상 광고	ca-app-pub-3940256099942544/5224354917
    네이티브 광고 고급형	ca-app-pub-3940256099942544/2247696110
    */

    /* iOS 테스트 ID
    배너 광고	ca-app-pub-3940256099942544/2934735716
    전면 광고	ca-app-pub-3940256099942544/4411468910
    보상형 동영상 광고	ca-app-pub-3940256099942544/1712485313
    네이티브 광고 고급형	ca-app-pub-3940256099942544/3986624511
     */

    /*
    안드로이드 앱 ID ca-app-pub-9800709682711040~4037387824
    배너 광고   ca-app-pub-9800709682711040/8898283523
    전면 광고   ca-app-pub-9800709682711040/3454385151
    보상형 동영상 광고  ca-app-pub-9800709682711040/6986984794
     */

    /*
    iOS 앱 ID ca-app-pub-9800709682711040~7166095533
    배너 광고 ca-app-pub-9800709682711040/3226850528
    전면 광고 ca-app-pub-9800709682711040/9600687188
    보상형 동영상 광고 ca-app-pub-9800709682711040/2676186187
     */

    /// <summary>
    ///  전면: 
    ///       ca-app-pub-9800709682711040~9795767229
    ///       ca-app-pub-9800709682711040/3230358870
    ///  보상:     
    ///       ca-app-pub-9800709682711040~9795767229
    ///       ca-app-pub-9800709682711040/8156200730
    ///       
    /// </summary>
#if UNITY_ANDROID
    ///// 안드로이드
    //private readonly string bannerID = "ca-app-pub-9800709682711040/8898283523";  // 실제 배너광고 아이디
    //private readonly string topbannerID = "ca-app-pub-9800709682711040/2180591655"; // 실제 탑배너광고 아이디
    private readonly string test_bannderID = "ca-app-pub-3940256099942544/6300978111"; // 테스트 배너광고 아이디

    private readonly string InterstitialID = "ca-app-pub-9800709682711040/3230358870";  // 실제 전면광고 아이디
    //private readonly string InterstitialID = "ca-app-pub-3940256099942544/1033173712"; // 테스트 전면광고 아이디

    private readonly string rewardID = "ca-app-pub-9800709682711040/8156200730";  // 실제 보상광고 아이디
    //private readonly string rewardID = "ca-app-pub-3940256099942544/5224354917"; // 테스트 보상광고 아이디    
#elif UNITY_IPHONE
    ///// iOS
    private readonly string bannerID = "ca-app-pub-9800709682711040/3226850528";  // 실제 배너광고 아이디
    private readonly string test_bannderID = "ca-app-pub-3940256099942544/2934735716"; // 테스트 배너광고 아이디

    private readonly string InterstitialID = "ca-app-pub-9800709682711040/9600687188";  // 실제 전면광고 아이디
    private readonly string test_InterstitialID = "ca-app-pub-3940256099942544/4411468910"; // 테스트 전면광고 아이디

    private readonly string rewardID = "ca-app-pub-9800709682711040/2676186187";  // 실제 보상광고 아이디
    private readonly string test_rewardID = "ca-app-pub-3940256099942544/1712485313"; // 테스트 보상광고 아이디    
#endif

    //private readonly string test_deviceID = "A11DF88A1BF6C6D7061F325F46B51BB4";

    private RewardedAd rewardedAd;
    private InterstitialAd screenAd;
    private BannerView banner;
    private BannerView bottombanner;

    private int clickNum = 0;

    public AdPosition topPosition; // enum 으로 등록되어있어서 Component 에서 선택해줘야함
    public AdPosition smartPosition; // enum 으로 등록되어있어서 Component 에서 선택해줘야함

    public static AdManager Instance { get; private set; }

    //public static int count = 0;
    //private int index;
    void Awake()
    {
        var objs = FindObjectsOfType<AdManager>();

        if (objs.Length != 1)
        {
            Destroy(this);

            return;
        }

        DontDestroyOnLoad(this.gameObject);

        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //RequestBanner();      // 배너 수정 2020-08-20
        //RequestBannerBottom();
        RequestInterstitial();
        RequestReward();
    }

    public void AllRequest()
    {
        //RequestBanner();
        //RequestBannerBottom();
        RequestInterstitial();
        RequestReward();
    }

    public void RequestReward()
    {
        StopAllCoroutines();
        StartCoroutine(_RequestReward());
    }

    IEnumerator _RequestReward()
    {
        //string id = Debug.isDebugBuild ? rewardID : test_rewardID;
        string id = rewardID;
        //string id = test_rewardID;
        // Get singleton reward based video ad reference.
        rewardedAd = new RewardedAd(id);

        // Called when an ad request has successfully loaded.
        //this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
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
        // Load the rewarded video ad with the request.
        rewardedAd.LoadAd(request);

        yield return null;
    }

    // 보상형 광고닫기
    private void HandleRewardedAdClosed(object sender, EventArgs e)
    {
        if (PopUpSystem.PopUpState.Equals(false))
        {
            Time.timeScale = 1f;    // 게임 다시 시작
        }
        else
        {
            Time.timeScale = 0f;    // 게임 다시 시작
        }

        // 보상형광고 객체생성
        RequestReward();
    }

    // 보상받기
    private void HandleUserEarnedReward(object sender, Reward e)
    {
        //throw new NotImplementedException();
        string type = e.Type;
        double amount = e.Amount;
        //광고 횟수 차감
        GameManager.Instance.FreeADCount -= 1;
        PlayerPrefs.SetInt("TodayAD", GameManager.Instance.FreeADCount);
        //광고 보상 제공
        TimeManager.instance.diamondSu += 5;
        PlayerPrefs.SetInt("Diamond", TimeManager.instance.diamondSu);
       // TimeManager.instance.DiamondSu_Check();
        //쿨타임
        GameManager.Instance.AdCool = true;
        PlayerPrefs.SetString("ADCoolTime", GameManager.Instance.AdCool.ToString());
        PlayerPrefs.SetString("ADEndCoolTime", DateTime.Now.AddMinutes(5).ToString("yyyy-MM-dd HH:mm:ss"));

        PlayerPrefs.Save();
    }

    // 보여주기 실패
    private void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs e)
    {
        if (PopUpSystem.PopUpState.Equals(false))
        {
            Time.timeScale = 1f;    // 게임 다시 시작
        }
        else
        {
            Time.timeScale = 0f;    // 게임 다시 시작
        }
       //PopUpSystem.Instance.ShowPopUp(UIManager.Instance.FailCallAd); //광고 송출 실패
        RequestReward();
    }

    // 광고를 누른사람 수 추적
    private void HandleRewardedAdOpening(object sender, EventArgs e)
    {

        if (PopUpSystem.PopUpState.Equals(false))
        {
            Time.timeScale = 1f;    // 게임 다시 시작
        }
        else
        {
            Time.timeScale = 0f;    // 게임 다시 시작
        }
        clickNum += 1;
        //throw new NotImplementedException();
        //Debug.Log("영상확인 횟수 : " + clickNum);
        RequestReward();
    }

    // 로드 실패
    private void HandleRewardedAdFailedToLoad(object sender, EventArgs e)
    {
        RequestReward();
    }

    // 보상형 광고 보기
    public void ShowRewardAd()
    {
        if (!rewardedAd.IsLoaded())
        {
            Debug.Log("로드 안뎀");
            PopUpSystem.Instance.ShowPopUp(UIManager.Instance.FailCallAd); //광고 송출 실패
            RequestReward();
        }
        else
        {
            Debug.Log("로드뎀");
            StopAllCoroutines();
            StartCoroutine(_ShowRewardAd());
        }

    }

    // 보상형 광고 보기 코루틴
    private IEnumerator _ShowRewardAd()
    {
        while (!rewardedAd.IsLoaded())
        {
            yield return null;
        }

        Time.timeScale = 0f;    // 게임 정지
        rewardedAd.Show();
    }
    ///////////////////////////////// 전면 광고 /////////////////////////////////////////////////////////
    public void RequestInterstitial()
    {
        StopAllCoroutines();
        StartCoroutine(_RequestInterstitial());
    }

    IEnumerator _RequestInterstitial()
    {
        //string id = Debug.isDebugBuild ? InterstitialID : test_InterstitialID;
        string id = InterstitialID;
       //string id = test_InterstitialID;

        screenAd = new InterstitialAd(id);  // 광고를 끝내고 다른 광고를 띄우기위해서는 새로운 객체를 만들어야함.

        AdRequest request = new AdRequest.Builder().Build();   // 나중에 테스트 기기 빼야함!!
       //s Debug.Log("???");
        screenAd.LoadAd(request);   // 지연시간이 발생해서 따로 광고 로드된지 확인하고 출력해줘야함

        // Handle CallBack
        //Called when an ad request has successfully loaded.
        //screenAd.OnAdLoaded += HandleOnInterstitialAdLoaded;
        //Called when an ad request failed to load.
        screenAd.OnAdFailedToLoad += HandleOnInterstitialAdAdFailedToLoad;
        // Called when an ad is shown.
        this.screenAd.OnAdOpening += HandleOnInterstitialAdAdOpened;
        // Called when the ad is closed.
        screenAd.OnAdClosed += HandleOnInterstitialAdClosed;
        // Called when the ad click caused the user to leave the application.
        //this.screenAd.OnAdLeavingApplication += HandleOnInterstitialAdAdLeavingApplication;

        yield return null;
    }

    private void HandleOnInterstitialAdAdLeavingApplication(object sender, EventArgs e)
    {
        if (PopUpSystem.PopUpState.Equals(false))
        {
            Time.timeScale = 1f;    // 게임 다시 시작
        }
        else
        {
            Time.timeScale = 0f;    // 게임 다시 시작
        }
        RequestInterstitial();
    }

    private void HandleOnInterstitialAdAdOpened(object sender, EventArgs e)
    {
        //throw new NotImplementedException();
      //  Debug.LogError("HandleOnInterstitialAdAdOpened");
        if (PopUpSystem.PopUpState.Equals(false))
        {
            Time.timeScale = 1f;    // 게임 다시 시작
        }
        else
        {
            Time.timeScale = 0f;    // 게임 다시 시작
        }
    }

    private void HandleOnInterstitialAdAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        //screenAd.OnAdFailedToLoad -= HandleOnInterstitialAdAdFailedToLoad;
        //screenAd.OnAdClosed -= HandleOnInterstitialAdClosed;

        Debug.LogError("HandleOnInterstitialAdAdFailedToLoad");

        //screenAd.Destroy();

        RequestInterstitial();
    }

    private void HandleOnInterstitialAdClosed(object sender, EventArgs e)
    {
        //screenAd.OnAdFailedToLoad -= HandleOnInterstitialAdAdFailedToLoad;
        //screenAd.OnAdClosed -= HandleOnInterstitialAdClosed;

        if (PopUpSystem.PopUpState.Equals(false))
        {
            Time.timeScale = 1f;    // 게임 다시 시작
        }
        else
        {
            Time.timeScale = 0f;    // 게임 다시 시작
        }

        //screenAd.Destroy();

        RequestInterstitial();  // 광고 닫히면 전면광고 다시 로드
    }
    //////////////// 전면광고 AD ////////////////////////////////
    public void ShowScreenAd()
    {
        StopAllCoroutines();
        StartCoroutine(_ShowScreenAd());
    }

    private IEnumerator _ShowScreenAd()
    {
        while (!screenAd.IsLoaded())
        {
            yield return null;
        }

        Time.timeScale = 0f;    // 게임 정지

        screenAd.Show();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////
    public void RequestBannerBottom()
    {
        StopAllCoroutines();
        StartCoroutine(_RequestBannerBottom());
    }
    IEnumerator _RequestBannerBottom()
    {
        // 임시변수: 디버깅용 , 릴리즈용
        string id = test_bannderID;
        //   string id = test_bannerID;
        //AdSize adSize = new AdSize(320, 50);
        // 광고 오브젝트 생성
        //banner = new BannerView(id, AdSize.SmartBanner, AdPosition.Bottom);
        //banner = new BannerView(id, size, position);
        //banner = new BannerView(id, AdSize.SmartBanner, smartPosition); // 게임 끝날 때 호출
        bottombanner = new BannerView(id, AdSize.SmartBanner, smartPosition); // 게임 끝날 때 호출


        // 새로운 광고 요청 : 유니티 빌더 패턴 
        AdRequest request = new AdRequest.Builder().Build();

        // 광고 로드
        bottombanner.LoadAd(request);

        bottombanner.OnAdLoaded += HandleOnBottomAdLoaded;
        bottombanner.OnAdClosed += HandleOnBannderBottomAdClosed;

        yield return null;
    }

    private void HandleOnBottomAdLoaded(object sender, EventArgs e)
    {
        ToggleBottomAd(true);
    }

    private void HandleOnBannderBottomAdClosed(object sender, EventArgs e)
    {
        RequestBannerBottom();
    }
  
    public void RequestBanner()
    {
        StopAllCoroutines();
        StartCoroutine(_RequestBanner());
    }

    IEnumerator _RequestBanner()
    {
        // 임시변수: 디버깅용 , 릴리즈용
        string id = test_bannderID;
        // string id = topbannerID;
        //AdSize adSize = new AdSize(320, 50);
        // 광고 오브젝트 생성
        //banner = new BannerView(id, AdSize.SmartBanner, AdPosition.Bottom);
        //banner = new BannerView(id, size, position);
        //banner = new BannerView(id, AdSize.SmartBanner, smartPosition); // 게임 끝날 때 호출
        banner = new BannerView(id, AdSize.Banner, topPosition); // 게임 끝날 때 호출


        // 새로운 광고 요청 : 유니티 빌더 패턴 
        AdRequest request = new AdRequest.Builder().Build();

        // 광고 로드
        banner.LoadAd(request);

        banner.OnAdLoaded += HandleOnAdLoaded;
        banner.OnAdClosed += HandleOnBannderAdClosed;

        yield return null;
    }

    public void ToggleAd(bool active)
    {
        if (active)
        {
            banner.Show();
        }
        else
        {
            banner.Hide();
        }
    }

    public void ToggleBottomAd(bool active)
    {
        if (active)
        {
            bottombanner.Show();
        }
        else
        {
            bottombanner.Hide();
        }
    }

    private void HandleOnAdLoaded(object sender, EventArgs e)
    {
        ToggleAd(true);
    }

    private void HandleOnBannderAdClosed(object sender, EventArgs e)
    {
        RequestBanner();
    }

    public void DestroyBannerAd()
    {
        banner.Destroy();
    }

    private void DestroyInterstitialAd()
    {
        screenAd.Destroy();
    }
  
}
