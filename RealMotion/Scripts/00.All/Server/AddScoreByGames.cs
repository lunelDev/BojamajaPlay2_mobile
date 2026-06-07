using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddScoreByGames : MonoBehaviour
{
    [SerializeField] private Text totalScoreNumber;
    [SerializeField] private Text monsterBirdName, monsterBirdScore;
    [SerializeField] private Text sandwichesName, sandwichesScore;
    [SerializeField] private Text toyfactoryName, toyfactoryScore;
    [SerializeField] private Text hotpotName, hotPotScore;
    [SerializeField] private Text statueName, statueScore;
    [SerializeField] private Text zombiesName, zombiesScore;
    [SerializeField] private Text flagName, flagScore;
    [SerializeField] private Text pipeName, pipeScore;
    [SerializeField] private Text dustName, dustScore;
    [SerializeField] private Text pizzaName, pizzaScore;


    private float leftTime = 1;

    //private void OnEnable()
    //{
    //}

    private void Update()
    {
        // 총점
        totalScoreNumber.text = ServerManager.Instance.scorebyGame[0].totalscore.ToString();
        // 괴물새
        monsterBirdName.text = "괴물새를 쫓아라";
        monsterBirdScore.text = ServerManager.Instance.scorebyGame[0].monsterBird_Score.ToString();
        // 샌드위치를 만들어라
        sandwichesName.text = "샌드위치를 만들어라";
        sandwichesScore.text = ServerManager.Instance.scorebyGame[0].sandwiches_Score.ToString();
        // 장난감을 조립하라
        toyfactoryName.text = "장난감을 조립하라";
        toyfactoryScore.text = ServerManager.Instance.scorebyGame[0].toyfactory_Score.ToString();
        // 전골, 점수
        hotpotName.text = "전골을 끓여라";
        hotPotScore.text = ServerManager.Instance.scorebyGame[0].hotpot_Score.ToString();
        // 조각상
        statueName.text = "조각상을 만들어라";
        statueScore.text = ServerManager.Instance.scorebyGame[0].statue_Score.ToString();
        // 좀비를 맞춰라
        zombiesName.text = "좀비를 맞춰라";
        zombiesScore.text = ServerManager.Instance.scorebyGame[0].zombies_Score.ToString();
        // 청기백기
        flagName.text = "청기백기를 올려라";
        flagScore.text = ServerManager.Instance.scorebyGame[0].flag_Score.ToString();
        // 밸브를 잠가라
        pipeName.text = "밸브를 잠가라";
        pipeScore.text = ServerManager.Instance.scorebyGame[0].pipe_Score.ToString();
        // 먼지, 점수
        dustName.text = "먼지를 털어라";
        dustScore.text = ServerManager.Instance.scorebyGame[0].dust_Score.ToString();
        // 피자, 점수
        pizzaName.text = "피자도우를 돌려라";
        pizzaScore.text = ServerManager.Instance.scorebyGame[0].pizza_Score.ToString();
    }
}