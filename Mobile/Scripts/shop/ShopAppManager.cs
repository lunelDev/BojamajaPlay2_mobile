using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.UI;
//전체적인 다이아상점 관리 
public class ShopAppManager : MonoBehaviour
{
    public GameObject Store;
    //광고 쿨타임
    public GameObject HaveCoolOb;
    public Text CoolTimeStr;

    //광고 횟수 
    public GameObject DontHaveFreeAdOb;
    //
    public GameObject PurchasefailedOb; 
    public GameObject PurchaseCompleteOb;
    public static ShopAppManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else Instance = this;
    }

    public void Update()
    {
        //광고 쿨타임이 남았으면
        if (GameManager.Instance.AdCool.Equals(true))
        {
            HaveCoolOb.SetActive(true); //판넬활성화 
            CoolTimeStr.text = GameManager.Instance.LeftTimeMin + ":" + GameManager.Instance.LeftTimeSec;
        }
        else
        {
            HaveCoolOb.SetActive(false); //판넬활성화 
        }

    }
    public void OnClickShopButton()
    {
        if (Store.activeSelf.Equals(false))
        {
            PlayerPrefs.SetString("PlayEndTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")); //현재시간 저장
            GameManager.ShopPanel = true; //팝업 활성화 중
            MainUIManager.instance.ClassicModAni.Rebind(); //클래식 섬 애니메이션 Reset
            MainUIManager.instance.RankModAni.Rebind(); //랭킹 섬 애니메이션 Reset
            Store.SetActive(true);
            SoundManager.Instance.bgm.clip = SoundManager.Instance.GetClipByName("BoboShopBGM");
            SoundManager.Instance.bgm.Play();
        }
        else
        {
            GameManager.Instance.OnClick_BtnSound3();
        }
    }
    ///////////////////////////무료 광고 보기/////////////////////////////////////////
    public void ClickShowAd()
    {
        //남은 광고가 있고 광고 쿨타임이 없을 경우,
        if (GameManager.Instance.FreeADCount > 0
            && GameManager.Instance.AdCool.Equals(false))
        {
            GameManager.Instance.OnClick_BtnSound1();
            AdManager.Instance.ShowRewardAd(); //광고 보기
        }
        // 오늘 볼수 있는 광고를 모두 보았거나 쿨타임이 남았을 경우, 
        else if (GameManager.Instance.FreeADCount <= 0
            || GameManager.Instance.AdCool.Equals(true))
        {
            GameManager.Instance.OnClick_BtnSound3();
            //오늘 볼수있는 광고 횟수를 초과 
            if (GameManager.Instance.FreeADCount <= 0)
            {
            // DontHaveFreeAdOb.SetActive(true); //판넬 활성화 
                PopUpSystem.Instance.ShowPopUp(DontHaveFreeAdOb);
            }
        }
    }
    //////////////////////////////////////////////////////////////////////////
}
