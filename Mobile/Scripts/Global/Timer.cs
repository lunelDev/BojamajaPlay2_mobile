using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.Text;

public class Timer : MonoBehaviour
{
    [HideInInspector]
    public float timeLeft { get; private set; }
    public int roundLength;

    public GameObject TimebarUI;
    public Text _TimeTxt;
    public Text _ScoreTxt;
    public Color Red;
    public Color White;

    public static UnityAction RoundEnd = null;
    public static UnityAction RoundStart = null;

    bool _warningOn;

    public void StartTimer()
    {
        timeLeft = roundLength;
        _warningOn = false;
        StartCoroutine(Clock());
    }

    private IEnumerator Clock()
    {
        if (RoundStart != null)
            RoundStart.Invoke();
        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            _TimeTxt.text = timeLeft.ToString("N2") + "s";
            yield return new WaitForEndOfFrame();

            //5초 이하 남았을 떄, 사운드 플레이
            if (timeLeft <= 5f && _warningOn == false)
            {
                _warningOn = true;
                SoundManager.Instance.timer.Play();
                Left5Min();
            }
        }
        timeLeft = 0;
        _TimeTxt.text = timeLeft.ToString("N2") + "s";
        //점수가 0보다 크면 성공 사운드 플레이
        if (DataManager.Instance.scoreManager.score > 0)
        {
            SoundManager.Instance.success.Play();
        }
        else
            SoundManager.Instance.fail.Play();
        //게임 끝났을 때 함수 실행
        if (RoundEnd != null)
            RoundEnd.Invoke();
    }
    //순간적으로 캔버스 뿌옇게 만들어줌
    public void Left5Min()
    {
        StartCoroutine(_Left5Min());
    }
    IEnumerator _Left5Min()
    {
        while (AppManager.Instance.gameRunning.Equals(true))
        {
            float value = 1;
            Red.a = value;
            _TimeTxt.color = Red;
            while (value > 0.1f)
            {
                value -= Time.deltaTime * 5f;
                if (GameManager.RankMode.Equals(true))
                {
                    Red.a = value;
                    _TimeTxt.color = Red;
                }
                yield return null;
            }
            //점수는 다시 검정색으로 만들어주기   
            White.a = 1;
            _TimeTxt.color = White;
            yield return new WaitForSeconds(0.2f);
        }
        yield return null;
        Red.a = 1;
        _TimeTxt.color = Red;
    }
}
