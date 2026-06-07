using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PopUpPanelManager : MonoBehaviour
{
    public GameObject PausePanel_Rnak;
    public GameObject PausePanel_Classic;
    public GameObject NoDiaPanel;
    public GameObject WaitPanel;
    public GameObject Rank_CurrentScore;
    private void OnEnable()
    {
        //랭킹모드 일경우,
        if (GameManager.RankMode.Equals(true))
        {
            PausePanel_Rnak.SetActive(true);
            PausePanel_Classic.SetActive(false);
        }
        else
        {
            PausePanel_Rnak.SetActive(false);
            PausePanel_Classic.SetActive(true);
        }
    }

    private void Update()
    {
        //랭킹모드 일경우,
        if (PausePanel_Rnak.activeSelf)
        {
            //처음부터
            if (Input.GetKeyDown(KeyCode.Q))
            {
                OnClickRetry_rank();
            }
            //포기하기 
            else if (Input.GetKeyDown(KeyCode.W))
            {
                OnClickGiveUp_rank();
            }
        }
        else if(PausePanel_Classic.activeSelf)
        {
            //다시하기
            if (Input.GetKeyDown(KeyCode.Q))
            {
                OnRetry_Classic(); 
            }
            //그만하기
            else if (Input.GetKeyDown(KeyCode.W))
            {
                OnClickStop_Classic();
            }
        }
    }
    ///////////////////////////////// 게임도중 팝업 판넬 관련 데이터 ////////////////////////////////////

    //랭크모드에서 다시하기 버튼을 눌러을때, 랜덤게임 재시작
    public void OnClickRetry_rank()
    {
        GameManager.Instance.OnClick_BtnSound1();
                Time.timeScale = 1;
                WaitPanel.SetActive(true);
                RandStartDataSave(); //랜덤 게임 리스트를 재생성
                GameManager.RandListCountCheck = 0; //게임플레이 횟수를 0으로 초기화
            
                AppManager.Instance.gameRunning = true;
                PopUpSystem.PopUpState = false; //팝업이 비활성화 되었다고 체크 해줌
                GameManager.Instance.NextScenes();
                GameManager.Instance.OnclickRankStartBtn_Restdata(); //기존에 있던 데이터를 지움
    }
    //랭크 게임에서 포기하기 버튼을 눌렀을 때, 메인으로 이동하고 게임 플레이 횟수를 0으로 초기화 시켜줌
    public void OnClickGiveUp_rank()
    {
        GameManager.Instance.OnClick_BtnSound1();
        SceneManager.LoadScene("Main");
        GameManager.RankPanel = true;
        GameManager.RankMode = false;
        GameManager.RandListCountCheck = 0;
        PopUpSystem.PopUpState = false; //팝업이 비활성화 되었다고 체크 해줌
        MainAppManager.RandomImgPageEnable = false;
        Time.timeScale = 1;
     
    }
    //랜덤으로 게임 플레이 순서를 저장 
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
           // Debug.Log(GameManager.Season2_NextCallNum[i]);
        }
    }

    ///////////////////////////////////////////클래식 모드 팝업///////////////////////////////////////////////////

    //게임이 끝나고, 클래식 모드로 이동 버튼
    public void OnClickStop_Classic()
    {
        GameManager.Instance.OnClick_BtnSound1();
        SceneManager.LoadScene("Main");
        GameManager.ClassicPanel = true;
        GameManager.RandListCountCheck = 0;
        PopUpSystem.PopUpState = false; //팝업이 비활성화 되었다고 체크 해줌
        MainAppManager.RandomImgPageEnable = false;
  
        Time.timeScale = 1;
    }
    //클래식 모드에서 재도전 버튼 클릭
    public void OnRetry_Classic()
    {     
                // Debug.Log(GameManager.HaveOneDayFree);
                Time.timeScale = 1;
                GameManager.Instance.OnClick_BtnSound1();       
                RetryCheckScene(); //게임 다시 시작 //다이아 감소
                WaitPanel.SetActive(true);
    }
    //재시작 하려는 게임 확인하고 씬 불러오기
    public void RetryCheckScene()
    {
        if (SceneManager.GetActiveScene().name.Equals("MonsterBird"))
        {
            SceneManager.LoadScene("MonsterBird");
            PopUpSystem.PopUpState = false;
            GameManager.RankMode = false;
       
        }
        else if (SceneManager.GetActiveScene().name.Equals("Sandwiches"))
        {

            SceneManager.LoadScene("Sandwiches");
            PopUpSystem.PopUpState = false;
            GameManager.RankMode = false;
           
        }
        else if (SceneManager.GetActiveScene().name.Equals("ToyFactory"))
        {

            SceneManager.LoadScene("ToyFactory");
            PopUpSystem.PopUpState = false;
            GameManager.RankMode = false;
          
        }
        else if (SceneManager.GetActiveScene().name.Equals("Hotpot"))
        {
            SceneManager.LoadScene("Hotpot");
            PopUpSystem.PopUpState = false;
            GameManager.RankMode = false;
           
        }
        else if (SceneManager.GetActiveScene().name.Equals("Statue"))
        {
            SceneManager.LoadScene("Statue");
            PopUpSystem.PopUpState = false;
            GameManager.RankMode = false;
       
        }
        else if (SceneManager.GetActiveScene().name.Equals("Zombies"))
        {
            SceneManager.LoadScene("Zombies");
            PopUpSystem.PopUpState = false;
            GameManager.RankMode = false;
         
        }
        else if (SceneManager.GetActiveScene().name.Equals("Flag"))
        {
            SceneManager.LoadScene("Flag");
            PopUpSystem.PopUpState = false;
            GameManager.RankMode = false;
           
        }
        else if (SceneManager.GetActiveScene().name.Equals("Pipe"))
        {
            SceneManager.LoadScene("Pipe");
            PopUpSystem.PopUpState = false;
            GameManager.RankMode = false;
           
        }
        else if (SceneManager.GetActiveScene().name.Equals("Dust"))
        {
            SceneManager.LoadScene("Dust");
            PopUpSystem.PopUpState = false;
            GameManager.RankMode = false;
           
        }
        else if (SceneManager.GetActiveScene().name.Equals("Pizza"))
        {
            SceneManager.LoadScene("Pizza");
            PopUpSystem.PopUpState = false;
            GameManager.RankMode = false;
           
        }
     
    }
  
    ////////////////////////(랭킹)토탈 점수 보기 클릭 시/////////////////////////////
    public void OnClick_CurrentScore()
    {
        GameManager.Instance.OnClick_BtnSound1();
        Rank_CurrentScore.SetActive(true); //현재 획득한 점수 보여줌 
    }
     public void OnClick_CloseCurrentScore()
    {
        GameManager.Instance.OnClick_BtnSound2();
        Rank_CurrentScore.SetActive(false); //현재 획득한 점수 보여줌 
    }
 
}
