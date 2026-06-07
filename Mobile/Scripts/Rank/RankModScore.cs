using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RankModScore : MonoBehaviour
{
    public Sprite[] NumCollect; //이미지에 쓰일 sprite
    public Image[] ScorePosition; //rank 판넬안의 이미지 위치

    string currentScoreStr;
    string[] _score; //문자열 하나하나 담기 
    int currentScoreInt;
    private void Awake()
    {
        _score = new string[5];
        if (SceneManager.GetActiveScene().name != "EndScene")
        {
            currentScoreStr = DataManager.Instance.scoreManager.score.ToString("D5");
        }
        else
        {
            currentScoreInt = PlayerPrefs.GetInt("GameTotalScore");
            currentScoreStr = PlayerPrefs.GetInt("GameTotalScore").ToString("D5");
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
            _score[i] = currentScoreStr.Substring(i, 1);
            Debug.Log(_score[i]);
        }
        SetScoreSprite();
    }
    void SetScoreSprite()
    {
        if (SceneManager.GetActiveScene().name != "EndScene")
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
            if (currentScoreInt != 0)
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
                            /*  else
                              {
                                  ScorePosition[i].sprite = NumCollect[0];
                              }*/
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
                ScorePosition[4].sprite = NumCollect[0];
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

        if (ServerManager.Instance.isConnected)
        {
            //StartCoroutine(_CurrentRankSearch());
            ServerManager.Instance.RankingSearching(PlayerPrefs.GetInt("GameTotalScore"));
        }
    }

    //IEnumerator _CurrentRankSearch()
    //{
    //    // 서버에 서치 닉네임
    //    ServerManager.Instance.RankingSearching(PlayerPrefs.GetInt("GameTotalScore"));

    //    yield return new WaitUntil(() => ServerManager.Instance.isSearchComplete);

    //    // 있으면 중복으로 존재하는 닉네임 처리 
    //    //if (ServerManager.Instance.isExistName)
    //    //{
    //    //    // 닉네임이 있으면
    //    //    DuplicatedNamePanel.SetActive(true);
    //    //    GameManager.Instance.OnClick_BtnSound3();
    //    //    StartCoroutine(_DisappearPopUp());
    //    //}
    //    //else
    //    //{
    //    //    // 닉네임이 없으면 등록하기
    //    //    ServerManager.Instance.RankingReg(nickname.text);

    //    //    RegCompleted.SetActive(true);
    //    //}

    //    ServerManager.Instance.isSearchComplete = false;
    //}
}