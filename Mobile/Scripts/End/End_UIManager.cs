using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class End_UIManager : MonoBehaviour
{
    [Header("Stage")]
    public GameScore[] stageNum;
    public int TotalScore = 0;
    [Header("Rank")]
    //public GameObject RankPanel;
    //public GameObject Set;
    //public InputField inputField;
    public GameObject ChancePanel;
    public Text TotalText_chance;
    public Text currentTotalScore;
    [Header("BoboShop")]
    public GameObject boboShopPanel;
    public GameObject TimeSave; 
    public Animator boboMove;

    public GameObject OneDayFreeOb;
    public GameObject OneDayFreeTimeOb;
    public Text OneDayFreeTimeText;

    public GameObject HaveAdOb;
    public GameObject HaveAdTimeOb;
    public Text HaveAdCountText;

    [Header("팝업 UI")]
    public GameObject HaveCoolTimeOb; //쿨타임 시간
    public Text HaveCoolTimeText;
    public GameObject NoDiaOb; //크리스탈 부족
    public GameObject NoFreeAdOb; //무료 광고 횟수 초과
    public GameObject FailCallAdOb; //광고 송출 실패

    //광고 쿨타임
    public GameObject HaveCoolOb;
    private Ray ray;
    private RaycastHit hit;
    public static End_UIManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else Instance = this;
    }
    private void Start()
    {
        if (GameManager.HaveChance.Equals(true))
        {
            ChancePanel.SetActive(true);
            TotalText_chance.text = PlayerPrefs.GetInt("GameTotalScore").ToString();
        }
        else
        {
            FinishGameAd();
            OnClick_SoundOn(); //바로 빵빠레 시작!
        }
        StartCoroutine(_SetScore());
    }
    private void Update()
    {
        //랭크 게임 안, t1애니메이션 +
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000f))
            {
                //보보 클릭시
                if (hit.collider.gameObject.name.Equals("BoBo"))
                {
                    // Debug.Log(hit.collider.gameObject.name);
                    boboMove.SetTrigger("magic");
                    SoundManager.Instance.PlaySFX("BoboMagic");
                }
            }
        }
        if (TimeSave.activeSelf.Equals(true))
        {
            //1일 무료 플레이권 소지시,
            if (GameManager.HaveOneDayFree.Equals(true))
            {
                OneDayFreeOb.SetActive(true);
            }
            else
            {
                OneDayFreeOb.SetActive(false);
            }
            //광고 제거권 소지시
            if (GameManager.HaveAdRemove.Equals(true))
            {
                HaveAdOb.SetActive(true);
            }
            else
            {
                HaveAdOb.SetActive(false);
            }
        }
        //1일 플레이권 남은 시간 표시 
        if (TouchTicket.Equals(true))
        {
            OneDayFreeTimeText.text = GameManager.Instance.FreeTimeHour.ToString()
     + ":" + GameManager.Instance.FreeTimeMin.ToString()
     + ":" + GameManager.Instance.FreeTimeSec.ToString();
        }
        //1일 플레이권 남은 시간 표시 
        if (TouchTicketAd.Equals(true))
        {
            HaveAdCountText.text = GameManager.HaveAdRemoveCount + "/" + 100;
        }
    }
    /////////////////1일 무료 플레이권을 클릭 했을 때 //////////////////////////////////        
    bool TouchTicket = false;
    public void ShowLeftTime()
    {
        GameManager.Instance.OnClick_BtnSound1();
        StartCoroutine(_ShowLeftTime());
    }
    IEnumerator _ShowLeftTime()
    {
        OneDayFreeTimeOb.SetActive(true);
        TouchTicket = true;
        yield return new WaitForSeconds(2.5f);
        OneDayFreeTimeOb.SetActive(false);
        TouchTicket = false;
        yield return null;
    }
    /////////////////100회 무료 광고 권 클릭 했을 때 //////////////////////////////////        
    bool TouchTicketAd = false;
    public void ShowLeftHaveAd()
    {
        GameManager.Instance.OnClick_BtnSound1();
        StartCoroutine(_ShowLeftHaveAd());
    }
    IEnumerator _ShowLeftHaveAd()
    {
        HaveAdTimeOb.SetActive(true);
        TouchTicketAd = true;
        yield return new WaitForSeconds(2.5f);
        HaveAdTimeOb.SetActive(false);
        TouchTicketAd = false;
        yield return null;
    }
    //////////////////////////게임을 플레아한 순서대로 점수를 넣어주고 해당 게임 이름을 넣어줌 //////////////////////////////
    IEnumerator _SetScore()
    {
        for (int i = 0; i < stageNum.Length; i++)
        {
            //   Debug.Log(i);
            //처음 저장된 게임의 순서대로 게임에 대한 정보를 저장함
            if (GameManager.Season2_NextCallNum[i].Equals(0))
            {
                stageNum[i].ScoreNUM = PlayerPrefs.GetFloat("Bowling_Score");
                stageNum[i].GameName = "볼링공을 굴려라";
            }
            else if (GameManager.Season2_NextCallNum[i].Equals(1))
            {
                stageNum[i].ScoreNUM = PlayerPrefs.GetFloat("Dust_Score");
                stageNum[i].GameName = "먼지를 털어라";
            }
            else if (GameManager.Season2_NextCallNum[i].Equals(2))
            {
                stageNum[i].ScoreNUM = PlayerPrefs.GetFloat("Hotpot_Score");
                stageNum[i].GameName = "전골을 끓여라";
            }
            else if (GameManager.Season2_NextCallNum[i].Equals(3))
            {
                stageNum[i].ScoreNUM = PlayerPrefs.GetFloat("Pirates_Score");
                stageNum[i].GameName = "해적선을 맞춰라";
            }
            else if (GameManager.Season2_NextCallNum[i].Equals(4))
            {
                stageNum[i].ScoreNUM = PlayerPrefs.GetFloat("Pizza_Score");
                stageNum[i].GameName = "피자도우를 돌려라";
            }
            else if (GameManager.Season2_NextCallNum[i].Equals(5))
            {
                stageNum[i].ScoreNUM = PlayerPrefs.GetFloat("Sandwiches_Score");
                stageNum[i].GameName = "샌드위치를 만들어라";
            }
            else if (GameManager.Season2_NextCallNum[i].Equals(6))
            {
                stageNum[i].ScoreNUM = PlayerPrefs.GetFloat("Soccer_Score");
                stageNum[i].GameName = "축구공을 넣어라";
            }
            else if (GameManager.Season2_NextCallNum[i].Equals(7))
            {
                stageNum[i].ScoreNUM = PlayerPrefs.GetFloat("Submarine_Score");
                stageNum[i].GameName = "보석을 찾아라";
            }
            else if (GameManager.Season2_NextCallNum[i].Equals(8))
            {
                stageNum[i].ScoreNUM = PlayerPrefs.GetFloat("ToyFactory_Score");
                stageNum[i].GameName = "장난감을 조립하라";
            }
            else if (GameManager.Season2_NextCallNum[i].Equals(9))
            {
                stageNum[i].ScoreNUM = PlayerPrefs.GetFloat("Zombies_Score");
                stageNum[i].GameName = "좀비를 막아라";
            }
        }
        yield return null;
    }
    /////////////////////// UI에서 사용되는 스크립트 //////////////////////////////////////////////// 

    //엔딩 씬에서 다시시작 버튼 클릭 시
    public void OnClickRetry_rank()
    {
        int LeftDia = TimeManager.instance.diamondSu - 2;
        //남아있는 다이아 갯수 확인
        if (LeftDia < 0)
        {
            GameManager.Instance.OnClick_BtnSound3();
            //다이아 부족 판넬 활성화 
            NoDiaOb.SetActive(true);
        }
        else
        {
            GameManager.Instance.OnClick_BtnSound1();
            TimeManager.instance.diamondSu -= 2; //다이아 감소 
            PlayerPrefs.SetInt("Diamond", TimeManager.instance.diamondSu);
            RandStartDataSave(); //랜덤 게임 리스트를 재생성
            GameManager.RandListCountCheck = 0; //게임플레이 횟수를 0으로 초기화
            Time.timeScale = 1;
            AppManager.Instance.gameRunning = true;
            GameManager.HaveChance = true;
            PopUpSystem.PopUpState = false; //팝업이 비활성화 되었다고 체크 해줌
            GameManager.Instance.NextScenes();
            GameManager.Instance.OnclickRankStartBtn_Restdata(); //기존에 있던 데이터를 지움
        }
    }

    public void RandStartDataSave()
    {
        //게임의 갯수 만큼 배열을 생성
        bool isSame; //배열안에 똑같은 숫자가 있는지 확인
        for (int i = 0; i < GameManager.Season2_GameKindCount; ++i)
        {
            while (true)
            {
                //Season2_NextCallNum [0] 부터 배열의 길이 만큼 확인하고 중복 체크
                GameManager.Season2_NextCallNum[i] = Random.Range(0, GameManager.Season2_GameKindCount);
                isSame = false;
                for (int j = 0; j < i; j++)
                {
                    if (GameManager.Season2_NextCallNum[j].Equals(GameManager.Season2_NextCallNum[i]))
                    {
                        isSame = true;
                        break;
                    }
                }
                if (!isSame) break;
            }
        }
    }
    /////////////////////게임진행 다이아 부족 ////////////////////////////////////////
    //엔딩씬에서 상점가기 버튼 클릭했을때
    public void OnClickGoShop()
    {
        GameManager.Instance.OnClick_BtnSound1();
        NoDiaOb.SetActive(false);
        boboShopPanel.SetActive(true);
        SoundManager.Instance.bgm.clip = SoundManager.Instance.GetClipByName("BoboShopBGM");
        SoundManager.Instance.bgm.Play();
        GameManager.ShopPanel = true;
    }
    public void OnClickCloseShop()
    {
        GameManager.Instance.OnClick_BtnSound2();
        boboShopPanel.SetActive(false);
        SoundManager.Instance.bgm.clip = SoundManager.Instance.GetClipByName("EndBGM");
        SoundManager.Instance.bgm.Play();
        GameManager.ShopPanel = false;
    }
    //광고 보기 버튼 클릭
    public void OnClickShowAd()
    {
        //광고 보기 버튼 클릭
        Time.timeScale = 0;
        //남은 광고가 있고 광고 쿨타임이 없을 경우,
        if (GameManager.Instance.FreeADCount > 0
            && GameManager.Instance.AdCool.Equals(false))
        {
            GameManager.Instance.OnClick_BtnSound1();
            AdManager.Instance.ShowRewardAd(); //광고 보기
        }
        // 오늘 볼수 있는 광고를 모두 보았거나 쿨타임이 남았을 경우, 
        else if (GameManager.Instance.FreeADCount <= 0
            || GameManager.Instance.AdCool.Equals(true))
        {
            GameManager.Instance.OnClick_BtnSound3();
            //오늘 볼수있는 광고 횟수를 초과 
            if (GameManager.Instance.FreeADCount <= 0)
            {
                PopUpSystem.Instance.ShowPopUp(NoFreeAdOb);//남은 무료 광고 없음
            }
            else if (GameManager.Instance.AdCool.Equals(true))
            {
                PopUpSystem.Instance.ShowPopUp(HaveCoolOb);
            }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////
    public void NoDiaEnable()
    {
        StopAllCoroutines();
        StartCoroutine(_NoDiaEnable());
    }
    IEnumerator _NoDiaEnable()
    {
        NoDiaOb.SetActive(true);
        yield return new WaitForSeconds(1f);
        NoDiaOb.SetActive(false);
        yield return null;
    }
    /////////////////////찬스타임을 하지 않고 종료할 경우/////////////////////////////////////     
    public void OnClick_SoundOn()
    {
        StartCoroutine(_OnClick_SoundOn());

    }
    IEnumerator _OnClick_SoundOn()
    {
        SoundManager.Instance.audioMixer.SetFloat("volumeBGM", -10f);
        SoundManager.Instance.End_Sfx();
        yield return null;
    }
    ////////////////////////////////////////////////////////////////////////////////////    

    void FinishGameAd()
    {
        if (GameManager.HaveAdRemove.Equals(false))
        {
            AdManager.Instance.ShowScreenAd();
        }
        else
        {
            GameManager.HaveAdRemoveCount -= 1;
            PlayerPrefs.SetInt("RemoveADCount", GameManager.HaveAdRemoveCount);
        }

    }
}
