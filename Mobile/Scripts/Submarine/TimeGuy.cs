using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//수영하는 T1 스크립트
public class TimeGuy : MonoBehaviour
{
    public float collisionInterval = 1f;
    public float maxSpeed = 100f;
    public float acceleration = 200f;
    public ParticleSystem bubbles;

    Rigidbody _rigidbody;
    Collider _collider;
    float _timeSinceContact;
    float _myTime;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();

        StartCoroutine(Breathe()); //공기 방울
        StartCoroutine(RandomizeDirection());
    }

    private IEnumerator RandomizeDirection()
    {
        float min = 1f;
        float max = 9f;
        float interval = UnityEngine.Random.Range(min, max);
        float time = 0f;
        float rotationDegrees = 0.1f;

        while (true)
        {
            time += Time.deltaTime;
            transform.Rotate(0f, 0f, rotationDegrees);

            if (time >= interval)
            {
                time = 0f;
                rotationDegrees = -rotationDegrees;
                interval = UnityEngine.Random.Range(min, max);
            }
            yield return null;
        }
    }

    private void Update()
    {
        _timeSinceContact += Time.deltaTime;
        _myTime += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        Move();
        transform.Rotate(new Vector3(Mathf.Cos(_myTime), 0f, 0f));
    }
    //T1이 부딪혔을 때, 방향 전환
    private void OnCollisionStay(Collision other)
    {
        if (_timeSinceContact >= collisionInterval)
        {
            StopCoroutine("ChangeDirection");
            StartCoroutine("ChangeDirection", other.contacts[0].normal);
            _timeSinceContact = 0f;
        }
    }
    private void Move()
    {
        //속도 조절, 스피드가 느려졌을때 속도를 올려준다 
        if (_rigidbody.velocity.magnitude < maxSpeed)
        {
            _rigidbody.AddForce(transform.up * acceleration);
        }
    }
    //방향 전환 하기 
    private IEnumerator ChangeDirection(Vector3 normal)
    {
        float time = 0f;
        var targetQuat = Quaternion.LookRotation(transform.forward, normal); //잠앞을 향해 바라봤을 떄의 회전값
        while (time < 2f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetQuat, 2f * Time.deltaTime);
            time += Time.deltaTime;
            yield return null;
        }
    }
    //공기방울 보여주기 
    private IEnumerator Breathe()
    {
        float time = 0f;
        float interval = 4f;

        yield return new WaitForSeconds(1f);
        bubbles.Play();

        while (true)
        {
            time += Time.deltaTime;
            if (time >= interval)
            {
                time = 0f;
                bubbles.Play();
            }
            yield return null;
        }
    }
}
