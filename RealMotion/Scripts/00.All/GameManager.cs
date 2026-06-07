
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
    //TimeManager _timeManager;

    //게임이 끝나고 다음 선택지에 따라 판넬을 활성화/비활성화
    public static bool RankMode = false; //랭크 모드인지 클래식모드인지 판단
    public static bool ModePanel = false;
    public static bool ClassicPanel = false;
    public static bool RankPanel = false;
    public static bool ChangePanel = false; 
    //public static bool ShopPanel = false;
    //public static bool HaveChance = true;
    //
    public static int countForAdvertising;
    public int FreeADCount;
    //
    public static bool HaveAdRemove = false; //광고 제거권을 갖고있을 경우
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
     //   _timeManager = FindObjectOfType<TimeManager>();
        SetDragThreshold();

 
    }
    private void Start()
    {
        ChangePanel = false;
    }
    public int FreeTimeDay = 0, FreeTimeHour = 0, FreeTimeMin = 0, FreeTimeSec = 0;
    public int LeftTimeMin = 0, LeftTimeSec = 0;
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
    }

    //게임 스타트 버튼을 눌렀을 떄, 해당 이미지와 같은 씬을 불러봄
    public void SceneCheck()
    {
        //randArray값과 같은 씬을 불러온다
        if (Season2_NextCallNum[RandListCountCheck].Equals(0))
        {
            SceneManager.LoadScene("MonsterBird");
        }
        else if (Season2_NextCallNum[RandListCountCheck].Equals(1))
        {
            SceneManager.LoadScene("Sandwiches");
        }
        else if (Season2_NextCallNum[RandListCountCheck].Equals(2))
        {
            SceneManager.LoadScene("ToyFactory");
        }
        else if (Season2_NextCallNum[RandListCountCheck].Equals(3))
        {
            SceneManager.LoadScene("Hotpot");
        }
        else if (Season2_NextCallNum[RandListCountCheck].Equals(4))
        {
            SceneManager.LoadScene("Statue");
        }
        else if (Season2_NextCallNum[RandListCountCheck].Equals(5))
        {
            SceneManager.LoadScene("Zombies");
        }
        else if (Season2_NextCallNum[RandListCountCheck].Equals(6))
        {
            SceneManager.LoadScene("Flag");
        }
        else if (Season2_NextCallNum[RandListCountCheck].Equals(7))
        {
            SceneManager.LoadScene("Pipe");
        }
        else if (Season2_NextCallNum[RandListCountCheck].Equals(8))
        {
            SceneManager.LoadScene("Dust");
        }
        else if (Season2_NextCallNum[RandListCountCheck].Equals(9))
        {
            SceneManager.LoadScene("Pizza");
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
        ServerManager.Instance.isConnCompleted = false;
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
        SceneManager.LoadScene("Main");
        RandListCountCheck = 0;
        PopUpSystem.PopUpState = false;
        RankMode = false;
        MainAppManager.RandomImgPageEnable = false;
        Time.timeScale = 1;
    }

    IEnumerator _OnClick_Home_Button()
    {
        // 네트워크 연결상태 확인하기
        ServerManager.Instance.GetNetworkState();

        yield return null;
    }
    //home 버튼을 클릭하면 메인화면으로 이동한다. 
    public void OnClick_Home_Button_rank()
    {
        OnClick_BtnSound1();
        StartCoroutine(_OnClick_Home_Button_rank());
    }
    IEnumerator _OnClick_Home_Button_rank()
    {
        // 네트워크 연결상태 확인하기
        ServerManager.Instance.GetNetworkState();
        yield return new WaitUntil(() => ServerManager.Instance.isConnCompleted);
        ServerManager.Instance.isConnCompleted = false;

        // 확인 후 메인에서 1~5위까지 데이터 가져오기
        ServerManager.Instance.RankingSearch(5);
        yield return new WaitUntil(() => ServerManager.Instance.isHighListStackUp);
        ServerManager.Instance.isHighListStackUp = false;

        SceneManager.LoadScene("Main");
        RandListCountCheck = 0;
        PopUpSystem.PopUpState = false;
        RankMode = false;
        RankPanel = true;
        MainAppManager.RandomImgPageEnable = false;
        Time.timeScale = 1;

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
        PlayerPrefs.DeleteKey("MonsterBird_Score");
        PlayerPrefs.DeleteKey("Dust_Score");
        PlayerPrefs.DeleteKey("Hotpot_Score");
        PlayerPrefs.DeleteKey("Statue_Score");
        PlayerPrefs.DeleteKey("Pizza_Score");
        PlayerPrefs.DeleteKey("Sandwiches_Score");
        PlayerPrefs.DeleteKey("Flag_Score");
        PlayerPrefs.DeleteKey("Pipe_Score");
        PlayerPrefs.DeleteKey("ToyFactory_Score");
        PlayerPrefs.DeleteKey("Zombies_Score");
        // PlayerPrefs.DeleteKey("TodayAD");
        // PlayerPrefs.DeleteKey("ADCoolTime");
    
    }

    /////////////////////////// 터치 감도 올리기 ////////////////////////////////////
    private const float inchToCm = 0.25f;
    [SerializeField]
    private EventSystem eventSystem = null;
    [SerializeField]
    private float dragThresholdCM = 1f;
    //For drag Threshold
    private void SetDragThreshold()
    {
        if (eventSystem != null)
        {
            eventSystem.pixelDragThreshold = (int)(dragThresholdCM * Screen.dpi / inchToCm);
        }
    }
    //현재 오브젝트의 텍스트를 받아서 해당 씬을 불러온다. 
    public void LoadThisNameScene(GameObject ob)
    {
       string name = ob.gameObject.name;
        Debug.Log(name);
        SceneManager.LoadScene(name); 
    }
    ///////////////////////////////////////////////////////////////////////////////
}