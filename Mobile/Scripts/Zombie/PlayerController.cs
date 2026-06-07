using System.Diagnostics;
using TMPro;
using UnityEngine;

namespace Zombies
{
    public class PlayerController : MonoBehaviour
    {
        public Gun leftGun;
        public Gun rightGun;

        Zombie _touchZ;
        public int damage; //총알의 데미지 
        bool _shootingAllowed;
        private int _touchesPreviousFrame;
        private Ray ray;
        private RaycastHit hit;
        void Update()
        {
            //     ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (_shootingAllowed && Input.GetMouseButtonDown(0)
            && ZombieSpawner.ZombieSpawnOn.Equals(true)
            && PopUpSystem.PopUpState.Equals(false))
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 1000f))
                {
                    if (hit.collider.gameObject.layer.Equals(14))
                    {
                        _touchZ = hit.collider.gameObject.GetComponent<Zombie>();
                        if (_shootingAllowed && Input.touchCount > 0)
                        {
                            foreach (var v in Input.touches)
                            {
                                if (v.phase == TouchPhase.Began)
                                {
                                    Shoot(v);

                                }
                            }
                        }
                        _touchesPreviousFrame = Input.touchCount;
                        if (_shootingAllowed && Input.GetMouseButtonDown(0)
                             && ZombieSpawner.ZombieSpawnOn.Equals(true)
                             && PopUpSystem.PopUpState.Equals(false))
                        {
                            Shoot();

                        }
                    }

                }


            }


            /*    //#if UNITY_ANDROID && !UNITY_EDITOR
                if (_shootingAllowed && Input.touchCount > 0)
                {
                    foreach (var v in Input.touches)
                    {
                        if (v.phase == TouchPhase.Began)
                            Shoot(v);
                    }
                }
                _touchesPreviousFrame = Input.touchCount;
                //#elif UNITY_EDITOR
                if (_shootingAllowed && Input.GetMouseButtonDown(0)
                    && ZombieSpawner.ZombieSpawnOn.Equals(true) 
                    && PopUpSystem.PopUpState.Equals(false))
                {
                    Shoot();
                }*/
            ///#endif
        }

        private void Shoot(Touch touch = default)
        {
            if (touch.position.x < Screen.width * 0.5f)
            {
                _touchZ.Hit(damage);
              
                if (Input.mousePosition.x < Screen.width * 0.5f)

                // left
                {
                    leftGun.Shoot();
                    Gun._bullet.GetComponent<Bullet>().Init(_touchZ.weakspot);
                }
                // right
                else
                {
                    rightGun.Shoot();
                    Gun._bullet.GetComponent<Bullet>().Init(_touchZ.weakspot);
                }
            }
          
        }

        void OnEnable()
        {
            Timer.RoundStart += OnRoundStart;
            Timer.RoundEnd += OnRoundEnd;
        }

        void OnDisable()
        {
            Timer.RoundStart -= OnRoundStart;
            Timer.RoundEnd -= OnRoundEnd;
        }

        void OnRoundStart()
        {
            _shootingAllowed = true;
            leftGun.enabled = true;
            rightGun.enabled = true;
        }

        void OnRoundEnd()
        {
            _shootingAllowed = false;
            leftGun.enabled = false;
            rightGun.enabled = false;
        }
    }
}
