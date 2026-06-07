using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : MonoBehaviour
{
    [Header("조각상 Part")]
    public GameObject part1; //미완성
    public GameObject part2; 
    public GameObject part3; 
    public GameObject part4; 
    public GameObject part5; //완성

    public GameObject StatueOb;
    public GameObject FallStonPeb;
    public Transform StonPos;
    public float hitCount;

    public Vector3 StartPos;
    public Vector3 EndPos;
    public Vector3 StonPosV;
    private void Start()
    {
        hitCount = 0;       
        part2.SetActive(false);
        part3.SetActive(false);
        part4.SetActive(false);
        part5.SetActive(false);
        ChangePart();
        StartPos = this.gameObject.transform.position;
        EndPos = new Vector3(1.2f, StartPos.y, StartPos.z);
        StonPosV = StonPos.transform.position;
    }
    //파트가 변할 때 마다 돌을 생성
    public void FallSton()
    {
        Instantiate(FallStonPeb, StonPosV, Quaternion.identity);
    }
    //hitCount의 정도에 따라 오브젝트 형상 변경
    public void ChangePart()
    {
        StartCoroutine(_ChangePart()); 
    }
    IEnumerator _ChangePart()
    {
        //게임 라운드가 진행되면 hitCount를 입력받음
        yield return new WaitUntil(() => AppManager.Instance.gameRunning.Equals(true));
        while (DataManager.Instance.timerManager.timeLeft > 0)
        {
            if (hitCount == 10)
            {
                FallSton();
                StonPosV.y = 5.7f;
                hitCount += 1;
            }
            if (hitCount >= 10 && hitCount < 20)
            {
                part1.SetActive(false);
                part2.SetActive(true);
              
            }
            if(hitCount == 20)
            {
                FallSton();
                hitCount += 1;
                StonPosV.y = 2.8f;
            }
            if (hitCount >= 20 && hitCount < 30)
            {
                part2.SetActive(false);
                part3.SetActive(true);
              
            }
            if (hitCount == 30)
            {
                FallSton();
                StonPosV.y = 2.31f;
                hitCount += 1;
            }
            if (hitCount >= 30 && hitCount < 40)
            {
                part3.SetActive(false);
                part4.SetActive(true);
                
            }
            if (hitCount == 35)
            {
                FallSton();
                StonPosV.y = -2.14f;
                hitCount += 1;
            }
            if (hitCount >= 35)
            {
                part4.SetActive(false);
                part5.SetActive(true);             
                Statue_PlayerCtrl.Instance.ParticleON(); //파티클 활성화
   
                Statue_PlayerCtrl.Instance.StatueCompletSet = true;
                yield return new WaitForSeconds(0.5f);           
                Statue_PlayerCtrl.Instance.ParticleOFF(); //파티클 비활성화
                hitCount += 1;               
            }
            if (hitCount >= 40)
            {
                SoundManager.Instance.ObSFXPlay2();
                Statue_PlayerCtrl.Instance.GetScore_();  //점수 추가 
                Statue_PlayerCtrl.Instance.Move(); //카메라 이동
                Statue_PlayerCtrl.Instance.StatueCompletSet = false;
            
                //사라지게 하기               
                Destroy(this.gameObject);
            }
            yield return null;
        }
        
    }
}
