using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }
   // public GameObject fireworks;
    public Score scoreManager;    //점수
    public Timer timerManager; //타이머

    public Score RankUI_score;
    public Timer RankUI_timer;

    public Score ClassicUI_score;
    public Timer ClassicUI_timer;

    int TotalScore = 0;
    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else Instance = this;

        if (GameManager.RankMode.Equals(true))
        {
            scoreManager = RankUI_score.GetComponent<Score>();
            timerManager = RankUI_timer.GetComponent<Timer>();
        }
        else
        {
            scoreManager = ClassicUI_score.GetComponent<Score>();
            timerManager = ClassicUI_timer.GetComponent<Timer>();
        }
    }
    public IEnumerator OnRoundStart()
    {
     //   fireworks.SetActive(false); //clear Particle 비활성화
        timerManager.StartTimer(); // 타이머 시작
        scoreManager.Set(0);    // 점수 초기화    
        TotalScore = PlayerPrefs.GetInt("GameTotalScore");
        scoreManager.UpdateStarRating();
        yield return null;
    }
    public IEnumerator OnRoundEnd()
    {
        if (scoreManager.score > 0)
        {
         //   fireworks.SetActive(true); //clear Particle 활성화                                 
        }
        SaveDataScore();

        yield return null;
    }
    //총점, 획득점수, 현재게임이름, 현재별점수 저장
    public void SaveDataScore()
    {
        //랭킹 모드 일 경우,
        if (GameManager.RankMode.Equals(true))
        {
  
                PlayerPrefs.SetInt("GameTotalScore", TotalScore + scoreManager.score); //total점수 저장
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_Score", scoreManager.score); //획득 점수 저장
                PlayerPrefs.SetString(SceneManager.GetActiveScene().name, SceneManager.GetActiveScene().name); //현재 게임 이름 저장
                PlayerPrefs.Save();
                Debug.Log("Total Score : " + PlayerPrefs.GetInt("GameTotalScore"));
        
        }
        else
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_Score_Classic", scoreManager.score); //획득 점수 저장
            PlayerPrefs.SetString(SceneManager.GetActiveScene().name, SceneManager.GetActiveScene().name); //현재 게임 이름 저장
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_Star_Classic", scoreManager.starPick); //별 저장 
            PlayerPrefs.Save();
        }
    }


    // // debug fps
    // float time;
    // float fps;
    // float highest;
    // float lowest = Mathf.Infinity;
    // void OnGUI()
    // {
    //     GUIStyle style = new GUIStyle();
    //     style.fontSize = 80;
    //     style.normal.textColor = Color.white;
    //     GUI.Label(new Rect(20, Screen.height - 300, 100, 100), $"<color=red><b>{lowest.ToString("F2")}</b></color>", style);
    //     GUI.Label(new Rect(20, Screen.height - 200, 100, 100), $"<b>{fps.ToString("F2")}</b>", style);
    //     GUI.Label(new Rect(20, Screen.height - 100, 100, 100), $"<color=green><b>{highest.ToString("F2")}</b></color>", style);
    //     if (time > 0f)
    //     {
    //         time -= Time.deltaTime;
    //         return;
    //     }
    //     fps = 1f / Time.deltaTime;
    //     if (fps > highest) highest = fps;
    //     if (fps < lowest) lowest = fps;


    //     time = 0.5f;
    // }
}
