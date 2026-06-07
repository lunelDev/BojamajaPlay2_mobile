using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyRankingManager : MonoBehaviour
{
    [SerializeField] private GameObject myRankList;
    [SerializeField] private GameObject myRankList_event;
    [SerializeField] private Text searchedNickName;
    [SerializeField] private Text searchedNickName_event;
    [SerializeField] private GameObject scoreByGamesPanel;

    private Color yellowColor = new Color(255, 202, 28, 255);
    private Color whiteColor = new Color(255, 255, 255, 255);
    private Color blackColor = new Color(0, 0, 0, 255);

    private void OnEnable()
    {
        if (ServerManager.Instance.RankingEventDoing)
        {
            SettingSetActiveAble_event();
            SettingMyRanking_event();
        }
        else
        {
            SettingSetActiveAble();
            SettingMyRanking();
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

    // 기본 //
    // 오브젝트 활성화
    void SettingSetActiveAble()
    {
        for (int i = 0; i < ServerManager.Instance.myRank.Count; i++)   // 최대 5개 : 1,2,MyRank,4,5
        {
            for (int j = 0; j < myRankList.transform.GetChild(i).childCount; j++)   // childCount 6개 : TargetPanel Num Name Score Button RedButton
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
        for (int i = 0; i < myRankList.transform.childCount; i++)   // 5개 : 1, 2, 3, MyRank, 4, 5
        {
            for (int j = 0; j < myRankList.transform.GetChild(i).childCount; j++)   // childCount 5개 : TargetPanel Num Name Score Button RedButton
            {
                if (j == 0 || j == 5)
                {
                    myRankList.transform.GetChild(i).transform.GetChild(j).transform.gameObject.SetActive(false);    // highlight, redButton object
                }
            }
        }

        for (int i = 0; i < myRankList.transform.childCount; i++)   // 5개 : 1,2,MyRank,4,5
        {
            myRankList.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    // 데이터 세팅
    void SettingMyRanking()
    {
        // 데이터 입력
        for (int i = 0; i < ServerManager.Instance.myRank.Count; i++)   // 실제 데이터 갯수 최대 5개
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

    // 이벤트 중//

    // 오브젝트 활성화
    void SettingSetActiveAble_event()
    {
        for (int i = 0; i < ServerManager.Instance.myRank.Count; i++)   // 최대 5개 : 1,2,MyRank,4,5
        {
            for (int j = 0; j < myRankList_event.transform.GetChild(i).childCount; j++)   // childCount 7개 : TargetPanel Num Name PhoneNumber Score Button RedButton
            {
                if (j == 1) // 폰트 노랑
                {
                    myRankList_event.transform.GetChild(i).transform.GetChild(j).transform.gameObject.SetActive(true);    // Num 활성화
                    myRankList_event.transform.GetChild(i).transform.GetChild(j).transform.gameObject.GetComponent<Text>().color = yellowColor;
                }
                else if (j == 2)    // 폰트 흰색
                {
                    myRankList_event.transform.GetChild(i).transform.GetChild(j).transform.gameObject.SetActive(true);    // Name 활성화
                    myRankList_event.transform.GetChild(i).transform.GetChild(j).transform.gameObject.GetComponent<Text>().color = whiteColor;
                }
                else if (j == 3) // 폰트 흰색
                {
                    myRankList_event.transform.GetChild(i).transform.GetChild(j).transform.gameObject.SetActive(true);    // Score 활성화
                    myRankList_event.transform.GetChild(i).transform.GetChild(j).transform.gameObject.GetComponent<Text>().color = whiteColor;
                }
                else if (j == 4)
                {
                    myRankList_event.transform.GetChild(i).transform.GetChild(j).transform.gameObject.SetActive(true);    // Score 활성화
                    myRankList_event.transform.GetChild(i).transform.GetChild(j).transform.gameObject.GetComponent<Text>().color = whiteColor;
                }
                else if (j == 5)
                {
                    myRankList_event.transform.GetChild(i).transform.GetChild(j).transform.gameObject.SetActive(true);    // Default Button 활성화
                }
            }
        }
    }

    // 오브젝트 비활성화
    void SettingSetActiveDisable_event()
    {
        for (int i = 0; i < myRankList_event.transform.childCount; i++)   // 5개 : 1, 2, 3, MyRank, 4, 5
        {
            for (int j = 0; j < myRankList_event.transform.GetChild(i).childCount; j++)   // childCount 5개 : TargetPanel Num Name Score Button RedButton
            {
                if (j == 0 || j == 6)
                {
                    myRankList_event.transform.GetChild(i).transform.GetChild(j).transform.gameObject.SetActive(false);    // highlight, redButton object
                }
            }
        }

        for (int i = 0; i < myRankList_event.transform.childCount; i++)   // 5개 : 1,2,MyRank,4,5
        {
            myRankList_event.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    // 데이터 세팅
    void SettingMyRanking_event()
    {
        // 데이터 입력
        for (int i = 0; i < ServerManager.Instance.myRank.Count; i++)   // 등수 데이터 갯수 최대 5개
        {
            for (int j = 0; j < myRankList_event.transform.GetChild(i).childCount; j++)   // childCount 6개 : TargetPanel Num Name PhoneNumber Score Button RedButton
            {
                if (j == 0)
                {
                    if (ServerManager.Instance.myRank[i].nickname == searchedNickName_event.text)
                    {
                        myRankList_event.transform.GetChild(i).transform.GetChild(j).transform.gameObject.SetActive(true);    // TargetPanel
                    }
                }
                else if (j == 1)
                {
                    myRankList_event.transform.GetChild(i).transform.GetChild(j).transform.gameObject.GetComponent<Text>().text = ServerManager.Instance.myRank[i].ranking.ToString();      // Num

                    if (ServerManager.Instance.myRank[i].nickname == searchedNickName_event.text)
                    {
                        myRankList_event.transform.GetChild(i).transform.GetChild(j).transform.gameObject.GetComponent<Text>().color = blackColor;
                    }
                }
                else if (j == 2)
                {
                    myRankList_event.transform.GetChild(i).transform.GetChild(j).transform.gameObject.GetComponent<Text>().text = ServerManager.Instance.myRank[i].nickname;                // Name

                    if (ServerManager.Instance.myRank[i].nickname == searchedNickName_event.text)
                    {
                        myRankList_event.transform.GetChild(i).transform.GetChild(j).transform.gameObject.GetComponent<Text>().color = blackColor;
                    }
                }
                else if (j == 3)
                {
                    myRankList_event.transform.GetChild(i).transform.GetChild(j).transform.gameObject.GetComponent<Text>().text = ServerManager.Instance.phoneNumber[i];                    // PhoneNumber

                    if (ServerManager.Instance.myRank[i].nickname == searchedNickName_event.text)
                    {
                        myRankList_event.transform.GetChild(i).transform.GetChild(j).transform.gameObject.GetComponent<Text>().color = blackColor;
                    }
                }
                else if (j == 4)
                {
                    myRankList_event.transform.GetChild(i).transform.GetChild(j).transform.gameObject.GetComponent<Text>().text = ServerManager.Instance.myRank[i].totalscore.ToString();   // Score

                    if (ServerManager.Instance.myRank[i].nickname == searchedNickName_event.text)
                    {
                        myRankList_event.transform.GetChild(i).transform.GetChild(j).transform.gameObject.GetComponent<Text>().color = blackColor;
                    }
                }
                else if (j == 5)
                {
                    if (ServerManager.Instance.myRank[i].nickname == searchedNickName_event.text)
                    {
                        // 노랑버튼
                        myRankList_event.transform.GetChild(i).transform.GetChild(j).transform.gameObject.SetActive(false);    // Button
                    }
                }
                else if (j == 6)
                {
                    if (ServerManager.Instance.myRank[i].nickname == searchedNickName_event.text)
                    {
                        // 분홍버튼
                        myRankList_event.transform.GetChild(i).transform.GetChild(j).transform.gameObject.SetActive(true);    // redButton
                    }
                }
            }
        }

        ServerManager.Instance.isMyListStackUp = false;
    }


    public void ScorebyGames(int _rankingButton)
    {
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
            string _nickname = ServerManager.Instance.myRank[_rankingButton].nickname;
            ServerManager.Instance.GetScoreByGames(_nickname);
            scoreByGamesPanel.SetActive(true);
        }
    }
}
