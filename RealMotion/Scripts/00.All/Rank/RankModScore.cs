using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RankModScore : MonoBehaviour
{
    public Sprite[] NumCollect; //이미지에 쓰일 sprite
    public Image[] ScorePosition; //rank 판넬안의 이미지 위치

    string currentScore;
    string[] _score; //문자열 하나하나 담기 
   float currentScoreFloat; 
    private void Awake()
    {
        _score = new string[5];
        if (SceneManager.GetActiveScene().name != "EndScene")
        {
            currentScore = DataManager.Instance.scoreManager.score.ToString("D5");
        }
        else
        {
            currentScore = PlayerPrefs.GetInt("GameTotalScore").ToString(); //string형
            currentScoreFloat = PlayerPrefs.GetInt("GameTotalScore");
            Debug.Log("currentScore : " + currentScore);
        }

    }
    private void OnEnable()
    {
        SplitScore();
        if (SceneManager.GetActiveScene().name == "EndScene")
        {
            CurrentRankSearch();
        }
    }
    void SplitScore()
    {
        for (int i = 0; i < 5; i++)
        {
            _score[i] = currentScore.Substring(i, 1);
            Debug.Log(_score[i]);
        }
        SetScoreSprite();
    }
    void SetScoreSprite()
    {
        if(SceneManager.GetActiveScene().name != "EndScene")
        {
            if (DataManager.Instance.scoreManager.score != 0)
            {
                for (int i = 0; i < ScorePosition.Length; i++)
                {
                    if (_score[i].Equals("0"))
                    {
                        if (_score[0].Equals("0"))
                        {
                            ScorePosition[0].gameObject.SetActive(false);
                            if (_score[1].Equals("0"))
                            {
                                ScorePosition[1].gameObject.SetActive(false);
                                if (_score[2].Equals("0"))
                                {
                                    ScorePosition[2].gameObject.SetActive(false);
                                    if (_score[3].Equals("0"))
                                    {
                                        ScorePosition[3].gameObject.SetActive(false);
                                        if (_score[3].Equals("0"))
                                        {
                                            ScorePosition[3].gameObject.SetActive(false);
                                            if (_score[4].Equals("0"))
                                            {
                                                ScorePosition[4].gameObject.SetActive(false);
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                ScorePosition[i].sprite = NumCollect[0];
                            }
                        }
                    }
                    else if (_score[i].Equals("1"))
                    {
                        ScorePosition[i].sprite = NumCollect[1];
                    }
                    else if (_score[i].Equals("2"))
                    {
                        ScorePosition[i].sprite = NumCollect[2];
                    }
                    else if (_score[i].Equals("3"))
                    {
                        ScorePosition[i].sprite = NumCollect[3];
                    }
                    else if (_score[i].Equals("4"))
                    {
                        ScorePosition[i].sprite = NumCollect[4];
                    }
                    else if (_score[i].Equals("5"))
                    {
                        ScorePosition[i].sprite = NumCollect[5];
                    }
                    else if (_score[i].Equals("6"))
                    {
                        ScorePosition[i].sprite = NumCollect[6];
                    }
                    else if (_score[i].Equals("7"))
                    {
                        ScorePosition[i].sprite = NumCollect[7];
                    }
                    else if (_score[i].Equals("8"))
                    {
                        ScorePosition[i].sprite = NumCollect[8];
                    }
                    else if (_score[i].Equals("9"))
                    {
                        ScorePosition[i].sprite = NumCollect[9];
                    }
                }
            }
            //점수가 0점일 경우,
            else
            {
                for (int i = 0; i < ScorePosition.Length - 1; i++)
                {
                    ScorePosition[i].gameObject.SetActive(false);
                }
            }
        }
        else
        {
            if (currentScoreFloat != 0)
            {
                for (int i = 0; i < ScorePosition.Length; i++)
                {
                    if (_score[i].Equals("0"))
                    {
                        if (_score[0].Equals("0"))
                        {
                            ScorePosition[0].gameObject.SetActive(false);
                            if (_score[1].Equals("0"))
                            {
                                ScorePosition[1].gameObject.SetActive(false);
                                if (_score[2].Equals("0"))
                                {
                                    ScorePosition[2].gameObject.SetActive(false);
                                    if (_score[3].Equals("0"))
                                    {
                                        ScorePosition[3].gameObject.SetActive(false);
                                        if (_score[3].Equals("0"))
                                        {
                                            ScorePosition[3].gameObject.SetActive(false);
                                            if (_score[4].Equals("0"))
                                            {
                                                ScorePosition[4].gameObject.SetActive(false);
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                ScorePosition[i].sprite = NumCollect[0];
                            }
                        }
                    }
                    else if (_score[i].Equals("1"))
                    {
                        ScorePosition[i].sprite = NumCollect[1];
                    }
                    else if (_score[i].Equals("2"))
                    {
                        ScorePosition[i].sprite = NumCollect[2];
                    }
                    else if (_score[i].Equals("3"))
                    {
                        ScorePosition[i].sprite = NumCollect[3];
                    }
                    else if (_score[i].Equals("4"))
                    {
                        ScorePosition[i].sprite = NumCollect[4];
                    }
                    else if (_score[i].Equals("5"))
                    {
                        ScorePosition[i].sprite = NumCollect[5];
                    }
                    else if (_score[i].Equals("6"))
                    {
                        ScorePosition[i].sprite = NumCollect[6];
                    }
                    else if (_score[i].Equals("7"))
                    {
                        ScorePosition[i].sprite = NumCollect[7];
                    }
                    else if (_score[i].Equals("8"))
                    {
                        ScorePosition[i].sprite = NumCollect[8];
                    }
                    else if (_score[i].Equals("9"))
                    {
                        ScorePosition[i].sprite = NumCollect[9];
                    }
                }
            }
            //점수가 0점일 경우,
            else
            {
                for (int i = 0; i < ScorePosition.Length - 1; i++)
                {
                    ScorePosition[i].gameObject.SetActive(false);
                }
            }
        }
     
    }

    public void CurrentRankSearch()
    {
        StartCoroutine(_GetNetworkState());
    }

    IEnumerator _GetNetworkState()
    {
        ServerManager.Instance.GetNetworkState();

        yield return new WaitUntil(() => ServerManager.Instance.isConnCompleted);
        ServerManager.Instance.isConnCompleted = false;

        if (ServerManager.Instance.isConnected)
        {
            //StartCoroutine(_CurrentRankSearch());
            ServerManager.Instance.RankingSearching(PlayerPrefs.GetInt("GameTotalScore"));
        }
    }
}
