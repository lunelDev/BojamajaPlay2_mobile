using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassicMod : MonoBehaviour
{
    public int BestScoreStar;
    public int CurrentStarScore;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("SaveStar_" + this.gameObject.name))
        {
            BestScoreStar = 0;
            CurrentStarScore = 0;
            PlayerPrefs.SetInt("SaveStar_" + this.gameObject.name, BestScoreStar);
            PlayerPrefs.SetInt(this.gameObject.name+"_Star_Classic", CurrentStarScore);
        }
    }
    private void Update()
    {
        if (PlayerPrefs.HasKey("SaveStar_" + this.gameObject.name))
        {
            SetStarImg();
        }
    }
    private void SetStarImg()
    {
        if (BestScoreStar >= 0)
        {
            BestScoreStar = PlayerPrefs.GetInt("SaveStar_" + this.gameObject.name);
            CurrentStarScore = PlayerPrefs.GetInt(this.gameObject.name + "_Star_Classic");
            if (BestScoreStar <= CurrentStarScore)
            {
                PlayerPrefs.SetInt("SaveStar_" + this.gameObject.name, CurrentStarScore);
                BestScoreStar = CurrentStarScore;
            }
            PlayerPrefs.Save();
        }
    }
    public void RestClassicData()
    {
        PlayerPrefs.DeleteKey("SaveStar_Bowling");
        PlayerPrefs.DeleteKey("SaveStar_Dust");
        PlayerPrefs.DeleteKey("SaveStar_Hotpot");
        PlayerPrefs.DeleteKey("SaveStar_Pirates");
        PlayerPrefs.DeleteKey("SaveStar_Pizza");
        PlayerPrefs.DeleteKey("SaveStar_Sandwiches");
        PlayerPrefs.DeleteKey("SaveStar_Soccer");
        PlayerPrefs.DeleteKey("SaveStar_Submarine");
        PlayerPrefs.DeleteKey("SaveStar_ToyFactory");
        PlayerPrefs.DeleteKey("SaveStar_Zombies");
        PlayerPrefs.DeleteKey("TodayAD");
        PlayerPrefs.DeleteKey("Tomorrow");
        PlayerPrefs.DeleteKey("RemoveAD");
    }
}
