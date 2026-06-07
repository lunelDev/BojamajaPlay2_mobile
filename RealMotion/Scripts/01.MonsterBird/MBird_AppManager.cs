using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBird_AppManager : MonoBehaviour
{
    public bool Playing = false;
    public GameObject ParticleGrup;
    public Animator BirdDeath; 
    public static MBird_AppManager Instance { get; private set; }

    public void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else Instance = this;
    }
    public void Start()
    {
       // MBird_UIManager.Instance.StartCount();
    }
    public void GameRoundStart()
    {
       // Playing = true;
        //AppManager.Instance.gameRunning = true;
       // MBird_DataManager.Instance.TimerOn();
       // MBird_PlayerCtrl.Instance.HitDamage();
    }
    public void GameRoundEnd()
    {  
        Playing = false;
        //MBird_SoundManager.Instance.PlayBirdHit();
        //MBird_SoundManager.Instance.StopAllSound();
        if (MBird_PlayerCtrl.Instance.Score > 0)
        {
          
        }
        else
        {
            //MBird_UIManager.Instance.Fail();
        }

    }
}
