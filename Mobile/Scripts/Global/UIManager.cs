using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    // Main panels
    public GameObject panel_Intro;
    public GameObject StartButton;
    public GameObject panel_NextNumber; //다음게임시작 
    // UI
    public GameObject uiPanel;
/*
    [Header("NewCanvas")]
   
    public GameObject PopupPanel;*/
    [Header("클래식")]
    public GameObject GameEndPanel_Classic;
    public Text getScore_clas;
    public Text gameName_clas;
    public GameObject Classic_ScoreUI; 
    //버튼
    public GameObject chooseBtn_clas; 
    public GameObject chooseModeBtn_clas; //게임 선택 btn
    public GameObject retryBtn_clas; //다시하기 btn
    [Header("랭크")]
    public GameObject GameEndPanel_Rank;
    public Text gameName_rank;
    public GameObject Rank_ScoreUI;
    //버튼
    public GameObject nextGameBtn_rank; //다음게임 시작 btn
    public GameObject showScoreBtn_rank;//토탈점수 보기 btn
    public GameObject giveUpBtn_rank; //포기하기 btn
    public GameObject goResult_rank;

    //public GameObject MapRemoveOb;
    //광고
    public GameObject FailCallAd;
    public GameObject NoFreeAD;
    //광고 쿨타임
    public GameObject HaveCoolOb;

    public bool RoundEnd_openPanel = false; //승/패 판넬 열릴 때 
    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else Instance = this;

        //랭킹모드에서는 별점수가 필요없음, 별 UI 제거
        if (GameManager.RankMode.Equals(true))
        {
            Rank_ScoreUI.SetActive(true);
            Classic_ScoreUI.SetActive(false);
        }
        else
        {
            Classic_ScoreUI.SetActive(true);
            Rank_ScoreUI.SetActive(false);
        }
    }
 
    //게임 시작 화면 초기화 
    public void Reset()
    {
        panel_NextNumber.SetActive(false);
        panel_Intro.SetActive(true); // 시작 판넬 활성화
        RoundEnd_openPanel = false;
    }
  
    //게임 시작
    public IEnumerator OnRoundStart()
    {
        uiPanel.SetActive(true); // UI bar
        panel_Intro.SetActive(false);
   //     panel_NextNumber.SetActive(false);
        yield return null;
    }
    //게임 라운드 끝나고 
    public IEnumerator OnRoundEnd()
    {
        //랭크 모드 일 경우,
        if (GameManager.RankMode.Equals(true))
        {
            GameEndPanel_Rank.SetActive(true); //랭크 모드 팝업 활성화
            //마지막 게임일 경우
            if (GameManager.LastGameCheck.Equals(true))
            {
                LastScene();
            }
        
        }
        //클래식 모드 일 경우,
        if (GameManager.RankMode.Equals(false))
        {
            GameEndPanel_Classic.SetActive(true); //클래식 모드 팝업 활성화 
        }
        SceneName();
        
        yield return null;
    }
    //마지막 게임일 경우, 최종점수 보기 버튼 활성화
    public void LastScene()
    {
        nextGameBtn_rank.SetActive(false); //다음게임 시작 btn
        showScoreBtn_rank.SetActive(false);//토탈점수 보기 btn
        giveUpBtn_rank.SetActive(false); //포기하기 btn
        goResult_rank.SetActive(true);
    }
    //라운드 끝나고 선택 모드에 따라 게임이름과 점수를 적어줌
    public void SceneName()
    {
        //볼링
        if (SceneManager.GetActiveScene().buildIndex.Equals(1))
        {
            if (GameManager.RankMode.Equals(true))
            {
                gameName_rank.text = "볼링공을 굴려라";           
            }
            else
            {
                gameName_clas.text = "볼링공을 굴려라";
                getScore_clas.text = DataManager.Instance.scoreManager.score.ToString();
            }
        }
        //먼지
        else if (SceneManager.GetActiveScene().buildIndex.Equals(2))
        {
            if (GameManager.RankMode.Equals(true))
            {
                gameName_rank.text = "먼지를 털어라";            
            }
            else
            {
                gameName_clas.text = "먼지를 털어라";
                getScore_clas.text = DataManager.Instance.scoreManager.score.ToString();
            }
        }
        //전골
        else  if (SceneManager.GetActiveScene().buildIndex.Equals(3))
        {
            if (GameManager.RankMode.Equals(true))
            {
                gameName_rank.text = "전골을 끓여라";            
            }
            else
            {
                gameName_clas.text = "전골을 끓여라";
                getScore_clas.text = DataManager.Instance.scoreManager.score.ToString();
            }
        }
        //해적선
        else if (SceneManager.GetActiveScene().buildIndex.Equals(4))
        {
            if (GameManager.RankMode.Equals(true))
            {
                gameName_rank.text = "해적선을 맞춰라";              
            }
            else
            {
                gameName_clas.text = "해적선을 맞춰라";
                getScore_clas.text = DataManager.Instance.scoreManager.score.ToString();
            }
        }
        //피자
        else if (SceneManager.GetActiveScene().buildIndex.Equals(5))
        {
            if (GameManager.RankMode.Equals(true))
            {
                gameName_rank.text = "피자도우를 돌려라";   
            }
            else
            {
                gameName_clas.text = "피자도우를 돌려라";
                getScore_clas.text = DataManager.Instance.scoreManager.score.ToString();
            }
        }
        //샌드위치
        else if (SceneManager.GetActiveScene().buildIndex.Equals(6))
        {
            if (GameManager.RankMode.Equals(true))
            {
                gameName_rank.text = "샌드위치를 만들어라";          
            }
            else
            {
                gameName_clas.text = "샌드위치를 만들어라";
                getScore_clas.text = DataManager.Instance.scoreManager.score.ToString();
            }
        }
        //축구
        else if (SceneManager.GetActiveScene().buildIndex.Equals(7))
        {
            if (GameManager.RankMode.Equals(true))
            {
                gameName_rank.text = "축구공을 넣어라";             
            }
            else
            {
                gameName_clas.text = "축구공을 넣어라";
                getScore_clas.text = DataManager.Instance.scoreManager.score.ToString();
            }
        }
        //보석
        else if (SceneManager.GetActiveScene().buildIndex.Equals(8))
        {
            if (GameManager.RankMode.Equals(true))
            {
                gameName_rank.text = "보석을 찾아라";        
            }
            else
            {
                gameName_clas.text = "보석을 찾아라";
                getScore_clas.text = DataManager.Instance.scoreManager.score.ToString();
            }

        }
        //장난감
        else if (SceneManager.GetActiveScene().buildIndex.Equals(9))
        {
            if (GameManager.RankMode.Equals(true))
            {
                gameName_rank.text = "장난감을 조립하라";          
            }
            else
            {
                gameName_clas.text = "장난감을 조립하라";
                getScore_clas.text = DataManager.Instance.scoreManager.score.ToString();
            }
        }
        //좀비
        else if (SceneManager.GetActiveScene().buildIndex.Equals(10))
        {
            if (GameManager.RankMode.Equals(true))
            {
                gameName_rank.text = "좀비를 막아라";        
            }
            else
            {
                gameName_clas.text = "좀비를 막아라";
                getScore_clas.text = DataManager.Instance.scoreManager.score.ToString();
            }
        }
    }
    //클래식 게임 끝났을 때 선택지 _게임선택
   public void OnClick_choseGame_classic()
    {
        SceneManager.LoadScene("Main");
        GameManager.Instance.OnClick_BtnSound1();
        Time.timeScale = 1;
        GameManager.ClassicPanel = true;       
    }
    //닫기 버튼 클릭시 
    public void OncClick_closeBtn(GameObject ob)
    {
        GameManager.Instance.OnClick_BtnSound2();
        ob.SetActive(false);
    }
    //InGame에서 사용
    public void ShowAD()
    {
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
                PopUpSystem.Instance.ShowPopUp(NoFreeAD);//남은 무료 광고 없음
            }
           else if (GameManager.Instance.AdCool.Equals(true)){
                PopUpSystem.Instance.ShowPopUp(HaveCoolOb);
            }
        }
    }

}


