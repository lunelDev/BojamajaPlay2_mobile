using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MainAppManager : MonoBehaviour
{

    public UnityEvent onClick = null;
    [Header("[ 게임 UI ]")]
    public GameObject MainCanvasImg;
    [SerializeField] GameObject copyText;
    [SerializeField] GameObject copyVer;

    public GameObject VideoImg;
    public GameObject ChangeGameCavasImg;

    [Header("[ 게임 상점 ]")]
    public GameObject StoreTimerButton;
    public bool ClickBack = false;
    public static bool RandomImgPageEnable = false;
    private Ray ray;
    private RaycastHit hit;
    public static MainAppManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;
    }
    public void Start()
    {
        SoundManager.Instance.PlayBGM();
    }
    public void RankGameStartBtnClick()
    {
        GameManager.Instance.OnClick_BtnSound1();
        MainCanvasImg.SetActive(false); //메인 캔버스
        copyText.SetActive(false); //카피라이트
        copyVer.SetActive(false);   //카피라이트
        VideoImg.SetActive(false);
        StoreTimerButton.SetActive(false);
        ChangeGameCavasImg.SetActive(true);
    }
    public void onClick_ClassicBtn()
    {
        GameManager.Instance.OnClick_BtnSound1();
        GameManager.ClassicPanel = true;
        MainUIManager.instance.ClassicModePanel.SetActive(true);
        MainUIManager.instance.ClassicModAni.Rebind();
        MainUIManager.instance.RankModAni.Rebind();

    }
    public void onClick_RankBtn()
    {
        GameManager.Instance.OnClick_BtnSound1();
        GameManager.RankPanel = true;
        MainUIManager.instance.RankingModePanel.SetActive(true);
        MainUIManager.instance.ClassicModAni.Rebind();
        MainUIManager.instance.RankModAni.Rebind();
    }
    public void onClick_BackBtn(GameObject ob)
    {
        GameManager.Instance.OnClick_BtnSound2();
        if (ob.name.Equals("ClassicModePanel"))
        {
            GameManager.ClassicPanel = false;
            ob.SetActive(false);
            SoundManager.Instance.bgm.clip = SoundManager.Instance.GetClipByName("Main");
            SoundManager.Instance.bgm.Play();
            if (GameManager.HaveAdRemove.Equals(false))
            {
                AdManager.Instance.ShowScreenAd();
            }
            else
            {
                GameManager.HaveAdRemoveCount -= 1;
                PlayerPrefs.SetInt("RemoveADCount", GameManager.HaveAdRemoveCount);
            }
        }
        else if (ob.name.Equals("RankModPanel"))
        {
            GameManager.RankPanel = false;
            ob.SetActive(false);
            SoundManager.Instance.bgm.clip = SoundManager.Instance.GetClipByName("Main");
            SoundManager.Instance.bgm.Play();
            if (GameManager.HaveAdRemove.Equals(false))
            {
                AdManager.Instance.ShowScreenAd();
            }
            else
            {
                GameManager.HaveAdRemoveCount -= 1;
                PlayerPrefs.SetInt("RemoveADCount", GameManager.HaveAdRemoveCount);
            }
        }
        else if (ob.name.Equals("ModeSelect"))
        {
            GameManager.ModePanel = false;
            ob.SetActive(false);
            SoundManager.Instance.bgm.clip = SoundManager.Instance.GetClipByName("Main");
            SoundManager.Instance.bgm.Play();
        }


    }

}
      
