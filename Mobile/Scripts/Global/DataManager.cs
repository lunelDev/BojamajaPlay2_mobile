using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }
    public GameObject fireworks;
    public Score scoreManager;    //점수
    public Timer timerManager; //타이머

    public Score RankUI_score;
    public Timer RankUI_timer;

    public Score ClassicUI_score;
    public Timer ClassicUI_timer;

    float TotalScore = 0;
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
        fireworks.SetActive(false); //clear Particle 비활성화
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
            fireworks.SetActive(true); //clear Particle 활성화                                 
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
            if (GameManager.HaveChance.Equals(true))
            {
                PlayerPrefs.SetInt("GameTotalScore", (int)(TotalScore + scoreManager.score)); //total점수 저장
                PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_Score", scoreManager.score); //획득 점수 저장
                PlayerPrefs.SetString(SceneManager.GetActiveScene().name, SceneManager.GetActiveScene().name); //현재 게임 이름 저장
                PlayerPrefs.Save();
            }
            //찬스 타임! 1.5배 추가 점수
            else
            {
                PlayerPrefs.SetInt("GameTotalScore", (int)(TotalScore + scoreManager.score * 1.5f)); //total점수 저장
                PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_Score", scoreManager.score * 1.5f); //획득 점수 저장
                PlayerPrefs.SetString(SceneManager.GetActiveScene().name, SceneManager.GetActiveScene().name); //현재 게임 이름 저장
                PlayerPrefs.Save();
            }
        }
        else
        {
            PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_Score_Classic", scoreManager.score); //획득 점수 저장
            PlayerPrefs.SetString(SceneManager.GetActiveScene().name, SceneManager.GetActiveScene().name); //현재 게임 이름 저장
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_Star_Classic", scoreManager.starPick); //별 저장 
            PlayerPrefs.Save();
        }
    }
}
