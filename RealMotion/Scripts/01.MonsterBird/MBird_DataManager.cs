using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBird_DataManager : MonoBehaviour
{
    public float Timer;
   
    public static MBird_DataManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else Instance = this;
        //
        Timer = 30f;

    }


}
