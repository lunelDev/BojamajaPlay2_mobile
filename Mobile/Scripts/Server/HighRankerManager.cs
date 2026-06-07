using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighRankerManager : MonoBehaviour
{
    [SerializeField] private GameObject myRankList;
    [SerializeField] private GameObject myRankList_event;
    [SerializeField] private GameObject scoreByGamesPanel;
    private void OnEnable()
    {
        if (ServerManager.Instance.RankingEventDoing)
        {
            SettingSetActiveAble_event();
            SettingHighRanker_event();
        }
        else
        {
            SettingSetActiveAble();
            SettingHighRanker();
        }
    }

    private void OnDisable()
    {
        if (ServerManager.Instance.RankingEventDoing)
        {
            SettingSetActiveDisable_event();
        }
        else
        {
            SettingSetActiveDisable();
        }
    }

    // 일반 //
    // 오브젝트 활성화
    void SettingSetActiveAble()
    {
        for (int i = 0; i < ServerManager.Instance.highRanker.Count; i++)   // 5개 : 1,2,MyRank,4,5
        {
            myRankList.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    // 오브젝트 비활성화
    void SettingSetActiveDisable()
    {
        for (int i = 0; i < myRankList.transform.childCount ; i++)   // 5개 : 1,2,MyRank,4,5
        {
            myRankList.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    // 데이터 입력
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
    }

    // 이벤트 //
    // 오브젝트 활성화
    void SettingSetActiveAble_event()
    {
        for (int i = 0; i < ServerManager.Instance.highRanker.Count; i++)   // 5개 : 1,2,MyRank,4,5
        {
            myRankList_event.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    // 오브젝트 비활성화
    void SettingSetActiveDisable_event()
    {
        for (int i = 0; i < myRankList_event.transform.childCount; i++)   // 5개 : 1,2,MyRank,4,5
        {
            myRankList_event.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    // 데이터 입력
    void SettingHighRanker_event()
    {
        // 데이터 입력
        for (int i = 0; i < ServerManager.Instance.highRanker.Count; i++)   // 실제 데이터 갯수
        {
            for (int j = 0; j < myRankList_event.transform.GetChild(i).childCount - 1; j++)   // 4개 : Num Name Score Button
            {
                if (j == 0)
                {
                    myRankList_event.transform.GetChild(i).transform.GetChild(j).transform.gameObject.GetComponent<Text>().text = ServerManager.Instance.highRanker[i].ranking.ToString();      // Num
                }
                else if (j == 1)
                {
                    myRankList_event.transform.GetChild(i).transform.GetChild(j).transform.gameObject.GetComponent<Text>().text = ServerManager.Instance.highRanker[i].nickname;                // Name
                }
                else if (j == 2)
                {
                    myRankList_event.transform.GetChild(i).transform.GetChild(j).transform.gameObject.GetComponent<Text>().text = ServerManager.Instance.phoneNumber[i];   // phoneNumber
                }
                else if (j == 3)
                {
                    myRankList_event.transform.GetChild(i).transform.GetChild(j).transform.gameObject.GetComponent<Text>().text = ServerManager.Instance.highRanker[i].totalscore.ToString();   // Score
                }
            }
        }
    }

    public void ScorebyGames(int _rankingButton)
    {
        GameManager.Instance.OnClick_BtnSound1();
        StopAllCoroutines();
        StartCoroutine(_ScorebyGames(_rankingButton));

    }

    IEnumerator _ScorebyGames(int _rankingButton)
    {
        ServerManager.Instance.GetNetworkState();
        yield return new WaitUntil(() => ServerManager.Instance.isConnCompleted);
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
