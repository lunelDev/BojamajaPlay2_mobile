using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//골키퍼
public class GoalKeeper : MonoBehaviour
{
    public float maxDefenceSpeed = 0.2f;
    public float strafeSpeed = 2f;
    [HideInInspector] public Soccer.Ball activeBall;

    Vector3 _defenceDirection { get { return new Vector3(Mathf.Clamp(-transform.InverseTransformPoint(activeBall.transform.position).x * 10f, -maxDefenceSpeed, maxDefenceSpeed), 0f, 0f); } }
    Vector3 _posLastFrame;
    Vector3 _posCurrentFrame;
    Vector3 _velocity;
    Animator _animator;
    Soccer.PlayerController _player;


    void Start()
    {
        _player = FindObjectOfType<Soccer.PlayerController>();
        _animator = GetComponent<Animator>();
        _posLastFrame = transform.position;
    }

    void Update()
    {
        _posCurrentFrame = transform.position;

        _velocity = _posCurrentFrame - _posLastFrame;
        _velocity /= Time.deltaTime;
        _velocity = transform.InverseTransformVector(_velocity);

        // print("X velocity: " + velocity.x + ", Z velocity: " + velocity.z);

        _posLastFrame = transform.position;
    }

    public void Defend(Soccer.Ball _ball)
    {
        activeBall = _ball;

        StopCoroutine("_Defend");
        StopCoroutine("_Return");
        StartCoroutine("_Defend");
    }
    //공을 넣지 못했을 떄
    public void GoalReturn(bool goal = false)
    {
        if (!goal) SoundManager.Instance.PlaySFX("08 block", 6f); //공을 못넣었을 때
        StopCoroutine("_Defend");
        StopCoroutine("_Return");
        StartCoroutine("_Return");
    }
    float jumpTime = 0;
    private IEnumerator _Defend()
    {
        _animator.SetBool("Idle", false);
        jumpTime += Random.Range(2, 10);
        while (activeBall != null)
        {
            if (Mathf.Abs(_defenceDirection.x) > 1f)
            {
                transform.Translate(_defenceDirection.normalized * maxDefenceSpeed * Time.deltaTime, Space.World);

                _animator.SetFloat("Right", _velocity.x);
                _animator.SetFloat("Forward", _velocity.z);
               
                if (jumpTime > 5)
                {
                    _animator.SetTrigger("jump");
                    jumpTime = 0;
                  
                }
             
                //골키퍼 움직임
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, -4f, 4f), transform.position.y, transform.position.z);
            }
            //_animator.SetBool("Idle", true);

            yield return null;
        }
    }

    private IEnumerator _Return()
    {
        float offset = (-(_player.currEuler - 180f) / 70f * 4f);
        _player.StopIntermission();

        StopCoroutine("_LookAtPlayer");
        StartCoroutine("_LookAtPlayer");

        while (Vector3.Distance(transform.position, new Vector3(offset, transform.position.y, transform.position.z)) > 0.5f)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(offset, transform.position.y, transform.position.z), Time.deltaTime * strafeSpeed);

            _animator.SetFloat("Right", _velocity.x);
            _animator.SetFloat("Forward", _velocity.z);

            yield return null;
        }

        _animator.SetBool("Idle", true);
    }
    //플레이어가 보는 방향으로 회전
    private IEnumerator _LookAtPlayer()
    {
        float time = 1f;
        while (time > 0)
        {
            time -= Time.deltaTime;

            Vector3 dir = _player.transform.position - transform.position;
            dir = new Vector3(dir.x, 0f, dir.z);
            transform.rotation = Quaternion.LookRotation(dir, Vector3.up);

            yield return null;
        }
    }
}
