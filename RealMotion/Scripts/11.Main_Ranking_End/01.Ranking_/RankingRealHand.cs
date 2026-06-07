using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingRealHand : MonoBehaviour
{
    public RankingTop20Manager _rankingTop;
    public RankingUIManager _rankingUI;
    public static bool OnScorePanel = false;
    private void OnCollisionExit(Collision collision)
    {
        GameObject ob = collision.gameObject;
        Debug.Log(ob);
        if (ob.name.Equals("LeftBtn"))
        {
            _rankingTop.BackRank();
        }
        if (ob.name.Equals("RightBtn"))
        {
            _rankingTop.NextRank();
        }
        //////////////////////////////////////////////////////////
        if (!OnScorePanel)
        {
            if (ob.name.Equals("Check1"))
            {
                OnScorePanel = true;
                _rankingUI.HighRankerScorebyGames(0);
                GameManager.Instance.OnClick_BtnSound1();
            }
            if (ob.name.Equals("Check2"))
            {
                OnScorePanel = true;
                _rankingUI.HighRankerScorebyGames(1);
                GameManager.Instance.OnClick_BtnSound1();
            }
            if (ob.name.Equals("Check3"))
            {
                OnScorePanel = true;
                _rankingUI.HighRankerScorebyGames(2);
                GameManager.Instance.OnClick_BtnSound1();
            }
            if (ob.name.Equals("Check4"))
            {
                OnScorePanel = true;
                _rankingUI.HighRankerScorebyGames(3);
                GameManager.Instance.OnClick_BtnSound1();
            }
            if (ob.name.Equals("Check5"))
            {
                OnScorePanel = true;
                _rankingUI.HighRankerScorebyGames(4);
                GameManager.Instance.OnClick_BtnSound1();
            }

            if (ob.name.Equals("Check6"))
            {
                OnScorePanel = true;
                _rankingUI.HighRankerScorebyGames(5);
                GameManager.Instance.OnClick_BtnSound1();
            }
            if (ob.name.Equals("Check7"))
            {
                OnScorePanel = true;
                _rankingUI.HighRankerScorebyGames(6);
                GameManager.Instance.OnClick_BtnSound1();
            }
            if (ob.name.Equals("Check8"))
            {
                OnScorePanel = true;
                _rankingUI.HighRankerScorebyGames(7);
                GameManager.Instance.OnClick_BtnSound1();
            }
            if (ob.name.Equals("Check9"))
            {
                OnScorePanel = true;
                _rankingUI.HighRankerScorebyGames(8);
                GameManager.Instance.OnClick_BtnSound1();

            }
            if (ob.name.Equals("Check10"))
            {
                OnScorePanel = true;
                _rankingUI.HighRankerScorebyGames(9);
                GameManager.Instance.OnClick_BtnSound1();

            }
            if (ob.name.Equals("Check11"))
            {
                OnScorePanel = true;
                _rankingUI.HighRankerScorebyGames(10);
                GameManager.Instance.OnClick_BtnSound1();

            }
            if (ob.name.Equals("Check12"))
            {
                OnScorePanel = true;
                _rankingUI.HighRankerScorebyGames(11);
                GameManager.Instance.OnClick_BtnSound1();
            }
        }
    }
}

