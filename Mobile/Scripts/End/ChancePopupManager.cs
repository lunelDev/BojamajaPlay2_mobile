using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChancePopupManager : MonoBehaviour
{
    public ChanceScore[] stageNum;
    public GameObject PlzChooseOb; 
    public static int checkState = 0; //체크 상태 0: 체크 안되있음 1: 체크 되어있음
    public static string CheckGameName;
    public static float CheckGameScore = 0;
    private void OnEnable()
    {
        StartCoroutine(_ChanceSetScore());
    }
    // 현재 획득한 점수를 넣어줌
    IEnumerator _ChanceSetScore()
    {
        for (int i = 0; i < stageNum.Length; i++)
        {
            //처음 저장된 게임의 순서대로 게임에 대한 정보를 저장함
            if (GameManager.Season2_NextCallNum[i].Equals(0))
            {
                stageNum[i].ScoreNUM = PlayerPrefs.GetFloat("Bowling_Score");
                stageNum[i].GameName = "볼링공을 굴려라";
                stageNum[i].GameSceneName = "Bowling";
            }
            else if (GameManager.Season2_NextCallNum[i].Equals(1))
            {
                stageNum[i].ScoreNUM = PlayerPrefs.GetFloat("Dust_Score");
                stageNum[i].GameName = "먼지를 털어라";
                stageNum[i].GameSceneName = "Dust";
            }
            else if(GameManager.Season2_NextCallNum[i].Equals(2))
            {

                stageNum[i].ScoreNUM = PlayerPrefs.GetFloat("Hotpot_Score");
                stageNum[i].GameName = "전골을 끓여라";
                stageNum[i].GameSceneName = "Hotpot";

            }
            else if(GameManager.Season2_NextCallNum[i].Equals(3))
            {
                stageNum[i].ScoreNUM = PlayerPrefs.GetFloat("Pirates_Score");
                stageNum[i].GameName = "해적선을 맞춰라";
                stageNum[i].GameSceneName = "Pirates";
            }
            else if(GameManager.Season2_NextCallNum[i].Equals(4))
            {
                stageNum[i].ScoreNUM = PlayerPrefs.GetFloat("Pizza_Score");
                stageNum[i].GameName = "피자도우를 돌려라";
                stageNum[i].GameSceneName = "Pizza";
            }
            else if(GameManager.Season2_NextCallNum[i].Equals(5))
            {
                stageNum[i].ScoreNUM = PlayerPrefs.GetFloat("Sandwiches_Score");
                stageNum[i].GameName = "샌드위치를 만들어라";
                stageNum[i].GameSceneName = "Sandwiches";
            }
            else if(GameManager.Season2_NextCallNum[i].Equals(6))
            {
                stageNum[i].ScoreNUM = PlayerPrefs.GetFloat("Soccer_Score");
                stageNum[i].GameName = "축구공을 넣어라";
                stageNum[i].GameSceneName = "Soccer";
            }
            else if(GameManager.Season2_NextCallNum[i].Equals(7))
            {
                stageNum[i].ScoreNUM = PlayerPrefs.GetFloat("Submarine_Score");
                stageNum[i].GameName = "보석을 찾아라";
                stageNum[i].GameSceneName = "Submarine";
            }
            else if(GameManager.Season2_NextCallNum[i].Equals(8))
            {
                stageNum[i].ScoreNUM = PlayerPrefs.GetFloat("ToyFactory_Score");
                stageNum[i].GameName = "장난감을 조립하라";
                stageNum[i].GameSceneName = "ToyFactory";
            }
            else if(GameManager.Season2_NextCallNum[i].Equals(9))
            {
                stageNum[i].ScoreNUM = PlayerPrefs.GetFloat("Zombies_Score");
                stageNum[i].GameName = "좀비를 막아라";
                stageNum[i].GameSceneName = "Zombies";
            }
        }
        yield return null;
    }

    // 토글상태 체크
    private bool CheckisOnToggle()
    {
        bool check = false;

        for (int i = 0; i < stageNum.Length; i++)
        {
            if (stageNum[i].tgGameSelect.isOn)  // 전체 스테이지를 돌려서 토글이 선택된 것이 있는지 체크
            {
                check = true;
            }
        }
      
        return check;
    }

    //1.5배 찬스 타임!
    public void ChanceTime()
    {
        //다시 플레이할 게임을 선택했을 때,
        if (CheckisOnToggle())
        {
            int LeftDia = TimeManager.instance.diamondSu - 2;
            //남은 다이아 부족
            if (LeftDia < 0)
            {
                GameManager.Instance.OnClick_BtnSound3();
                End_UIManager.Instance.NoDiaOb.SetActive(true);
                //보보상점 활성화 
            }
            else
            {
                GameManager.Instance.OnClick_BtnSound1();
                int score = PlayerPrefs.GetInt("GameTotalScore");
                TimeManager.instance.diamondSu -= 2;
                PlayerPrefs.SetInt("Diamond", TimeManager.instance.diamondSu);
                PlayerPrefs.SetInt("GameTotalScore", (int)(score - CheckGameScore));
                SceneManager.LoadScene(CheckGameName); //체크된 이름의 씬 호출
                GameManager.HaveChance = false;
                GameManager.LastGameCheck = true;
                PopUpSystem.PopUpState = false;
                Time.timeScale = 1;  
               // Debug.Log("score : " + score);
                //Debug.Log("current score : " + PlayerPrefs.GetFloat("GameTotalScore"));
            }
        }
        //게임을 선택 하지 않았을 때,
        else
        {
            GameManager.Instance.OnClick_BtnSound3();
            //게임을 선택하지 않았다는 팝업 띄우기
            StartCoroutine(_PlzChooseGame());       
        }
        
    }
    public void Togle_IsCheck()
    {
        if (CheckisOnToggle())
        {
            GameManager.Instance.OnClick_BtnSound1();
        }
        else
        {
            GameManager.Instance.OnClick_BtnSound2();
        }
    }
    IEnumerator _PlzChooseGame()
    {
        PlzChooseOb.SetActive(true);
        yield return new WaitForSeconds(0.7f);
        PlzChooseOb.SetActive(false);
        yield return null; 

    }
  
}
