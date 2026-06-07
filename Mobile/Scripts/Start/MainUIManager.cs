using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class MainUIManager : MonoBehaviour/** IDragHandler, IPointerDownHandler*/

{
    public MainAppManager _mainAppManager;
    [Header("[ 메인 UI ]")]
    public GameObject MainUI;
    public GameObject ModeSelect;
    public GameObject TimeSave;
    public GameObject VideoCanvas;
    public Animator ClassicModAni;
    public Animator RankModAni;

    public GameObject OneDayFreeOb;
    public GameObject OneDayFreeTimeOb;
    public Text OneDayFreeTimeText;

    public GameObject   HaveAdOb;
    public GameObject HaveAdTimeOb;
    public Text HaveAdCountText;

    [Header("클래식 모드")]
    public GameObject ClassicModePanel;

    [Header("랭킹 모드")]
    public GameObject RankingModePanel;
    public GameObject[] T1ImgeChange;

    [Header("[ 게임 이미지 ]")]
    public Image RandomGameImg;
    public Sprite[] ChangeGameImg;
    public GameObject StartButton;

    [Header("[ 보보상점 ]")]
    public GameObject BoboShop;
    public Animator Bobo;
   // public GameObject noDiaPanel;
    [Header("팝업 UI")]
    public GameObject HaveCoolTimeOb; //쿨타임 시간
    public Text HaveCoolTimeText; 
    public GameObject NoDiaOb; //크리스탈 부족
    public GameObject NoFreeAdOb; //무료 광고 횟수 초과
    public GameObject FailCallAdOb; //광고 송출 실패
    public GameObject eventPopup; // 이벤트 팝업
    public Toggle eventToggle;
    public static MainUIManager instance { get; private set; }
    private float changeTime = 1;
    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;
    }
    public void Start()
    {
        StartButton.SetActive(false);

        //PlayerPrefs.DeleteKey("Today");
        //PlayerPrefs.DeleteKey("Check");

        if (!PlayerPrefs.HasKey("Today"))
        {
            string todayDt = System.DateTime.Now.ToString("dd");
            // 오늘 날짜가 없다면 등록하고 처음에 띄워줌
            PlayerPrefs.SetInt("Check", 0);
            PlayerPrefs.SetInt("Today", int.Parse(todayDt));
            eventPopup.SetActive(true);
        }
        else
        {
            //public static bool ModePanel = false;
            //public static bool ClassicPanel = false;
            //public static bool RankPanel = false;
            //public static bool ShopPanel = false;
            if(GameManager.ModePanel.Equals(false) && GameManager.ClassicPanel.Equals(false)
                && GameManager.RankPanel.Equals(false) && GameManager.ShopPanel.Equals(false))
            {
                string todayDt = System.DateTime.Now.ToString("dd");
                // 날짜가 있다면 오늘인지 아닌지 체크
                if (PlayerPrefs.GetInt("Today") == int.Parse(todayDt))
                {
                    // 오늘
                    if (PlayerPrefs.GetInt("Check") == 1)
                    {
                        // 체크함 : 안띄움
                        eventPopup.SetActive(false);
                    }
                    else
                    {
                        // 체크 안함 : 띄움
                        eventPopup.SetActive(true);
                    }
                }
                else
                {
                    // 오늘 아님 > 무조건 띄움, 날짜 저장
                    PlayerPrefs.SetInt("Today", int.Parse(todayDt));
                    PlayerPrefs.SetInt("Check", 0);
                    eventPopup.SetActive(true);
                }
            }

        }
    }

    private Ray ray;
    private RaycastHit hit;
    public void Update()
    {
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
        //랭크 게임 안, t1애니메이션 +
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000f))
            {
                if (PopUpSystem.PopUpState.Equals(false))
                {
                    if (hit.collider.gameObject.name.Equals("T1"))
                    {
                        AniNum += 1;
                        ClcikT1_PlayAni();
                    }
                    //"여기를 터치해주숑" 클릭시 
                    if (hit.collider.gameObject.name.Equals("TextSecImg"))
                    {
                        if (eventPopup.activeSelf)
                        {
                            // 이벤트 팝업이 활성화 되어있다면
                        }
                        else
                        {
                            GameManager.ModePanel = true;
                            ModeSelect.SetActive(true);
                            TimeSave.SetActive(true);
                            VideoCanvas.SetActive(false);
                        }
                    }
                    if (hit.collider.gameObject.name.Equals("BoBo"))
                    {
                        Debug.Log(hit.collider.gameObject.name);
                        Bobo.SetTrigger("magic");
                        SoundManager.Instance.PlaySFX("BoboMagic");

                    }
                }

            }
        }
        ///////////////////////////////////////////////////////////////
        //상황에 따라 메인 판넬 활성화 비활성화
        if (GameManager.ModePanel.Equals(true))
        {
            ModeSelect.SetActive(true);
            TimeSave.SetActive(true);

            //ClassicModePanel.SetActive(false);
            //RankingModePanel.SetActive(false);
           // BoboShop.SetActive(false);
            //SoundManager.Instance.PlayBGM();
        }

        if (GameManager.RankPanel.Equals(true))
        {
            //if (ServerManager.Instance.RankingEventDoing)
            //{
            //    RankingModePanel_event.SetActive(true);
            //    RankingModePanel.SetActive(false);
            //}
            //else
            //{
            //    RankingModePanel_event.SetActive(false);
            //    RankingModePanel.SetActive(true);
            //}

            RankingModePanel.SetActive(true);
            //ClassicModePanel.SetActive(false);
            ModeSelect.SetActive(false);
            TimeSave.SetActive(true);
            //BoboShop.SetActive(false);
        
            //SoundManager.Instance.StopBGM();
        }

        if (GameManager.ClassicPanel.Equals(true))
        {
            ClassicModePanel.SetActive(true);
            ModeSelect.SetActive(false);
            TimeSave.SetActive(true);
            //RankingModePanel.SetActive(false);
            //BoboShop.SetActive(false);
            //SoundManager.Instance.StopBGM();
        }
        //////////////////////////////////////////////////////////////
        if (GameManager.ShopPanel.Equals(true))
        {
            BoboShop.SetActive(true);
            ModeSelect.SetActive(false);
            ClassicModePanel.SetActive(false);
            RankingModePanel.SetActive(false);
            TimeSave.SetActive(true);
            //SoundManager.Instance.StopBGM();
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
            HaveAdCountText.text = GameManager.HaveAdRemoveCount+ "/"+100;
        }
    }

    public void EventCheckBoxState()
    {
        if (eventToggle.isOn)
        {
            string todayDt = System.DateTime.Now.ToString("dd");
            
            PlayerPrefs.SetInt("Check", 1);
            PlayerPrefs.SetInt("Today", int.Parse(todayDt));
        }
        else
        {
            PlayerPrefs.SetInt("Check", 0);
        }
    }

    void soundChange()
    {
        //랭킹
        if (GameManager.RankPanel.Equals(true))
        {
            SoundManager.Instance.bgm.clip = SoundManager.Instance.GetClipByName("RankingModeBGM");
            SoundManager.Instance.bgm.Play();
        }
        //클래식
        if (GameManager.ClassicPanel.Equals(true))
        {
            SoundManager.Instance.bgm.clip = SoundManager.Instance.GetClipByName("ClassicModeBGM");
            SoundManager.Instance.bgm.Play();
        }
        //상점
        if (GameManager.ShopPanel.Equals(true))
        {
            SoundManager.Instance.bgm.clip = SoundManager.Instance.GetClipByName("BoboShopBGM");
            SoundManager.Instance.bgm.Play();
        }
    }
      
