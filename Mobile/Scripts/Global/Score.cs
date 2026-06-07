using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int score = 0;
    public int highscore;
    public int starPick = 0;
    public Text text_Score;
    //   public Image starRating;
    public Sprite[] stars;
    public Slider Slider;
    int Min;

    public GameObject[] StarPickOb;
    private void Awake()
    {
        Slider.maxValue = highscore;
        Min = highscore/5;
    }
    //스코어를 Points값으로 설정 +텍스트 적용
    public void Set(float points)
    {
        score = (int)points;
        text_Score.text = score.ToString() + "p";
    }
    //스코어 점수를 추가 해줌 + 텍스트 적용
    public void Add(float points)
    {
        score += (int)points;
        text_Score.text = score.ToString() + "p";
    }
    //점수 감소
    public void Subtract(float points)
    {
        if (score - points < 0)   //0보자 작으면 점수를 0으로 고정
            Set(0);
        else
            Add(-points);   // points 값만큼 점수 감소
    }
    //게임 중, 별 점수 체크 
    public void UpdateStarRating()
    {
        //클래식 모드 일 경우
        if (GameManager.RankMode.Equals(false))
        {
            StartCoroutine(UpdateStarRating_());
        }
    }
    IEnumerator UpdateStarRating_()
    {
        Min = (highscore / 4) + 1;
        
        yield return new WaitUntil(() => AppManager.Instance.gameRunning.Equals(true));
        while (AppManager.Instance.gameRunning.Equals(true))
        {
            //스코어가 0이상이고, 하이스코어보다 작을 때 ( 맥스 별 4개까지 나옴)
            if (score != 0 && score <= highscore)
                starPick = (int)((score / Min) + 1);
            else if (score > highscore) // 하이스코어 이상일 때 별은 항상 5개이다.
                starPick = 5;
            if (score <= 0)
                starPick = 0;
            yield return null;
        }
        yield return null;
    }
    int LeftStar;
    int Check = 0;
    private void Update()
    {
        if (AppManager.Instance.gameRunning.Equals(true))
        {
            Slider.value = score; // 현재 점수에 따라 슬라이드 움직임
            if(starPick == 0)
            {
                StarPickOb[0].SetActive(false);
            }
            else if (starPick == 1)
            {
                StarPickOb[0].SetActive(true);
               // SoundManager.Instance.PlaySFX("별획득사운드_alt1");
                //점수가 낮아졌을 때, 앞이 별이 켜져있으면 꺼줌
                if (StarPickOb[1].activeSelf == true){
                    StarPickOb[1].SetActive(false);
                }
            }
            else if (starPick == 2)
            {
               // SoundManager.Instance.PlaySFX("별획득사운드_alt1");
                StarPickOb[0].SetActive(true);
                StarPickOb[1].SetActive(true);
                //점수가 낮아졌을 때, 앞이 별이 켜져있으면 꺼줌
                if (StarPickOb[2].activeSelf == true)
                {
                    StarPickOb[2].SetActive(false);
                }
            }
            else if (starPick == 3)
            {
               // SoundManager.Instance.PlaySFX("별획득사운드_alt1");
                StarPickOb[0].SetActive(true);
                StarPickOb[1].SetActive(true);
                StarPickOb[2].SetActive(true);
                //점수가 낮아졌을 때, 앞이 별이 켜져있으면 꺼줌
                if (StarPickOb[3].activeSelf == true)
                {
                    StarPickOb[3].SetActive(false);
                }
            }
            else if (starPick == 4)
            {
               // SoundManager.Instance.PlaySFX("별획득사운드_alt1");
                StarPickOb[0].SetActive(true);
                StarPickOb[1].SetActive(true);
                StarPickOb[2].SetActive(true);
                StarPickOb[3].SetActive(true);
                //점수가 낮아졌을 때, 앞이 별이 켜져있으면 꺼줌
                if (StarPickOb[4].activeSelf == true)
                {
                    StarPickOb[4].SetActive(false);
                }
            }
            //별 5개
            else if (starPick == 5)
            {
               // SoundManager.Instance.PlaySFX("별획득사운드_alt1");
                StarPickOb[0].SetActive(true);
                StarPickOb[1].SetActive(true);
                StarPickOb[2].SetActive(true);
                StarPickOb[3].SetActive(true);
                StarPickOb[4].SetActive(true);
               
            }

        }
    }
}