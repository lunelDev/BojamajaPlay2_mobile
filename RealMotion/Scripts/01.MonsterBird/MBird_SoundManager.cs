using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MBird_SoundManager : MonoBehaviour
{
    [Header("오디오 소스")]
    //기본
    public AudioSource Bgm;
    public AudioSource Clear;
    public AudioSource Fail;
    public AudioSource Left10;
    public AudioSource TimerImgChange;
    public AudioSource CountDown;
    //
    public AudioSource BirdHit;
    public AudioSource Gun;
    public AudioSource Leave;
    public AudioSource BabyBird;
    public AudioSource TreeShake;

    [Header("오디오 클립")]
    //기본
    public AudioClip clear_;
    public AudioClip fail_;
    public AudioClip left10_;
    public AudioClip timerImgChange_;
    public AudioClip countDown_;
    //
    public AudioClip BirdHit_;
    public AudioClip []Gun_;
    public AudioClip Leave_;
   
    public AudioClip BabyBird_;
    public AudioClip TreeShake_;
    public static MBird_SoundManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else
            Instance = this; 
    }
  
    //게임 시작 했을 때, 카운트 다운
    public void CountSound()
    {
        CountDown.PlayOneShot(countDown_);
    }
    //타임바 이미지 바뀔 때
    public void TimerImgChangSound()
    {
        TimerImgChange.PlayOneShot(timerImgChange_);
    }
    //10초 남았을 때
    public void Left10Sound()
    {
        Left10.PlayOneShot(left10_);
    }
    //게임을 클리어 했을 때
    public void PlayClearSound()
    {
        Clear.PlayOneShot(clear_);
    }
    //게임을 실패 했을 떄 
    public void PlayFailSound()
    {
        Fail.PlayOneShot(fail_);
    }
    //어미새가 맞았을 때
    public void PlayBirdHit()
    {
        BirdHit.PlayOneShot(BirdHit_);
        BirdHit.volume = 0.3f;
    }
    //총을 쐇을 때
    public void PlayGun()
    {
        Gun.clip = Gun_[Random.Range(0, Gun_.Length)];
        Gun.Play();
    }
    //새가 날아 갈 때 
    public void PlayLeave()
    {
        Leave.PlayOneShot(Leave_);
    }
    //아기 새 소리
    public void PlayBabyBird()
    {
        BabyBird.PlayOneShot(BabyBird_);
        BabyBird.volume = 0.042f;
    }
    public void PlayTreeShake()
    {
        TreeShake.PlayOneShot(TreeShake_);
      
    }
    //사운드 끔
    public void StopAllSound()
    {
   
        BabyBird.Stop();
        Bgm.Stop();
        Left10.Stop();
    }

}
