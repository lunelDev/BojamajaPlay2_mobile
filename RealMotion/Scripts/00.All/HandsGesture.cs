using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HandsGesture : MonoBehaviour
{
    private bool isExtendedThumb;
    private bool isExtendedIndex;
    private bool isExtendedMiddle;
    private bool isExtendedRing;
    private bool isExtendedPinky;

    [Header("GuidLine")]
    public float leftTime = 3f;
    public GameObject guidLineObj;
    public Text guidLineText;

    public GameObject RankingZonePanel;
    public GameObject RankingShowScore;
    public PopUpPanelManager _popUpManager;
    public GameObject EndScene_RankRegPopUp;
    public RankRegistration rankRegistration;

    bool active_Panel = false;
    bool adaptation_time = true;
    bool protection_time = true;

    // Start is called before the first frame update
    void Start()
    {
        isExtendedThumb = false;
        isExtendedIndex = false;
        isExtendedMiddle = false;
        isExtendedRing = false;
        isExtendedPinky = false;
        active_Panel = false;
        adaptation_time = true;
        protection_time = true;
        if(SceneManager.GetActiveScene().name == "EndScene")
        {
            EndScene_RankRegPopUp.SetActive(false); // 랭킹패널 없애기
        }
    
    }

    IEnumerator _Adaptation_Time(float time)
    {
        yield return new WaitForSeconds(time);
        adaptation_time = false;
        protection_time = true;
    }

    private void Update()
    {
        if (adaptation_time)    // 적응시간
        {
            if (protection_time)
            {
                protection_time = false;
                StopAllCoroutines();
                StartCoroutine(_Adaptation_Time(3f)); // adaptation_time = false 동작
            }
            return;
        }

        if (SceneManager.GetActiveScene().name == "Ranking")
        {
            if (isExtendedThumb && isExtendedIndex && !isExtendedMiddle && !isExtendedRing && isExtendedPinky)
            {
                if ( RankingShowScore.activeSelf.Equals(false))
                {
                    Debug.Log("----------1");
                    leftTime -= Time.deltaTime * 0.8f;
                    guidLineText.text = "랭킹게임 창으로 이동합니다. 이동까지 " + leftTime.ToString("N0") + "초";
                    // 엄검소지 펴고, 나머지접음
                    // 2초 지속시간 딜레이
                    guidLineObj.SetActive(true);
                    if (leftTime < 0f)
                    {
                        ServerManager.Instance.RankingSearch(5);
                        SceneManager.LoadScene("Main");
                        GameManager.RankPanel = true;
                        guidLineObj.SetActive(false);
                        leftTime = 2f;
                        adaptation_time = true; // 적응시간
                    }
                }
                else if (RankingShowScore.activeSelf.Equals(true))
                {
                    Debug.Log("----------2");
                    leftTime -= Time.deltaTime * 0.8f;
                    guidLineText.text = "창을 닫습니다. 이동까지 " + leftTime.ToString("N0") + "초";
                    // 엄검소지 펴고, 나머지접음
                    // 2초 지속시간 딜레이
                    guidLineObj.SetActive(true);
                    if (leftTime < 0f)
                    {
                        guidLineObj.SetActive(false);
                        RankingShowScore.SetActive(false);
                        RankingRealHand.OnScorePanel = false;
                        leftTime = 2f;
                        adaptation_time = true; // 적응시간
                    }
                }
             
            }
            else
            {
                guidLineObj.SetActive(false);
                leftTime = 2f;
            }
        }
        //////////////////////////// 메인화면 제스쳐 /////////////////////////////////////////////////////
        // 게임 중이 아닌 경우
        // 뒤 로 가 기!
        else if (SceneManager.GetActiveScene().name == "Main")
        {
            // 클래식 모드 패널
            if (isExtendedThumb && isExtendedIndex && !isExtendedMiddle && !isExtendedRing && isExtendedPinky && GameManager.ClassicPanel)
            {
                leftTime -= Time.deltaTime * 0.8f;
                guidLineText.text = "모드선택으로 이동합니다. 이동까지 " + leftTime.ToString("N0") + "초";
                // 엄검소지 펴고, 나머지접음
                // 2초 지속시간 딜레이
                guidLineObj.SetActive(true);
                if (leftTime < 0f)
                {
                    GameManager.ClassicPanel = false;
                    GameManager.ModePanel = true;
                    guidLineObj.SetActive(false);
                    adaptation_time = true; // 적응시간
                }
            }
            // 랭킹 모드 패널
            else if (isExtendedThumb && isExtendedIndex && !isExtendedMiddle && !isExtendedRing && isExtendedPinky && GameManager.RankPanel)
            {
                leftTime -= Time.deltaTime * 0.8f;
                guidLineText.text = "모드선택 창으로 이동합니다. 이동까지 " + leftTime.ToString("N0") + "초";
                // 엄검소지 펴고, 나머지접음
                // 2초 지속시간 딜레이
                guidLineObj.SetActive(true);

                if (leftTime < 0f)
                {
                    GameManager.RankPanel = false;
                    GameManager.ModePanel = true;
                    guidLineObj.SetActive(false);
                    adaptation_time = true; // 적응시간
                }
            }
            // 랭킹 스타트 버튼 눌렀을 경우, 
            else if (isExtendedThumb && isExtendedIndex && !isExtendedMiddle && !isExtendedRing && isExtendedPinky && GameManager.ChangePanel)
            {
                leftTime -= Time.deltaTime * 0.8f;
                guidLineText.text = "랭킹모드 창으로 이동합니다. 이동까지 " + leftTime.ToString("N0") + "초";
                // 엄검소지 펴고, 나머지접음
                // 2초 지속시간 딜레이
                guidLineObj.SetActive(true);

                if (leftTime < 0f)
                {
                    GameManager.RankPanel = true;
                    GameManager.ChangePanel = false;
                    guidLineObj.SetActive(false);
                    adaptation_time = true; // 적응시간
                }
            }
            else
            {
                guidLineObj.SetActive(false);
                leftTime = 2f;
            }
        }
        else if (SceneManager.GetActiveScene().name == "EndScene")
        {
            ///////////////////////////////// 랭킹등록 판넬 //////////////////////////////////////////////////
            if (active_Panel.Equals(true) && End_UIManager.Instance.Ranking_registration.Equals(true))  // 랭킹패널이 활성화되어있다면
            {
                if (isExtendedThumb && !isExtendedIndex && !isExtendedMiddle && !isExtendedRing && !isExtendedPinky)    // 1 : 엄지
                {
                    leftTime -= Time.deltaTime * 2f;
                    // 엔드씬
                    Debug.Log("엄지 : 랜덤닉네임");

                    // 엄검소지 펴고, 나머지접음
                    // 2초 지속시간 딜레이
                    guidLineObj.SetActive(true);

                    guidLineText.text = "닉네임 생성. " + leftTime.ToString("N0") + "초";
                    if (leftTime < 0f)
                    {
                        GameManager.Instance.OnClick_BtnSound1();
                        // 닉네임 랜덤 생성 - 함수 : 8글자 제한
                        rankRegistration.RandomNickName(8);
                        guidLineObj.SetActive(false);
                        leftTime = 2f;

                        adaptation_time = true; // 적응시간
                    }
                }   // 2 : 엄검지
                else if (isExtendedThumb && isExtendedIndex && !isExtendedMiddle && !isExtendedRing && !isExtendedPinky)
                {
                    leftTime -= Time.deltaTime * 0.8f;
                    // 2초 지속시간 딜레이
                    guidLineObj.SetActive(true);
                    Debug.Log("검지 : 랭킹등록");

                    //토탈점수
                    guidLineText.text = "랭킹등록 중.. " + leftTime.ToString("N0") + "초";
                    if (leftTime < 0f)
                    {
                        GameManager.Instance.OnClick_BtnSound1();
                        // 닉네임 등록하기 : RankRegistration 스크립트 접근
                        rankRegistration.SearchingNickName();
                        guidLineObj.SetActive(false);
                        leftTime = 2f;

                        //EndScene_RankRegPopUp.SetActive(false); // 랭킹패널 없애기

                        adaptation_time = true; // 적응시간
                    }
                }  // 3 : 엄검중지
                else if (isExtendedThumb && isExtendedIndex && isExtendedMiddle && !isExtendedRing && !isExtendedPinky)
                {
                    leftTime -= Time.deltaTime * 0.8f;
                    // 엄검소지 펴고, 나머지접음
                    // 2초 지속시간 딜레이
                    guidLineObj.SetActive(true);
                    Debug.Log("검지 : 취소");

                    guidLineText.text = "취소 중.. " + leftTime.ToString("N0") + "초";
                    if (leftTime < 0f)
                    {
                        GameManager.Instance.OnClick_BtnSound2();
                        active_Panel = false;
                        EndScene_RankRegPopUp.SetActive(false);    // 랭크등록패널 비활성화
                        guidLineObj.SetActive(false);
                        leftTime = 2f;

                        adaptation_time = true; // 적응시간
                    }
                }
                else
                {
                    guidLineObj.SetActive(false);
                    leftTime = 2f;
                }
            }
            /////////////////////////////////////// 편지지화면 판넬 ////////////////////////////////////////////////////////
            // 1 : 엄지
            else if (isExtendedThumb && !isExtendedIndex && !isExtendedMiddle && !isExtendedRing && !isExtendedPinky && active_Panel.Equals(false))   // 랭킹패널이 활성화되어있지 않다면
            {
                leftTime -= Time.deltaTime * 0.8f;
                //랭킹 모드 
                Debug.Log("엄지 : 랭킹등록");
                // 엄검소지 펴고, 나머지접음
                // 2초 지속시간 딜레이
                guidLineObj.SetActive(true);

                guidLineText.text = "랭킹등록 선택 " + leftTime.ToString("N0") + "초";
                if (leftTime < 0f)
                {
                    GameManager.Instance.OnClick_BtnSound1();
                    active_Panel = true;
                    // 등록하고 나서 랭킹 보기 팝업
                    EndScene_RankRegPopUp.SetActive(true);
                    guidLineObj.SetActive(false);
                    leftTime = 2f;

                    adaptation_time = true; // 적응시간
                }
            }   // 2 : 엄검지
            else if (isExtendedThumb && isExtendedIndex && !isExtendedMiddle && !isExtendedRing && !isExtendedPinky && active_Panel.Equals(false))
            {
                leftTime -= Time.deltaTime * 0.8f;
                // 2초 지속시간 딜레이
                guidLineObj.SetActive(true);
                Debug.Log("검지 : 메인화면");

                //토탈점수
                guidLineText.text = "메인화면 이동까지 " + leftTime.ToString("N0") + "초";
                if (leftTime < 0f)
                {
                    GameManager.Instance.OnClick_BtnSound1();
                    // 메인화면 이동 스크립트
                    GameManager.Instance.OnClick_Home_Button_rank();
                    guidLineObj.SetActive(false);
                    leftTime = 2f;

                    adaptation_time = true; // 적응시간
                }
            }  // 3 : 엄검중지
            else if (isExtendedThumb && isExtendedIndex && isExtendedMiddle && !isExtendedRing && !isExtendedPinky && active_Panel.Equals(false))
            {
                leftTime -= Time.deltaTime * 0.8f;
                // 엄검소지 펴고, 나머지접음
                // 2초 지속시간 딜레이
                guidLineObj.SetActive(true);
                Debug.Log("중지 : 다시하기");

                guidLineText.text = "게임 다시 시작.. " + leftTime.ToString("N0") + "초";
                if (leftTime < 0f)
                {
                    GameManager.Instance.OnClick_BtnSound1();
                    // 다시하기 씬 스크립트
                    End_UIManager.Instance.OnClickRetry_rank();
                    guidLineObj.SetActive(false);
                    leftTime = 2f;

                    adaptation_time = true; // 적응시간
                }
            }
            //////////////////// MyRanking Panel화면이 켜져있을 떄 /////////////////////////
            else if (End_UIManager.Instance.Ranking_registration.Equals(false))
            {
                Debug.Log("My Rank활성화 중");
                if (isExtendedThumb && !isExtendedIndex && !isExtendedMiddle && !isExtendedRing && !isExtendedPinky)   // 랭킹패널이 활성화되어있지 않다면
                {
                    leftTime -= Time.deltaTime * 0.8f;
                    //랭킹 모드 
                    Debug.Log("엄지 : 메인화면");
                    // 엄검소지 펴고, 나머지접음
                    // 2초 지속시간 딜레이
                    guidLineObj.SetActive(true);

                    guidLineText.text = "메인화면으로 이동 선택, 이동까지 남은시간 " + leftTime.ToString("N0") + "초";
                    if (leftTime < 0f)
                    {
                        GameManager.Instance.OnClick_BtnSound1();
                        // 메인화면 이동 스크립트
                        GameManager.Instance.OnClick_Home_Button_rank();
                        guidLineObj.SetActive(false);
                        leftTime = 2f;

                        adaptation_time = true; // 적응시간
                    }
                }
                else
                {
                    guidLineObj.SetActive(false);
                    leftTime = 2f;
                }
            }
            else
            {
                guidLineObj.SetActive(false);
                leftTime = 2f;
            }
            /////////////////////////////////////////////////////////////////////////////////////////////

        }
        ///////////////////////// 한 게임이 끝나고 났을 때, 다음 행동 선택 //////////////////////////////////////
        //게임이 끝났을 경우, 게임 결과 판넬
        else if (UIManager.Instance.GameEndPanel_Classic.activeSelf || UIManager.Instance.GameEndPanel_Rank.activeSelf)
        {
            //마지막게임이 아닐 경우, 
            if (GameManager.LastGameCheck.Equals(false))
            {
              
                // 1 : 엄지
                if (isExtendedThumb && !isExtendedIndex && !isExtendedMiddle && !isExtendedRing && !isExtendedPinky && active_Panel.Equals(false))
                {
                    leftTime -= Time.deltaTime * 0.8f;
                    // 엄검소지 펴고, 나머지접음
                    // 2초 지속시간 딜레이
                    guidLineObj.SetActive(true);

                    //다음게임
                    if (GameManager.RankMode.Equals(true))
                    {
                        guidLineText.text = "다음게임을 선택하셨습니다. 이동까지 " + leftTime.ToString("N0") + "초";
                        if (leftTime < 0f)
                        {
                            GameManager.Instance.NextScenes();
                            guidLineObj.SetActive(false);
                            leftTime = 2f;

                            adaptation_time = true; // 적응시간
                        }
                    }
                    //클래식 모드 
                    else
                    {
                        //guidLineObj.SetActive(true);
                        //게임선택
                        guidLineText.text = "게임선택를 선택하셨습니다. 이동까지 " + leftTime.ToString("N0") + "초";
                        if (leftTime < 0f)
                        {

                            _popUpManager.OnClickStop_Classic();
                            guidLineObj.SetActive(false);
                            leftTime = 2f;

                            adaptation_time = true; // 적응시간
                        }

                    }
                }   // 2 : 엄검지
                else if (isExtendedThumb && isExtendedIndex && !isExtendedMiddle && !isExtendedRing && !isExtendedPinky && active_Panel.Equals(false))
                {
                    leftTime -= Time.deltaTime * 0.8f;
                    // 2초 지속시간 딜레이
                    guidLineObj.SetActive(true);
                    Debug.Log("검지");
                    //랭킹 모드 
                    if (GameManager.RankMode.Equals(true))
                    {
                        //토탈점수
                        guidLineText.text = "토탈점수를 선택하셨습니다. 이동까지 " + leftTime.ToString("N0") + "초";
                        if (leftTime < 0f)
                        {
                            _popUpManager.OnClick_CurrentScore();
                            guidLineObj.SetActive(false);
                            active_Panel = true;
                            leftTime = 2f;

                            adaptation_time = true; // 적응시간
                        }
                    }
                   
                    //클래식 모드 
                    else
                    { 
                        //다시하기 
                        guidLineText.text = "다시하기를 선택하셨습니다. 이동까지 " + leftTime.ToString("N0") + "초";
                        if (leftTime < 0f)
                        {
                            _popUpManager.OnRetry_Classic();
                            guidLineObj.SetActive(false);
                            leftTime = 2f;
                            adaptation_time = true; // 적응시간
                        }
                    }
                    Debug.Log(leftTime);
                }  // 3 : 엄검중지
                else if (isExtendedThumb && isExtendedIndex && isExtendedMiddle && !isExtendedRing && !isExtendedPinky && active_Panel.Equals(false))
                {

                    leftTime -= Time.deltaTime * 0.8f;
                    //랭킹 모드 
                    Debug.Log("엄지");
                    if (GameManager.RankMode.Equals(true))
                    {
                        // 엄검소지 펴고, 나머지접음
                        // 2초 지속시간 딜레이
                        guidLineObj.SetActive(true);

                        //포기하기
                        guidLineText.text = "포기하기를 선택하셨습니다. 이동까지 " + leftTime.ToString("N0") + "초";
                        if (leftTime < 0f)
                        {
                            _popUpManager.OnClickGiveUp_rank();
                            guidLineObj.SetActive(false);
                            leftTime = 2f;

                            adaptation_time = true; // 적응시간
                        }
                    }
                }
                //엄지 검지 소지 
                //토탈 점수에서 뒤로가기 
                else if (isExtendedThumb && isExtendedIndex && !isExtendedMiddle && !isExtendedRing && isExtendedPinky
                    && UIManager.Instance.GameEndPanel_Rank.activeSelf && active_Panel.Equals(true))
                {
                    guidLineObj.SetActive(true);
                    leftTime -= Time.deltaTime * 0.8f;
                    guidLineText.text = "창 닫기를 시도. 이동까지 " + leftTime.ToString("N0") + "초";
                    if (leftTime < 0f)
                    {
                        _popUpManager.OnClick_CloseCurrentScore();
                        guidLineObj.SetActive(false);
                        active_Panel = false;

                        adaptation_time = true; // 적응시간
                    }
                }
                else
                {
                    guidLineObj.SetActive(false);
                    leftTime = 2f;
                }
            }
        }
    }

    public void SetExtendedGesture(string _finger)
    {
        switch (_finger)
        {
            case "THUMB":
                //thumb.text = "엄지!!";
                isExtendedThumb = true;
                break;
            case "INDEX":
                //index.text = "검지!!";
                isExtendedIndex = true;
                break;
            case "MIDDLE":
                //middle.text = "중지!!";
                isExtendedMiddle = true;
                break;
            case "RING":
                //ring.text = "약지!!";
                isExtendedRing = true;
                break;
            case "PINKY":
                //pinky.text = "소지!!";
                isExtendedPinky = true;
                break;
        }
    }

    public void SetFoldGesture(string _finger)
    {
        switch (_finger)
        {
            case "THUMB":
                //thumb.text = "엄지쥬금";
                isExtendedThumb = false;
                break;
            case "INDEX":
                //index.text = "검지쥬금";
                isExtendedIndex = false;
                break;
            case "MIDDLE":
                //middle.text = "중지쥬금";
                isExtendedMiddle = false;
                break;
            case "RING":
                //ring.text = "약지쥬금";
                isExtendedRing = false;
                break;
            case "PINKY":
                //pinky.text = "소지쥬금";
                isExtendedPinky = false;
                break;
        }
    }

}