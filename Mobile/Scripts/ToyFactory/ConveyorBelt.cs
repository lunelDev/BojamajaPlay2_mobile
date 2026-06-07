using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyFactory
{
  public class ConveyorBelt : MonoBehaviour
  {
    public GameObject[] toyPartPrefabs;
        public Animator ConveyerAni; 


    string _lastToy = "";
    int _index = -3;
    int _counter = -3;
    [SerializeField, HideInInspector] List<Transform> _conveyorBeltBoards = new List<Transform>();
    [SerializeField, HideInInspector] List<GameObject> _toyPartsToSpawn;

        private void Update()
        {

            //기본 애니메이션 스피트 0.5f
            if (AppManager.Instance.gameRunning.Equals(true))
            {
                if (DataManager.Instance.timerManager.timeLeft <= 20 && DataManager.Instance.timerManager.timeLeft > 10)
                {
                    ConveyerAni.SetFloat("speed",0.8f);
                }
                if (DataManager.Instance.timerManager.timeLeft <= 10)
                {
                    ConveyerAni.SetFloat("speed", 1.2f);            
                }
            }
        }

        private void OnValidate()
    {
      _conveyorBeltBoards.Clear();
      for (int i = 0; i < 67; i++)
      {
        _conveyorBeltBoards.Add(transform.GetChild(i));
      }
      _toyPartsToSpawn = new List<GameObject>(toyPartPrefabs);
    }

    private void OnTriggerEnter(Collider other)
    {
      if (other.gameObject.layer == LayerMask.NameToLayer("Water"))
      {
        _counter++;
        if (_counter == 3) _counter = 0;

        _index++;
        if (_index == 67) _index = 0;

      //  Debug.Log("Counter:" + _counter + " | Index:" + _index);
        if (_counter == 0)
        {
          GameObject removable;

          if (_toyPartsToSpawn.Count < 1) _toyPartsToSpawn = new List<GameObject>(toyPartPrefabs);

          removable = _toyPartsToSpawn[Random.Range(0, _toyPartsToSpawn.Count)];
          SpawnBackToy(_conveyorBeltBoards[_index], removable);
          _toyPartsToSpawn.Remove(removable);
        }
      }
    }

    ToyPart SpawnBackToy(Transform conveyorBeltBoard, GameObject spawnable)
    {
      ToyPart toyInstance = Instantiate(spawnable).GetComponent<ToyPart>();

      toyInstance.transform.SetParent(conveyorBeltBoard);
      toyInstance.transform.localPosition = new Vector3(0f, 0f, 0.036138f);
      toyInstance.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
      toyInstance.transform.Rotate(0f, Random.Range(0f, 360f), 0f, Space.Self);
      toyInstance.transform.localScale *= 0.38f;

      _lastToy = spawnable.name;
      return toyInstance;
    }
  }
}