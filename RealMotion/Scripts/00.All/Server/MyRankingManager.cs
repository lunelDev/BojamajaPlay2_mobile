using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyRankingManager : MonoBehaviour
{
    [SerializeField] private GameObject myRankList;
    [SerializeField] private Text searchedNickName;
    [SerializeField] private GameObject scoreByGamesPanel;

    private Color yellowColor = new Color(255, 202, 28, 255);
    private Color whiteColor = new Color(255, 255, 255, 255);
    private Color blackColor = new Color(0, 0, 0, 255);

    private void OnEnable()
    {
        SettingSetActiveAble();
        SettingMyRanking();
    }

    private void OnDisable()
    {
        SettingSetActiveDisable();
    }

    // 오브젝트 활성화
    void SettingSetActiveAble()
    {
        for (int i = 0; i < ServerManager.Instance.myRank.Count; i++)   // 최대 3개 : 1,MyRank,3
        {
            //myRankList.transform.GetChild(i).gameObject.SetActive(true);
            for (int j = 0; j < myRankList.transform.GetChild(i).childCount; j++)   // childCount 7개(TargetPanel Num Name Score Button RedButton Check1) 중 Num Name Score Button만 활성화
            {
                if (j == 1) // 폰트 노랑
                {
                    myRankList.transform.GetChild(i).transform.GetChild(j).transform.gameObject.SetActive(true);    // Num 활성화
                    myRankList.transform.GetChild(i).transform.GetChild(j).transform.gameObject.GetComponent<Text>().color = yellowColor;
                }
                else if (j == 2)    // 폰트 흰색
                {
                    myRankList.transform.GetChild(i).transform.GetChild(j).transform.gameObject.SetActive(true);    // Name 활성화
                    myRankList.transform.GetChild(i).transform.GetChild(j).transform.gameObject.GetComponent<Text>().color = whiteColor;
                }
                else if (j == 3) // 폰트 흰색
                {
                    myRankList.transform.GetChild(i).transform.GetChild(j).transform.gameObject.SetActive(true);    // Score 활성화
                    myRankList.transform.GetChild(i).transform.GetChild(j).transform.gameObject.GetComponent<Text>().color = whiteColor;
                }
                else if (j == 4)
                {
                    myRankList.transform.GetChild(i).transform.GetChild(j).transform.gameObject.SetActive(true);    // Default Button 활성화
                }
            }
        }
    }

    // 오브젝트 비활성화
    void SettingSetActiveDisable()
    {
        // 데이터 입력
        for (int i = 0; i < myRankList.transform.childCount; i++)   // 3개 : 1, MyRank, 3
        {
            for (int j = 0; j < myRankList.transform.GetChild(i).childCount; j++)   // childCount 7개 : TargetPanel Num Name Score Button RedButton check
            {
                if (j == 0 || j == 5)
                {
                    myRankList.transform.GetChild(i).transform.GetChild(j).transform.gameObject.SetActive(false);    // highlight, redButton object
                }
            }
        }

        for (int i = 0; i < myRankList.transform.childCount; i++)   // 5개 : 1,MyRank,3
        {
            myRankList.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    // 데이터 세팅
    void SettingMyRanking()
    {
        // 데이터 입력
        for (int i = 0; i < ServerManager.Instance.myRank.Count; i++)   // 실제 데이터 갯수 최대 3개
        {
            for (int j = 0; j < myRankList.transform.GetChild(i).childCount; j++)   // childCount 5개 : TargetPanel Num Name Score Button RedButton
            {
                if (j == 0)
                {
                    if (ServerManager.Instance.myRank[i].nickname == searchedNickName.text)
                    {
                        myRankList.transform.GetChild(i).transform.GetChild(j).transform.gameObject.SetActive(true);    // TargetPanel
                    }
                }
                else if (j == 1)
                {
                    myRankList.transform.GetChild(i).transform.GetChild(j).transform.gameObject.GetComponent<Text>().text = ServerManager.Instance.myRank[i].ranking.ToString();      // Num

                    if (ServerManager.Instance.myRank[i].nickname == searchedNickName.text)
                    {
                        myRankList.transform.GetChild(i).transform.GetChild(j).transform.gameObject.GetComponent<Text>().color = blackColor;
                    }
                }
                else if (j == 2)
                {
                    myRankList.transform.GetChild(i).transform.GetChild(j).transform.gameObject.GetComponent<Text>().text = ServerManager.Instance.myRank[i].nickname;                // Name

                    if (ServerManager.Instance.myRank[i].nickname == searchedNickName.text)
                    {
                        myRankList.transform.GetChild(i).transform.GetChild(j).transform.gameObject.GetComponent<Text>().color = blackColor;
                    }
                }
                else if (j == 3)
                {
                    myRankList.transform.GetChild(i).transform.GetChild(j).transform.gameObject.GetComponent<Text>().text = ServerManager.Instance.myRank[i].totalscore.ToString();   // Score

                    if (ServerManager.Instance.myRank[i].nickname == searchedNickName.text)
                    {
                        myRankList.transform.GetChild(i).transform.GetChild(j).transform.gameObject.GetComponent<Text>().color = blackColor;
                    }
                }
                else if (j == 4)
                {
                    if (ServerManager.Instance.myRank[i].nickname == searchedNickName.text)
                    {
                        // 노랑버튼
                        myRankList.transform.GetChild(i).transform.GetChild(j).transform.gameObject.SetActive(false);    // Button
                    }
                }
                else if (j == 5)
                {
                    if (ServerManager.Instance.myRank[i].nickname == searchedNickName.text)
                    {
                        // 분홍버튼
                        myRankList.transform.GetChild(i).transform.GetChild(j).transform.gameObject.SetActive(true);    // redButton
                    }
                }
            }
        }

        ServerManager.Instance.isMyListStackUp = false;
    }


    public void ScorebyGames(int _rankingButton)
    {
        /*SELECT r.totalscore, s.bowling_score, s.dust_score, s.hotpot_score, s.pirates_score, s.pizza_score,
        s.sandwiches_score, s.soccer_score, s.submarine_score, s.toyfactory_score, s.zombies_score   
        FROM bmp_two_ranking AS r, bmp_two_scorebygames AS s WHERE r.nickname = '피카츄' AND s.nickname = '피카츄';*/

        StopAllCoroutines();
        StartCoroutine(_ScorebyGames(_rankingButton));

    }

    IEnumerator _ScorebyGames(int _rankingButton)
    {
        ServerManager.Instance.GetNetworkState();
        yield return new WaitUntil(() => ServerManager.Instance.isConnCompleted);
        ServerManager.Instance.isConnCompleted = false;
        // 네트워크 연결 처리
        if (ServerManager.Instance.isConnected)
        {
            // _rankingButton > 0, 1, 2, 3, 4 중 클릭한 순번을 가져온다.
            string _nickname = ServerManager.Instance.myRank[_rankingButton].nickname;
            ServerManager.Instance.GetScoreByGames(_nickname);
            scoreByGamesPanel.SetActive(true);
        }
    }
}