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
    public int monsterBird_Score;
    public int sandwiches_Score;
    public int toyfactory_Score;
    public int hotpot_Score;
    public int statue_Score;
    public int zombies_Score;
    public int flag_Score;
    public int pipe_Score;
    public int dust_Score;
    public int pizza_Score;

    public ScorebyGame() { /*초기화*/ }

    // 순서대로 저장
    public ScorebyGame(int _totalscore, int _monsterBird_Score, int _sandwiches_Score, int _toyfactory_Score, int _hotpot_Score, int _statue_Score,
                       int _zombies_Score, int _flag_Score, int _pipe_Score, int _dust_Score, int _pizza_Score)
    {
        totalscore = _totalscore;
        monsterBird_Score = _monsterBird_Score;
        sandwiches_Score = _sandwiches_Score;
        toyfactory_Score = _toyfactory_Score;
        hotpot_Score = _hotpot_Score;
        statue_Score = _statue_Score;
        zombies_Score = _zombies_Score;
        flag_Score = _flag_Score;
        pipe_Score = _pipe_Score;
        dust_Score = _dust_Score;
        pizza_Score = _pizza_Score;
    }
}

//public enum Games
//{
//    Bowling,
//    Dust,
//    Hotpot,
//    Pirates,
//    Pizza,
//    Sandwiches,
//    Soccer,
//    SubMarine,
//    ToyFactory,
//    Zombies
//}

public class ServerManager : MonoBehaviour
{
    public static ServerManager Instance { get; private set; }
    public List<Rank> highRanker = new List<Rank>();    // 1~5위 리스트, 1~20위 리스트
    public List<Rank> myRank = new List<Rank>();    // 내 랭크 리스트
    public List<ScorebyGame> scorebyGame = new List<ScorebyGame>(); // 게임별 점수 클래스 저장 클래스
    public bool isExistName = false;
    public bool isSearchComplete = false;
    public bool isMyListStackUp = false;
    public bool isHighListStackUp = false;

    public bool isConnected = false;
    public bool isConnCompleted = false;
    public GameObject networkConnectStatePopUp;

    void Awake()
    {
        //var objs = FindObjectsOfType<TimeManager>();
        //if (objs.Length != 1)
        //{
        //    Destroy(this);
        //    return;
        //}

        if (Instance != null)
            Destroy(this.gameObject);
        else
            Instance = this;

        DontDestroyOnLoad(this.gameObject);
    }
    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    public void RankingSearch(int amount)
    {
        StartCoroutine(_RankingSearch(amount));
    }

