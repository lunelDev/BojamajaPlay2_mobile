using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FlagManager : MonoBehaviour
{
   [SerializeField] Punisher _penaltyPanel;
    public GameObject UIob;
    public GameObject[] EffectOb;
    public GameObject[] flagKinds; //선택지 종류
    public GameObject BubbleX;
    public GameObject BubbleO;
    public int RandomKind; //선택된 플래그 종류
    public Flag flagScript;
    public string CompareRandomFlagName;
    public int btnCount = 0;

    public string _ClickButton_1 = null;
    public string _ClickButton_2 = null;

    public string _ClickString_1 = null;
    public string _ClickString_2 = null;

    private float leftDeltaDis;
    private float rightDeltaDis;

    [Header("Reset Line")]
    public Transform left_resetPosion;
    public Transform right_resetPosion;
    public GameObject resetLineGameObject;
    private bool isLeftReset = false;
    private bool isRightReset = false;

    [Header("Hands")]
    public GameObject leftHand;
    public GameObject rightHand;

    [Header("String Display")]
    ///////////입력한 값을 보여주는 OB ///////////////////////////
    public Text showString;
    public bool matchStart = false;

    public static FlagManager instance { get; private set; }
    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;
    }

    private void Update()
    {
        ////매 프레임당 손과 리셋라인 거리체크 : 랜덤지문 받으려면 리셋 포지션에 와야함.
        //if (leftHand.activeSelf)
        //{
        //    leftDeltaDis = Vector3.Distance(leftHand.transform.position, left_resetPosion.position);
        //    //Debug.Log("leftDeltaDis : " + leftDeltaDis);
        //    if (leftDeltaDis < 1f)
        //    {
        //        isLeftReset = true;
        //    }
        //}
        //else
        //{
        //    isLeftReset = false;
        //}

        //if (rightHand.activeSelf)
        //{
        //    rightDeltaDis = Vector3.Distance(rightHand.transform.position, right_resetPosion.position);
        //    //Debug.Log("rightDeltaDis : " + rightDeltaDis);
        //    if (rightDeltaDis < 1f)
        //    {
        //        isRightReset = true;
        //    }
        //}
        //else
        //{
        //    isRightReset = false;
        //}
    }

    public void ShowUiString()
    {
        StartCoroutine(_ShowUiString());
    }
    IEnumerator _ShowUiString()
    {
        while (AppManager.Instance.gameRunning.Equals(true))
        {
            showString.text = _ClickString_1 + " " + _ClickString_2;
            yield return null;
        }
    }
    /////////////////// 랜덤으로 지문을 선택 /////////////////////////////////    
    public void RandomChooseFlag()
    {
        StartCoroutine(_RandomChooseFlag());
    }
    IEnumerator _RandomChooseFlag()
    {
        RandomKind = Random.Range(0, flagKinds.Length); //지문 선택
        flagKinds[RandomKind].SetActive(true); //텍스트 활성화
        UIob.SetActive(true);
        CompareRandomFlagName = flagKinds[RandomKind].name;

        _ClickButton_1 = "";
        _ClickButton_2 = "";

        _ClickString_1 = "";
        _ClickString_2 = "";

        matchStart = true;

        //StartCoroutine(_ResetLine());

        yield return null;

    }
    float waitT = 5f;
    public void WaitTime()
    {
        waitT = 5f;
        StartCoroutine(_WaitTime());
    }
    IEnumerator _WaitTime()
    {
        while (waitT > 0)
        {
            waitT -= Time.deltaTime;
            //남은시간 단, 3초
            if (waitT <= 3)
            {
                Debug.Log(waitT);
            }
            if (btnCount > 1)
            {
                break;
            }
            yield return null;
        }

        //실패!
        Fail();
        yield return null;
    }

    public void MatchStringName()
    {
        StopCoroutine(_MatchStringName());
        StartCoroutine(_MatchStringName());
    }
    IEnumerator _MatchStringName()
    {
        //yield return new WaitUntil(() => AppManager.Instance.gameRunning.Equals(true));
        /*  while (AppManager.Instance.gameRunning.Equals(true))
           {*/
        //Debug.Log("_MatchStringName Running");
        //Debug.Log("matchStart : " + matchStart);
        //Debug.Log("_ClickButton_1 : " + _ClickButton_1);
        //Debug.Log("_ClickButton_2 : " + _ClickButton_2);

        if (matchStart.Equals(true))
        {
            if (!_ClickButton_1.Equals(""))
            {
                //Debug.Log("_ClickButton1_matchString" + _ClickButton_1);
                //Debug.Log("_ClickButton2_matchString" + _ClickButton_2);
                //2글자
                if (CompareRandomFlagName.Length.Equals(2))
                {
                    //Debug.Log("----------------1");
                    //첫번재 입력이 같으면
                    if (CompareRandomFlagName.Equals(_ClickButton_1))
                    {
                        Good();
                    }
                    else
                    {
                        Fail();
                    }
                }
                //4글자
                else if (CompareRandomFlagName.Length.Equals(4))
                {
                    //Debug.Log("(0,2)" + CompareRandomFlagName.Substring(0, 2));
                    //Debug.Log("(2,2)" + CompareRandomFlagName.Substring(2, 2));
                    //Debug.Log("_ClickButton1_matchString" + _ClickButton_1);
                    //Debug.Log("_ClickButton2_matchString" + _ClickButton_2);
                    //Debug.Log("CompareRandomFlagName" + CompareRandomFlagName);
                    //Debug.Log("btnCount : " + btnCount);
                    //첫번재 입력이 같으면
                    if (CompareRandomFlagName.Substring(0, 2).Equals(_ClickButton_1))
                    {
                        if (btnCount > 1)
                        {
                            if (CompareRandomFlagName.Substring(2, 2).Equals(_ClickButton_2))
                            {
                                Debug.Log("----------------3");
                                Good();
                                //  NextQuestion();
                            }
                            else
                            {
                                //하나 틀림
                                print("---------------4");
                                Fail();
                                //  NextQuestion();
                            }
                        }

                    }
                    else if (CompareRandomFlagName.Substring(2, 2).Equals(_ClickButton_1))
                    {
                        if (btnCount > 1)
                        {
                            if (CompareRandomFlagName.Substring(0, 2).Equals(_ClickButton_2))
                            {
                                Debug.Log("----------------3");
                                Good();
                            }
                            else
                            {
                                //하나 틀림
                                print("---------------4");
                                Fail();

                            }
                        }

                    }
                    else
                    {
                        //하나 틀림
                        print("---------------5");
                        Fail();
                        // NextQuestion();
                    }
                }
            }
            yield return null;
        }

        yield return null;

    }
    public void Good()
    {
        isLeftReset = false;
        isRightReset = false;
        matchStart = false;
        print("Good"); //둘다 맞춤
        EffectOb[0].GetComponent<ParticleSystem>().Play();
        BubbleO.SetActive(true);
        DataManager.Instance.scoreManager.Add(800);
        SoundManager.Instance.ObSFXPlay1();
        //StartCoroutine(_ResetLine()); 
        NextQuestion();
        //StartCoroutine(_HandResetAndQuestion());

    }
    public void Fail()
    {
        matchStart = false;
        print("Fail");
        BubbleX.SetActive(true);
        SoundManager.Instance.PlaySFX("ScoreDown");
        _penaltyPanel.ExecutePenalty();
        DataManager.Instance.scoreManager.Subtract(100);

        //StartCoroutine(_ResetLine());
        NextQuestion();
        //StartCoroutine(_HandResetAndQuestion());
    }
    IEnumerator _HandResetAndQuestion()
    {
        yield return new WaitUntil(() => isLeftReset && isRightReset);

        Debug.Log("_HandResetAndQuestion 통과");

        NextQuestion();
    }

    //IEnumerator _ResetLine()
    //{
    //    // 손이 없거나 있는데 리셋 안되어있으면 반짝
    //    while ( !leftHand.activeSelf || !rightHand.activeSelf || (!isLeftReset && !isRightReset) )
    //    {
    //        resetLineGameObject.SetActive(true);

    //        yield return null;
    //    }

    //    resetLineGameObject.SetActive(false);

    //    yield return null;
    //}


    public void NextQuestion()
    {
        StartCoroutine(_NextQuestion());
    }
    IEnumerator _NextQuestion()
    {
        flagKinds[RandomKind].SetActive(false); //텍스트 활성화
        yield return new WaitForSeconds(0.5f);
        if (BubbleO.activeSelf.Equals(true) || BubbleX.activeSelf.Equals(true))
        {
            if (BubbleO.activeSelf.Equals(true))
            {
                BubbleO.SetActive(false);
            }
            else if (BubbleX.activeSelf.Equals(true))
            {
                BubbleX.SetActive(false);
            }
        }
        btnCount = 0;
        /*   _ClickButton_1 = "";
           _ClickButton_2 = "";

           _ClickString_1 = "";
           _ClickString_2 = "";*/
        UIob.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        RandomChooseFlag(); //랜덤으로 지문를 선택

        yield return null;
    }
}