using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance { get; private set; }
    float freeTime = 900; //15분 
    public float currFreeTime;
    float remain = 0; //다이아 추가해주고 남은 시간
    double quotient = 0;
    public int diamondSu;  //다이아 갯수
    int noDiamond;  //없는 다이아 갯수
                    //   int hours, minute, second;  //다이아 충전 시간
    string playStartTimeStr;    //게임 접속 시간 
    string playEndTimeStr;  //게임종료 시간
                            //  private DateTime playEndTime;
    TimeSpan noGameTime;    // 게임시작시간 - 게임종료시간(게임 안한 시간 구하기 위한 변수)
    int noGameDay = 0, noGameHour = 0, noGameMin = 0, noGameSec = 0, noGameTatal = 0;
    private bool bPaused;
    private void OnEnable()
    {
          DiamondSu_Check();
    }
    private void Awake()
    {
        var objs = FindObjectsOfType<TimeManager>();
        if (objs.Length != 1)
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this.gameObject);

        if (instance != null)
            Destroy(this);
        else
            instance = this;

        //OverTime이라는 키를 가지고 있지 않으면 
        if (!PlayerPrefs.HasKey("OverTime"))
        {
            currFreeTime = freeTime;    //시간 초기화
            PlayerPrefs.SetFloat("OverTime", currFreeTime); //900초     
        }
        //Diamod라는 키를 가지고 있지 않으면 
        if (!PlayerPrefs.HasKey("Diamond"))
        {
            PlayerPrefs.SetInt("Diamond", 10); //20개의 다이아몬드 제공
            diamondSu = PlayerPrefs.GetInt("Diamond"); //diamondSu에 20개를 넣어줌                                                  
        }
        else
        {
            currFreeTime = PlayerPrefs.GetFloat("OverTime");
            DiamondSu_Check();
        }
        //Debug.Log("다이아:" + PlayerPrefs.GetInt("Diamond"));
    }

    //게임을 종료했을 때
    private void OnApplicationQuit()
    {
        PlayEndTime(); //게임을 종료했을 때의 시간을 저장한다.
    }
    private void OnApplicationPause(bool pause)
    {
        //앱 내렸을 때
        if (pause)
        {
            bPaused = true;
            //게임 일시정지
            PlayEndTime();
            playEndTimeStr = PlayerPrefs.GetString("PlayEndTime");  //string 형식          
            DateTime playEndTime = Convert.ToDateTime(playEndTimeStr); //DateTime형식
        }
        //올렸을때
        else
        {
            bPaused = false;
            DiamondSu_Check(); //다이아 체크 
        }
    }
    public void PlayEndTime()
    {
        PlayerPrefs.SetString("PlayEndTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")); //앱 내렸을 때의 시간 저장
        PlayerPrefs.SetInt("Diamond", diamondSu);   //현재 남은 다이아몬드 저장
        PlayerPrefs.SetFloat("OverTime", currFreeTime); //현재 남은 시간저장
    }

    //대기한 만큼의 다이아를 제공
    public void DiamondSu_Check()
    {
        PlayerPrefs.SetString("PlayStartTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));   // 현재 접속시간 저장
        //게임 시작 시간을 가지고옴
        playStartTimeStr = PlayerPrefs.GetString("PlayStartTime");  //String형식                          
        DateTime playStartTime = Convert.ToDateTime(playStartTimeStr);   //DateTime형식 

        //게임을 종료한 시간을 가지고옴
        playEndTimeStr = PlayerPrefs.GetString("PlayEndTime");  //string 형식          
        DateTime playEndTime = Convert.ToDateTime(playEndTimeStr); //DateTime형식

        // 시작시간에서 내린시간 빼서 차를 구한다.
        noGameTime = playStartTime - playEndTime;
        noGameDay = noGameTime.Days;    //날
        noGameHour = noGameTime.Hours;  //시간
        noGameMin = noGameTime.Minutes; //분
        noGameSec = noGameTime.Seconds; //초

        //총 시간을 초로 변경해서 저장
        noGameTatal = (noGameDay * 24 * 60 * 60) + (noGameHour * 60 * 60) + (noGameMin * 60) + noGameSec;
    
        ///////////////////////게임하지 않은 시간을 구해서 다이아몬드 충전해주는 조건식////////////////////////////

        diamondSu = PlayerPrefs.GetInt("Diamond");  //저장 된 다이아몬드 갯수 들고오기
        currFreeTime = PlayerPrefs.GetFloat("OverTime");    //저장 된 시간 들고오기
        Debug.Log(" 1 timte : "+ currFreeTime);
        noDiamond = 20 - diamondSu; //모자란 다이아를 계산
        quotient = System.Math.Truncate((noGameTatal / freeTime)); //몫 값
        //다이아 차이가 1개 이상일 떄
        if (noDiamond > 0)
        {
            //시간 차가 하루 이상이면
            if (noGameDay > 0)
            {
                DiamondMax_and_TimerInit();
            }
            //시간 차가 하루가 지나지 않았으면
            else
            {
                //시간 차가 1시간 이상일 때
                if (noGameTatal >= 3600)
                {
                    //안한시간이 2시간 30분을 넘겼을 때
                    if (noGameTatal >= 18000) //20*15분
                    {
                        DiamondMax_and_TimerInit();
                    }
                    else
                    {
                        //게임을 안한시간에 대한 다이아 보상에 기존 다이아보상을 합쳤을 때, 10개 이상일 경우 
                        if (noDiamond <= quotient) //noDiamond는 부족한 다이아 수
                        {
                            DiamondMax_and_TimerInit(); //10개 
                        }
                        //쉬는 동안 무료 보상 10개를 충족하지 못했을 때
                        else
                        {
                            diamondSu += (int)quotient;//게임을 안한 초만큼 다이아몬드 충전
                            remain = noGameTatal - (freeTime * (int)quotient); //남은 초 
                            Debug.Log("111");
                            Min_and_SecTimeSetting();
                        }
                    }
                }
                //시간 차가 1시간이 안될때
                else
                {
                    //게임을 안한시간에 대한 다이아 보상에 기존 다이아보상을 합쳤을 때, 10개 이상일 경우 
                    if (noDiamond <= quotient) //noDiamond는 부족한 다이아 수
                    {
                        DiamondMax_and_TimerInit(); //10개 
                    }
                    //쉬는 동안 무료 보상 10개를 충족하지 못했을 때
                    else
                    {
                        diamondSu += (int)quotient;//게임을 안한 초만큼 다이아몬드 충전
                        remain = noGameTatal - (freeTime * (int)quotient); //남은 초                      
                        Debug.Log("222");
                        Min_and_SecTimeSetting();
                    }
                }
            }
        }
    }

    //다이아몬드 맥스, 시간 초기화 해주는 함수
    void DiamondMax_and_TimerInit()
    {
        diamondSu = 10; //다이아 max로 지정
        PlayerPrefs.SetInt("Diamond", diamondSu);
        currFreeTime = freeTime;    //시간 만땅
    }

    //남은 시간과 안한시간(분,초)에 대한 마무리 작업해주는 함수
    void Min_and_SecTimeSetting()
    {
        //남은시간이 15분 보다 적게 남았을때
        if (remain < freeTime)
        {
            currFreeTime = currFreeTime - remain;
            PlayerPrefs.SetInt("Diamond", diamondSu);
            PlayerPrefs.SetFloat("OverTime", currFreeTime); //시간저장

            Debug.Log(" 2 timte : " + currFreeTime);
            // Debug.Log("현재 시간에서 남은시간 제거후 : " + currFreeTime);
        }
    }
    void Update()
    {
        //다이아몬드가 10개보다 작을 경우, 타이머 돌아가기
        if (diamondSu < 10)
        {
            Timeing();
        }

        else if (diamondSu >= 10)
        {
            currFreeTime = freeTime;
            PlayerPrefs.SetFloat("OverTime", currFreeTime); //시간저장
            PlayerPrefs.SetInt("Diamond", diamondSu);
        }
    }
    //다이아몬드가 10개보다 작을경우, 타이머 움직임
    void Timeing()
    {
        int absRE = System.Math.Abs((int)currFreeTime);
        currFreeTime -= Time.deltaTime;
        if (currFreeTime <= 0f)
        {
            diamondSu += 1; //다이아 갯수 증가
            currFreeTime = freeTime - absRE; //타이머 초기화       
        }
        PlayerPrefs.SetInt("Diamond", diamondSu);
        PlayerPrefs.SetFloat("OverTime", currFreeTime); //시간저장

        Debug.Log("현재시간 : " + currFreeTime);
    }
    //게임 종료 시간


    //start버튼을 눌렀을 때 다이아몬드 감소 
    //메인가기를 눌렀을 떄. 시간 저장
    public void SaveTime_GoMain()
    {
        DiamondSu_Check();
    }
    //계속 하기를 했을 떄, 시간 저장
    public void SaveTime_Continue()
    {
        DiamondSu_Check();
    }
}