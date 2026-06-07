using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PinSpawner : MonoBehaviour
{
    public GameObject pinsPrefab;
    public int scorePerPin;

    int _phase = 1;
    List<BowlingPin> _standingPins;
    List<BowlingPin> _fallenPins = new List<BowlingPin>();
    Bowling.PlayerController _playerController;


    void Start()
    {
        _standingPins = GetComponentsInChildren<BowlingPin>().ToList();
        _playerController = FindObjectOfType<Bowling.PlayerController>();
    }

    public void TransitionToPhase(int _phase)
    {
        this._phase = _phase;
    }
    //핀이 넘어 갔는지 확인
    public void FellPin(BowlingPin pin)
    {
        _standingPins.Remove(pin);//넘어진 핀은 제거
        _fallenPins.Add(pin);//넘어갔다고 체크 
    }

    public void EvaluatePins(float waitTime)
    {
        StopCoroutine("_EvaluatePins");
        StartCoroutine("_EvaluatePins", waitTime);
    }

    private IEnumerator _EvaluatePins(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        //   print("Standing: " + _standingPins.Count + "/10");
        //  print("Fallen: " + _fallenPins.Count + "/10");

        //넘어진 볼링 핀의 개수만큼 점수를 추가 해준다.
        DataManager.Instance.scoreManager.Add(scorePerPin * _fallenPins.Count);

        if (_standingPins.Count > 0 && _phase == 1)
        {
            WipeFallen();
        }

        else
        {
            WipeAll();
        }
        
    }

    void WipeAll()
    {
        Destroy(transform.GetChild(0).gameObject);
        //볼링핀 그룹을 생성한다.
        GameObject go = Instantiate(pinsPrefab);
        go.transform.SetParent(transform, false);
        go.transform.localPosition = Vector3.zero;
        go.transform.localScale = Vector3.one;
        //볼링 핀 리스트를 리셋해준다.
        _standingPins.Clear();
        _fallenPins.Clear();
        _standingPins = go.GetComponentsInChildren<BowlingPin>().ToList();
        if (_phase.Equals(1))
        {
            DataManager.Instance.scoreManager.Add(1000); //1000점 추가
        }   
        TransitionToPhase(1);
    }

    void WipeFallen()
    {
        foreach (var pin in _fallenPins)
            pin.gameObject.SetActive(false);

        TransitionToPhase(2); // 굴린 횟수
        _fallenPins.Clear();
    }
}
