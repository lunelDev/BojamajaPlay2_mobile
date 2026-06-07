using UnityEngine;
using System.Collections;
using System;

namespace Pirates
{
    public class PlayerController : MonoBehaviour
    {
        public LayerMask layerMask;
        public BezierCurve line;
        public Cannon cannon;
        public GameObject pointer;
        public Punisher penaltyPanel;

        Ray _ray { get { return Camera.main.ScreenPointToRay(Input.mousePosition); } }
        PirateShip _pirateShip;
        RaycastHit _hit;


        void Start()
        {
            _pirateShip = FindObjectOfType<PirateShip>();
            Input.multiTouchEnabled = false;
        }

        void Update()
        {
            transform.localEulerAngles = new Vector3(Mathf.Sin(Time.time * 1.5f) + 3.092f, 45f, 0f);

            if (AppManager.Instance.gameRunning && !PopUpSystem.PopUpState.Equals(true))
                HandleInput();
        }

        private void HandleInput()
        {
            if (Input.GetMouseButtonDown(0) && Physics.Raycast(_ray, out _hit, 5f, layerMask) && cannon.Ready)
            {
                pointer.SetActive(true);
                line.gameObject.SetActive(true);

                StartCoroutine(Aim());
            }
        }
        //총을 발사 할 떄 생성되는 라인 랜더러
        private IEnumerator Aim()
        {
            Vector3 mouseDelta = Vector3.zero;
            Vector3 mousePrevPos = Input.mousePosition;
            float z = pointer.transform.localPosition.z;

            while (pointer.activeSelf == true && AppManager.Instance.gameRunning)
            {
                mouseDelta = Input.mousePosition - mousePrevPos;
                mousePrevPos = Input.mousePosition;
                z += mouseDelta.x * 0.15f;
                z = Mathf.Clamp(z, -7.5f, 142.1331311379186f);
                pointer.transform.localPosition = new Vector3(0f, pointer.transform.localPosition.y, z);

                line.DrawCurve(new Vector3(0f, Input.mousePosition.y / Screen.height * 4f, 0f));
                cannon.Aim(line.transform.TransformPoint(line.lineRenderer.GetPosition(5)));
                //원하는 방향에 에임을 맞추고 놓았을 때, 총알 발사
                if (Input.GetMouseButtonUp(0))
                {
                    pointer.SetActive(false);

                    line.gameObject.SetActive(false); //에임 활성화
                    cannon.Fire(); // 총알 발사
                }
                yield return null;
            }
            line.gameObject.SetActive(false);
        }
    }
}