using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//좀비 스폰 스크립트
public class ZombieSpawner : MonoBehaviour
{
    public int minPerBatch = 3;
    public int maxPerBatch = 16;
    public float minInterval = 0.5f;
    public GameObject[] spawnPrefabs;

    [SerializeField] List<GameObject> _pool = new List<GameObject>();
    BoxCollider _spawnArea;
    Transform _player;
    float _time;
    float _interval = 1f;

    public static bool ZombieSpawnOn = false;


    void Start()
    {
        _spawnArea = GetComponent<BoxCollider>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        _time += Time.deltaTime;
        if (_time > _interval && _pool.Count > 0)
        {
            int r = Random.Range(0, 4); //랜덤 좀비 사운드 
            _interval = Random.Range(1f, 2f);
            switch (r)
            {
                case 0:
                    SoundManager.Instance.PlaySFX("03 Zombie"); break;
                case 1:
                    SoundManager.Instance.PlaySFX("04 zombie"); break;
                case 2:
                    SoundManager.Instance.PlaySFX("05 zombie"); break;
                case 3:
                    SoundManager.Instance.PlaySFX("06 zombie"); break;
            }
            _time = 0f;
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
    //좀비 생성
    public void StartSpawner()
    {
        StopCoroutine("Spawner");
        StartCoroutine("Spawner");
    }

    public Zombie GetClosest()
    {
        GameObject closest = _pool[0];
        foreach (GameObject z in _pool)
        {
            //좀비의 거리가 가까워 지면 좀비가 정지 
            if (Vector3.Distance(z.transform.position, _player.position) < Vector3.Distance(closest.transform.position, _player.position))
            {
                closest = z;
            }
        }
        return closest.GetComponent<Zombie>();
    }

    IEnumerator Spawner()
    {
        GameObject obj;
        int batchCount;

        while (true)
        {
            batchCount = Random.Range(minPerBatch, maxPerBatch);

            yield return new WaitForSeconds(minInterval + (batchCount * 0.2f));
            //좀비의 수가 30마리 이상이면 좀비를 생성하지 않음
            if (_pool.Count < 30f)
            {
                for (int i = 0; i < batchCount; i++)
                {
                    ZombieSpawnOn = true;
                    obj = Instantiate(spawnPrefabs[Random.Range(0, spawnPrefabs.Length)]);
                    obj.transform.SetParent(this.transform);
                    obj.transform.rotation = Quaternion.identity;
                    obj.transform.position = GetRandomPointInCollider();

                    obj.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;

                    _pool.Add(obj);
                }
            }
            yield return null; 
          
        }
    }

    Vector3 GetRandomPointInCollider()
    {
        Bounds bounds = _spawnArea.bounds;
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            0f,
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    public void RemoveZombie(GameObject _zombie)
    {
        if (_pool.Contains(_zombie))
        {
            _pool.RemoveAt((_pool.IndexOf(_zombie)));
        }
    }

    void OnRoundEnd()
    {
        StopCoroutine("Spawner");
        ZombieSpawnOn = false; 
        // foreach (var obj in pool)
        // {
        //     Destroy(obj);
        // }
        // pool.Clear();
    }
}
