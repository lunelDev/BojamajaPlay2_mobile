using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 랭킹 확인에서 점수에 따라 별 이미지를 변경 시키기
/// </summary>
public class ClassicScoreManager : MonoBehaviour
{
    [Header("Star Score Imge Collect")]
    public Sprite[] StarImg;
 /*   public Text GameName;
    public Text Score;*/
    public Image StarUI;
    int CurrentGameStar;
    private void Awake()
    {
       
        CurrentGameStar = DataManager.Instance.scoreManager.starPick;
        StartCoroutine(_SetStarPanelImg());
    }
    IEnumerator _SetStarPanelImg()
    {
        if (CurrentGameStar.Equals(0))
        {
            StarUI.sprite = StarImg[0];
        }
        else if (CurrentGameStar.Equals(1))
        {
            StarUI.sprite = StarImg[1];
        }
        else if (CurrentGameStar.Equals(2))
        {
            StarUI.sprite = StarImg[2];
        }
        else if (CurrentGameStar.Equals(3))
        {
            StarUI.sprite = StarImg[3];
        }
        else if (CurrentGameStar.Equals(4))
        {
            StarUI.sprite = StarImg[4];
        }
        else if (CurrentGameStar.Equals(5))
        {
            StarUI.sprite = StarImg[5];
        }
        yield return null;
    }
}
