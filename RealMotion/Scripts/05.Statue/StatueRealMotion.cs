using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueRealMotion : MonoBehaviour
{
    public bool StatueCompletSet = false;

    public ParticleSystem[] HitParticle;
    private void OnTriggerEnter(Collider other)
    {
        GameObject ob = other.gameObject;
        //석상이 완성 되지 않았을 때, hitCount와 사운드를 재생
        if (StatueCompletSet.Equals(false) && AppManager.Instance.gameRunning.Equals(true))
        {        //layer.가 Bulidings 일경우,
            if (ob.layer.Equals(16))
            {
                ob.GetComponent<Statue>().hitCount += 1;
                SoundManager.Instance.ObSFXPlay1(); //석상 때리는 소리
                Statue_PlayerCtrl.Instance.GetName(ob);
                DataManager.Instance.scoreManager.Add(10);
                Instantiate(HitParticle[Random.Range(0, HitParticle.Length)], ob.transform.position, Quaternion.identity);

            }
        }
    }
}
