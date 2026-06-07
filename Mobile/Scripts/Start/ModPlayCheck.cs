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
       
        if (GameManager.HaveOneDayFree.Equals(false))
        {
            int leftDia = TimeManager.instance.diamondSu - 1;
            if (leftDia < 0)
            {
                //다이아 없다는 판넬
                GameManager.Instance.OnClick_BtnSound3();
                NoDiaPanel.SetActive(true);
            }
            else
            {
                GameManager.Instance.OnClick_BtnSound1();
                SceneManager.LoadScene(this.gameObject.name);
                TimeManager.instance.diamondSu -= 1;
                PlayerPrefs.SetInt("Diamond", TimeManager.instance.diamondSu);   //다이아몬드 저장
            }
        }
        else
        {
            //1일 무료권 소지
            SceneManager.LoadScene(this.gameObject.name);
        }
       
    }
    //랭킹 모드에서 다이아 2개 소모,
    public void UseDia_Rank()
    {
        if (GameManager.HaveOneDayFree.Equals(false))
        {

            int leftDia = TimeManager.instance.diamondSu - 2;
            if (leftDia < 0)
            {
                GameManager.Instance.OnClick_BtnSound3();
                //다이아 없다는 판넬
                NoDiaPanel.SetActive(true);
            }
            else
            {
                GameManager.Instance.OnClick_BtnSound1();

                t1.SetActive(false); //t1사라지게하기 
                TimeManager.instance.diamondSu -= 2;
                PlayerPrefs.SetInt("Diamond", TimeManager.instance.diamondSu);   //다이아몬드 저장
                MainUIManager.instance.RandGameImge(); //게임 시작시 랜덤으로 이미지 돌아가게 함
                MainAppManager.instance.RankGameStartBtnClick(); //이미지 캔버스 활성화 관리 
                MainDataManager.instance.RandStartDataSave(); //게임 랜덤으로 넣기
                GameManager.Instance.OnclickRankStartBtn_Restdata(); //앞 전의 게임의 데이터 값을 지움
            }
        }
        else
        {
            //1일 무료권 소지
            t1.SetActive(false); //t1사라지게하기 
            MainUIManager.instance.RandGameImge(); //게임 시작시 랜덤으로 이미지 돌아가게 함
            MainAppManager.instance.RankGameStartBtnClick(); //이미지 캔버스 활성화 관리 
            MainDataManager.instance.RandStartDataSave(); //게임 랜덤으로 넣기
            GameManager.Instance.OnclickRankStartBtn_Restdata(); //앞 전의 게임의 데이터 값을 지움
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////
}
