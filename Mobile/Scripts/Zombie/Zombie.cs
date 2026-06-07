using System.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    public int health; //100
    public int points;    //300
    public Transform weakspot;
    public ParticleSystem bloodSplat;
    public float growthSpeed = 10f;

    Punisher _penaltyPanel;
    Transform _player;
    NavMeshAgent _navMeshAgent;
    Animator _animator;
    ZombieSpawner _zombieSpawner;
    Vector3 _ogScale;


    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _zombieSpawner = FindObjectOfType<ZombieSpawner>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _ogScale = transform.localScale;
        _penaltyPanel = FindObjectOfType<Punisher>();

        _navMeshAgent.SetDestination(_player.position);
        StartCoroutine(Grow());
    }

    void Update()
    {
        //좀비와 거리가 가깝고 움직이고 있으면 움직임 멈추기
        if (Vector3.Distance(transform.position, _player.position) < 2f && _navMeshAgent.isStopped == false)
        {
            _navMeshAgent.isStopped = true;
            _animator.SetBool("Walk", false);
        }
    }

    void OnEnable()
    {
        Timer.RoundEnd += OnRoundEnd;
    }

    void OnDisable()
    {
        Timer.RoundEnd -= OnRoundEnd;
    }
    //총에 맞았을 떄
    public void Hit(int _damage)
    {
        bloodSplat.Play();
        health -= _damage;
        if (health < 1) Perish();
    }

    IEnumerator Grow()
    {
        Vector3 scale = Vector3.zero;
        float size = 0.5f;

        while (size < _ogScale.x)
        {
            size += growthSpeed * Time.deltaTime;
            scale = new Vector3(size, size, size);
            transform.localScale = scale;

            yield return new WaitForFixedUpdate();
        }

        transform.localScale = _ogScale;
    }
    //좀비 죽음
    private void Perish()
    {
        _zombieSpawner.RemoveZombie(this.gameObject);

        // GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        // animator.enabled = false;
        _animator.SetTrigger("Death");
        _navMeshAgent.enabled = false;
        Destroy(this.gameObject, 2f);
        DataManager.Instance.scoreManager.Add(points); //300점 추가

        this.enabled = false;
    }

    private void OnRoundEnd()
    {
        _zombieSpawner.RemoveZombie(this.gameObject);
        Destroy(gameObject);
    }

    public void HitPlayer()
    {
        _penaltyPanel.ExecutePenalty();
        DataManager.Instance.scoreManager.Subtract(15);
        SoundManager.Instance.PlaySFX("ScoreDown");

    }
}
