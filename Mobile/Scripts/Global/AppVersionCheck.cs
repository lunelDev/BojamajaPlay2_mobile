using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

public class AppVersionCheck : MonoBehaviour
{
    public string URL = ""; // 버전체크를 위한 URL
    public GameObject newVersionAvailable; // 버전확인 UI

    void Start()
    {
        StartCoroutine(ActionVersionCheck());
    }

    IEnumerator ActionVersionCheck()
    {
        #if !UNITY_EDITOR && UNITY_ANDROID
                string _AppID = "https://play.google.com/store/apps/details?id=com.gateways.bojamaja2";
        #elif UNITY_EDITOR
            string _AppID = "https://play.google.com/store/apps/details?id=com.gateways.bojamaja2";
        #endif

        UnityWebRequest _WebRequest = UnityWebRequest.Get(_AppID);

        yield return _WebRequest.SendWebRequest();

        // 정규식으로 전체 문자열중 버전 정보가 담겨진 태그를 검색한다.
        string _Pattern = @"<span class=""htlgb"">[0-9]{1,3}[.][0-9]{1,3}[.][0-9]{1,3}<";
        Regex _Regex = new Regex(_Pattern, RegexOptions.IgnoreCase);    // RegexOptions.IgnoreCase 대소문자 구분 안함
        Match _Match = _Regex.Match(_WebRequest.downloadHandler.text);

        /*
                 메타문자  의미            
        ------------------------
        ^        라인의 처음      
        $        라인의 마지막    
        \w       문자(영숫자) [a-zA-Z_0-9]
        \s       Whitespace (공백,뉴라인,탭..)
        \d       숫자
        *        Zero 혹은 그 이상
        +        하나 이상
        ?        Zero 혹은 하나
        .        Newline을 제외한 한 문자
        [ ]      가능한 문자들
        [^ ]     가능하지 않은 문자들
        [ - ]    가능 문자 범위
        {n,m}    최소 n개, 최대 m개
        (  )     그룹
        |        논리 OR
         */

        if (_Match != null)
        {
            // 버전 정보가 담겨진 태그를 찾음
            // 해당 태그에서 버전 넘버만 가져온다
            _Match = Regex.Match(_Match.Value, "[0-9]{1,3}[.][0-9]{1,3}[.][0-9]{1,3}");

            try
            {

                Debug.Log("Application.version : " + Application.version);
                Debug.Log("_Match.Value : " + _Match.Value);

                Debug.Log("  Application.version : " + Application.version + ", AppStore version :" + _Match.Value);

                // 같으면
                if (Application.version.Equals(_Match.Value))
                {
                    yield break;
                }
                else // 같지 않으면
                {
                    newVersionAvailable.SetActive(true);
                    PopUpSystem.PopUpState = true;
                }
            }
            catch (Exception Ex)
            {
                // 비정상 버전정보 파싱중 Exception처리

                Debug.LogError("비정상 버전 정보 Exception : " + Ex);
                Debug.LogError("  Application.version : " + Application.version + ", AppStore version :" + _Match.Value);
            }
        }
        else
        {
            Debug.LogError("Not Found AppStoreVersion Info");
        }
    }
    
    // 앱 다운로드 URL
    public void GoURL()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.gateways.bojamaja2");
    }
}
