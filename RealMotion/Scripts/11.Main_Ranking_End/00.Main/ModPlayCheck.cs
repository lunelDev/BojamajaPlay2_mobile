using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModPlayCheck : MonoBehaviour
{
    public GameObject NoDiaPanel;
    public GameObject t1;
    ///////////////////////////////////////////////////////////////////////////////////////////////
    //클래식 모드에서 다이아 1개 소모,
    public void UseDia_Classic()
    {

        GameManager.Instance.OnClick_BtnSound1();
        SceneManager.LoadScene(this.gameObject.name);

    }
    //랭킹 모드에서 다이아 2개 소모,
    public void UseDia_Rank()
    {
        GameManager.Instance.OnClick_BtnSound1();
        GameManager.ChangePanel = true;
        MainAppManager.instance.RankGameStartBtnClick(); //이미지 캔버스 활성화 관리 
        MainUIManager.instance.RandGameImge();
        MainDataManager.instance.RandStartDataSave(); //게임 랜덤으로 넣기
        GameManager.Instance.OnclickRankStartBtn_Restdata(); //앞 전의 게임의 데이터 값을 지움
        
    }
    /////////////////////////////////////////////////////////////////////////////////////////////
}
