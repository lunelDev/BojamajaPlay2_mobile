using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue_PlayerCtrl : MonoBehaviour
{
    public Camera cam;
    public GameObject playerOB; 
    public LayerMask layerMask;
    public Vector3 MovePos;
    public Vector3 playerPos;
 
    public ParticleSystem [] HitParticle;
    public GameObject[] CompleteParticle; //완성했을 때
  
    public int StatueCount = 1;
    public int particleNum; 
    public bool StatueCompletSet = false;

    public int RandomScore;
    private string StatueName;

    public static Statue_PlayerCtrl Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else Instance = this;
       
        playerPos = this.gameObject.transform.position;
        MovePos = new Vector3(playerPos.x, playerPos.y, playerPos.z + 10);
    
   }
    //랜덤으로 파티클 활성화
    public void ParticleON()
    {
        particleNum = Random.Range(0, CompleteParticle.Length);
        CompleteParticle[particleNum].SetActive(true);
    }
    //활성화 된 파티클 비활성화 
    public void ParticleOFF()
    {
        CompleteParticle[particleNum].SetActive(false);
    }

    //조각상이 깨졌을 때 다음 조각상으로 이동
    public void Move()
    {
        playerPos = this.gameObject.transform.position;
        MovePos = new Vector3(Statue_SpwanPos.Instance.posV[StatueCount].x, playerPos.y, playerPos.z + 10);     
        StartCoroutine(_Move());
        StatueCount += 1;
    }
    IEnumerator _Move()
    {
        //오브젝트가 파괴됬을 때 카메라 무빙
        while (playerPos != MovePos)
        {
            transform.position = Vector3.Lerp(transform.position, MovePos, Time.deltaTime * 2);
            yield return null;
        }
        StatueCompletSet = true;
        yield return null;
    }
    public string GetName(GameObject ob)
    {
        return StatueName = ob.name;
    }
    //조각상의 이름을 받아와 이름에 따라 점수를 나눔
    public void GetScore_()
    {
        if (StatueName.Equals("T1_Statue(Clone)"))
        {
            RandomScore = Random.Range(2500, 3500);
            DataManager.Instance.scoreManager.Add(RandomScore);
        }
        else if (StatueName.Equals("Harp_Statue(Clone)"))
        {
            RandomScore = Random.Range(2000, 3000);
            DataManager.Instance.scoreManager.Add(RandomScore);
        }
        else if (StatueName.Equals("Flamingo_Statue(Clone)"))
        {
            RandomScore = Random.Range(1500, 2500);
            DataManager.Instance.scoreManager.Add(RandomScore);
        }
     //   Debug.Log(DataManager.Instance.scoreManager.score);
    }

}
