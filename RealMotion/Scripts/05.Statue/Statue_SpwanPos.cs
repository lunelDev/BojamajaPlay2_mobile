using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue_SpwanPos : MonoBehaviour
{
    public GameObject[] statue;
    public int StatueKind = 3;
    public Transform[] pos;
    public Vector3[] posV;
    public static Statue_SpwanPos Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else Instance = this;
        SetRandPos();
        
    }
    // 조각상을 랜덤으로 생성
    public void SetRandPos()
    {
        for (int i = 0; i < pos.Length; i++)
        {
            Instantiate(statue[Random.Range(0, StatueKind)],pos[i]);          
        }
        for (int i = 0; i < pos.Length; i++)
        {
            posV[i] = new Vector3(pos[i].transform.position.x, pos[i].transform.position.y, pos[i].transform.position.z);
        }
    }
}
