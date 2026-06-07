using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using SimpleJSON;
using System;

[System.Serializable]
public class Rank
{
    public int ranking; // 랭킹
    public string nickname; // 닉네임
    public int totalscore;  // 최종점수

    public Rank() { /*초기화*/ }

    public Rank(int _ranking, string _nickname, int _totalscore)
    {
        ranking = _ranking;
        nickname = _nickname;
        totalscore = _totalscore;
    }
}
[System.Serializable]
public class ScorebyGame
{
    public int totalscore;
    public int bowling_Score;
    public int dust_Score;
    public int hotpot_Score;
    public int pirates_Score;
    public int pizza_Score;
    public int sandwiches_Score;
    public int soccer_Score;
    public int submarine_Score;
    public int toyfactory_Score;
    public int zombies_Score;

    public ScorebyGame() { /*초기화*/ }

    // 순서대로 저장
    public ScorebyGame(int _totalscore, int _bowling_Score, int _dust_Score, int _hotpot_Score, int _pirates_Score, int _pizza_Score,
                       int _sandwiches_Score, int _soccer_Score, int _submarine_Score, int _toyfactory_Score, int _zombies_Score)
    {
        totalscore = _totalscore;
        bowling_Score = _bowling_Score;
        dust_Score = _dust_Score;
        hotpot_Score = _hotpot_Score;
        pirates_Score = _pirates_Score;
        pizza_Score = _pizza_Score;
        sandwiches_Score = _sandwiches_Score;
        soccer_Score = _soccer_Score;
        submarine_Score = _submarine_Score;
        toyfactory_Score = _toyfactory_Score;
        zombies_Score = _zombies_Score;
    }
}

public enum Games
{
    Bowling,
    Dust,
    Hotpot,
    Pirates,
    Pizza,
    Sandwiches,
    Soccer,
    SubMarine,
    ToyFactory,
    Zombies
}

public class ServerManager : MonoBehaviour
{
    public static ServerManager Instance { get; private set; }
    public List<Rank> highRanker = new List<Rank>();    // 1~5위 리스트, 1~20위 리스트
    public List<Rank> myRank = new List<Rank>();    // 내 랭크 리스트
    public List<string> phoneNumber = new List<string>();    // 내 랭크 리스트
    public List<ScorebyGame> scorebyGame = new List<ScorebyGame>(); // 게임별 점수 클래스 저장 클래스
    public bool isExistName = false;
    public bool isSearchComplete = false;
    public bool isMyListStackUp = false;
    public bool isHighListStackUp = false;

    public bool isConnected = false;
    public bool isConnCompleted = false;
    public GameObject networkConnectStatePopUp;

    [Header("Event Trigger")]
    public bool RankingEventDoing;

    void Awake()
    {
        var objs = FindObjectsOfType<TimeManager>();
        if (objs.Length != 1)
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(this.gameObject);

        if (Instance != null)
            Destroy(this);
        else
            Instance = this;
    }

    public void RankingSearch(int amount)
    {
        StartCoroutine(_RankingSearch(amount));
    }

