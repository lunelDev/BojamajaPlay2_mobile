
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;
public class GameManager : MonoBehaviour
{   //다음 게임 저장
    public static int Season2_GameKindCount = 10;
    public static int[] Season2_NextCallNum = new int[Season2_GameKindCount];
    public static int[] SceneNum = new int[Season2_GameKindCount];//씬 넘버
    public static int RandListCountCheck = 0;
    public static bool LastGameCheck = false;
    public int num = 0;

    public float GameTotalScore_Rank = 0;
    TimeManager _timeManager;

    //게임이 끝나고 다음 선택지에 따라 판넬을 활성화/비활성화
    public static bool RankMode = false; //랭크 모드인지 클래식모드인지 판단
    public static bool ModePanel = false;
    public static bool ClassicPanel = false;
    public static bool RankPanel = false;
    public static bool ShopPanel = false;
    public static bool HaveChance = true;
    //
    public static int countForAdvertising;
    public int FreeADCount;
    //
    public static bool HaveAdRemove = false; //광고 제거권을 갖고있을 경우
    public static int HaveAdRemoveCount;
    public static bool HaveOneDayFree = false; //1일 무제한 플레이권을 가지고 있을 경우
    public bool AdCool = false;
    //
    string NextDayStr;
    DateTime NextDay;
    string TodayStr;
    DateTime Today;
    //
    TimeSpan FreeTime;
    TimeSpan LeftCoolTime;
    public String LeftTimeStr;
    public static GameManager Instance { get; private set; }
  
    private void Awake()
    {
        Screen.SetResolution(1920, 1080, true);
        if (Instance != null)
            Destroy(this);
        else Instance = this;

        _timeManager = FindObjectOfType<TimeManager>();
        SetDragThreshold();

        /////////////// 1일 무제한 플레이권을 가지고 있는지 확인 ////////////////
        if (!PlayerPrefs.HasKey("FreeTime"))
        {
            HaveOneDayFree = false;
            PlayerPrefs.SetString("FreeTime", GameManager.HaveOneDayFree.ToString());
        }
        else
        {
            string FreeTimeStr = PlayerPrefs.GetString("FreeTime");
            HaveOneDayFree = System.Convert.ToBoolean(FreeTimeStr);

        }
        ////////////////////// 광고 제거권을 가지고 있는지 확인 ////////////////////
        if (!PlayerPrefs.HasKey("RemoveADCount"))
        {
            HaveAdRemoveCount = 0;
            PlayerPrefs.SetInt("RemoveADCount", HaveAdRemoveCount); //int  ; 100
        }
        else
        {
            HaveAdRemoveCount = PlayerPrefs.GetInt("RemoveADCount");
        }
        // Debug.Log("HaveAdRemove: " + HaveAdRemove);
        ///////////////////// 오늘 남은 광고 갯수 확인 /////////////////////////////////
        if (!PlayerPrefs.HasKey("TodayAD"))
        {
            FreeADCount = 5;
            PlayerPrefs.SetInt("TodayAD", FreeADCount);

            //내일 날짜 저장
            NextDayStr = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            PlayerPrefs.SetString("Tomorrow", NextDayStr);
            NextDay = Convert.ToDateTime(NextDayStr);

            //오늘 날짜 저장
            TodayStr = DateTime.Now.ToString("yyyy-MM-dd");
            // PlayerPrefs.SetString("Today", TodayStr);
            Today = Convert.ToDateTime(TodayStr);
        }
        else
        {
            FreeADCount = PlayerPrefs.GetInt("TodayAD"); //남은 광고 횟수
            //내일
            NextDayStr = PlayerPrefs.GetString("Tomorrow");
            NextDay = Convert.ToDateTime(NextDayStr);
            //오늘
            TodayStr = DateTime.Now.ToString("yyyy-MM-dd");
            Today = Convert.ToDateTime(TodayStr);
            // 하루 지났을 때 광고 횟수 초기화 
            if (Today >= NextDay)
            {
                FreeADCount = 5;
                PlayerPrefs.SetInt("TodayAD", FreeADCount); //남은 광고 횟수
                //다음 날짜 저장
                NextDayStr = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
                PlayerPrefs.SetString("Tomorrow", NextDayStr);
                NextDay = Convert.ToDateTime(NextDayStr);
            }

        }
        /////////////////////////////////////////////////////////////////////////////////////////////


        ///////////////////// 광고 쿨타임 /////////////////////////////////////
        if (!PlayerPrefs.HasKey("ADCoolTime"))
        {
            AdCool = false;
            PlayerPrefs.SetString("ADCoolTime", Instance.AdCool.ToString());
        }
        else
        {
            string AdCoolStr = PlayerPrefs.GetString("ADCoolTime");
            AdCool = System.Convert.ToBoolean(AdCoolStr);
        }
        ///////////////// 클래식 모드에서 사용되는 광고 횟수 관리 ///////////////////////////
        if (!PlayerPrefs.HasKey("AdPlayCount"))
        {
            countForAdvertising = 0;
            PlayerPrefs.SetInt("AdPlayCount", countForAdvertising);
        }
        else
        {
            countForAdvertising = PlayerPrefs.GetInt("AdPlayCount");
        }
        ////////////////////////////////////////////////////////////////////////
        
    }