    IEnumerator _RankingSearch(int amount)
    {
        WWWForm form = new WWWForm();

        form.AddField("search_Amount", amount);

        using (UnityWebRequest www = UnityWebRequest.Post("http://52.78.74.47/bmp_two_rm/bmp_two_ranking_search.php", form))
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
            for (int i = 0; i < Array.Count; i++)
            {
                // 1. 랭커 정보 클래스에 저장 > Rank
                highRanker.Add(new Rank(Int32.Parse(Array[i]["RANKING"].Value), Array[i]["NICKNAME"].Value, Int32.Parse(Array[i]["TOTALSCORE"].Value)));

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

        using (UnityWebRequest www = UnityWebRequest.Post("http://52.78.74.47/bmp_two_rm/bmp_two_ranking_searchByNick.php", form))
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
    // 오버로딩 : int
    public void RankingSearching(int totalscore)
    {
        StartCoroutine(_RankingSearching(totalscore));
    }

    IEnumerator _RankingSearching(int _totalscore)
    {
        WWWForm form = new WWWForm();

        form.AddField("TotalScore", _totalscore);

        Debug.Log("현재 총점 : "+_totalscore);

        using (UnityWebRequest www = UnityWebRequest.Post("http://52.78.74.47/bmp_two_rm/bmp_two_myranking_search_byscore.php", form))
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
    // 오버로딩 : string
    public void RankingSearching(string _nickname)
    {
        StartCoroutine(_RankingSearching(_nickname));
    }

    IEnumerator _RankingSearching(string _nickname)
    {
        WWWForm form = new WWWForm();

        form.AddField("NickName", _nickname);

        using (UnityWebRequest www = UnityWebRequest.Post("http://52.78.74.47/bmp_two_rm/bmp_two_myranking_search.php", form))
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

    // 랭킹 등록
    public void RankingReg(string _nickname)
    {
        StartCoroutine(_RankingReg(_nickname));
    }

    IEnumerator _RankingReg(string _nickname)
    {
        WWWForm form = new WWWForm();

        form.AddField("NickName", _nickname);
        form.AddField("GameTotalScore", PlayerPrefs.GetInt("GameTotalScore").ToString());
        form.AddField("MonsterBird_Score", PlayerPrefs.GetInt("MonsterBird_Score").ToString());
        form.AddField("Dust_Score", PlayerPrefs.GetInt("Dust_Score").ToString());
        form.AddField("Hotpot_Score", PlayerPrefs.GetInt("Hotpot_Score").ToString());
        form.AddField("Statue_Score", PlayerPrefs.GetInt("Statue_Score").ToString());
        form.AddField("Pizza_Score", PlayerPrefs.GetInt("Pizza_Score").ToString());
        form.AddField("Sandwiches_Score", PlayerPrefs.GetInt("Sandwiches_Score").ToString());
        form.AddField("Flag_Score", PlayerPrefs.GetInt("Flag_Score").ToString());
        form.AddField("Pipe_Score", PlayerPrefs.GetInt("Pipe_Score").ToString());
        form.AddField("ToyFactory_Score", PlayerPrefs.GetInt("ToyFactory_Score").ToString());
        form.AddField("Zombies_Score", PlayerPrefs.GetInt("Zombies_Score").ToString());

        //form.AddField("NickName", _nickname);
        //form.AddField("GameTotalScore", 50000);
        //form.AddField("Bowling_Score", 5000);
        //form.AddField("Dust_Score", 5000);
        //form.AddField("Hotpot_Score", 5000);
        //form.AddField("Pirates_Score", 5000);
        //form.AddField("Pizza_Score", 5000);
        //form.AddField("Sandwiches_Score", 5000);
        //form.AddField("Soccer_Score", 5000);
        //form.AddField("Submarine_Score", 5000);
        //form.AddField("ToyFactory_Score", 5000);
        //form.AddField("Zombies_Score", 5000);

        using (UnityWebRequest www = UnityWebRequest.Post("http://52.78.74.47/bmp_two_rm/bmp_two_ranking_reg.php", form))
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
            for (int i = 0; i < Array.Count; i++)
            {
                // 1. 나의 랭킹 정보 클래스에 저장 > Rank
                myRank.Add(new Rank(Int32.Parse(Array[i]["RANKING"].Value), Array[i]["NICKNAME"].Value, Int32.Parse(Array[i]["TOTALSCORE"].Value)));
            }

            isMyListStackUp = true;
        }
        else
        {
            Debug.Log("값이 존재하지 않습니다.");
        }
    }

    public void GetScoreByGames(string _nickname)
    {
        StartCoroutine(_GetScoreByGames(_nickname));
    }

    IEnumerator _GetScoreByGames(string _nickname)
    {
        WWWForm form = new WWWForm();

        form.AddField("NickName", _nickname);

        using (UnityWebRequest www = UnityWebRequest.Post("http://52.78.74.47/bmp_two_rm/bmp_two_get_scorebygames.php", form))
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
                                                Int32.Parse(Array[i]["MONSTERBIRD_SCORE"].Value), Int32.Parse(Array[i]["SANDWICHES_SCORE"].Value),
                                                Int32.Parse(Array[i]["TOYFACTORY_SCORE"].Value), Int32.Parse(Array[i]["HOTPOT_SCORE"].Value),
                                                Int32.Parse(Array[i]["STATUE_SCORE"].Value), Int32.Parse(Array[i]["ZOMBIES_SCORE"].Value),
                                                Int32.Parse(Array[i]["FLAG_SCORE"].Value), Int32.Parse(Array[i]["PIPE_SCORE"].Value),
                                                Int32.Parse(Array[i]["DUST_SCORE"].Value), Int32.Parse(Array[i]["PIZZA_SCORE"].Value)));

                //Debug.Log("TOTALSCORE :" + Array[i]["TOTALSCORE"].Value);
                //Debug.Log("BOWLING_SCORE :" + Array[i]["BOWLING_SCORE"].Value);
                //Debug.Log("ZOMBIES_SCORE :" + Array[i]["ZOMBIES_SCORE"].Value);
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

        using (UnityWebRequest www = UnityWebRequest.Post("http://52.78.74.47/bmp_two_rm/bmp_two_conn_state.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                isConnected = false;
                // 여기다 네트워크 연결안됬다 팝업창 띄우기
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