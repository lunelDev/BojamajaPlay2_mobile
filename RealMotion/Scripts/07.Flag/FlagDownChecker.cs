using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagDownChecker : MonoBehaviour
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
                onClick_BDown();
                left_Anim.SetTrigger("Left_DOWN");
            }
        }
        else if (other.CompareTag("R_Hand"))
        {
            if (AppManager.Instance.gameRunning)
            {
                onClick_WDown();
                right_Anim.SetTrigger("Right_DOWN");
            }
        }
    }

    //청기내려 버튼
    public void onClick_BDown()
    {
        if (FlagManager.instance.matchStart.Equals(true))
        {
            //1가지 지문
            if (FlagManager.instance.CompareRandomFlagName.Length.Equals(2))
            {
                if (FlagManager.instance._ClickButton_1.Equals(""))
                {
                    //  FlagManager.instance._ClickButton1 = new StringBuilder("BD");
                    FlagManager.instance._ClickButton_1 += "BD";
                    FlagManager.instance._ClickString_1 += "청기 내려";
                }

            }
            //2가지 지문
            else if (FlagManager.instance.CompareRandomFlagName.Length.Equals(4))
            {
                if (FlagManager.instance._ClickButton_1.Equals(""))
                {
                    // FlagManager.instance._ClickButton1 = new StringBuilder("BD");
                    FlagManager.instance._ClickButton_1 += "BD";
                    FlagManager.instance._ClickString_1 += "청기 내려";
                }
                else
                {
                    FlagManager.instance._ClickButton_2 += "BD";
                    FlagManager.instance._ClickString_2 += "청기 내려";
                }
            }

            FlagManager.instance.btnCount += 1; //버튼 클릭 횟수
            FlagManager.instance.MatchStringName();
        }
    }
    //백기내려 버튼
    public void onClick_WDown()
    {
        if (FlagManager.instance.matchStart.Equals(true))
        {
            if (FlagManager.instance.CompareRandomFlagName.Length.Equals(2))
            {
                if (FlagManager.instance._ClickButton_1.Equals(""))
                {
                    FlagManager.instance._ClickButton_1 += "WD";
                    FlagManager.instance._ClickString_1 += "백기 내려";
                }

            }
            //2가지 지문
            else if (FlagManager.instance.CompareRandomFlagName.Length.Equals(4))
            {
                if (FlagManager.instance._ClickButton_1.Equals(""))
                {
                    FlagManager.instance._ClickButton_1 += "WD";
                    FlagManager.instance._ClickString_1 += "백기 내려";
                }
                else
                {
                    FlagManager.instance._ClickButton_2 += "WD";
                    FlagManager.instance._ClickString_2 += "백기 내려";
                }
            }

            FlagManager.instance.btnCount += 1; //버튼 클릭 횟수
            FlagManager.instance.MatchStringName();
        }
    }
}