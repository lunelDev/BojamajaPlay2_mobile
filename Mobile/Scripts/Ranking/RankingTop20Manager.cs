using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RankingTop20Manager : MonoBehaviour
{
    public GameObject[] beActivatedPanel;   // 0 : normal, 1 : event
    public GameObject[] RankTopPanel;
    public GameObject[] RankTopPanel_event;
    public GameObject NextBtn;  //다음 랭킹
    public GameObject BackBtn; //전 랭킹
    public int _currentPage = 0; //0:5위 1:10위 2:15위 3:20위
    private int _beforePage = -1;

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

    private void Update()
    {
        if (ServerManager.Instance.RankingEventDoing)
        {
            RankPanel_event();
        }
        else
        {
            RankPanel();
        }
    }

    // 일반 //
    public void RankPanel()
    {
        //Debug.Log(_currentPage);
        //Debug.Log(_beforePage);
        if(_currentPage>=1&&_currentPage<RankTopPanel.Length)   // RankTopPanel.Length : 4
        {
            RankTopPanel[_beforePage].SetActive(false);
            RankTopPanel[_currentPage].SetActive(true);
            if(_currentPage != 3)
            {
                RankTopPanel[_currentPage + 1].SetActive(false);
            }     
            BackBtn.SetActive(true);
            NextBtn.SetActive(true);
        }
        if (_currentPage == 0)
        {
            RankTopPanel[_currentPage].SetActive(true);
            RankTopPanel[_currentPage+1].SetActive(false);
            BackBtn.SetActive(false);
            NextBtn.SetActive(true);
        }
        if(_currentPage == RankTopPanel.Length-1)
        {
            RankTopPanel[_beforePage].SetActive(false);
            BackBtn.SetActive(true);
            NextBtn.SetActive(false);
        }
     
    }

    // 이벤트 //

    public void RankPanel_event()
    {
        //Debug.Log(_currentPage);
        //Debug.Log(_beforePage);
        if (_currentPage >= 1 && _currentPage < RankTopPanel_event.Length)   // RankTopPanel.Length : 4
        {
            RankTopPanel_event[_beforePage].SetActive(false);
            RankTopPanel_event[_currentPage].SetActive(true);
            if (_currentPage != 3)
            {
                RankTopPanel_event[_currentPage + 1].SetActive(false);
            }
            BackBtn.SetActive(true);
            NextBtn.SetActive(true);
        }
        if (_currentPage == 0)
        {
            RankTopPanel_event[_currentPage].SetActive(true);
            RankTopPanel_event[_currentPage + 1].SetActive(false);
            BackBtn.SetActive(false);
            NextBtn.SetActive(true);
        }
        if (_currentPage == RankTopPanel_event.Length - 1)
        {
            RankTopPanel_event[_beforePage].SetActive(false);
            BackBtn.SetActive(true);
            NextBtn.SetActive(false);
        }

    }

    public void BackRank()
    {
        GameManager.Instance.OnClick_BtnSound1();
        _currentPage -= 1;
        _beforePage -= 1;
    }
    public void NextRank()
    {
        GameManager.Instance.OnClick_BtnSound1();
        _currentPage += 1; 
        _beforePage += 1; 
    }
    //랭킹보기에서 뒤로가기를 클릭했을 때,
    public void OnClick_GoBackMain()
    {
        _currentPage = 0;
        _beforePage = -1;
        SceneManager.LoadScene("Main");
    }
}
