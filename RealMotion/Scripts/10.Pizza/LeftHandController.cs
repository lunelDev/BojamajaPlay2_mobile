using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace Pizza
{
    public class LeftHandController : MonoBehaviour
    {
        public float pointerFollowSpeed;
        public GameObject pizzaPrefab;
        //public bool isLeftHold = false;
        public bool isActivate;
        public bool isEnable;
        Pizza.OrderManager _orderManager;

        public SphereCollider sphereCollider;
        public GameObject rightHand;

        public static LeftHandController Instance { private set; get; }

        private void Awake()
        {
            if (Instance != null)
                Destroy(this);
            else
                Instance = this;
        }

        void Start()
        {
            _orderManager = FindObjectOfType<Pizza.OrderManager>();
        }

        private void OnEnable()
        {
            isEnable = true;
        }

        private void OnDisable()
        {
            isEnable = false;
        }

        private void OnTriggerExit(Collider other)
        {
            if (isActivate && other.CompareTag("Dough") && AppManager.Instance.gameRunning)
            {
                sphereCollider.enabled = false;

                if (rightHand.activeSelf)
                {
                    RightHandController.Instance.sphereCollider.enabled = false;
                }
                // 쥐고있을 때는 하나만 올라와야함
                StartNewPizza();
            }
        }

        public void PinchActivate(bool _isActivate)
        {
            isActivate = _isActivate; ;
        }

        public void PinchDeActivate(bool isDeActivate)
        {
            isActivate = isDeActivate;
            sphereCollider.enabled = true;
            if (rightHand.activeSelf)
            {
                RightHandController.Instance.sphereCollider.enabled = true;
            }
        }

        public void StartNewPizza()
        {
            StopCoroutine("_StartNewPizza");
            StartCoroutine("_StartNewPizza");
        }

        private IEnumerator _StartNewPizza()
        {
            var handPrevious = transform.position;    // 마우스 포지션 이전위치 > 손위치 이전 위치
            var currentPizza = Instantiate(pizzaPrefab).GetComponent<PizzaDough>(); //피자 도우 생성
            float handDelta, buildUp;  // 마우스 거리 비교 > 손거리 비교
            float time = 0f;    // 시간
            int rand = 0;       // 도우 크기

            SoundManager.Instance.PlaySFX("07 spin a pizza dough");
            currentPizza.transform.position = new Vector3(-3.48f, -0.45f, 5.86f);   // -3.48f, -0.45f, 5.86f 생성되는위치
            handDelta = buildUp = 0f;

            while (isActivate && AppManager.Instance.gameRunning && buildUp < .9999f)
            {
                handDelta = (transform.position - handPrevious).magnitude;//바뀐 마우스 위치까지의 거리
                handPrevious = transform.position;

                buildUp += handDelta * 0.00005f;
                buildUp += 0.15f * Time.deltaTime;
                buildUp = Mathf.Clamp(buildUp, 0, .9999f); //buildup의 값이 최소 최대 값을 넘기지 않도록함

                currentPizza.transform.Rotate(0, (handDelta * 0.5f) + (100f * Time.deltaTime), 0f, Space.World); //피자 회전 Rotate 회전값만큼 + 됨
                currentPizza.Knead(buildUp); //애니메이션 시작

                currentPizza.transform.position = Vector3.Lerp(currentPizza.transform.position,
                                                               new Vector3(transform.position.x, transform.position.y + 0.75f, transform.position.z),
                                                               pointerFollowSpeed * Time.deltaTime);


                //피자를 돌릴 떄 나는 사운드
                time += Time.deltaTime;
                if (time > 0.4f + (rand * 0.1f))
                {
                    rand = Random.Range(0, 2);
                    switch (rand)
                    {
                        case 0:
                            SoundManager.Instance.PlaySFX("07 spin a pizza dough");
                            break;
                        case 1:
                            SoundManager.Instance.PlaySFX("09 spin a pizza dough");
                            break;
                    }
                    time = 0f;
                }
                yield return new WaitForFixedUpdate();
            }

            _orderManager.EvaluatePizza(currentPizza);
        }
    }
}