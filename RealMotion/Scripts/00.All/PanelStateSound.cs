using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelStateSound : MonoBehaviour
{

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
        else if (this.gameObject.name.Equals("ModePanel"))
        {
            GameManager.ModePanel = false;
        }
    }
}