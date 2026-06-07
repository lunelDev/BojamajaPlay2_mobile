using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ToyFactory
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance { get; private set; }
        public float pointerFollowSpeed;
        public float dragThresholdPercent = 5f;
        public Animator giftBoxAnimator;
        public Transform toySpawnPoint;

        float _dragThreshold;
        ToyPart _currToyPart;
        Camera _cam;
        List<Vector3> _toyDeltaPosArray = new List<Vector3>();


        void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(this);
            else
                Instance = this;

            Input.multiTouchEnabled = false;
        }

        void Start()
        {
            _dragThreshold = Screen.height * dragThresholdPercent * 0.01f;
            _cam = Camera.main;
        }
        //조립한 장난감을 상장안에서 생성
        public void Bueno(GameObject toyPrefab)
        {
            var toyCompleted = Instantiate(toyPrefab, toySpawnPoint.position, Quaternion.identity);
            toyCompleted.transform.localScale *= 0.55f; 
            DataManager.Instance.scoreManager.Add(500); //점수 추가 

            giftBoxAnimator.SetTrigger("Bueno");
        }

        public void Grab(ToyPart toyPart)
        {
            SoundManager.Instance.PlaySFX("19 toy packing", 1f);
            StopCoroutine("_Hold");
            StartCoroutine("_Hold", toyPart);
        }

        public void CheckWhetherToRelease(ToyPart toyPart = null)
        {
            if (_currToyPart == toyPart)
            {
                Release();
                Debug.Log("Releasing...");
                StopCoroutine("_Hold");
            }
        }
        //집어 들어올려진 물건 확인
        public void CheckForDragUp(ToyPart draggable)
        {
            if (AppManager.Instance.gameRunning && PopUpSystem.PopUpState.Equals(false))
            {
                StopCoroutine("_CheckForDragUp");
                StartCoroutine("_CheckForDragUp", draggable);
            }
        }
        //놓았을 때
        private void Release()
        {
            if (_currToyPart != null)
            {
                var avgDelta = (_toyDeltaPosArray[0] + _toyDeltaPosArray[1] + _toyDeltaPosArray[2] + _toyDeltaPosArray[3] + _toyDeltaPosArray[4] + _toyDeltaPosArray[5] + _toyDeltaPosArray[6] + _toyDeltaPosArray[7] + _toyDeltaPosArray[8] + _toyDeltaPosArray[9]) / 10f;
                Debug.Log("avgDelta: " + avgDelta);
                _currToyPart.Detach(avgDelta);
                _currToyPart = null;
            }
        }
        //물건을 들어올렸을 때, 마우스를 따라 올라오도록 함
        private IEnumerator _Hold(ToyPart toyPart)
        {
            _currToyPart = toyPart;
            Vector3 toyDeltaPos = Vector3.zero;
            Vector3 toyPartPrevPos = toyPart.transform.position;
            toyPart.transform.SetParent(null);

            _toyDeltaPosArray.Clear();
            for (int i = 0; i < 10; i++)
            {
                _toyDeltaPosArray.Add(Vector3.zero);
            }

            while (Input.GetMouseButton(0) && AppManager.Instance.gameRunning &&PopUpSystem.PopUpState.Equals(false))
            {
                toyPart.transform.position =
                  Vector3.Lerp(
                    toyPart.transform.position,
                    _cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y + (Screen.height * 15 * 0.01f), _cam.WorldToScreenPoint(toyPart.transform.position).z)),
                    pointerFollowSpeed * Time.deltaTime);

                CodeWrapper.AbsoluteZ(toyPart.transform, -3.480368f, Space.World);
                toyDeltaPos = toyPart.transform.position - toyPartPrevPos;

                _toyDeltaPosArray.RemoveAt(0);
                _toyDeltaPosArray.Add(toyDeltaPos);

                toyPartPrevPos = toyPart.transform.position;
                yield return null;
            }

            Release(); //손을 놓앗을 떄
        }
        //들어올렸음 
        IEnumerator _CheckForDragUp(ToyPart toyPart)
        {
            Vector3 startPos, currentPos;
            startPos = currentPos = Input.mousePosition;

            float deltaY, deltaX, absDeltaY, absDeltaX, time, duration;
            time = 0f;
            duration = 2f;
      
            while (time < duration && Input.GetMouseButton(0))
            {
                time += Time.deltaTime;
                currentPos = Input.mousePosition;

                deltaY = startPos.y - currentPos.y;
                deltaX = startPos.x - currentPos.x;

                absDeltaY = Mathf.Abs(deltaY);
                absDeltaX = Mathf.Abs(deltaX);

                //스와이프 크기가 드래그 임계값을 초과할 경우 이 작업을 수행하십시오.
                if (absDeltaY > _dragThreshold || absDeltaX > _dragThreshold)
                {
                    Debug.Log("Above drag threshold"); 
                    toyPart.Active = true;
                    Grab(toyPart);
                    yield break;
                }
                if (AppManager.Instance.gameRunning.Equals(false))
                {
                    yield break;
                }
                yield return null;
            }
        }
    }
}