using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeRealMotion : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (AppManager.Instance.gameRunning.Equals(true))
        {
            GameObject ob = collision.gameObject;
            SoundManager.Instance.ObSFXPlay1(); //밸브 돌리는 소리 
            if (ob.GetComponent<Valve>().ValveOpen.Equals(true))
            {
                ob.transform.gameObject.GetComponent<Valve>().ValveCloseCount += 1;
            }
            ob.transform.gameObject.GetComponent<Valve>().TrunValve(); //밸브 돌리는 모션
        }
    }
}
