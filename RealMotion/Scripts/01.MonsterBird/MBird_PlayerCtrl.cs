using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBird_PlayerCtrl : MonoBehaviour
{
    public Camera cam;
    public int Score;

    public LayerMask layerMask;

    public Vector3 StartPos;
    public Vector3 EndPos;
    RaycastHit Hit;
    Ray ray;

    public ParticleSystem[] HitParticle;
    public static MBird_PlayerCtrl Instance { get; private set; }

    public void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else Instance = this;
    }
    public void HitDamage()
    {
        StartCoroutine(HitDamage_());
    }
    IEnumerator HitDamage_()
    {
        while (DataManager.Instance.timerManager.timeLeft > 0)
        {
            Vector3 Mouse = Input.mousePosition;
            ray = cam.ScreenPointToRay(Mouse);
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 hitPos = Hit.point;
                ray = cam.ScreenPointToRay(Mouse);

                if (Physics.Raycast(ray, out Hit, 100f, layerMask))
                {
                    if (Hit.transform.gameObject.layer.Equals(LayerMask.NameToLayer("Buildings")))
                    {
                        SoundManager.Instance.ObSFXPlay1();
                        Hit.transform.gameObject.GetComponent<Eagle>().Damage();
                        Instantiate(HitParticle[Random.Range(0, HitParticle.Length)], hitPos, Quaternion.identity);
                        GetScore(); //랜덤으로 점수 증가   
                 
                    }
                }
            }
            yield return null;
        }

    }
    ////////////////////////////// 랜덤으로 점수를 올려줌 /////////////////////////////////////////////////////////////
    public void GetScore()
    {
        Score = 150;
        DataManager.Instance.scoreManager.Add(Score);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
}


