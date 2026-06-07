using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddScoreByGames : MonoBehaviour
{
    [SerializeField] private Text totalScoreNumber;
    [SerializeField] private Text bowlingName, bowlingScore;
    [SerializeField] private Text dustName, dustScore;
    [SerializeField] private Text hotpotName, hotPotScore;
    [SerializeField] private Text piratesName, piratesScore;
    [SerializeField] private Text pizzaName, pizzaScore;
    [SerializeField] private Text sandwichesName, sandwichesScore;
    [SerializeField] private Text soccerName, soccerScore;
    [SerializeField] private Text submarineName, submarineScore;
    [SerializeField] private Text toyfactoryName, toyfactoryScore;
    [SerializeField] private Text zombiesName, zombiesScore;

    private void Update()
    {
        // 총점
        totalScoreNumber.text = ServerManager.Instance.scorebyGame[0].totalscore.ToString();

        // 볼링게임,점수
        bowlingName.text = "볼링공을 굴려라";
        bowlingScore.text = ServerManager.Instance.scorebyGame[0].bowling_Score.ToString();

        // 먼지, 점수
        dustName.text = "먼지를 털어라";
        dustScore.text = ServerManager.Instance.scorebyGame[0].dust_Score.ToString();

        // 전골, 점수
        hotpotName.text = "전골을 끓여라";
        hotPotScore.text = ServerManager.Instance.scorebyGame[0].hotpot_Score.ToString();

        // 해적, 점수
        piratesName.text = "해적선을 맞춰라";
        piratesScore.text = ServerManager.Instance.scorebyGame[0].pirates_Score.ToString();

        // 피자, 점수
        pizzaName.text = "피자도우를 돌려라";
        pizzaScore.text = ServerManager.Instance.scorebyGame[0].pizza_Score.ToString();

        // 샌드위치를 만들어라
        sandwichesName.text = "샌드위치를 만들어라";
        sandwichesScore.text = ServerManager.Instance.scorebyGame[0].sandwiches_Score.ToString();

        // 축구공을 넣어라
        soccerName.text = "축구공을 넣어라";
        soccerScore.text = ServerManager.Instance.scorebyGame[0].soccer_Score.ToString();

        // 보석을 찾아라
        submarineName.text = "보석을 찾아라";
        submarineScore.text = ServerManager.Instance.scorebyGame[0].submarine_Score.ToString();

        // 장난감을 조립하라
        toyfactoryName.text = "장난감을 조립하라";
        toyfactoryScore.text = ServerManager.Instance.scorebyGame[0].toyfactory_Score.ToString();

        // 좀비를 맞춰라
        zombiesName.text = "좀비를 맞춰라";
        zombiesScore.text = ServerManager.Instance.scorebyGame[0].zombies_Score.ToString();
    }
}
