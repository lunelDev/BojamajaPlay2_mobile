using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class Eagle : MonoBehaviour
{
    Animator BirdAni;
    Animator PlayerAni;

    public BoxCollider BoxCollider;
    public GameObject ThisBird;
    public GameObject CameraShack;

    public Vector3 StartPos;
    public Vector3 MovePos;
    public Vector3 RandomPos;
    public bool hitBool = false;
    public float HitCountNum = 0;

    public float mul;
    private float mulCount;
    public float Rand;
    private void Awake()
    {
        BirdAni = ThisBird.gameObject.GetComponent<Animator>();
        PlayerAni = CameraShack.gameObject.GetComponent<Animator>();
        mulCount = 1;
        HitCount();

        mul = 15 * mulCount;
        StartPos = ThisBird.transform.position;
    }
    //맞은 횟수 카운트
    public void Damage()
    {
        HitCountNum += 1;
        hitBool = false; 
    }
    //괴물 새가 맞은 횟수 + 랜덤 애니 실행
    public void HitCount()
    {
        StartCoroutine(HitCount_());
    }
    IEnumerator HitCount_()
    {
        yield return new WaitUntil(() => AppManager.Instance.gameRunning.Equals( true));
        while (DataManager.Instance.timerManager.timeLeft > 0)
        {
            mul = 15 * mulCount;
            if (HitCountNum == mul)
            {
                Rand = Random.Range(1, 4);
                if (Rand.Equals(1))
                {
                  //  BoxCollider.enabled = false;
                    AttackAni();
                    PlayerAni.SetTrigger("Attacking");

                }
                else if (Rand.Equals(2))
                {
                  //  BoxCollider.enabled = false;
                    LeaveAni();

                }
                else if (Rand.Equals(3))
                {
                   // BoxCollider.enabled = false;
                    MoveAround();
                }
                mulCount += 1;
            }
      /*      if (BirdAni.GetCurrentAnimatorStateInfo(0).IsName("Attack_ani") 
                || BirdAni.GetCurrentAnimatorStateInfo(0).IsName("Leave_ani")
                || BirdAni.GetCurrentAnimatorStateInfo(0).IsName("MoveAround")
                && BirdAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
                BoxCollider.enabled = true;
            }*/

            if (DataManager.Instance.timerManager.timeLeft < 0.1f)
            {
                DeathAni();
            }
            yield return null;
        }
        yield return null;
    }
    //애니메이션
    public void AttackAni()
    {
        BirdAni.SetTrigger("Attack");
        SoundManager.Instance.PlaySFX("13 Bump to Tree");
        DataManager.Instance.scoreManager.Subtract(Random.Range(100, 200));
    }
    //////////////////// 괴물 새 관련 애니메이션 //////////////////////////
    public void LeaveAni()
    {
        BirdAni.SetTrigger("Leave");
        SoundManager.Instance.PlaySFX("07 Flying Bird"); //새가 날아 갈 때
    }
    public void DeathAni()
    {
        BirdAni.SetTrigger("Death");//죽었을 때
    }
    public void MoveAround()
    {
        BirdAni.SetTrigger("MoveAround");//주위를 날떄 
    }
}
