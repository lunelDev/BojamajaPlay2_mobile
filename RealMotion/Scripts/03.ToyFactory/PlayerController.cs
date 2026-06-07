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

        List<Vector3> _toyDeltaPosArray = new List<Vector3>();
        public bool holdBool;
        public Transform holdPos;
        void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(this);
            else
                Instance = this;

            Input.multiTouchEnabled = false;
        }

        //조립한 장난감을 상장안에서 생성
        public void Bueno(GameObject toyPrefab)
        {
            var toyCompleted = Instantiate(toyPrefab, toySpawnPoint.position, Quaternion.identity);
            toyCompleted.transform.localScale *= 0.55f; 
            DataManager.Instance.scoreManager.Add(1200); //점수 추가 

            giftBoxAnimator.SetTrigger("Bueno");
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

        //들어올렸음 
        IEnumerator _CheckForDragUp(ToyPart toyPart)
        {
        
            toyPart.transform.position = Vector3.Lerp(toyPart.transform.position, holdPos.transform.position, 1);
            SoundManager.Instance.PlaySFX("19 toy packing");
            Debug.Log(toyPart.transform.position);
            yield return null;

        }
    }
}