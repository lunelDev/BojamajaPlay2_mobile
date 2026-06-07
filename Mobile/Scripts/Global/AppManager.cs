using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AppManager : MonoBehaviour
{
    public static AppManager Instance { get; private set; }
    public GameObject countdownPanel;
    Image coutndownPanel_Color;
    public GameObject StartCountImg;
    Image StartCountImg_Color;
    //public Sprite ClassicTutorialImg;
    public GameObject GameStartImg;

    public bool gameRunning { get; set; }
    public UnityEvent onStart = null; //시작 할 때, 이벤트
    public UnityEvent onEnd = null; //끝 났을 때, 이벤트 
    SceneLoader _sceneLoader;
    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else Instance = this;
        coutndownPanel_Color = countdownPanel.GetComponent<Image>();
        StartCountImg_Color = StartCountImg.GetComponent<Image>();
        //Time.timeScale = 1f;
    }
    private void Start()
    {

        StartCountdown();

    }
    void OnEnable()
    {
        SetInitialReferences();
        // StartCountdown();
        Timer.RoundEnd += OnRoundEnd;

    }
    //비활성화시 호출되는 함수 
    void OnDisable()
    {
        Timer.RoundEnd -= OnRoundEnd;
    }

    public void ResetLevel()
    {
        //New Scene을 현재 씬에 추가적으로 로드(single은 기존 씬 close)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
    //초기 참조 설정
    protected virtual void SetInitialReferences()
    {
        _sceneLoader = FindObjectOfType<SceneLoader>();
    }
    //게임을 시작 할때 카운트 다운 시작
    public void StartButtonON()
    {
        StartCoroutine(StartCountdown_());
    }
    public void StartCountdown()
    {
        StopAllCoroutines();
        StartCoroutine(StartCountdown_());

    }
    IEnumerator StartCountdown_()
    {
        //랭킹모드 일 경우
        if (GameManager.RankMode.Equals(true))
        {
            UIManager.Instance.panel_Intro.SetActive(true);
            yield return new WaitForSeconds(1f);
            UIManager.Instance.panel_Intro.SetActive(false);
            countdownPanel.SetActive(true); //카운트 다운 판넬 활성화 
            yield return new WaitForSeconds(1f);
            SoundManager.Instance.countdown.Play(); //카운트 다운 사운드 Play
            yield return new WaitForSeconds(2.5f);
            AccelerometerCalibrator.Calibrate(); //
        }
        //클래식 모드일 경우
        else
        {
            GameManager.countForAdvertising += 1; //다음 광고까지 횟수 
            PlayerPrefs.SetInt("AdPlayCount", GameManager.countForAdvertising); //추가된 광고 횟수 저장하기 
            //countdownPanel.GetComponent<Image>().sprite = ClassicTutorialImg;
            countdownPanel.SetActive(true); //카운트 다운 판넬 활성화 
            StartCountImg.SetActive(true);
            yield return new WaitForSeconds(1f);//2
            SoundManager.Instance.countdown.Play(); //카운트 다운 사운드 Play         
            yield return new WaitForSeconds(2f);//2
            InStartFadeAnim();
            yield return new WaitForSeconds(0.5f);//1       
            OnRoundStart();
            countdownPanel.SetActive(false);
            AccelerometerCalibrator.Calibrate(); //
        }

        yield return null;
    }

    public void InStartFadeAnim()
    {
        StartCoroutine(fadeoutplay());
    }

    IEnumerator fadeoutplay()
    {
        Color Panelcolor1 = StartCountImg_Color.color;
        Color Panelcolor2 = coutndownPanel_Color.color;
        float time = 0f;
        float start = 1;
        float end = 0;
        float FadeTime = 0.5f;

        Panelcolor1.a = Mathf.Lerp(start, end, time);
        while (Panelcolor1.a > 0f)
        {
            time += Time.deltaTime / FadeTime;
            Panelcolor1.a = Mathf.Lerp(start, end, time);
            Panelcolor2.a = Mathf.Lerp(start, end, time);
            StartCountImg_Color.color = Panelcolor1;
            coutndownPanel_Color.color = Panelcolor2;
            yield return null;
        }



    }
    //씬 로드 
    public void GlobalLoad(int index)
    {
        _sceneLoader.LoadScene(index);
    }
    //라운드 시작
    public void OnRoundStart()
    {
        AccelerometerCalibrator.Calibrate(); //장치의 움직임 받음
        StartCoroutine(_OnRoundStart()); //코루틴 시작
    }

    protected virtual IEnumerator _OnRoundStart()
    {
        GameStartImg.SetActive(true);
        yield return new WaitForSeconds(1f);
        GameStartImg.SetActive(false);
        yield return UIManager.Instance.OnRoundStart(); //UI
        yield return DataManager.Instance.OnRoundStart(); //Data

        gameRunning = true; //bool check
        if (onStart != null) onStart.Invoke(); //onStart Event 실행
    }
    //라운드 끝
    protected void OnRoundEnd()
    {
        StartCoroutine(_OnRoundEnd());
    }

    protected virtual IEnumerator _OnRoundEnd()
    {
        if (onEnd != null) onEnd.Invoke(); //onEnd Event 실행
        yield return DataManager.Instance.OnRoundEnd(); //Data
        yield return UIManager.Instance.OnRoundEnd(); //UI
        gameRunning = false; //Bool unCheck       
        yield return new WaitForSeconds(0.5f);
        ShowAd();
    }

    void ShowAd()
    {
        //클래식 모드일 경우, 광고 재생
        if (GameManager.countForAdvertising >= 2 && GameManager.RankMode.Equals(false))
        {
            if (GameManager.HaveAdRemove.Equals(false))
            {
                AdManager.Instance.ShowScreenAd();
                GameManager.countForAdvertising = 0;
                PlayerPrefs.SetInt("AdPlayCount", GameManager.countForAdvertising); //클래식 모드 광고 초기화 // 0
            }
            else
            {
                GameManager.HaveAdRemoveCount -= 1;
                PlayerPrefs.SetInt("RemoveADCount", GameManager.HaveAdRemoveCount);
            }

        }
        //랭킹 모드일 경우, 광고 재생
        if ((GameManager.RandListCountCheck.Equals(3) || GameManager.RandListCountCheck.Equals(7)) && GameManager.RankMode.Equals(true))
        {
            if (GameManager.HaveAdRemove.Equals(false))
            {
                AdManager.Instance.ShowScreenAd();
            }
            else
            {
                GameManager.HaveAdRemoveCount -= 1;
                PlayerPrefs.SetInt("RemoveADCount", GameManager.HaveAdRemoveCount);
            }
        }
    }
}