////////////////// 랜덤 이미지 변환 /////////////////////
public void RandGameImge()
    {
        StartCoroutine(RandGameImge_());
    }
    IEnumerator RandGameImge_()
    {
        while (changeTime > 0)
        {
            //SoundManager.Instance.StopBGM();
            SoundManager.Instance.PlaySFX("changeScreen");
            int num = Random.Range(0, ChangeGameImg.Length);
            RandomGameImg.sprite = ChangeGameImg[num];
            yield return new WaitForSeconds(0.09f);
            changeTime -= Time.deltaTime * 3f;
            if (changeTime < 0.2)
            {
                yield return new WaitForSeconds(0.12f);
            }
            yield return null;
        }
        StartButton.SetActive(true);
        RandomGameImg.sprite = ChangeGameImg[GameManager.Season2_NextCallNum[0]];

        yield return null;
    }
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
    public void ShowHaveFreeAd()
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
    //////////////////// T1 애니메이션 ////////////////////////////////////

    int AniNum = 0;// 애니메이션 번호
    public void ClcikT1_PlayAni()
    {
       // Debug.Log(AniNum);
        //기본 0
        if (AniNum.Equals(0))
        {
            T1ImgeChange[0].SetActive(true);
            T1ImgeChange[1].SetActive(false);
            T1ImgeChange[2].SetActive(false);
        }
        //도발 1
        else if (AniNum.Equals(1))
        {
            T1ImgeChange[1].SetActive(true);
            T1ImgeChange[0].SetActive(false);
            T1ImgeChange[2].SetActive(false);
        }
        //탐색 2
        else if (AniNum.Equals(2))
        {
            T1ImgeChange[2].SetActive(true);
            T1ImgeChange[1].SetActive(false);
            T1ImgeChange[0].SetActive(false);
        }
        else if (AniNum.Equals(3))
        {
            T1ImgeChange[0].SetActive(true);
            T1ImgeChange[2].SetActive(false);
            T1ImgeChange[1].SetActive(false);
            AniNum = 0;
        }
    }
    public void OnClick_GoShop()
    {
        NoDiaOb.SetActive(false);
        BoboShop.SetActive(true);
        ClassicModePanel.SetActive(false);
        RankingModePanel.SetActive(false);
        ModeSelect.SetActive(false);
        GameManager.ClassicPanel = false;
        GameManager.RankPanel = false;
        GameManager.ModePanel = false;
        GameManager.ShopPanel = true;
        SoundManager.Instance.bgm.clip = SoundManager.Instance.GetClipByName("BoboShopBGM");
        SoundManager.Instance.bgm.Play();


    }

}