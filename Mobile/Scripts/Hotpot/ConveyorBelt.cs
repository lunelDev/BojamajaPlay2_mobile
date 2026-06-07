using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hotpot
{
    public class ConveyorBelt : MonoBehaviour
    {
        public int interval = 3;
        public LayerMask conveyorBladeLayer;
        public float gainOver30;
        public Animator latchAnimator;
        public Animator ConveyorAni; 
        public GameObject[] foodPrefabs;

        [SerializeField, HideInInspector] List<Transform> _conveyorBeltBlades = new List<Transform>();
        [SerializeField, HideInInspector] List<GameObject> _foodToSpawn;
        public List<GameObject> _FoodList;
        [SerializeField, HideInInspector] Animator _animator;
        string _lastFood = "";
        int _index = -4;
        int _counter = -4;


        private void OnValidate()
        {
            _animator = GetComponent<Animator>();
            _conveyorBeltBlades.Clear();
            for (int i = 0; i < 104; i++)
            {
                _conveyorBeltBlades.Add(transform.GetChild(i));
            }
            _foodToSpawn = new List<GameObject>(foodPrefabs);
        }

        private void Update()
        {
            _animator.speed += gainOver30 / 30f * Time.deltaTime;
            if (AppManager.Instance.gameRunning.Equals(true))
            {
                if (DataManager.Instance.timerManager.timeLeft <= 20 && DataManager.Instance.timerManager.timeLeft > 10)
                {
                    ConveyorAni.SetFloat("speed", 2.5f);
                    //Debug.Log("dddd");
                }
                if (DataManager.Instance.timerManager.timeLeft <= 10)
                {
                    ConveyorAni.SetFloat("speed", 3f);
                  //  Debug.Log("aaaaasa");
                }
            }
        
        }

        public void StartConveyor()
        {
            _animator.enabled = true;
            latchAnimator.enabled = true;
        }

        public void StopConveyor()
        {
            _animator.enabled = false;
            latchAnimator.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if ((conveyorBladeLayer >> other.gameObject.layer) == 1)
            {
                _counter++;
                if (_counter == interval) _counter = 0;

                _index++;
                if (_index == 104) _index = 0;

                if (_counter == interval - 3 || _counter == 0 - 3)
                {
                    latchAnimator.SetTrigger("OpenAndClose");
                }
                else if (_counter == 0)
                {
                    if (_foodToSpawn.Count < 1) _foodToSpawn = new List<GameObject>(foodPrefabs);
                    GameObject removable = _foodToSpawn[Random.Range(0, _foodToSpawn.Count)];
                    SpawnBackFood(_conveyorBeltBlades[_index], removable);
                    _foodToSpawn.Remove(removable);

                }
            }
        }

        GameObject SpawnBackFood(Transform conveyorBeltBoard, GameObject spawnable)
        {
            Transform foodInstance = Instantiate(spawnable).transform; //음식 생성
            _FoodList.Add(foodInstance.gameObject);
            foodInstance.SetParent(conveyorBeltBoard, true);
            foodInstance.localPosition = new Vector3(0f, 0f, 0.0058737f);
            foodInstance.localEulerAngles = new Vector3(90f, 0f, 0f);
            foodInstance.localScale *= 0.8f;
            foodInstance.Rotate(0f, 90f, 0f, Space.Self);
            _lastFood = spawnable.name;
            return foodInstance.gameObject;
        }
    }
}