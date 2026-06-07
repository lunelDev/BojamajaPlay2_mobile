using UnityEngine;
using System.Collections;
using System;

namespace Soccer
{
    public class PlayerController : MonoBehaviour
    {
        public BezierCurve aimCurve;
        public float rotSpede;//3200
        public float strength;//19
        public float pointerFollowSpeed = 25f;//50
        public Transform goal; //골대의 위치
        public LayerMask layerMask; //ball
        public GameObject mainBall;
        public GameObject ghostBall;
        public GameObject footballPrefab; //생성 프리팹
        public Vector3 offset;
      
        [HideInInspector] public float currEuler = 180f;

        GoalKeeper _goalKeeper;
        float _screenWidthCenter;
        Ray _ray { get { return Camera.main.ScreenPointToRay(Input.mousePosition); } }


        void Start()
        {
            _screenWidthCenter = Screen.width * 0.5f;
            _goalKeeper = FindObjectOfType<GoalKeeper>();
            Input.multiTouchEnabled = false;
        }

        void Update()
        {
            if (AppManager.Instance.gameRunning && PopUpSystem.PopUpState.Equals(false))
                HandleInput();
        }

        private void HandleInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;

                if (Physics.Raycast(_ray, out hit, 5f, layerMask))
                {//main ball 
                    if (hit.rigidbody.isKinematic)
                    {
                        ghostBall.SetActive(true); //고스트 볼 활성화 
                        ghostBall.transform.position = mainBall.transform.position + _ray.direction + offset;

                        StartCoroutine(Aim());
                    }
                }
            }
        }

        //곡선 그리기, 고스트볼 추종, 마우스 위치에 따라 회전
        // 볼을 인스턴스화하여 마우스 델타를 추가 포워드 포워드 적용
        private IEnumerator Aim()
        {
            aimCurve.gameObject.SetActive(true);
            Vector3 mousePrevPos = Input.mousePosition;
            float mouseDelta = 0f;
            float rotValue;

            while (ghostBall.activeSelf == true && AppManager.Instance.gameRunning)
            {
                rotValue = -(Input.mousePosition.x - _screenWidthCenter) / _screenWidthCenter; 

                aimCurve.DrawCurve(new Vector3(-rotValue, Input.mousePosition.y / Screen.height * 0.6667f, 0f));
                float t = Mathf.Lerp(0.2f, 0.6f, Math.Abs((Input.mousePosition.y / Screen.height) - 1f));
                //고스트 볼의 위치와 회전
                ghostBall.transform.position = Vector3.Lerp(ghostBall.transform.position, 
                    mainBall.transform.position + _ray.direction + new Vector3(0f, t, 0f),  
                    pointerFollowSpeed * Time.deltaTime);
                ghostBall.transform.Rotate(Vector3.up, rotValue * rotSpede * Time.deltaTime, Space.World);
               
                if (Input.GetMouseButtonUp(0))
                {
                    mouseDelta = (Input.mousePosition - mousePrevPos).magnitude;
                    //볼을 생성하고 공의 힘과 회전값을 받음
                    Ball ball = Instantiate(footballPrefab, mainBall.transform.position + _ray.direction + new Vector3(0f, t, 0f), ghostBall.transform.rotation).GetComponent<Ball>();
                    ball.Throw(transform.right, (ghostBall.transform.position - mainBall.transform.position) + (transform.forward * Mathf.Clamp((mouseDelta * 0.005f), 0f, 0.8f)), strength, rotValue * rotSpede);

                    if (_goalKeeper.activeBall != null)
                        _goalKeeper.activeBall.gameObject.layer = 0;
                    _goalKeeper.Defend(ball); //골키퍼 움직임
                    aimCurve.gameObject.SetActive(false); //애임 비활성화
                    StartCoroutine("_Intermission");
                    ghostBall.SetActive(false);
                }
                mousePrevPos = Input.mousePosition;
                yield return null;
            }
            aimCurve.gameObject.SetActive(false);
            ghostBall.SetActive(false);
        }

        public void HitSound()
        {
            SoundManager.Instance.PlaySFX("07 block", 2f);
        }

        public void StopIntermission()
        {
            StopCoroutine("_Intermission");
            mainBall.SetActive(true);
        }

        private IEnumerator _Intermission()
        {   
            mainBall.SetActive(false);
            float time = 0f;
            //2초동안 골 넣었는지 확인
            while (_goalKeeper.activeBall != null && time < 2f)
            {
                time += Time.deltaTime;
                yield return null;
            }

            _goalKeeper.GoalReturn(false); //골 못넣음
        }

        public void ChangePosition()
        {
            StopCoroutine("_ChangePosition");
            StartCoroutine("_ChangePosition");
        }
        //공을 차는 위치 변경
        private IEnumerator _ChangePosition()
        {
            Vector3 rot = new Vector3(0f, currEuler = UnityEngine.Random.Range(110f, 250f), 0f);

            _goalKeeper.activeBall.Confetti(); // 공을 넣었을 때 파티클 생성, 점수 추가, 사운드
            _goalKeeper.GoalReturn(true); //골키퍼 방향 회전

            while (Mathf.Abs(transform.parent.eulerAngles.y - currEuler) > 1f)
            {
                transform.parent.eulerAngles = Vector3.Lerp(transform.parent.eulerAngles, rot, Time.deltaTime * 5f);
                Vector3 dir = goal.position - transform.position;
                dir = new Vector3(dir.x, 0f, dir.z);
                transform.rotation = Quaternion.LookRotation(dir, Vector3.up);

                yield return null;
            }
        }
    }
}