using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TutorialManager : MonoBehaviour
{
    public GameObject[] realHands;
    public GameObject[] StapOB;
    //
    public Image[] GestureIMG;
    public Sprite[] OriginIMG;
    public Sprite[] SelectIMG;
    public Sprite[] CompleteIMG;
    //
    public GameObject stap2_cool;
    public GameObject stap5_cool;
    public GameObject stap6_cool;
    public GameObject stap7_cool;
    public GameObject stap8_Awesome;
    public GameObject stap9_Fantastic;

    public int stapPart = 0;
    public int gestureStap = 0;
    public bool GestureTutorial = false;

    public static TutorialManager Instance { get; private set; }
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else
            Instance = this;
    }
    private void OnEnable()
    {
        StapOB[0].SetActive(true);
        StartCoroutine(_Tutorial_Start());
    }
    IEnumerator _Tutorial_Start()
    {
        //리얼 핸즈 둘다 활성화 되면
        yield return new WaitUntil(() => realHands[0].activeSelf.Equals(true) && realHands[1].activeSelf.Equals(true));

        yield return new WaitForSeconds(1f);
        stapPart += 1; //1
        StapOB[stapPart].SetActive(true);
        StapOB[stapPart - 1].SetActive(false);
        //stap2, 다음으로 버튼을 터치 하기를 기다림
        yield return new WaitUntil(() => stap2_cool.activeSelf.Equals(true));
        //stap2를 마치면, 3초후에 stap3활성화 
        yield return new WaitForSeconds(5f);
        stapPart += 1; //2
        StapOB[stapPart].SetActive(true);
        StapOB[stapPart - 1].SetActive(false);
        //3초후 stap4활성화
        yield return new WaitForSeconds(5f);
        stapPart += 1; //3
        StapOB[stapPart].SetActive(true);
        StapOB[stapPart - 1].SetActive(false);
        yield return new WaitForSeconds(5f);
        //3초후 stap5활성화
        stapPart += 1; //4
        StapOB[stapPart].SetActive(true);
        StapOB[stapPart - 1].SetActive(false);
        GestureTutorial = true;

        //동작중인 이미지 
        //첫번째 동작, Select,수행완료 complete
        //Image[] GestureIMG;
        Debug.Log("1    "+gestureStap);
        yield return new WaitUntil(() => gestureStap.Equals(0));
        if (gestureStap.Equals(0))
        {
          
            GestureIMG[0].sprite = SelectIMG[0];
          
        }
        Debug.Log("2   " + gestureStap);
        yield return new WaitUntil(() => gestureStap.Equals(1));
        //두번째 동작, 
        if (gestureStap.Equals(1))
        {
            GestureIMG[0].sprite = CompleteIMG[0];
            GestureIMG[1].sprite = SelectIMG[1];
          
        }
        yield return new WaitUntil(() => gestureStap.Equals(2));
        //세번째 동작, 
        if (gestureStap.Equals(2))
        {
            GestureIMG[0].sprite = CompleteIMG[0];
            GestureIMG[1].sprite = CompleteIMG[1];
            GestureIMG[2].sprite = SelectIMG[2];
           
        }
        yield return new WaitUntil(() => gestureStap.Equals(3));
        //네번째 동작,
        if (gestureStap.Equals(3))
        {
            GestureIMG[0].sprite = CompleteIMG[0];
            GestureIMG[1].sprite = CompleteIMG[1];
            GestureIMG[2].sprite = CompleteIMG[2];
            GestureIMG[3].sprite = SelectIMG[3];
            yield return new WaitUntil(() => gestureStap.Equals(4));

            yield return null;
        }
        stap5_cool.SetActive(true);
        yield return new WaitForSeconds(5f);
        //3초후 stap6활성화
        stapPart += 1; //5
        StapOB[stapPart].SetActive(true);
        StapOB[stapPart - 1].SetActive(false);
        yield return new WaitUntil(() => stap6_cool.activeSelf.Equals(true));
        yield return new WaitForSeconds(5f);
        //3초후 stap7활성화
        stapPart += 1; //6
        StapOB[stapPart].SetActive(true);
        StapOB[stapPart - 1].SetActive(false);
        yield return new WaitUntil(() => stap7_cool.activeSelf.Equals(true));
        yield return new WaitForSeconds(5f);
        //3초후 stap8활성화
        stapPart += 1; //6
        StapOB[stapPart].SetActive(true);
        StapOB[stapPart - 1].SetActive(false);
        yield return new WaitUntil(() => stap8_Awesome.activeSelf.Equals(true));
        yield return new WaitForSeconds(5f);
        //3초후 stap9활성화
        stapPart += 1; //6
        StapOB[stapPart].SetActive(true);
        StapOB[stapPart - 1].SetActive(false);
        yield return new WaitUntil(() => stap9_Fantastic.activeSelf.Equals(true));
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Main");
        yield return null;
    }
}
