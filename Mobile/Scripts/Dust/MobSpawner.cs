using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
  public int poolLimit = 15;
  public int minPerBatch = 1;
  public int maxPerBatch = 5;
  public float minInterval = 0.5f;
  public GameObject[] toSpawn;

  List<GameObject> _pool = new List<GameObject>();
  BoxCollider _collider;


  void Start()
  {
    _collider = GetComponent<BoxCollider>();
  }

  public void StartSpawner()
  {
    StopCoroutine("Spawner");
    StartCoroutine("Spawner");
  }
    //먼지 생성
  public void StopSpawner()
  {
    StopCoroutine("Spawner");
  }

  IEnumerator Spawner()
  {
    GameObject obj;
    Vector3 randPos;
    int batchSize;

    while (true)
    {
    TrySpawnBatch:
      batchSize = Random.Range(minPerBatch, maxPerBatch + 1);
      yield return new WaitForSeconds(minInterval + (batchSize * 0.2f));

      if (batchSize < 1 || _pool.Count + batchSize > poolLimit) goto TrySpawnBatch;

      for (int i = 0; i < batchSize; i++)
      {
        randPos = GetRandomPointInCollider();
        obj = Instantiate(toSpawn[Random.Range(0, toSpawn.Length)], randPos, Quaternion.Euler(0f, Random.Range(0f, 359f), 0f), this.transform); //랜덤한 위치에서 랜덤한 오브젝트 생성
        _pool.Add(obj);
      }
      SoundManager.Instance.PlaySFX("20 show up Dust", 1.5f);
      yield return null;
    }
  }

  public void RemoveFromPool(GameObject obj) => _pool.Remove(obj);

  public Vector3 GetRandomPointInCollider()
  {
    Bounds bounds = _collider.bounds;
    return new Vector3(
        Random.Range(bounds.min.x, bounds.max.x),
        Random.Range(bounds.min.y, bounds.max.y),
        Random.Range(bounds.min.z, bounds.max.z)
    );
  }
}
