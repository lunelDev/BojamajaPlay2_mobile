using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBirdRealMotion : MonoBehaviour
{
    public ParticleSystem[] HitParticle;
    private void OnCollisionEnter(Collision collision)
    {
        if (AppManager.Instance.gameRunning.Equals(true))
        {
            GameObject ob = collision.gameObject;
            SoundManager.Instance.ObSFXPlay1();
            ob.transform.gameObject.GetComponent<Eagle>().Damage();
            Instantiate(HitParticle[Random.Range(0, HitParticle.Length)], this.transform.position, Quaternion.identity);
            MBird_PlayerCtrl.Instance.GetScore(); //랜덤으로 점수 증가   

        }

    }
  
}
