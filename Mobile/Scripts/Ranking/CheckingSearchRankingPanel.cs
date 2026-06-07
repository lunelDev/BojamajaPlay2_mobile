using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckingSearchRankingPanel : MonoBehaviour
{
    public GameObject[] beActivatedPanel;   // 0 : normal, 1 : event

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
}
