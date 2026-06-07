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
    public GameObject VideoCanvas;
    public Animator ClassicModAni;
    public Animator RankModAni;

    [Header("클래식 모드")]
    public GameObject ClassicModePanel;

    [Header("랭킹 모드")]
    public GameObject RankingModePanel;
    public GameObject[] T1ImgeChange;

    [Header("[ 게임 이미지 ]")]
    public Image RandomGameImg;
    public Sprite[] ChangeGameImg;
    public GameObject StartButton;
    public static MainUIManager instance { get; private set; }
    private float changeTime = 0.45f;
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
    }

    private Ray ray;
    private RaycastHit hit;
    public void Update()
    {
        ///////////////////////////////////////////////////////////////
        //상황에 따라 메인 판넬 활성화 비활성화
        if (GameManager.ModePanel.Equals(true))
        {
            ModeSelect.SetActive(true);
        }
        //랭킹 모드     
        if (GameManager.RankPanel.Equals(true))
        {
            RankingModePanel.SetActive(true);
            //ClassicModePanel.SetActive(false);
            ModeSelect.SetActive(false);
        }
        else
        {
            RankingModePanel.SetActive(false);
        }
        //클래식 모드    
        if (GameManager.ClassicPanel.Equals(true))
        {
            ClassicModePanel.SetActive(true);
            ModeSelect.SetActive(false);
        }
        else
        {
            ClassicModePanel.SetActive(false);
        }
        if (GameManager.ChangePanel.Equals(true))
        {
            MainAppManager.instance.ChangeGameCavasImg.SetActive(true);
            GameManager.RankPanel = false;
            GameManager.ModePanel = false;
        }
        else
        {
            MainAppManager.instance.ChangeGameCavasImg.SetActive(false);
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
            yield return new WaitForSeconds(0.07f);
            changeTime -= Time.deltaTime * 5f;
            Debug.Log(changeTime);
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

}