using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingUIManager : MonoBehaviour
{
    //TopRank와 MyRank관리 하는 캔버스 Manager
    public GameObject TopRankPanel;
    public GameObject MyRankPanel;
    [SerializeField] private GameObject[] rankZone_List;
    [SerializeField] private GameObject[] rankZone_List_event;
    [SerializeField] private GameObject scoreByGamesPanel;
    public bool MyRank = false;
    
    public static RankingUIManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;    
    }

    private void Update()
    {
        if (MyRank.Equals(true))
        {
            MyRankPanel.SetActive(true); //내 랭킹 활성화
        }
        else
        {
            TopRankPanel.SetActive(true);
        }
    }

    private void OnEnable()
    {
        if (ServerManager.Instance.RankingEventDoing)
        {
            SettingRanking_event();
        }
        else
        {
            SettingRanking();
        }
        
    }

    private void OnDisable()
    {
        if (ServerManager.Instance.RankingEventDoing)
        {
            SettingOnEnableRanking_event();
        }
        else
        {
            SettingOnEnableRanking();
        }
    }

    //해당닉네임을 찾을 수 없다고 떳을 때
    public void DontHaveName(GameObject ob)
    {
        StartCoroutine(_DontHaveName(ob));
    }
    IEnumerator _DontHaveName(GameObject ob)
    {
        yield return new WaitForSeconds(1f);
        ob.SetActive(false);
        yield return null; 
    }

    // 일반 //
    // 랭크 활성화
    void SettingOnEnableRanking()
    {
        // ranklist 1~20 : 4개
        for (int i = 0; i < rankZone_List.Length; i++)
        {
            // 각각의 랭킹리스트에 한페이지에 5개씩
            for (int j = 0; j < rankZone_List[i].transform.childCount; j++)
            {
                // Object의 갯수가 Top 티어가 존재하면 5개, 존재하지 않으면 4개
                if (rankZone_List[i].transform.GetChild(j).transform.childCount == 5)
                {
                    rankZone_List[i].transform.GetChild(j).transform.GetChild(1).transform.gameObject.SetActive(true);
                    rankZone_List[i].transform.GetChild(j).transform.GetChild(2).transform.gameObject.SetActive(true);
                    rankZone_List[i].transform.GetChild(j).transform.GetChild(3).transform.gameObject.SetActive(true);
                    rankZone_List[i].transform.GetChild(j).transform.GetChild(4).transform.gameObject.SetActive(true);
                }
                else if (rankZone_List[i].transform.GetChild(j).transform.childCount == 4)
                {
                    rankZone_List[i].transform.GetChild(j).transform.GetChild(0).transform.gameObject.SetActive(true);
                    rankZone_List[i].transform.GetChild(j).transform.GetChild(1).transform.gameObject.SetActive(true);
                    rankZone_List[i].transform.GetChild(j).transform.GetChild(2).transform.gameObject.SetActive(true);
                    rankZone_List[i].transform.GetChild(j).transform.GetChild(3).transform.gameObject.SetActive(true);
                }
            }
        }
    }

    // 1~20위까지 랭크세팅
    void SettingRanking()
    {
        int rankingcnt = 0;

        // ranklist 1~20 : 4개
        for (int i = 0; i < rankZone_List.Length; i++)
        {
            // 각각의 랭킹리스트에 한페이지에 5개씩
            for (int j = 0; j < rankZone_List[i].transform.childCount; j++)
            {
                // Object의 갯수가 Top 티어가 존재하면 5개, 존재하지 않으면 4개
                if (rankZone_List[i].transform.GetChild(j).transform.childCount == 5)
                {
                    if (ServerManager.Instance.highRanker.Count - 1 >= rankingcnt)
                    {
                        rankZone_List[i].transform.GetChild(j).transform.GetChild(1).transform.gameObject.GetComponent<Text>().text = ServerManager.Instance.highRanker[rankingcnt].ranking.ToString();
                        rankZone_List[i].transform.GetChild(j).transform.GetChild(2).transform.gameObject.GetComponent<Text>().text = ServerManager.Instance.highRanker[rankingcnt].nickname;
                        rankZone_List[i].transform.GetChild(j).transform.GetChild(3).transform.gameObject.GetComponent<Text>().text = ServerManager.Instance.highRanker[rankingcnt].totalscore.ToString();
                    }
                    else
                    {
                        rankZone_List[i].transform.GetChild(j).transform.GetChild(1).transform.gameObject.SetActive(false);
                        rankZone_List[i].transform.GetChild(j).transform.GetChild(2).transform.gameObject.SetActive(false);
                        rankZone_List[i].transform.GetChild(j).transform.GetChild(3).transform.gameObject.SetActive(false);
                        rankZone_List[i].transform.GetChild(j).transform.GetChild(4).transform.gameObject.SetActive(false);
                    }
                }
                else if (rankZone_List[i].transform.GetChild(j).transform.childCount == 4)
                {
                    if (ServerManager.Instance.highRanker.Count - 1 >= rankingcnt)
                    {
                        rankZone_List[i].transform.GetChild(j).transform.GetChild(0).transform.gameObject.GetComponent<Text>().text = ServerManager.Instance.highRanker[rankingcnt].ranking.ToString();
                        rankZone_List[i].transform.GetChild(j).transform.GetChild(1).transform.gameObject.GetComponent<Text>().text = ServerManager.Instance.highRanker[rankingcnt].nickname;
                        rankZone_List[i].transform.GetChild(j).transform.GetChild(2).transform.gameObject.GetComponent<Text>().text = ServerManager.Instance.highRanker[rankingcnt].totalscore.ToString();
                    }
                    else
                    {
                        rankZone_List[i].transform.GetChild(j).transform.GetChild(0).transform.gameObject.SetActive(false);
                        rankZone_List[i].transform.GetChild(j).transform.GetChild(1).transform.gameObject.SetActive(false);
                        rankZone_List[i].transform.GetChild(j).transform.GetChild(2).transform.gameObject.SetActive(false);
                        rankZone_List[i].transform.GetChild(j).transform.GetChild(3).transform.gameObject.SetActive(false);
                    }
                }
                rankingcnt++;
            }
        }
    }

    // 랭킹 이벤트 중 //
    // 랭크 활성화
    void SettingOnEnableRanking_event()
    {
        // ranklist 1~20 : 4개
        for (int i = 0; i < rankZone_List_event.Length; i++)
        {
            // 각각의 랭킹리스트에 한페이지에 5개씩
            for (int j = 0; j < rankZone_List_event[i].transform.childCount; j++)
            {
                // Object의 갯수가 Top 티어가 존재하면 5개, 존재하지 않으면 4개
                if (rankZone_List_event[i].transform.GetChild(j).transform.childCount == 6)
                {
                    rankZone_List_event[i].transform.GetChild(j).transform.GetChild(1).transform.gameObject.SetActive(true);
                    rankZone_List_event[i].transform.GetChild(j).transform.GetChild(2).transform.gameObject.SetActive(true);
                    rankZone_List_event[i].transform.GetChild(j).transform.GetChild(3).transform.gameObject.SetActive(true);
                    rankZone_List_event[i].transform.GetChild(j).transform.GetChild(4).transform.gameObject.SetActive(true);
                    rankZone_List_event[i].transform.GetChild(j).transform.GetChild(5).transform.gameObject.SetActive(true);
                }
                else if (rankZone_List_event[i].transform.GetChild(j).transform.childCount == 5)
                {
                    rankZone_List_event[i].transform.GetChild(j).transform.GetChild(0).transform.gameObject.SetActive(true);
                    rankZone_List_event[i].transform.GetChild(j).transform.GetChild(1).transform.gameObject.SetActive(true);
                    rankZone_List_event[i].transform.GetChild(j).transform.GetChild(2).transform.gameObject.SetActive(true);
                    rankZone_List_event[i].transform.GetChild(j).transform.GetChild(3).transform.gameObject.SetActive(true);
                    rankZone_List_event[i].transform.GetChild(j).transform.GetChild(4).transform.gameObject.SetActive(true);
                }
            }
        }
    }

    // 1~20위까지 랭크세팅
    void SettingRanking_event()
    {
        int rankingcnt = 0;

        // ranklist 1~20 : 4개
        for (int i = 0; i < rankZone_List_event.Length; i++)
        {
            // 각각의 랭킹리스트에 한페이지에 5개씩
            for (int j = 0; j < rankZone_List_event[i].transform.childCount; j++)
            {
                // Object의 갯수가 Top 티어가 존재하면 5개, 존재하지 않으면 4개
                if (rankZone_List_event[i].transform.GetChild(j).transform.childCount == 6)
                {
                    if (ServerManager.Instance.highRanker.Count - 1 >= rankingcnt)
                    {
                        rankZone_List_event[i].transform.GetChild(j).transform.GetChild(1).transform.gameObject.GetComponent<Text>().text = ServerManager.Instance.highRanker[rankingcnt].ranking.ToString();
                        rankZone_List_event[i].transform.GetChild(j).transform.GetChild(2).transform.gameObject.GetComponent<Text>().text = ServerManager.Instance.highRanker[rankingcnt].nickname;
                        rankZone_List_event[i].transform.GetChild(j).transform.GetChild(3).transform.gameObject.GetComponent<Text>().text = ServerManager.Instance.phoneNumber[rankingcnt];
                        rankZone_List_event[i].transform.GetChild(j).transform.GetChild(4).transform.gameObject.GetComponent<Text>().text = ServerManager.Instance.highRanker[rankingcnt].totalscore.ToString();
                    }
                    else
                    {
                        rankZone_List_event[i].transform.GetChild(j).transform.GetChild(1).transform.gameObject.SetActive(false);
                        rankZone_List_event[i].transform.GetChild(j).transform.GetChild(2).transform.gameObject.SetActive(false);
                        rankZone_List_event[i].transform.GetChild(j).transform.GetChild(3).transform.gameObject.SetActive(false);
                        rankZone_List_event[i].transform.GetChild(j).transform.GetChild(4).transform.gameObject.SetActive(false);
                        rankZone_List_event[i].transform.GetChild(j).transform.GetChild(5).transform.gameObject.SetActive(false);
                    }
                }
                else if (rankZone_List_event[i].transform.GetChild(j).transform.childCount == 5)
                {
                    if (ServerManager.Instance.highRanker.Count - 1 >= rankingcnt)
                    {
                        rankZone_List_event[i].transform.GetChild(j).transform.GetChild(0).transform.gameObject.GetComponent<Text>().text = ServerManager.Instance.highRanker[rankingcnt].ranking.ToString();
                        rankZone_List_event[i].transform.GetChild(j).transform.GetChild(1).transform.gameObject.GetComponent<Text>().text = ServerManager.Instance.highRanker[rankingcnt].nickname;
                        rankZone_List_event[i].transform.GetChild(j).transform.GetChild(2).transform.gameObject.GetComponent<Text>().text = ServerManager.Instance.phoneNumber[rankingcnt];
                        rankZone_List_event[i].transform.GetChild(j).transform.GetChild(3).transform.gameObject.GetComponent<Text>().text = ServerManager.Instance.highRanker[rankingcnt].totalscore.ToString();
                    }
                    else
                    {
                        rankZone_List_event[i].transform.GetChild(j).transform.GetChild(0).transform.gameObject.SetActive(false);
                        rankZone_List_event[i].transform.GetChild(j).transform.GetChild(1).transform.gameObject.SetActive(false);
                        rankZone_List_event[i].transform.GetChild(j).transform.GetChild(2).transform.gameObject.SetActive(false);
                        rankZone_List_event[i].transform.GetChild(j).transform.GetChild(3).transform.gameObject.SetActive(false);
                        rankZone_List_event[i].transform.GetChild(j).transform.GetChild(4).transform.gameObject.SetActive(false);
                    }
                }
                rankingcnt++;
            }
        }
    }

    public void HighRankerScorebyGames(int _rankingButton)
    {
        /*SELECT r.totalscore, s.bowling_score, s.dust_score, s.hotpot_score, s.pirates_score, s.pizza_score,
        s.sandwiches_score, s.soccer_score, s.submarine_score, s.toyfactory_score, s.zombies_score	
        FROM bmp_two_ranking AS r, bmp_two_scorebygames AS s WHERE r.nickname = '피카츄' AND s.nickname = '피카츄';*/

        StopAllCoroutines();
        StartCoroutine(_HighRankerScorebyGames(_rankingButton));
    }

    IEnumerator _HighRankerScorebyGames(int _rankingButton)
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

    public void MyRankScorebyGames(int _rankingButton)
    {
        /*SELECT r.totalscore, s.bowling_score, s.dust_score, s.hotpot_score, s.pirates_score, s.pizza_score,
        s.sandwiches_score, s.soccer_score, s.submarine_score, s.toyfactory_score, s.zombies_score	
        FROM bmp_two_ranking AS r, bmp_two_scorebygames AS s WHERE r.nickname = '피카츄' AND s.nickname = '피카츄';*/

        StopAllCoroutines();
        StartCoroutine(_MyRankScorebyGames(_rankingButton));

    }

    IEnumerator _MyRankScorebyGames(int _rankingButton)
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
