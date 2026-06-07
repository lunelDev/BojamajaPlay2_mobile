using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Pistol에 존재 
public class Gun : MonoBehaviour
{
    public Transform gunPoint; //총의 위치 
    public ParticleSystem muzzleFlash; //총 화구에서 나오는 파티클
    public GameObject bullet; //총알
    public int damage; //총알의 데미지 

    Transform _pivot;
    Animator _animator;
    ZombieSpawner _zombieSpawner;
    Zombie _closestZ;
    Vector3 _startingEulerAngles;
    Vector3 _startingPosition;
    public static GameObject _bullet;

    void Start()
    {
        _startingPosition = transform.localPosition; //시작위치 
        _startingEulerAngles = transform.localEulerAngles; //시작 각도
        _zombieSpawner = FindObjectOfType<ZombieSpawner>();
        _pivot = transform.parent; //부모의 Transform
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (ZombieSpawner.ZombieSpawnOn.Equals(true))
        {
           // _closestZ = _zombieSpawner.GetClosest();
            // rotate pivot
       //     Vector3 dir = _closestZ.weakspot.position - _pivot.position;
       //     dir = new Vector3(dir.x, 0f, dir.z);
       //     _pivot.rotation = Quaternion.Lerp(_pivot.rotation, Quaternion.LookRotation(dir, Vector3.up), 5f * Time.deltaTime);
        }
   
    }
    //총알을 발사 했을 떄, 
    public void Shoot()
    {
        transform.localPosition = _startingPosition;
        transform.localEulerAngles = _startingEulerAngles;
        _animator.SetTrigger("Shoot"); //총이 움직이는 애니메이션

        muzzleFlash.Play(); //총 구,파티클
        SoundManager.Instance.PlaySFX("pistol shot", 3f); //총 발사, 사운드
                                                          // SoundManager.Instance.PlaySFX("02 gun shooting");

        // spawn bullet trail
         _bullet = Instantiate(bullet); //총 알 생성
        _bullet.transform.position = gunPoint.position; //gunPoint에서 총알 발사
    //    _bullet.GetComponent<Bullet>().Init(_closestZ.weakspot);       
      //가까이 위치한 좀비를 맞춤
   /*     Quaternion rotation = Quaternion.LookRotation(_closestZ.weakspot.position - _pivot.position, Vector3.up);
        _closestZ.Hit(damage);*/

    }
}
