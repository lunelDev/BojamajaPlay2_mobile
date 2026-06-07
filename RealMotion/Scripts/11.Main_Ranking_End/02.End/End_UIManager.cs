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
    public GameObject RankPanel;
    public GameObject Set;
    public InputField inputField;
    public Text currentTotalScore;
    public bool Ranking_registration = false; 
  //S  public GameObject ChancePanel;
  //  [Header("BoboShop")]
  //  public GameObject boboShopPanel;
  //  public Animator boboMove;

   /* [Header("팝업 UI")]
    public GameObject HaveCoolTimeOb; //쿨타임 시간
    public Text HaveCoolTimeText;
    public GameObject NoDiaOb; //크리스탈 부족
    public GameObject NoFreeAdOb; //무료 광고 횟수 초과
    public GameObject FailCallAdOb; //광고 송출 실패
   */
    //광고 쿨타임
   // public GameObject HaveCoolOb;
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
        Ranking_registration = true;
        OnClick_SoundOn(); //바로 빵빠레 시작!
        StartCoroutine(_SetScore());
    }

    IEnumerator _SetScore()
    {
        for (int i = 0; i < stageNum.Length; i++)
        {
            //   Debug.Log(i);
            //처음 저장된 게임의 순서대로 게임에 대한 정보를 저장함
            //처음 저장된 게임의 순서대로 게임에 대한 정보를 저장함
            if (GameManager.Season2_NextCallNum[i].Equals(0))
            {
                stageNum[i].ScoreNUM = PlayerPrefs.GetInt("MonsterBird_Score");
                stageNum[i].GameName = "괴물새를 쫓아라";

            }
            else if (GameManager.Season2_NextCallNum[i].Equals(1))
            {
                stageNum[i].ScoreNUM = PlayerPrefs.GetInt("Sandwiches_Score");
                stageNum[i].GameName = "샌드위치를 완성하라";

            }
            else if (GameManager.Season2_NextCallNum[i].Equals(2))
            {

                stageNum[i].ScoreNUM = PlayerPrefs.GetInt("ToyFactory_Score");
                stageNum[i].GameName = "장난감을 완성하라";

            }
            else if (GameManager.Season2_NextCallNum[i].Equals(3))
            {
                stageNum[i].ScoreNUM = PlayerPrefs.GetInt("Hotpot_Score");
                stageNum[i].GameName = "전골을 끓여라";

            }
            else if (GameManager.Season2_NextCallNum[i].Equals(4))
            {
                stageNum[i].ScoreNUM = PlayerPrefs.GetInt("Statue_Score");
                stageNum[i].GameName = "조각상을 만들어라";

            }
            else if (GameManager.Season2_NextCallNum[i].Equals(5))
            {
                stageNum[i].ScoreNUM = PlayerPrefs.GetInt("Zombies_Score");
                stageNum[i].GameName = "좀비를 막아라";

            }
            else if (GameManager.Season2_NextCallNum[i].Equals(6))
            {
                stageNum[i].ScoreNUM = PlayerPrefs.GetInt("Flag_Score");
                stageNum[i].GameName = "청기백기를 올려라";
            }
            else if (GameManager.Season2_NextCallNum[i].Equals(7))
            {
                stageNum[i].ScoreNUM = PlayerPrefs.GetInt("Pipe_Score");
                stageNum[i].GameName = "밸브를 잠가라";

            }
            else if (GameManager.Season2_NextCallNum[i].Equals(8))
            {
                stageNum[i].ScoreNUM = PlayerPrefs.GetInt("Dust_Score");
                stageNum[i].GameName = "먼지를 털어라";

            }
            else if (GameManager.Season2_NextCallNum[i].Equals(9))
            {
                stageNum[i].ScoreNUM = PlayerPrefs.GetInt("Pizza_Score");
                stageNum[i].GameName = "피자도우를 돌려라";

            }
        }
        yield return null;
    }
    /////////////////////// UI에서 사용되는 스크립트 //////////////////////////////////////////////// 

    //엔딩 씬에서 다시시작 버튼 클릭 시
    public void OnClickRetry_rank()
    {
      GameManager.Instance.OnClick_BtnSound1();
            RandStartDataSave(); //랜덤 게임 리스트를 재생성
            GameManager.RandListCountCheck = 0; //게임플레이 횟수를 0으로 초기화
            Time.timeScale = 1;
            AppManager.Instance.gameRunning = true;
 
            PopUpSystem.PopUpState = false; //팝업이 비활성화 되었다고 체크 해줌
            GameManager.Instance.NextScenes();
            GameManager.Instance.OnclickRankStartBtn_Restdata(); //기존에 있던 데이터를 지움
        
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
}

