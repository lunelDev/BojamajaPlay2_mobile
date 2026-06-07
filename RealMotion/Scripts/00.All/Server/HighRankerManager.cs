using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighRankerManager : MonoBehaviour
{
    [SerializeField] private GameObject myRankList;
    [SerializeField] private GameObject scoreByGamesPanel;
    private void OnEnable()
    {
        SettingSetActiveAble();
        SettingHighRanker();
    }

    private void OnDisable()
    {
        SettingSetActiveDisable();
    }

    void SettingSetActiveAble()
    {
        Debug.Log("갯수: " + ServerManager.Instance.highRanker.Count);

        for (int i = 0; i < ServerManager.Instance.highRanker.Count; i++)   // 5개 : 1,2,MyRank,4,5
        {
            myRankList.transform.GetChild(i).gameObject.SetActive(true);
        }
        //StartCoroutine(_SettingSetActiveAble());
    }

    //IEnumerator _SettingSetActiveAble()
    //{
    //    yield return new WaitUntil(() => ServerManager.Instance.isListStackUp);
    //    // 자신랭크 위아래 유저수를 5명을 못채울경우 대비
    //    for (int i = 0; i < ServerManager.Instance.myRank.Count; i++)   // 5개 : 1,2,MyRank,4,5
    //    {
    //        myRankList.transform.GetChild(i).gameObject.SetActive(true);
    //    }
    //}

    void SettingSetActiveDisable()
    {
        for (int i = 0; i < myRankList.transform.childCount ; i++)   // 5개 : 1,2,MyRank,4,5
        {
            myRankList.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    void SettingHighRanker()
    {
        // 데이터 입력
        for (int i = 0; i < ServerManager.Instance.highRanker.Count; i++)   // 실제 데이터 갯수
        {
            for (int j = 0; j < myRankList.transform.GetChild(i).childCount - 1; j++)   // 4개 : Num Name Score Button
            {
                if (j == 0)
                {
                    myRankList.transform.GetChild(i).transform.GetChild(j).transform.gameObject.GetComponent<Text>().text = ServerManager.Instance.highRanker[i].ranking.ToString();      // Num
                }
                else if (j == 1)
                {
                    myRankList.transform.GetChild(i).transform.GetChild(j).transform.gameObject.GetComponent<Text>().text = ServerManager.Instance.highRanker[i].nickname;                // Name
                }
                else if (j == 2)
                {
                    myRankList.transform.GetChild(i).transform.GetChild(j).transform.gameObject.GetComponent<Text>().text = ServerManager.Instance.highRanker[i].totalscore.ToString();   // Score
                }
            }
        }
        //StartCoroutine(_SettingMyRanking());
    }

    public void ScorebyGames(int _rankingButton)
    {
        /*SELECT r.totalscore, s.bowling_score, s.dust_score, s.hotpot_score, s.pirates_score, s.pizza_score,
        s.sandwiches_score, s.soccer_score, s.submarine_score, s.toyfactory_score, s.zombies_score	
        FROM bmp_two_ranking AS r, bmp_two_scorebygames AS s WHERE r.nickname = '피카츄' AND s.nickname = '피카츄';*/
        GameManager.Instance.OnClick_BtnSound1();
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
            string _nickname = ServerManager.Instance.highRanker[_rankingButton].nickname;
            ServerManager.Instance.GetScoreByGames(_nickname);
            scoreByGamesPanel.SetActive(true);
        }
    }
}
