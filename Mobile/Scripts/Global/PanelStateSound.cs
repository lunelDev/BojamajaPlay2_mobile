using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelStateSound : MonoBehaviour
{
    public GameObject[] beActivatedPanel;   // 0 : normal , 1 : event

    private void Start()
    {
        if (ServerManager.Instance.RankingEventDoing)
        {
            beActivatedPanel[0].SetActive(false);
            beActivatedPanel[1].SetActive(true);
        }
        else
        {
            beActivatedPanel[0].SetActive(true);
            beActivatedPanel[1].SetActive(false);
        }
    }

    private void OnEnable()
    {
        //랭킹
        if (GameManager.RankPanel.Equals(true))
        {
            SoundManager.Instance.bgm.clip = SoundManager.Instance.GetClipByName("RankingModeBGM");
            //SoundManager.Instance.bgm.Play();
        }
        //클래식
      else if (GameManager.ClassicPanel.Equals(true))
        {
            SoundManager.Instance.bgm.clip = SoundManager.Instance.GetClipByName("ClassicModeBGM");
            // SoundManager.Instance.bgm.Play();
        }
        //상점
       else if (GameManager.ShopPanel.Equals(true))
        {
            SoundManager.Instance.bgm.clip = SoundManager.Instance.GetClipByName("BoboShopBGM");

            // SoundManager.Instance.bgm.Play();
        }

        SoundManager.Instance.bgm.Play();

    }
    private void OnDisable()
    {
        if (this.gameObject.name.Equals("ClassicModePanel"))
        {
            GameManager.ClassicPanel = false;
        }
        else if (this.gameObject.name.Equals("RankModPanel"))
        {
            GameManager.RankPanel = false;

        }
        else if (this.gameObject.name.Equals("MagicShopPanel"))
        {
            GameManager.ShopPanel = false;

        }
        else if (this.gameObject.name.Equals("ModePanel"))
        {
            GameManager.ModePanel = false;
        }
    }
}