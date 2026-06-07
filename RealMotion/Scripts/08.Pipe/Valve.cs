using System.Collections;
using System.Collections.Generic;
//using System.Security.Policy;
//using TreeEditor;
using UnityEngine;

//벨브 물터치는 파티클 관리 
public class Valve : MonoBehaviour
{
    public int ValveCloseCount; 
    public float WaterTimer;
    public int ValveLevel;
    public bool ValveOpen = false;
    //
    public GameObject Water1;
    public GameObject Water2;
    public GameObject Water3;

    public GameObject[] ClearParticle;
    public AudioSource WaterOpen;

     int randomParticle;

    //밸브가 열렸을 때, 동작하는 코드
    public void Open()
    {
        if (AppManager.Instance.gameRunning.Equals(true))
        {
            WaterTimer = 10;
            ValveOpen = true;

            WaterSoundPlay(); //물터지는 소리 play
            StartCoroutine(Open_());    //파티클 재생, 3단계 조절
            StartCoroutine(Bound_());   //밸브 움직임
            StartCoroutine(TouchValve_()); 
        }
        else
        {
            StopAllCoroutines();
        }

    }
    //밸브가 열리면 시간에 따라서 파티클이 활성화 되고 단계 별로 파티클 점점 활성화
    IEnumerator Open_()
    {
        while (ValveOpen.Equals(true))
        {
            WaterTimer -= Time.deltaTime;
            //약
            if (WaterTimer >= 7 && WaterTimer <= 10)
            {
                Water1.SetActive(true);
                ValveLevel = 1;
            }
            //중
            else if (WaterTimer < 7 && WaterTimer >= 4)
            {

                Water1.SetActive(true);
                Water2.SetActive(true);
         
                ValveLevel = 2;

            }
            //강
            else if (WaterTimer < 4 && WaterTimer >= 0)
            {

                Water1.SetActive(true);
                Water2.SetActive(true);
                Water3.SetActive(true);

                ValveLevel = 3;
            }
            //밸브가 터짐
            else if (WaterTimer < 0)
            {
                
                ResetValve();
                SoundManager.Instance.PlaySFX("Boom!"); //쾅 소리
                WaveUp(); //물이 차오름
                DataManager.Instance.scoreManager.Subtract(200);
                Pipe_PlayerCtrl.Instance.BrokenShanking(); 
                ValveOpen = false;
            }
            //게임이 끝나면 활성화된 코루틴과 사운드를 멈춤
            if (AppManager.Instance.gameRunning.Equals(false))
            {
                StopAllCoroutines();
                WaterSoundStop();
                break;
            }
            yield return null;
        }
        SetActiveFalse();
    }
    //터치를 해서 밸브카운트를 올려주고 각 단계마다 카운트값을 다르게 해주고 해당 단계에 맞는 카운트가 되면 점수를 획득
    IEnumerator TouchValve_()
    {
        ValveCloseCount = 0;
        while (ValveOpen == true)
        {
            //1단계
            if (ValveLevel == 1)
            {
                if (ValveCloseCount >= 3)
                {
                    ResetValve();
                    SetActiveTrue();
                    SoundManager.Instance.ObSFXPlay2(); //밸브가 잠겼을 경우,
                    DataManager.Instance.scoreManager.Add(Random.Range(800, 1000));
                    yield return new WaitForSeconds(1f);
                    SetActiveFalse();
                }
            }
            //2단계
            if (ValveLevel == 2)
            {
                if (ValveCloseCount >= 4)
                {
                    ResetValve();
                    SetActiveTrue();
                    SoundManager.Instance.ObSFXPlay2(); //밸브가 잠겼을 경우,
                    DataManager.Instance.scoreManager.Add(Random.Range(800  , 1000));
                    yield return new WaitForSeconds(1f);
                    SetActiveFalse();
                }
            }
            //3단계
            if (ValveLevel == 3)
            {
                if (ValveCloseCount >= 5)
                {
                    ResetValve();
                    SetActiveTrue();
                    SoundManager.Instance.ObSFXPlay2(); //밸브가 잠겼을 경우,
                    DataManager.Instance.scoreManager.Add(Random.Range(800  , 1000));
                    yield return new WaitForSeconds(1f);
                    SetActiveFalse();
                }
            }
            yield return null;
        }
    }
    //밸브를 돌리는 모션 
    public void TrunValve()
    {
        transform.Rotate(new Vector3(0, 0, 30), Space.Self);
    }
    //밸브를 잠궜을 때, 모든 설정 리셋
    public void ResetValve()
    {
        Water1.SetActive(false);
        Water2.SetActive(false);
        Water3.SetActive(false);
       
        WaterTimer = 10f;
        ValveLevel = 0;
        ValveOpen = false;
        WaterSoundStop();   //물 터지는 소리 Off
    }
    //밸브가 열렸을 떄,핸들이 움직이는(바운드) 모션
    IEnumerator Bound_()
    {
        while (ValveOpen.Equals(true))
        {
            this.transform.localScale += new Vector3(0.4f, 0.4f, 0.4f);
            yield return new WaitForSecondsRealtime(0.1f);
            this.transform.localScale -= new Vector3(0.8f, 0.8f, 0.8f);
            yield return new WaitForSecondsRealtime(0.1f);
            this.transform.localScale += new Vector3(0.4f, 0.4f, 0.4f);
            yield return new WaitForSecondsRealtime(0.1f);
            yield return null;
        }
    }
    ////////////////////////// 밸브 관련 파티클 관리///////////////////////////////   
    //파티클 관리, 밸브가 잠겼을 때, 랜덤으로 파티클을 활성화 시켜줌
    public void SetActiveTrue()
    {
        randomParticle = Random.Range(0, ClearParticle.Length);
        ClearParticle[randomParticle].SetActive(true);
    }
    //활성화 된 파티클을 비활성화 시킴
    public void SetActiveFalse()
    {
        ClearParticle[randomParticle].SetActive(false);
    }
    //////////////////////////// 밸브 물 터지는 소리 //////////////////////////////////        
    //물소리 터지는 소리 관리 
    public void WaterSoundPlay()
    {
        WaterOpen.Play();
    }
    public void WaterSoundStop()
    {
        WaterOpen.Stop();
    }

    //////////////////밸브를 잠구지 못했을 때,물이 올라오도록 설정 /////////////////////////
    public void WaveUp()
    {
        LowPolyWater.LowPolyWater.Instance.waveHeight += 0.4f;
        LowPolyWater.LowPolyWater.Instance.waveLength += 0.4f;
    }

}