    public int FreeTimeDay = 0, FreeTimeHour = 0, FreeTimeMin = 0, FreeTimeSec = 0;
    public int LeftTimeMin = 0, LeftTimeSec = 0;
    private void Update()
    {
        /////////////// 100회 광고 제거권을 가지고 있는지 확인 ////////////////
        if (HaveAdRemoveCount>0)
        {
            HaveAdRemove = true; 
        }
        else
        {
            HaveAdRemove = false;
        }
        /////////////// 1일 무제한 플레이권을 가지고 있는지 확인 ////////////////
        if (HaveOneDayFree.Equals(true))
        {
            string currentTimeStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); //현재 시간
            DateTime currentTime = Convert.ToDateTime(currentTimeStr);
            string FreeEndTimeStr = PlayerPrefs.GetString("BuyEndOneDayFree"); //사고 하루 지난 시간
            DateTime FreeEndTime = Convert.ToDateTime(FreeEndTimeStr);
            // 시작시간에서 내린시간 빼서 차를 구한다.
            FreeTime = FreeEndTime - currentTime;
            FreeTimeDay = FreeTime.Days;    //날
            FreeTimeHour = FreeTime.Hours;  //시간
            FreeTimeMin = FreeTime.Minutes; //분
            FreeTimeSec = FreeTime.Seconds; //초

            //총 시간을 초로 변경해서 저장
            //    FreeTimeTatal = (FreeTimeDay * 24 * 60 * 60) + (FreeTimeHour * 60 * 60) + (FreeTimeMin * 60) + FreeTimeSec;
            Debug.Log(FreeTime + "--" + FreeTimeDay.ToString() + "::" + FreeTimeHour.ToString() + "::" + FreeTimeMin.ToString() + "::" + FreeTimeSec.ToString());

            //1일 플레이 무료 남은시간 
            if (currentTime > FreeEndTime)
            {
                HaveOneDayFree = false;
                PlayerPrefs.SetString("FreeTime", HaveOneDayFree.ToString());
            }
        }
        ////////////////////// 광고를 시청 했을때 5분의 쿨타임 /////////////////////////////////
        if (AdCool.Equals(true))
        {
            string ADCoolTimeStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); //현재 시간
            DateTime ADCoolTime = Convert.ToDateTime(ADCoolTimeStr);
            string ADEndCoolTimeStr = PlayerPrefs.GetString("ADEndCoolTime"); //광고 시청하고 5분
            DateTime ADEndCoolTime = Convert.ToDateTime(ADEndCoolTimeStr);

            LeftCoolTime = ADEndCoolTime - ADCoolTime;

            LeftTimeMin = LeftCoolTime.Minutes; //분
            LeftTimeSec = LeftCoolTime.Seconds; //초

