using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public float collisionInterval = 1f;
    public float maxSpeed = 50f;
    public float acceleration = 100f;

    Rigidbody _rigidbody;
    Collider _collider;
    float _timeSinceContact;
    float _myTime;


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();

        StartCoroutine(RandomizeDirection());
    }
    //물고기가 랜덤 값에 의해 자연스럽게 움직이도록 함
    private IEnumerator RandomizeDirection()
    {
        float min = 1f;
        float max = 9f;
        float interval = UnityEngine.Random.Range(min, max);
        float time = 0f;
        float rotationX = 0.2f;
        float rotationY = 1f;

        while (true)
        {
            time += Time.deltaTime;
            transform.Rotate(rotationX, 0f, 0f);
            transform.Rotate(0f, rotationY, 0f);

            if (time >= interval)
            {
                time = 0f;
                rotationX = -rotationX;
                rotationY = -rotationY;
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
        // transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Vector3.up), Time.deltaTime);
    }

    private void OnCollisionStay(Collision other)
    {
        if (_timeSinceContact >= collisionInterval)
        {
            StopCoroutine("ChangeDirection");
            StartCoroutine("ChangeDirection", other);
            _timeSinceContact = 0f;
        }
    }

    private void Move()
    {
        if (_rigidbody.velocity.magnitude < maxSpeed)
        {
            _rigidbody.AddForce(transform.forward * acceleration);
        }
    }

    private IEnumerator ChangeDirection(Collision other)
    {
        float time = 0f;
        var targetQuat = Quaternion.LookRotation(other.contacts[0].normal, Vector3.up);
        while (time < 2f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetQuat, 2f * Time.deltaTime);
            time += Time.deltaTime;
            yield return null;
        }
    }
}
