using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Hotpot
{
    public class PlayerController : MonoBehaviour
    {

        public float pointerFollowSpeed;
        public float dragThresholdPercent = 10f;
        public ParticleSystem splash;
        public Punisher penaltyOverlay;
        public ConveyorBelt conveyorBelt;
        public MeshRenderer water;
        public GameObject[] pots;
        public ParticleSystem NoFoodParticle;
        public Transform particlePos;

        public GameObject Left;
        public GameObject Right;
        public bool GetMousePos = false;
        Color _firstCol = new Color(0.2087932f, 0.6084777f, 0.6415094f);
        Color _secondCol = new Color(1f, 0.5252652f, 0.1745283f);
        Color _thirdCol = new Color(1f, 0.2229122f, 0.08018869f);

        List<Vector2> _deltaPosArray = new List<Vector2>();
        int _goodFoodCount = 0;
        float _dragThreshold;
        Food _currFood;
        Camera _cam;
        void Awake()
        {
            Input.multiTouchEnabled = false;
            _cam = Camera.main;
            _dragThreshold = Screen.width * dragThresholdPercent * 0.01f;
        }

        Vector3 startPos, currentPos;
        float deltaMagnitude;
        public void DragState()
        {
            StartCoroutine(_DragState());
        }
        string nowList0_Name = null;
        IEnumerator _DragState()
        {
            while (AppManager.Instance.gameRunning.Equals(true))
            {
                if (PopUpSystem.PopUpState.Equals(false))
                {
                    if (Input.GetMouseButtonDown(0) && (conveyorBelt._FoodList.Count > 0 && GetMousePos.Equals(false))
                        && AppManager.Instance.gameRunning.Equals(true))
                    {
                        nowList0_Name = conveyorBelt._FoodList[0].name;
                        GetMousePos = true;
                        startPos = Input.mousePosition;
                        yield return null;
                    }
                    if (Input.GetMouseButtonUp(0) && conveyorBelt._FoodList.Count > 0 && GetMousePos.Equals(true))
                    {
                        if (conveyorBelt._FoodList[0].GetComponent<Food>().over.Equals(false))
                        {
                            currentPos = Input.mousePosition;
                            deltaMagnitude = (startPos - currentPos).magnitude;
                            Debug.Log(deltaMagnitude);
                            if (deltaMagnitude > 50 && AppManager.Instance.gameRunning.Equals(true))
                            { 
                                CheckDragState();
                            }

                        }
                    }

                }
                yield return null;
            }

        }

        void CheckDragState()
        {
            if (deltaMagnitude > 50 && currentPos.x < Screen.width / 2)
            {
                conveyorBelt._FoodList[0].GetComponent<Food>().leftMove = true;
                conveyorBelt._FoodList[0].GetComponent<Food>().MoveUP();
            }
            if (deltaMagnitude > 50 && currentPos.x > Screen.width / 2)
            {
                conveyorBelt._FoodList[0].GetComponent<Food>().rightMove = true;
                conveyorBelt._FoodList[0].GetComponent<Food>().MoveUP();
            }

        }
        public void AddFoodToPot()
        {
            _goodFoodCount++;

            if (_goodFoodCount < 6)
                water.material.SetColor("_Color", Color.Lerp(_firstCol, _secondCol, _goodFoodCount / 5f));
            else if (_goodFoodCount < 11)
                water.material.SetColor("_Color", Color.Lerp(_secondCol, _thirdCol, (_goodFoodCount - 5) / 5f));

            if (_goodFoodCount < 5 || _goodFoodCount > 10)
            {
                return;
            }
            else if (_goodFoodCount == 5)
            {
                pots[0].SetActive(false);
                pots[1].SetActive(true);
            }
            else if (_goodFoodCount == 10)
            {
                pots[1].SetActive(false);
                pots[2].SetActive(true);
            }
        }

    }
}