            if (ADCoolTime > ADEndCoolTime)
            {
                AdCool = false;
                PlayerPrefs.SetString("ADCoolTime", AdCool.ToString());
            }
        }
        TodayStr = DateTime.Now.ToString("yyyy-MM-dd");
        Today = Convert.ToDateTime(TodayStr);
        // 하루 지났을 때 광고 횟수 초기화 
        if (Today >= NextDay)
        {
            FreeADCount = 5;
            PlayerPrefs.SetInt("TodayAD", FreeADCount); //남은 광고 횟수
            //다음 날짜 저장
            NextDayStr = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            PlayerPrefs.SetString("Tomorrow", NextDayStr);
            NextDay = Convert.ToDateTime(NextDayStr);
        }
    }
    public void ShowLeftTime()
    {
        StartCoroutine(_ShowLeftTime());
    }
    IEnumerator _ShowLeftTime()
    {

        yield return null;
    }
    public void ClickRankMode()
    {
        RankMode = true;
    }
    //다음 씬 전환(Rank게임일 경우)
    public void NextScenes()
    {
        GameManager.Instance.OnClick_BtnSound1();
        //진행이 되지 않는 게임이 남았는지 확인
        if (RankMode.Equals(true))
        {
            if (GameManager.HaveChance.Equals(true))
            {
                if (Season2_GameKindCount >= RandListCountCheck + 1)
                {

                    //마지막 게임
                    if (RandListCountCheck == 9)
                    {
                        Debug.Log(LastGameCheck);
                        LastGameCheck = true;
                    }
                    //다음 게임의 씬을 불러온다.
                    SceneCheck();
                    //카운터를 올려주면서 배열의 값을 증가시킬수 있도록 함
                    RandListCountCheck += 1;
                }
                else
                {
                    //마지막 씬 불러오기 
                    SceneManager.LoadScene("EndScene");
                    RandListCountCheck = 0;
                    LastGameCheck = false;

                }
            }
            else
            {
                SceneManager.LoadScene("EndScene");
                RandListCountCheck = 0;
                LastGameCheck = false;
            }
        }
    }

    //게임 스타트 버튼을 눌렀을 떄, 해당 이미지와 같은 씬을 불러봄
    public void SceneCheck()
    {
        //randArray값과 같은 씬을 불러온다
        if (Season2_NextCallNum[RandListCountCheck].Equals(0))
        {
            SceneManager.LoadScene("Bowling");
        }
        else if (Season2_NextCallNum[RandListCountCheck].Equals(1))
        {
            SceneManager.LoadScene("Dust");
        }
        else if (Season2_NextCallNum[RandListCountCheck].Equals(2))
        {
            SceneManager.LoadScene("Hotpot");
        }
        else if (Season2_NextCallNum[RandListCountCheck].Equals(3))
        {
            SceneManager.LoadScene("Pirates");
        }
        else if (Season2_NextCallNum[RandListCountCheck].Equals(4))
        {
            SceneManager.LoadScene("Pizza");
        }
        else if (Season2_NextCallNum[RandListCountCheck].Equals(5))
        {
            SceneManager.LoadScene("Sandwiches");
        }
        else if (Season2_NextCallNum[RandListCountCheck].Equals(6))
        {
            SceneManager.LoadScene("Soccer");
        }
        else if (Season2_NextCallNum[RandListCountCheck].Equals(7))
        {
            SceneManager.LoadScene("Submarine");
        }
        else if (Season2_NextCallNum[RandListCountCheck].Equals(8))
        {
            SceneManager.LoadScene("ToyFactory");
        }
        else if (Season2_NextCallNum[RandListCountCheck].Equals(9))
        {
            SceneManager.LoadScene("Zombies");
        }
    }
    /////////////////  랭킹 모드 가기 /////////////////////////
    public void Ranking(int _rank_search_amount)
    {
        StartCoroutine(_Ranking(_rank_search_amount));
    }

    IEnumerator _Ranking(int _rank_search_amount)
    {
        // 네트워크 연결상태 확인하기
        OnClick_BtnSound1();
        ServerManager.Instance.GetNetworkState();
        yield return new WaitUntil(() => ServerManager.Instance.isConnCompleted);
        // 네트워크 연결 처리
        if (ServerManager.Instance.isConnected)
        {
            // 랭킹 서치 원하는 갯수만큼 받아오면됨
            ServerManager.Instance.RankingSearch(_rank_search_amount);
            SceneManager.LoadScene("Ranking");
            PopUpSystem.PopUpState = false;
            RankMode = false;
            Time.timeScale = 1;
        }
    }
    //home 버튼을 클릭하면 메인화면으로 이동한다. 
    public void OnClick_Home_Button()
    {
        OnClick_BtnSound1();
        StartCoroutine(_OnClick_Home_Button());

        RandListCountCheck = 0;
        PopUpSystem.PopUpState = false;
        RankMode = false;
        MainAppManager.RandomImgPageEnable = false;
        Time.timeScale = 1;

        SceneManager.LoadScene("Main");
    }
    //home 버튼을 클릭하면 메인화면으로 이동한다. 
    public void OnClick_Home_Button_rank(int _rank_search_amount)
    {
        OnClick_BtnSound1();
        StartCoroutine(_OnClick_Home_Button());

        ServerManager.Instance.RankingSearch(_rank_search_amount);

        RandListCountCheck = 0;
        PopUpSystem.PopUpState = false;
        RankMode = false;
        RankPanel = true;
        MainAppManager.RandomImgPageEnable = false;
        Time.timeScale = 1;

        StartCoroutine(_SceneGoMain());
    }

    IEnumerator _SceneGoMain()
    {
        yield return new WaitUntil(() => ServerManager.Instance.isHighListStackUp);
        ServerManager.Instance.isHighListStackUp = false;

        //SceneManager.LoadScene("Main");
        //RandListCountCheck = 0;
        //PopUpSystem.PopUpState = false;
        //RankMode = false;
        //MainAppManager.RandomImgPageEnable = false;
        //Time.timeScale = 1;

        SceneManager.LoadScene("Main");
    }

    IEnumerator _OnClick_Home_Button()
    {
        // 네트워크 연결상태 확인하기
        ServerManager.Instance.GetNetworkState();
        //yield return new WaitUntil(() => ServerManager.Instance.isConnCompleted);
        yield return null;
    }
    //////////////////////////// 사운드 설정 //////////////////////////////////////    
    //클릭버튼
    public void OnClick_BtnSound1()
    {
        SoundManager.Instance.PlaySFX("01BtnClick");
    }
    //닫기,x 버튼
    public void OnClick_BtnSound2()
    {
        SoundManager.Instance.PlaySFX("02BtnClick");
    }
    //잘못된 선택,터치x 버튼
    public void OnClick_BtnSound3()
    {
        SoundManager.Instance.PlaySFX("03BtnClick");
    }
    /////////////////////////////////////////////////////////////////////////////
    //사용하지 않는 에셋을 지워준다
    private void OnDestroy()
    {
        Resources.UnloadUnusedAssets();
    }
    //////////////// 랭크게임을 시작하면 기존에있던 데이터를 지운다 //////////////////
    public void OnclickRankStartBtn_Restdata()
    {
        PlayerPrefs.DeleteKey("GameTotalScore");
        PlayerPrefs.DeleteKey("Bowling_Score");
        PlayerPrefs.DeleteKey("Dust_Score");
        PlayerPrefs.DeleteKey("Hotpot_Score");
        PlayerPrefs.DeleteKey("Pirates_Score");
        PlayerPrefs.DeleteKey("Pizza_Score");
        PlayerPrefs.DeleteKey("Sandwiches_Score");
        PlayerPrefs.DeleteKey("Soccer_Score");
        PlayerPrefs.DeleteKey("Submarine_Score");
        PlayerPrefs.DeleteKey("ToyFactory_Score");
        PlayerPrefs.DeleteKey("Zombies_Score");
        HaveChance = true;
    }

    ///////////////////////// 터치 감도 올리기 ////////////////////////////////////
    private const float inchToCm = 0.25f;
    [SerializeField]
    private EventSystem eventSystem = null;
    [SerializeField]
    private float dragThresholdCM = 1f;
    //For drag Threshold
    private void SetDragThreshold()
    {
        if (SceneManager.GetActiveScene().name != "Main")
        {
            if (eventSystem != null)
            {
                eventSystem.pixelDragThreshold = (int)(dragThresholdCM * Screen.dpi / inchToCm);
            }
        }
    }
    /////////////////////////////////////////////////////////////////////////////
}