    IEnumerator _RankingSearch(int amount)
    {
        WWWForm form = new WWWForm();

        if (RankingEventDoing)
        {
            form.AddField("Way", "1");
        }
        else
        {
            form.AddField("Way", "0");
        }

        form.AddField("search_Amount", amount);

        if (RankingEventDoing)
        {
            using (UnityWebRequest www = UnityWebRequest.Post("http://52.78.74.47/bmp_two_ranking_search_copy.php", form))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    Debug.Log(www.downloadHandler.text);

                    //byte[] results = www.downloadHandler.data;
                    //text.text = www.downloadHandler.text;
                    GetRankingJSON(www.downloadHandler.text);
                }
            }
        }
        else
        {
            using (UnityWebRequest www = UnityWebRequest.Post("http://52.78.74.47/bmp_two_ranking_search.php", form))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    Debug.Log(www.downloadHandler.text);

                    //byte[] results = www.downloadHandler.data;
                    //text.text = www.downloadHandler.text;
                    GetRankingJSON(www.downloadHandler.text);
                }
            }
        }
    }

    //// Get Ranking JSON
    public void GetRankingJSON(string _jsonData)
    {
        //전달 받은 데이터를 json 형식으로 파싱(변환)
        var N = JSON.Parse(_jsonData);
        //php에서 전달받은 데이터가 results 이므로 새로운 변수에 가공해줌 -> echo json_encode(array("results"=>$result));
        //results 안의 배열에 key와 value값만 남게
        var Array = N["results"];

        //결과값이 있다면
        if (Array.Count > 0)
        {
            highRanker.Clear();
            phoneNumber.Clear();
            for (int i = 0; i < Array.Count; i++)
            {
                // 1. 랭커 정보 클래스에 저장 > Rank
                highRanker.Add(new Rank(Int32.Parse(Array[i]["RANKING"].Value), Array[i]["NICKNAME"].Value, Int32.Parse(Array[i]["TOTALSCORE"].Value)));

                if (RankingEventDoing)
                {
                    if (Array[i]["PHONENUMBER"].Value == "-")
                    {
                        phoneNumber.Add("-");
                    }
                    else
                    {
                        string str = Array[i]["PHONENUMBER"].Value.Substring(Array[i]["PHONENUMBER"].Value.Length - 4, 2);
                        str = str + "**";
                        phoneNumber.Add(str);
                    }
                }
            }

            isHighListStackUp = true;
        }
        else
        {
            Debug.Log("값이 존재하지 않습니다.");
        }
    }
    // 랭킹 닉네임 서치
    public void RankingSearchByNickName(string _nickname)
    {
        StartCoroutine(_RankingSearchByNickName(_nickname));
    }

    IEnumerator _RankingSearchByNickName(string _nickname)
    {
        WWWForm form = new WWWForm();

        form.AddField("NickName", _nickname);

        if (RankingEventDoing)
        {
            using (UnityWebRequest www = UnityWebRequest.Post("http://52.78.74.47/bmp_two_ranking_searchByNick_copy.php", form))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    Debug.Log("Form upload complete!");
                    // 내랭킹 가져오기
                    Debug.Log(www.downloadHandler.text);
                    GetRankingByNickNameJSON(www.downloadHandler.text);
                }
            }
        }
        else
        {
            using (UnityWebRequest www = UnityWebRequest.Post("http://52.78.74.47/bmp_two_ranking_searchByNick.php", form))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    Debug.Log("Form upload complete!");
                    // 내랭킹 가져오기
                    Debug.Log(www.downloadHandler.text);
                    GetRankingByNickNameJSON(www.downloadHandler.text);
                }
            }
        }

    }

    // 닉네임으로 랭킹 검색
    public void GetRankingByNickNameJSON(string _jsonData)
    {
        //전달 받은 데이터를 json 형식으로 파싱(변환)
        var N = JSON.Parse(_jsonData);
        //php에서 전달받은 데이터가 results 이므로 새로운 변수에 가공해줌 -> echo json_encode(array("results"=>$result));
        //results 안의 배열에 key와 value값만 남게
        var Array = N["results"];

        //결과값이 있다면
        if (Array.Count > 0)
        {
            isExistName = true;
            Debug.Log("값이 존재 합니다.");
        }
        else
        {
            isExistName = false;
            Debug.Log("값이 존재하지 않습니다.");
        }

        isSearchComplete = true;
    }

    // 닉네임으로 검색
    public void RankingSearching(string _nickname)
    {
        StartCoroutine(_RankingSearching(_nickname));
    }

    IEnumerator _RankingSearching(string _nickname)
    {
        WWWForm form = new WWWForm();

        form.AddField("NickName", _nickname);

        if (RankingEventDoing)
        {
            using (UnityWebRequest www = UnityWebRequest.Post("http://52.78.74.47/bmp_two_myranking_search_copy.php", form))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    Debug.Log("Form upload complete!");
                    // 내랭킹 가져오기
                    Debug.Log(www.downloadHandler.text);
                    GetMyRankingJSON(www.downloadHandler.text);
                }
            }
        }
        else
        {
            using (UnityWebRequest www = UnityWebRequest.Post("http://52.78.74.47/bmp_two_myranking_search.php", form))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    Debug.Log("Form upload complete!");
                    // 내랭킹 가져오기
                    Debug.Log(www.downloadHandler.text);
                    GetMyRankingJSON(www.downloadHandler.text);
                }
            }
        }

    }

    // 랭킹 등록
    public void RankingReg(string _nickname, string phone_number="-")
    {
        StartCoroutine(_RankingReg(_nickname, phone_number));
    }

    IEnumerator _RankingReg(string _nickname, string _phone_number)
    {
        WWWForm form = new WWWForm();

        //if (RankingEventDoing && _phone_number != "")
        //{
        //    form.AddField("Way", "1");
        //    form.AddField("PhoneNumber", _phone_number);
        //}
        //else
        //{
        //    form.AddField("Way", "0");
        //    form.AddField("PhoneNumber", "-");
        //}

        form.AddField("PhoneNumber", _phone_number);

        form.AddField("NickName", _nickname);
        form.AddField("GameTotalScore", PlayerPrefs.GetInt("GameTotalScore").ToString());
        form.AddField("Bowling_Score", PlayerPrefs.GetFloat("Bowling_Score").ToString());
        form.AddField("Dust_Score", PlayerPrefs.GetFloat("Dust_Score").ToString());
        form.AddField("Hotpot_Score", PlayerPrefs.GetFloat("Hotpot_Score").ToString());
        form.AddField("Pirates_Score", PlayerPrefs.GetFloat("Pirates_Score").ToString());
        form.AddField("Pizza_Score", PlayerPrefs.GetFloat("Pizza_Score").ToString());
        form.AddField("Sandwiches_Score", PlayerPrefs.GetFloat("Sandwiches_Score").ToString());
        form.AddField("Soccer_Score", PlayerPrefs.GetFloat("Soccer_Score").ToString());
        form.AddField("Submarine_Score", PlayerPrefs.GetFloat("Submarine_Score").ToString());
        form.AddField("ToyFactory_Score", PlayerPrefs.GetFloat("ToyFactory_Score").ToString());
        form.AddField("Zombies_Score", PlayerPrefs.GetFloat("Zombies_Score").ToString());

        //int score = 300;
        //int totalscore = 3000;

        //form.AddField("NickName", _nickname);
        //form.AddField("GameTotalScore", totalscore);
        //form.AddField("Bowling_Score", score);
        //form.AddField("Dust_Score", score);
        //form.AddField("Hotpot_Score", score);
        //form.AddField("Pirates_Score", score);
        //form.AddField("Pizza_Score", score);
        //form.AddField("Sandwiches_Score", score);
        //form.AddField("Soccer_Score", score);
        //form.AddField("Submarine_Score", score);
        //form.AddField("ToyFactory_Score", score);
        //form.AddField("Zombies_Score", score);

        if (RankingEventDoing)
        {
            using (UnityWebRequest www = UnityWebRequest.Post("http://52.78.74.47/bmp_two_ranking_reg_copy.php", form))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    Debug.Log("Form upload complete!");
                    // 내랭킹 가져오기
                    Debug.Log(www.downloadHandler.text);
                    GetMyRankingJSON(www.downloadHandler.text);
                }
            }
        }
        else
        {
            using (UnityWebRequest www = UnityWebRequest.Post("http://52.78.74.47/bmp_two_ranking_reg.php", form))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    Debug.Log("Form upload complete!");
                    // 내랭킹 가져오기
                    Debug.Log(www.downloadHandler.text);
                    GetMyRankingJSON(www.downloadHandler.text);
                }
            }
        }
    }

    public void GetMyRankingJSON(string _jsonData)
    {
        //전달 받은 데이터를 json 형식으로 파싱(변환)
        var N = JSON.Parse(_jsonData);
        //php에서 전달받은 데이터가 results 이므로 새로운 변수에 가공해줌 -> echo json_encode(array("results"=>$result));
        //results 안의 배열에 key와 value값만 남게
        var Array = N["results"];

        //결과값이 있다면
        if (Array.Count > 0)
        {
            myRank.Clear();
            phoneNumber.Clear();
            for (int i = 0; i < Array.Count; i++)
            {
                // 1. 나의 랭킹 정보 클래스에 저장 > Rank
                myRank.Add(new Rank(Int32.Parse(Array[i]["RANKING"].Value), Array[i]["NICKNAME"].Value, Int32.Parse(Array[i]["TOTALSCORE"].Value)));

                if (RankingEventDoing)
                {
                    if (Array[i]["PHONENUMBER"].Value == "-")
                    {
                        phoneNumber.Add("-");
                    }
                    else
                    {
                        Debug.Log("Length : "+ Array[i]["PHONENUMBER"].Value.Length);
                        Debug.Log("Value : "+ Array[i]["PHONENUMBER"].Value);

                        string str = Array[i]["PHONENUMBER"].Value.Substring(Array[i]["PHONENUMBER"].Value.Length-4, 2);
                        str = str + "**";
                        phoneNumber.Add(str);
                    }
                }
            }

            isMyListStackUp = true;
        }
        else
        {
            Debug.Log("값이 존재하지 않습니다.");
        }
    }

    // 점수로 검색
    public void RankingSearching(int totalscore)
    {
        StartCoroutine(_RankingSearching(totalscore));
    }

    IEnumerator _RankingSearching(int _totalscore)
    {
        WWWForm form = new WWWForm();

        form.AddField("TotalScore", _totalscore);

        if (RankingEventDoing)
        {
            using (UnityWebRequest www = UnityWebRequest.Post("http://52.78.74.47/bmp_two_myranking_search_byscore_copy.php", form))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    Debug.Log("Form upload complete!");
                    // 내랭킹 가져오기
                    Debug.Log(www.downloadHandler.text);
                    GetMyRankingJSONbyScore(www.downloadHandler.text);
                }
            }
        }
        else
        {
            using (UnityWebRequest www = UnityWebRequest.Post("http://52.78.74.47/bmp_two_myranking_search_byscore.php", form))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    Debug.Log("Form upload complete!");
                    // 내랭킹 가져오기
                    Debug.Log(www.downloadHandler.text);
                    GetMyRankingJSONbyScore(www.downloadHandler.text);
                }
            }
        }

    }

    public void GetMyRankingJSONbyScore(string _jsonData)
    {
        //전달 받은 데이터를 json 형식으로 파싱(변환)
        var N = JSON.Parse(_jsonData);
        //php에서 전달받은 데이터가 results 이므로 새로운 변수에 가공해줌 -> echo json_encode(array("results"=>$result));
        //results 안의 배열에 key와 value값만 남게
        var Array = N["results"];

        //결과값이 있다면
        if (Array.Count > 0)
        {
            //myRank.Clear();
            for (int i = 0; i < Array.Count; i++)
            {
                // > Current Rank Panel 에 표기
                //myRank.Add(new Rank(Int32.Parse(Array[i]["RANKING"].Value), Array[i]["NICKNAME"].Value, Int32.Parse(Array[i]["TOTALSCORE"].Value)));
                End_UIManager.Instance.currentTotalScore.text = Array[i]["RANKING"].Value;

                Debug.Log("현재순위 : " + Array[i]["RANKING"].Value);
            }
        }
        else
        {
            Debug.Log("값이 존재하지 않습니다.");
        }

        //isSearchComplete = true;
    }

    public void GetScoreByGames(string _nickname)
    {
        StartCoroutine(_GetScoreByGames(_nickname));
    }

    IEnumerator _GetScoreByGames(string _nickname)
    {
        WWWForm form = new WWWForm();

        form.AddField("NickName", _nickname);

        if (RankingEventDoing)
        {
            using (UnityWebRequest www = UnityWebRequest.Post("http://52.78.74.47/bmp_two_get_scorebygames_copy.php", form))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    Debug.Log("Form upload complete!");
                    // 내랭킹 가져오기
                    Debug.Log(www.downloadHandler.text);
                    GetScoreByNickNameJSON(www.downloadHandler.text);
                }
            }
        }
        else
        {
            using (UnityWebRequest www = UnityWebRequest.Post("http://52.78.74.47/bmp_two_get_scorebygames.php", form))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    Debug.Log("Form upload complete!");
                    // 내랭킹 가져오기
                    Debug.Log(www.downloadHandler.text);
                    GetScoreByNickNameJSON(www.downloadHandler.text);
                }
            }
        }
    }

    public void GetScoreByNickNameJSON(string _jsonData)
    {
        //전달 받은 데이터를 json 형식으로 파싱(변환)
        var N = JSON.Parse(_jsonData);
        //php에서 전달받은 데이터가 results 이므로 새로운 변수에 가공해줌 -> echo json_encode(array("results"=>$result));
        //results 안의 배열에 key와 value값만 남게
        var Array = N["results"];

        //결과값이 있다면
        if (Array.Count > 0)
        {
            scorebyGame.Clear();
            for (int i = 0; i < Array.Count; i++)
            {
                // 1. 게임별 점수 저장 > ScoreByGames
                scorebyGame.Add(new ScorebyGame(Int32.Parse(Array[i]["TOTALSCORE"].Value),
                                                Int32.Parse(Array[i]["BOWLING_SCORE"].Value), Int32.Parse(Array[i]["DUST_SCORE"].Value),
                                                Int32.Parse(Array[i]["HOTPOT_SCORE"].Value), Int32.Parse(Array[i]["PIRATES_SCORE"].Value),
                                                Int32.Parse(Array[i]["PIZZA_SCORE"].Value), Int32.Parse(Array[i]["SANDWICHES_SCORE"].Value),
                                                Int32.Parse(Array[i]["SOCCER_SCORE"].Value), Int32.Parse(Array[i]["SUBMARINE_SCORE"].Value),
                                                Int32.Parse(Array[i]["TOYFACTORY_SCORE"].Value), Int32.Parse(Array[i]["ZOMBIES_SCORE"].Value)));

            }
        }
        else
        {
            Debug.Log("값이 존재하지 않습니다.");
        }
    }

    public void GetNetworkState()
    {
        StartCoroutine(_GetNetworkState());
    }

    IEnumerator _GetNetworkState()
    {
        WWWForm form = new WWWForm();

        using (UnityWebRequest www = UnityWebRequest.Post("http://52.78.74.47/bmp_two_conn_state.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                isConnected = false;
                // 네트워크 연결안됬다 팝업창 띄우기
                NetworkErrorPopUp();
            }
            else
            {
                isConnected = true;
                Debug.Log(www.downloadHandler.text);
            }
        }

        isConnCompleted = true;
    }

    private void NetworkErrorPopUp()
    {
        networkConnectStatePopUp.SetActive(true);
        StartCoroutine(_NetworkErrorPopUp());
    }

    IEnumerator _NetworkErrorPopUp()
    {
        WaitForSeconds ws = new WaitForSeconds(0.8f);
        yield return ws;
        networkConnectStatePopUp.SetActive(false);
    }
}