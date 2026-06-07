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
            currentNameStr = "괴물새를 쫓아라";
            currentNameScore = PlayerPrefs.GetInt("MonsterBird_Score");
        }
        //먼지
        else if(currentNum.Equals(1))
        {
            currentNameStr = "샌드위치를 완성하라";
            currentNameScore = PlayerPrefs.GetInt("Sandwiches_Score");
        }
        //전골
        else if(currentNum.Equals(2))
        {
            currentNameStr = "장난감을 완성하라";
            currentNameScore = PlayerPrefs.GetInt("ToyFactory_Score");
        }
        //전골
        else if(currentNum.Equals(3))
        {
            currentNameStr = "전골을 끓여라";
            currentNameScore = PlayerPrefs.GetInt("Hotpot_Score");
        }
        //조각상
        else if(currentNum.Equals(4))
        {
            currentNameStr = "조각상을 만들어라";
            currentNameScore = PlayerPrefs.GetInt("Statue_Score");

        }
        //샌드위치
        else if(currentNum.Equals(5))
        {
            currentNameStr = "좀비를 막아라";
            currentNameScore = PlayerPrefs.GetInt("Zombies_Score");
        }
        //축구
        else if(currentNum.Equals(6))
        {
            currentNameStr = "청기백기를 올려라";
            currentNameScore = PlayerPrefs.GetInt("Flag_Score");
        }
        //보석
        else  if (currentNum.Equals(7))
        {
            currentNameStr = "호스를 잠가라";
            currentNameScore = PlayerPrefs.GetInt("Pipe_Score");
        }
        //장난감
        else if(currentNum.Equals(8))
        {
            currentNameStr = "먼지를 털어라";
            currentNameScore = PlayerPrefs.GetInt("Dust_Score");
        }
        //좀비
        else if(currentNum.Equals(9))
        {
            currentNameStr = "피자도우를 돌려라";
            currentNameScore = PlayerPrefs.GetInt("Pizza_Score");
        }
    }
}
