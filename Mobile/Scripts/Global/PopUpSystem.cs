using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PopUpSystem : MonoBehaviour
{
    public GameObject PopUpPanel = null;
    //   public ShopUIManager _shopUI;
    [SerializeField] private GameObject PopUpPanel_ChoosePage;
    public static bool PopUpState = false;
    private bool bPaused;
    public static PopUpSystem Instance { get; private set; }
    private void Awake()
    {

        if (Instance != null)
            Destroy(this);
        else Instance = this;
    }
        // Update is called once per frame
        void Update()
    {
        //모바일
        if (Application.platform.Equals(RuntimePlatform.Android))
        {
            if (PopUpState.Equals(false))
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    PlayerPrefs.SetString("PlayEndTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")); //일시정지한 타이밍 저장
                    openPopup();
                }
            }
        }
        //팝업이 비활성화 일 떄 
        if (PopUpState.Equals(false))
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PlayerPrefs.SetString("PlayEndTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")); //일시정지한 타이밍 저장
                openPopup();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ContinueButtonClick();
            }
        }

    }

    void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            bPaused = true;
        }
        else
        {
            if (bPaused)
            {
                if (SceneManager.GetActiveScene().name.Equals("Main") 
                    || SceneManager.GetActiveScene().name.Equals("EndScene"))
                {
                    bPaused = false;
                    //openPopup();
                }
                else
                {
                    bPaused = false;
                    openPopup();
                }
             /*   if (!SceneManager.GetActiveScene().name.Equals("EndScene"))
                {
                    bPaused = false;
                    openPopup();
                }*/
            }
        }
    }
    public void openPopup()
    {
        // StopAllCoroutines();
        
        StartCoroutine(_OpenPopup());
    }
    IEnumerator _OpenPopup()
    {
        //판넬이 비활성화 상태 일떄 
        while (PopUpState.Equals(false))
        {
            //씬이름이 메인이면
            if (SceneManager.GetActiveScene().name.Equals("Main")
                || SceneManager.GetActiveScene().name.Equals("EndScene"))
            {
                GameManager.Instance.OnClick_BtnSound1();
                if (MainAppManager.RandomImgPageEnable.Equals(false))
                {
                    TimeManager.instance.PlayEndTime();
                    PopUpPanel.SetActive(true);
                    PopUpState = true;
                    Time.timeScale = 0;
                }
                else
                {
                    TimeManager.instance.PlayEndTime();
                    PopUpPanel_ChoosePage.SetActive(true);
                    PopUpState = true;
                    Time.timeScale = 0;
                }
            }
            //게임 중일 때
            else
            {
                if (AppManager.Instance.gameRunning.Equals(true)
                    || UIManager.Instance.RoundEnd_openPanel.Equals(true))
                {
                    GameManager.Instance.OnClick_BtnSound1();
                    TimeManager.instance.PlayEndTime();
                    PopUpPanel.SetActive(true);
                    PopUpState = true;
                    Time.timeScale = 0;
                }
            }
            yield return null;
        }
        //팝업이 활성화 중일 때 
        while (PopUpState.Equals(true))
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.Instance.OnClick_BtnSound2();

                //일시정지 창 닫기 
                TimeManager.instance.DiamondSu_Check(); //팝업창을 비활성화 했을 때 휴식시간 체크해서 다이아 저장
                PopUpPanel.SetActive(false); //활성화 된 판넬 비활성화
                PopUpState = false;
                Time.timeScale = 1;
                //타이틀일 경우, 게임 선정화면에서의 판넬 비활성화 시켜줌
                if (SceneManager.GetActiveScene().name.Equals("Main"))
                {
                    //게임 선정 화면일 경우, 
                    if (MainAppManager.RandomImgPageEnable.Equals(true))
                    {
                        PopUpPanel_ChoosePage.SetActive(false);
                        Time.timeScale = 1;
                    }
                }
            }
            yield return null;
        }
        yield return null;
    }
    //일시정지 버튼을 클릭했을 때 팝업 판넬 활성화
    public void PasueButtonClick()
    {      
        if (AppManager.Instance.gameRunning.Equals(true))
        {
            GameManager.Instance.OnClick_BtnSound1();
            TimeManager.instance.PlayEndTime();
            PlayerPrefs.SetString("PlayEndTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")); //일시정지한 타이밍 저장
            PopUpPanel.SetActive(true);
            PopUpState = true;
            Time.timeScale = 0;
        }
    }

    //Yes를 눌렀을 떄 앱 종료 
    public void CloseButtonClick()
    {
        GameManager.Instance.OnClick_BtnSound1();
        TimeManager.instance.PlayEndTime();
        Application.Quit();
    }

    //No를눌렀을 때 , 팝업창 닫음
    public void ContinueButtonClick()
    {
        GameManager.Instance.OnClick_BtnSound2();
        PopUpPanel.SetActive(false);
        PopUpState = false;
        Time.timeScale = 1;
        TimeManager.instance.DiamondSu_Check();
        /*  if (SceneManager.GetActiveScene().name.Equals("Main"))
          {
              PopUpPanel_ChoosePage.SetActive(false);
          }*/
    }
    public void BoboShopClose()
    {
        MainUIManager.instance.BoboShop.SetActive(false);
        GameManager.ShopPanel = false;
        GameManager.ModePanel = true;
        SoundManager.Instance.bgm.clip = SoundManager.Instance.GetClipByName("Main");
        SoundManager.Instance.bgm.Play();
       
    } 
    public RaycastHit _Hit;
    public LayerMask _RaycastCollidableLayers; //Set this in inspector, makes you able to say which layers should be collided with and which not.
    public float _CheckDistance = 100f;
    // UI터치 시 GameObject 터치 무시하는 코드
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
    public void ShowPopUp(GameObject ob)
    {
        StartCoroutine(_ShowPopUp(ob));
    }
    IEnumerator _ShowPopUp(GameObject ob)
    {
        ob.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        ob.SetActive(false);
        yield return null;
    }
    //////////////////////// 설정 창 ///////////////////////////
    public void OnClick_SoundSettingBtn()
    {
        GameManager.Instance.OnClick_BtnSound1();
       PopUpState = true;
    }
    public void OnClick_CloseSoundBtn()
    {
        GameManager.Instance.OnClick_BtnSound2();
       PopUpState = false;
    }
    ///////////////////////////////////////////////////////////////////////////
}
