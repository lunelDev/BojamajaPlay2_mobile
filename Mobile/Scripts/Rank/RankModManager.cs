using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankModManager : MonoBehaviour
{
    public Text TotalScore;
    public GameObject[] GamePanel; //텍스트 그룹

    public Text[] GameName;
    public Text[] GameNum;

    int currentNum;
    string currentNameStr;
    float currentNameScore;

    private void OnEnable()
    { 
      
        for(int i =0; i<GameManager.RandListCountCheck; i++)
        {
            GamePanel[i].SetActive(true); //게임을 플레이한 갯수만큼 활성화
            currentNum = GameManager.Season2_NextCallNum[i];
            SetName();
            GameName[i].text = currentNameStr;
            GameNum[i].text = currentNameScore.ToString()+" p";
        }
        //합계 점수
        TotalScore.text = PlayerPrefs.GetInt("GameTotalScore").ToString();
    }
    void SetName()
    {
        //볼링
        if (currentNum.Equals(0))
        {
            currentNameStr = "볼링공을 굴려라";
            currentNameScore = PlayerPrefs.GetFloat("Bowling_Score");
        }
        //먼지
        else if(currentNum.Equals(1))
        {
            currentNameStr = "먼지를 털어라";
            currentNameScore = PlayerPrefs.GetFloat("Dust_Score");
        }
        //전골
        else if(currentNum.Equals(2))
        {
            currentNameStr = "전골을 끓여라";
            currentNameScore = PlayerPrefs.GetFloat("Hotpot_Score");
        }
        //해적선
        else if(currentNum.Equals(3))
        {
            currentNameStr = "해적선을 맞춰라";
            currentNameScore = PlayerPrefs.GetFloat("Pirates_Score");
        }
        //피자
        else if(currentNum.Equals(4))
        {
            currentNameStr = "피자도우를 돌려라";
            currentNameScore = PlayerPrefs.GetFloat("Pizza_Score");

        }
        //샌드위치
        else if(currentNum.Equals(5))
        {
            currentNameStr = "샌드위치를 만들어라";
            currentNameScore = PlayerPrefs.GetFloat("Sandwiches_Score");
        }
        //축구
        else if(currentNum.Equals(6))
        {
            currentNameStr = "축구공을 넣어라";
            currentNameScore = PlayerPrefs.GetFloat("Soccer_Score");
        }
        //보석
        else  if (currentNum.Equals(7))
        {
            currentNameStr = "보석을 찾아라";
            currentNameScore = PlayerPrefs.GetFloat("Submarine_Score");
        }
        //장난감
        else if(currentNum.Equals(8))
        {
            currentNameStr = "장난감을 조립하라";
            currentNameScore = PlayerPrefs.GetFloat("ToyFactory_Score");
        }
        //좀비
        else if(currentNum.Equals(9))
        {
            currentNameStr = "좀비를 막아라";
            currentNameScore = PlayerPrefs.GetFloat("Zombies_Score");
        }
    }
}
