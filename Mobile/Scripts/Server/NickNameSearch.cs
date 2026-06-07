using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NickNameSearch : MonoBehaviour
{
    [SerializeField] private Text nickname;
    [SerializeField] private GameObject cantfindNickName;
    [SerializeField] private GameObject MyRankingPanel;
    // Start is called before the first frame update
    void Start()
    {
        MyRankingPanel.SetActive(false);
        cantfindNickName.SetActive(false);
    }

    public void SetRankingPanel()
    {
        MyRankingPanel.SetActive(true);
    }

    // 닉네임 서치 하는 함수
    public void SearchingNickName()
    {
        StopAllCoroutines();

        StartCoroutine(_GetNetworkState());
    }

    IEnumerator _GetNetworkState()
    {
        ServerManager.Instance.GetNetworkState();

        yield return new WaitUntil(() => ServerManager.Instance.isConnCompleted);

        if (ServerManager.Instance.isConnected)
        {
            StartCoroutine(_SearchingNickName());
        }
    }

    IEnumerator _SearchingNickName()
    {
        //서버에 서치 닉네임
        ServerManager.Instance.RankingSearchByNickName(nickname.text);

        yield return new WaitUntil(() => ServerManager.Instance.isSearchComplete);

        // 있으면 있다고 없으면 없다고 처리하기 
        if (ServerManager.Instance.isExistName)
        {
            // 닉네임이 있으면 검색
            ServerManager.Instance.RankingSearching(nickname.text);
            
            yield return new WaitUntil(() => ServerManager.Instance.isMyListStackUp);

            MyRankingPanel.SetActive(true);
        }
        else
        {
            // 닉네임을 찾을수 없으면
            cantfindNickName.SetActive(true);
            StartCoroutine(_DisappearPopUp());
            GameManager.Instance.OnClick_BtnSound3();
        }

        ServerManager.Instance.isSearchComplete = false;
    }

    IEnumerator _DisappearPopUp()
    {
        WaitForSeconds ws = new WaitForSeconds(2f);

        yield return ws;

        cantfindNickName.SetActive(false);
    }
}
