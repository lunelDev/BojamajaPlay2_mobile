using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneRealMotion : MonoBehaviour
{
    public GameObject My_rank_panel;
    public GameObject RankPanelOB; 
    public MyRankingManager _my_ranking_manager;
    public bool OnScorePanel = false;
    private void OnCollisionExit(Collision collision)
    {
        GameObject col = collision.gameObject;
        //랭킹보기
        if (col.name.Equals("Registration"))
        {
            GameManager.Instance.OnClick_BtnSound1();
            RankPanelOB.SetActive(true);
        }
        //메인으로
        if (col.name.Equals("GoMain"))
        {
            GameManager.Instance.OnClick_BtnSound1();
            GameManager.Instance.OnClick_Home_Button_rank();
        }
        //다시하기
        if (col.name.Equals("Retry"))
        {
            GameManager.Instance.OnClick_BtnSound1();
            End_UIManager.Instance.OnClickRetry_rank();
        }
        //////////////////////////// MyRanking //////////////////////////
       
        if (OnScorePanel.Equals(false))
        {
            if (col.name.Equals("Check1"))
            {
                OnScorePanel = true; 
                GameManager.Instance.OnClick_BtnSound1();
                _my_ranking_manager.ScorebyGames(0);
            }
            if (col.name.Equals("Check2"))
            {
                OnScorePanel = true;
                GameManager.Instance.OnClick_BtnSound1();
                _my_ranking_manager.ScorebyGames(1);
            }
            if (col.name.Equals("Check3"))
            {
                OnScorePanel = true;
                GameManager.Instance.OnClick_BtnSound1();
                _my_ranking_manager.ScorebyGames(2);
            }

        }
      
        //메인으로
        if (col.name.Equals("MainBtn"))
        {
            GameManager.Instance.OnClick_BtnSound1();
            GameManager.Instance.OnClick_Home_Button_rank();
        }
        //////////////////////////////////////////////////////////////////    
    }
}
