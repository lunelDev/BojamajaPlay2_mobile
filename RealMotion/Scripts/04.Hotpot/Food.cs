using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Hotpot
{
    public class Food : MonoBehaviour
    {
        public bool goodFood;
        bool outDoor = false;
        public bool over = false;
        Vector3 _servePos = new Vector3(0.001898955f, -0.05600641f, -0.302f);
        Hotpot.PlayerController _player;
        Rigidbody _rigidbody;
        SphereCollider _sphereCollider;
        MeshCollider _meshCollider;
        public Hotpot.ConveyorBelt _conveyorBelt;
        float _timeSinceHit;
        bool trashed = false;

        Hotpot.LeftButton _leftBoxCollider;
        Hotpot.RightButton _rightBoxCollider;

        public bool leftMove = false;
        public bool rightMove = false;

        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _sphereCollider = GetComponent<SphereCollider>();
            _meshCollider = GetComponentInChildren<MeshCollider>();
            //_leftBoxCollider = FindObjectOfType<Hotpot.LeftButton>();
            //_rightBoxCollider = FindObjectOfType<Hotpot.RightButton>();
            _player = FindObjectOfType<Hotpot.PlayerController>();
            _conveyorBelt = FindObjectOfType<Hotpot.ConveyorBelt>();
        }

        private void Update()
        {
            _timeSinceHit += Time.deltaTime;
        }

        public void MoveUP()
        {
            if (AppManager.Instance.gameRunning.Equals(true))
            {
                StartCoroutine(_MoveUP());
            }
        }
        IEnumerator _MoveUP()
        {
            // 위치까지 계속 움직여야함......while... 움직일 때 입력받으면안됌...! > 버튼 비활성화
            while (leftMove.Equals(true) && outDoor.Equals(true))
            {            
                transform.parent = null;
                transform.position = Vector3.Lerp(transform.position, _player.Left.transform.position, 1);
                yield return null;
            }
            while (rightMove.Equals(true) && outDoor.Equals(true))
            {
                transform.parent = null;
                transform.position = Vector3.Lerp(transform.position, _player.Right.transform.position, 1);
                yield return null;
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name.Equals("LeftMovePos") || other.gameObject.name.Equals("RightMovePos"))
            {
                rightMove = false;
                leftMove = false;
                _rigidbody.useGravity = true;          
            }
           //음식이 나왔을 떄 
            if (other.gameObject.name.Equals("outDoor"))
            {
                outDoor = true;
                _sphereCollider.isTrigger = false;
                _meshCollider.enabled = false;

            }
            if (other.gameObject.name.Equals("CanTouch"))
            {
                over = true;
                _sphereCollider.isTrigger = false;
                _meshCollider.enabled = false;

            }
        }
        // 파괴 조건
        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("TransparentFX")/* && over.Equals(true)*/)
            {
                SoundManager.Instance.PlaySFX("22 transfer material");
                //DataManager.Instance.scoreManager.Subtract(50);
                SoundManager.Instance.PlaySFX("ScoreDown");
                _player.penaltyOverlay.ExecutePenalty();
                _player.GetMousePos = false;
                //_leftBoxCollider.leftBoxCollider.enabled = true;
                //_rightBoxCollider.rightBoxCollider.enabled = true;
                Destroy(this.gameObject);
            }
            else if (collision.gameObject.layer == LayerMask.NameToLayer("Sliced Fruit")/* && _sphereCollider.enabled == false*/)
            {
                SoundManager.Instance.PlaySFX("cutting_board", 4f);
                Bueno();
            }
            else if (collision.gameObject.layer == 0 && _timeSinceHit > 0.5f)
            {
                if (trashed == false)
                {
                    trashed = true;
                    if (!goodFood)
                        DataManager.Instance.scoreManager.Add(200);
                    else
                    {
                       DataManager.Instance.scoreManager.Subtract(50);
                        SoundManager.Instance.PlaySFX("ScoreDown");
                        _player.penaltyOverlay.ExecutePenalty();
                    }
                }
                _player.GetMousePos = false;
                SoundManager.Instance.PlaySFX("13 trash can", 1.5f);
                Destroy(gameObject, 0.1f);
                //_leftBoxCollider.leftBoxCollider.enabled = true;
                //_rightBoxCollider.rightBoxCollider.enabled = true;
            }
            else if (collision.gameObject.layer == LayerMask.NameToLayer("Props") && _timeSinceHit > 0.5f)
            {
                if (trashed == false)
                {
                    trashed = true;
                    if (!goodFood)
                        DataManager.Instance.scoreManager.Add(200); //X음식을 쓰레기통에 넣었을 때, 점수 400점 획득
                    else
                    {
                        DataManager.Instance.scoreManager.Subtract(50);
                        SoundManager.Instance.PlaySFX("ScoreDown");
                        _player.penaltyOverlay.ExecutePenalty();
                    }
                }
                _player.GetMousePos = false;
                Destroy(gameObject, 0.1f);
                SoundManager.Instance.PlaySFX("20 the sound of material being placed on a cutting board", 2f);
                //_leftBoxCollider.leftBoxCollider.enabled = true;
                //_rightBoxCollider.rightBoxCollider.enabled = true;
            }
            _timeSinceHit = 0f;
            _conveyorBelt._FoodList.Remove(this.gameObject);
            Detach();
        }

        public void OnPickUp()
        {
            _sphereCollider.enabled = false;
        }
       public void Detach()
        {
            gameObject.layer = LayerMask.NameToLayer("Props");
            _rigidbody.useGravity = false;
            _sphereCollider.isTrigger = false;
            _meshCollider.enabled = false;
            StartCoroutine(_CheckIfAsleep());

            //_leftBoxCollider.leftBoxCollider.enabled = true;
            //_rightBoxCollider.rightBoxCollider.enabled = true;
        }
        IEnumerator _CheckIfAsleep()
        {
            while (true)
            {
                if (_rigidbody.IsSleeping())
                    this.enabled = false;
                yield return null;
            }
        }
        public void Bueno()
        {
            _rigidbody.useGravity = false;
            StopCoroutine("_FloatIntoPot");
            StartCoroutine("_FloatIntoPot");
        }

        IEnumerator _FloatIntoPot()
        {
            _sphereCollider.enabled = false;
            _meshCollider.enabled = false;
            _meshCollider.gameObject.layer = LayerMask.NameToLayer("UI");

            var list = new List<Vector3>();
            var points = 10;

            // Calculate points in an arc
            for (int i = 0; i < points + 1; i++)
            {

                list.Add(BezierCurve.QuadBezier(i / (float)points,
                                                 transform.position,
                                                 transform.position +(_servePos - transform.position)*0.01f+ new Vector3(0f, 0.5f, 0f),
                                                 _servePos));


            }
            float time = 0f;
            float duration = 0.0225f;
            for (int j = 0; j < points; j++)
            {
                while (time < duration)
                {
                    time += Time.deltaTime;
                    transform.position = Vector3.Lerp(list[j], list[j + 1], time / duration *0.5f);
                    yield return null;
                }
                time = 0f;
            }
            _player.splash.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            _player.splash.Play(true);
            SoundManager.Instance.PlaySFX("11 put ingredients in a pot", 2f);
            //X음식 이면
            if (!goodFood)
            {
               // DataManager.Instance.scoreManager.Subtract(100);
                _player.penaltyOverlay.ExecutePenalty();
                SoundManager.Instance.PlaySFX("ScoreDown");
                SoundManager.Instance.PlaySFX("15 assembly failure", 2f);
            }
            else
            {
                DataManager.Instance.scoreManager.Add(200); // 점수 획득
                _player.AddFoodToPot(); //전골안에 음식을 넣음
                SoundManager.Instance.PlaySFX("12 assembly success", 2f);
            }
            Destroy(gameObject);

            //_leftBoxCollider.leftBoxCollider.enabled = true;
            //_rightBoxCollider.rightBoxCollider.enabled = true;
        }
        public void EnableCollider()
        {
            _leftBoxCollider.leftBoxCollider.enabled = true;
            _rightBoxCollider.rightBoxCollider.enabled = true;
        }
    }
}