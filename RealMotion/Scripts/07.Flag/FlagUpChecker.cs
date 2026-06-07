using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlagUpChecker : MonoBehaviour
{
    [Header("Hands Animator")]
    public Animator left_Anim;
    public Animator right_Anim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("L_Hand"))
        {
            if (AppManager.Instance.gameRunning)
            {
                onClick_BUp();
                left_Anim.SetTrigger("Left_UP");
            }
        }
        else if (other.CompareTag("R_Hand"))
        {
            if (AppManager.Instance.gameRunning)
            {
                onClick_WUp();
                right_Anim.SetTrigger("Right_UP");
            }
        }
    }

    //청기올려 버튼
    public void onClick_BUp()
    {
        if (FlagManager.instance.matchStart.Equals(true))
        {
            //1가지 지문
            if (FlagManager.instance.CompareRandomFlagName.Length.Equals(2))
            {
                if (FlagManager.instance._ClickButton_1.Equals(""))
                {
                    // FlagManager.instance._ClickButton1.Append("BU");
                    FlagManager.instance._ClickButton_1 += "BU";
                    FlagManager.instance._ClickString_1 += "청기 올려";
                    //Debug.Log("_ClickButton1_matchString" + FlagManager.instance._ClickButton_1);
                }

            }
            //2가지 지문
            else if (FlagManager.instance.CompareRandomFlagName.Length.Equals(4))
            {
                if (FlagManager.instance._ClickButton_1.Equals(""))
                {
                    // FlagManager.instance._ClickButton1.Append("BU");
                    FlagManager.instance._ClickButton_1 += "BU";
                    FlagManager.instance._ClickString_1 += "청기 올려";
                }
                else
                {
                    //  FlagManager.instance._ClickButton2 = FlagManager.instance._ClickButton1.Append("BU");
                    FlagManager.instance._ClickButton_2 += "BU";
                    FlagManager.instance._ClickString_2 += "청기 올려";
                }
            }
            FlagManager.instance.btnCount += 1; //버튼 클릭 횟수
            FlagManager.instance.MatchStringName();
        }

    }

    //백기올려 버튼
    public void onClick_WUp()
    {
        if (FlagManager.instance.matchStart.Equals(true))
        {
            if (FlagManager.instance.CompareRandomFlagName.Length.Equals(2))
            {
                if (FlagManager.instance._ClickButton_1.Equals(""))
                {
                    FlagManager.instance._ClickButton_1 += "WU";
                    FlagManager.instance._ClickString_1 += "백기 올려";
                }

            }
            //2가지 지문
            else if (FlagManager.instance.CompareRandomFlagName.Length.Equals(4))
            {
                if (FlagManager.instance._ClickButton_1.Equals(""))
                {
                    FlagManager.instance._ClickButton_1 += "WU";
                    FlagManager.instance._ClickString_1 += "백기 올려";
                }
                else
                {
                    FlagManager.instance._ClickButton_2 += "WU";
                    FlagManager.instance._ClickString_2 += "백기 올려";
                }
            }
        }

        FlagManager.instance.btnCount += 1; //버튼 클릭 횟수
        FlagManager.instance.MatchStringName();
    }
}