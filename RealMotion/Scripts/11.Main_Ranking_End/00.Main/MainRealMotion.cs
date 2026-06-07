using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainRealMotion : MonoBehaviour
{
    public ModPlayCheck modePlayCheck;
    public ModeCanvasManager modeCanvasManager;
    public GameObject[] Classic_Game;
    int GamePos = 0;
    private void OnCollisionExit(Collision collision)
    {
        GameObject col = collision.gameObject;
        if (col.name.Equals("StartCol"))
        {
            GameManager.ModePanel = true;
            MainUIManager.instance.ModeSelect.SetActive(true);
            MainUIManager.instance.VideoCanvas.SetActive(false);
        }
        if (col.name.Equals("ClassicModMab"))
        {
            modeCanvasManager.ClassicMapRotationAni();
        }
        if (col.name.Equals("RankModMab"))
        {
            modeCanvasManager.RankMapRotationAni();
        }
        if (col.name.Equals("RankStartBtn"))
        {
            modePlayCheck.UseDia_Rank();
        }
        if (col.name.Equals("StartButton"))
        {
            //GameManager.ChangePanel = false;
            GameManager.Instance.ClickRankMode();
            GameManager.Instance.NextScenes();
            GameManager.Instance.OnClick_BtnSound1();
        }
        if (col.name.Equals("ShowRankBtn"))
        {
            GameManager.Instance.Ranking(12);
        }
        ///////////////// 클래식 모드 게임 선택 시, 사용되는 콜라이더 ///////////////////////// 
        if (col.name.Equals("LeftMove") && GameManager.ClassicPanel.Equals(true))
        {

            if (GamePos > 0)
            {
                Debug.Log("LeftMove" + GamePos);
                GamePos -= 1;
                Classic_Game[GamePos + 1].SetActive(false);
                Classic_Game[GamePos].SetActive(true);
            }


        }
        if (col.name.Equals("RightMove") && GameManager.ClassicPanel.Equals(true))
        {

            if (GamePos < Classic_Game.Length - 1)
            {
                Debug.Log("RightMove" + GamePos);
                GamePos += 1;
                Classic_Game[GamePos - 1].SetActive(false);
                Classic_Game[GamePos].SetActive(true);
            }
        }
        if (col.name.Equals("ClassicBtn"))
        {
            string currentGAME_str = Classic_Game[GamePos].name;
            Debug.Log("" + currentGAME_str);
            SceneManager.LoadScene(currentGAME_str);
        }
    }
}
