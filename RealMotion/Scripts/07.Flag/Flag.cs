using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
public class Flag : MonoBehaviour
{
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
                    Debug.Log("_ClickButton1_matchString" + FlagManager.instance._ClickButton_1);
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
            // print(FlagManager.instance._ClickButton1);
            FlagManager.instance.MatchStringName();
            FlagManager.instance.btnCount += 1; //버튼 클릭 횟수
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
            //print(FlagManager.instance._ClickButton1);
            FlagManager.instance.MatchStringName();
            FlagManager.instance.btnCount += 1; //버튼 클릭 횟수
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
        //print(FlagManager.instance._ClickButton1);
        FlagManager.instance.MatchStringName();
        FlagManager.instance.btnCount += 1; //버튼 클릭 횟수
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
                    // FlagManager.instance._ClickButton_1.Append("WU");
                    FlagManager.instance._ClickButton_1 += "WD";
                    FlagManager.instance._ClickString_1 += "백기 내려";
                }
                else
                {
                    FlagManager.instance._ClickButton_2 += "WD";
                    FlagManager.instance._ClickString_2 += "백기 내려";
                    //FlagManager.instance._ClickButton2 = FlagManager.instance._ClickButton1.Append("WU");
                }
            }
            FlagManager.instance.MatchStringName();
            FlagManager.instance.btnCount += 1; //버튼 클릭 횟수
        }
    }
    
}
