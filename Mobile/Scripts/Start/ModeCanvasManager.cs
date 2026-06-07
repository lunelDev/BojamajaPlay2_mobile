using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 메인에서 캔버스에서 사용되는 Data를 관리
/// </summary>
public class ModeCanvasManager : MonoBehaviour
{
    public GameObject ClassicMapScale;
    public GameObject RankMapScale;
    //classic
    public GameObject ClassicModePanel;
    public Sprite[] StarCollection; //게임의 별 이미지 저장
    public Image[] GamePanel;

    public Animator ClassicModAni;
    public Animator RankModAni;
    private Ray ray;
    private RaycastHit hit;
    bool animationTrue = false;
    private int rankingSearchAmount = 5;

   
    
    private void Update()
    {
        //클래식 팝업이 활성화 될 때, 획득한 별의 이미지로 변환
        if (ClassicModePanel.activeSelf.Equals(true))
        {
            SetStar_Classic();
        }
        //팝업창이 비활성화 일 경우,
        if (PopUpSystem.PopUpState.Equals(false))
        {
            //랭크 게임 안, t1애니메이션 +
            if (Input.GetMouseButtonDown(0))
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 1000f))
                {
                    if (animationTrue.Equals(false))
                    {                   
                        //섬을 선택했을 때, 애니메이션 시작
                        if (hit.collider.gameObject.name.Equals("ClassicModMab"))
                        {
                            ClassicModAni.SetTrigger("touch");
                            ClassicMapRotationAni();
                        }
                        else if (hit.collider.gameObject.name.Equals("RankModMab"))
                        {
                            RankModAni.SetTrigger("touch");
                            RankMapRotationAni();
                        }
                     
                    }
                }
            }
        }
    }
    ///////////////////// 맵을 클릭했을 때, 애니메이션이 시작하고 끝날때, 다음 해당 모드로 이동//////////////////
    //////////////////////////////  랭킹 모드 애니 /////////////////////////////////////
    float exitTime = 0.8f;
    public void RankMapRotationAni()
    {
        StartCoroutine(_RankMapRotationAni());
    }
    IEnumerator _RankMapRotationAni()
    {
        animationTrue = true;
       
        while (!RankModAni.GetCurrentAnimatorStateInfo(0).IsName("rotation"))
        {     
            //전환 중일 때 실행되는 부분
            RankScaleClick();
            yield return null;
        }
        // 서버에서 랭킹 5위까지 가져오는 부분
        ServerManager.Instance.RankingSearch(rankingSearchAmount);

        // 서버에서 데이터 들고올때 로딩이 필요한 부분///////////////////////////////////////////////

        //애니메이션 완료 후 실행되는 부 분     
        yield return new WaitForSeconds(1f);

        animationTrue = false;
        RankMapScale.transform.localScale = new Vector3(1f, 1f, 1f);
        MainAppManager.instance.onClick_RankBtn();

        yield return null;
    }

    //////////////////////////////  클래식 모드 애니 /////////////////////////////////////
    public void ClassicMapRotationAni()
    {
        StartCoroutine(_ClassicMapRotationAni());
    }
    IEnumerator _ClassicMapRotationAni()
    {
        animationTrue = true;
      
        while (!ClassicModAni.GetCurrentAnimatorStateInfo(0).IsName("rotation"))
        {
            //전환 중일 때 실행되는 부분
            ClassicScaleClick();
            yield return null;
        }
        yield return new WaitForSeconds(0.8f);
        animationTrue = false;
        //애니메이션 완료 후 실행되는 부 분
        ClassicMapScale.transform.localScale = new Vector3(1f, 1f, 1f);
        MainAppManager.instance.onClick_ClassicBtn();
        yield return null;
    }

    //클래식 모드, 게임의 획득한 별을 보여준다 
    void SetStar_Classic()
    {
        for (int i = 0; i < GamePanel.Length; i++)
        {
            int star = GamePanel[i].GetComponent<ClassicMod>().BestScoreStar;
            GamePanel[i].sprite = StarCollection[star];
        }
    }
    //////////////////////////////////////////////////////////////
    //모드선택 판넬에서 모드를 선택했을 때 글자의 크기가 확대 되도록 함
    public void RankScaleClick()
    {
        StartCoroutine(_RankScaleClick());
    }
    IEnumerator _RankScaleClick()
    {
        RankMapScale.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        yield return null;
    }
    public void ClassicScaleClick()
    {
        StartCoroutine(_ClassicScaleClick());
    }
    IEnumerator _ClassicScaleClick()
    {
        ClassicMapScale.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
       // yield return new WaitForSeconds(0.5f);
       // ClassicMapScale.transform.localScale = new Vector3(1f, 1f, 1f);
        // MainAppManager.instance.onClick_ClassicBtn();
        yield return null;
    }
    //////////////////////////////////////////////////////////////////////////

}
