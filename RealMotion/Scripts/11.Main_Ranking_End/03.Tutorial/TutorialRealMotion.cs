using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialRealMotion : MonoBehaviour
{
    private void OnCollisionExit(Collision collision)
    {
        GameObject col = collision.gameObject;
        if (col.name.Equals("Stap2_NextBtn"))
        {
            TutorialManager.Instance.stap2_cool.SetActive(true);
        }
    }
}
