using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 소모가능한 다이아몬드 갯수와 다음 무료 다이아몬드까지 남은 시간을 측정해주는 코드
/// </summary>
public class DiamondTracking : MonoBehaviour
{
    public Text DiaChargeTime; //다이아 남은 충전 시간
    public Text Diamond;        //현재 남은 다이아 
    private float time;
    int diaSu; 
    int hours, minute, second;  //다이아 충전 시간
    void OnEnable()
    {
        TimeManager.instance.DiamondSu_Check();
        DiaChargeTime = transform.Find("LeftTime").GetComponent<Text>();   // Timer 객체
        Diamond = transform.Find("Life").GetComponent<Text>();         // Heart 객체
    }
    // Update is called once per frame
    void Update()
    {
       InText();
    }

    private void InText()
    {
        diaSu = PlayerPrefs.GetInt("Diamond");
        time = TimeManager.instance.currFreeTime; // 현재 시간
        hours = (int)time / 3600;  //시
        minute = (int)time % 3600 / 60;    //분
        second = (int)time % 3600 % 60;    //초

        if (diaSu >= 20)
        {
            DiaChargeTime.text = " ";
            Diamond.text = diaSu.ToString() + "/10";//PlayerPrefs.GetInt("Diamond").ToString() + "/20";
        }
        else
        {
            DiaChargeTime.text = minute.ToString() + ":" + second.ToString("D2");
           // Diamond.text = TimeManager.instance.diamondSu.ToString()+"/20";
            Diamond.text = diaSu.ToString() + "/10"; //PlayerPrefs.GetInt("Diamond").ToString()+"/20";
        }
    }
}
