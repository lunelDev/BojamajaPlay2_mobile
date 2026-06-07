using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 설정창 안에있는 사이트 버튼을 눌렀을 때 사이트 가 열리게하는 스크립트
/// </summary>
public class OpenURL : MonoBehaviour
{
    //고객센터 
    public void CustomerService()
    {
        Application.OpenURL("http://bojamajaplay.com/question");
    }
    //유튜브

    public void Youtube()
    {
        Application.OpenURL("https://bit.ly/395RQRp");
    }
    //홈페이지
    public void Homepage()
    {
        Application.OpenURL("http://bojamajaplay.com/");
    }

}
