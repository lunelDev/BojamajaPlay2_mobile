using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialGesture : MonoBehaviour
{
   /* private bool L_isExtendedThumb;
    private bool L_isExtendedIndex;
    private bool L_isExtendedMiddle;
    private bool L_isExtendedRing;
    private bool L_isExtendedPinky;*/

    private bool R_isExtendedThumb;
    private bool R_isExtendedIndex;
    private bool R_isExtendedMiddle;
    private bool R_isExtendedRing;
    private bool R_isExtendedPinky;

    bool adaptation_time = true;
    bool protection_time = true;
    [Header("GuidLine")]
    public float leftTime = 2f;
    public GameObject guidLineObj;
    public Text guidLineText;
    void Start()
    {
      /*  L_isExtendedThumb = false;
        L_isExtendedIndex = false;
        L_isExtendedMiddle = false;
        L_isExtendedRing = false;
        L_isExtendedPinky = false;

        */
        R_isExtendedThumb = false;
        R_isExtendedIndex = false;
        R_isExtendedMiddle = false;
        R_isExtendedRing = false;
        R_isExtendedPinky = false;
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

        if (TutorialManager.Instance.GestureTutorial.Equals(true))
        {
            Debug.Log(TutorialManager.Instance.gestureStap);
            //1번 (오른손 엄지)
            if (R_isExtendedThumb && !R_isExtendedIndex && !R_isExtendedMiddle && !R_isExtendedRing && !R_isExtendedPinky)
            {
                if (TutorialManager.Instance.gestureStap.Equals(0) && TutorialManager.Instance.StapOB[4].activeSelf)
                {
                    Debug.Log("----" + leftTime);
                    leftTime -= Time.deltaTime * 0.8f;
                    guidLineText.text = "1번 버튼을 선택하셨습니다. " + leftTime.ToString("N0") + "초";
                    // 엄검소지 펴고, 나머지접음
                    // 2초 지속시간 딜레이
                    guidLineObj.SetActive(true);
                    if (leftTime < 0f)
                    {
                        TutorialManager.Instance.gestureStap += 1;
                        leftTime = 2f;
                        guidLineObj.SetActive(false);
                        adaptation_time = true; // 적응시간
                    }
                }
                else if (TutorialManager.Instance.StapOB[5].activeSelf && TutorialManager.Instance.stapPart.Equals(5))
                {
                    Debug.Log("----" + leftTime);
                    leftTime -= Time.deltaTime * 0.8f;
                    guidLineText.text = "'가' 버튼을 선택하셨습니다. " + leftTime.ToString("N0") + "초";
                    // 엄검소지 펴고, 나머지접음
                    // 2초 지속시간 딜레이
                    guidLineObj.SetActive(true);
                    if (leftTime < 0f)
                    {
                        TutorialManager.Instance.stap6_cool.SetActive(true); 
                        leftTime = 2f;
                        guidLineObj.SetActive(false);
                        adaptation_time = true; // 적응시간
                    }
                }
               
            }
            //2번 (오른손 엄검지)
            else if (R_isExtendedThumb && R_isExtendedIndex && !R_isExtendedMiddle && !R_isExtendedRing && !R_isExtendedPinky)
            {
                if (TutorialManager.Instance.gestureStap.Equals(1) && TutorialManager.Instance.StapOB[4].activeSelf)
                {
                    Debug.Log("----" + leftTime);
                    leftTime -= Time.deltaTime * 0.8f;
                    guidLineText.text = "2번 버튼을 선택하셨습니다. " + leftTime.ToString("N0") + "초";
                    // 엄검소지 펴고, 나머지접음
                    // 2초 지속시간 딜레이
                    guidLineObj.SetActive(true);
                    if (leftTime < 0f)
                    {
                        TutorialManager.Instance.gestureStap += 1;
                        leftTime = 2f;
                        guidLineObj.SetActive(false);
                        adaptation_time = true; // 적응시간
                    }

                }
                else if (TutorialManager.Instance.StapOB[6].activeSelf && TutorialManager.Instance.stapPart.Equals(6))
                {
                    Debug.Log("----" + leftTime);
                    leftTime -= Time.deltaTime * 0.8f;
                    guidLineText.text = "'나' 버튼을 선택하셨습니다. " + leftTime.ToString("N0") + "초";
                    // 엄검소지 펴고, 나머지접음
                    // 2초 지속시간 딜레이
                    guidLineObj.SetActive(true);
                    if (leftTime < 0f)
                    {
                        TutorialManager.Instance.stap7_cool.SetActive(true);
                        leftTime = 2f;
                        guidLineObj.SetActive(false);
                        adaptation_time = true; // 적응시간
                    }
                }
            }
            //3번 (오른손 엄검중지)
            else if (R_isExtendedThumb && R_isExtendedIndex && R_isExtendedMiddle && !R_isExtendedRing && !R_isExtendedPinky)
            {
                if (TutorialManager.Instance.gestureStap.Equals(2) && TutorialManager.Instance.StapOB[4].activeSelf)
                {
                    Debug.Log("----" + leftTime);
                    leftTime -= Time.deltaTime * 0.8f;
                    guidLineText.text = "3번 버튼을 선택하셨습니다. " + leftTime.ToString("N0") + "초";
                    // 엄검소지 펴고, 나머지접음
                    // 2초 지속시간 딜레이
                    guidLineObj.SetActive(true);
                    if (leftTime < 0f)
                    {
                        TutorialManager.Instance.gestureStap += 1;
                        leftTime = 2f;
                        guidLineObj.SetActive(false);
                        adaptation_time = true; // 적응시간
                    }
                }
                else if (TutorialManager.Instance.StapOB[7].activeSelf &&TutorialManager.Instance.stapPart.Equals(7))
                {
                    Debug.Log("----" + leftTime);
                    leftTime -= Time.deltaTime * 0.8f;
                    guidLineText.text = "'팝업창' 버튼을 선택하셨습니다. " + leftTime.ToString("N0") + "초";
                    // 엄검소지 펴고, 나머지접음
                    // 2초 지속시간 딜레이
                    guidLineObj.SetActive(true);
                    if (leftTime < 0f)
                    {
                        TutorialManager.Instance.stap8_Awesome.SetActive(true);
                        leftTime = 2f;
                        guidLineObj.SetActive(false);
                        adaptation_time = true; // 적응시간
                    }
                }
            }
            //뒤로가기 (오른손 엄검소지)
            else if (R_isExtendedThumb && R_isExtendedIndex && !R_isExtendedMiddle && !R_isExtendedRing && R_isExtendedPinky)
            {
                if (TutorialManager.Instance.gestureStap.Equals(3) && TutorialManager.Instance.StapOB[4].activeSelf)
                {
                    Debug.Log("----" + leftTime);
                    leftTime -= Time.deltaTime * 0.8f;
                    guidLineText.text = "뒤로가기를 선택하셨습니다. " + leftTime.ToString("N0") + "초";
                    // 엄검소지 펴고, 나머지접음
                    // 2초 지속시간 딜레이
                    guidLineObj.SetActive(true);
                    if (leftTime < 0f)
                    {
                        TutorialManager.Instance.gestureStap += 1;
                        leftTime = 2f;
                        guidLineObj.SetActive(false);
                        adaptation_time = true; // 적응시간
                    }
                }
                else if (TutorialManager.Instance.StapOB[8].activeSelf && TutorialManager.Instance.stapPart.Equals(8))
                {
                    Debug.Log("----" + leftTime);
                    leftTime -= Time.deltaTime * 0.8f;
                    guidLineText.text = "팝업창을 닫습니다. " + leftTime.ToString("N0") + "초";
                    // 엄검소지 펴고, 나머지접음
                    // 2초 지속시간 딜레이
                    guidLineObj.SetActive(true);
                    if (leftTime < 0f)
                    {
                        TutorialManager.Instance.stap9_Fantastic.SetActive(true);
                        leftTime = 2f;
                        guidLineObj.SetActive(false);
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
    }
    public void SetExtendedGesture(string _finger)
    {
        switch (_finger)
        {
        /*    case "L_THUMB":
                //thumb.text = "엄지!!";
                L_isExtendedThumb = true;
                break;
            case "L_INDEX":
                //index.text = "검지!!";
                L_isExtendedIndex = true;
                break;
            case "L_MIDDLE":
                //middle.text = "중지!!";
                L_isExtendedMiddle = true;
                break;
            case "L_RING":
                //ring.text = "약지!!";
                L_isExtendedRing = true;
                break;
            case "L_PINKY":
                //pinky.text = "소지!!";
                L_isExtendedPinky = true;
                break;
        */
            /////////////////////////////////
            case "R_THUMB":
                //thumb.text = "엄지!!";
                R_isExtendedThumb = true;
                break;
            case "R_INDEX":
                //index.text = "검지!!";
                R_isExtendedIndex = true;
                break;
            case "R_MIDDLE":
                //middle.text = "중지!!";
                R_isExtendedMiddle = true;
                break;
            case "R_RING":
                //ring.text = "약지!!";
                R_isExtendedRing = true;
                break;
            case "R_PINKY":
                //pinky.text = "소지!!";
                R_isExtendedPinky = true;
                break;
        }
    }

    public void SetFoldGesture(string _finger)
    {
        switch (_finger)
        {
           /* case "L_THUMB":
                //thumb.text = "엄지쥬금";
                L_isExtendedThumb = false;
                break;
            case "L_INDEX":
                //index.text = "검지쥬금";
                L_isExtendedIndex = false;
                break;
            case "L_MIDDLE":
                //middle.text = "중지쥬금";
                L_isExtendedMiddle = false;
                break;
            case "L_RING":
                //ring.text = "약지쥬금";
                L_isExtendedRing = false;
                break;
            case "L_PINKY":
                //pinky.text = "소지쥬금";
                L_isExtendedPinky = false;
                break;
           */
            /////////////////////////////////
            case "R_THUMB":
                //thumb.text = "엄지쥬금!!";
                R_isExtendedThumb = false;
                break;
            case "R_INDEX":
                //index.text = "검지쥬금!!";
                R_isExtendedIndex = false;
                break;
            case "R_MIDDLE":
                //middle.text = "중지쥬금!!";
                R_isExtendedMiddle = false;
                break;
            case "R_RING":
                //ring.text = "약지쥬금!!";
                R_isExtendedRing = false;
                break;
            case "R_PINKY":
                //pinky.text = "소지쥬금!!";
                R_isExtendedPinky = false;
                break;
        }
    }
}
