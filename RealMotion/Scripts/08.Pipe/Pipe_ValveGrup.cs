using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe_ValveGrup : MonoBehaviour
{
    public GameObject[] Valve;
    public GameObject ParticleGrup;
    public int RandomPipe;
    public int RandomTime; 
    public static Pipe_ValveGrup Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else
            Instance = this; 
    }
    //게임이 시작되면 랜덤하게 밸브가 열리도록 설정하는 부분
    public void ValveBroken()
    {
        StartCoroutine(ValveBroken_());
    }
    IEnumerator ValveBroken_()
    {
        while(DataManager.Instance.timerManager.timeLeft>0){
            RandomTime = Random.Range(0, 5);
            RandomPipe = Random.Range(0, Valve.Length);
            yield return new WaitForSeconds(RandomTime);
            //랜덤 밸브가 열리지 않았으면 열어준다
            if (Valve[RandomPipe].GetComponent<Valve>().ValveOpen.Equals(false))
            {
                //게임이 진행중이면 밸브를 열어주고 게임이 끝났으면 빠져나옴
                if (AppManager.Instance.gameRunning.Equals(true))
                {
                    Valve[RandomPipe].GetComponent<Valve>().Open();
                }
                else
                {
                    break;
                }                
            }
            yield return null;
        }
        ParticleGrup.SetActive(false); //게임이 끝나면 실행되고 있는 파티클을 꺼줌
        yield return null;
    }
}
