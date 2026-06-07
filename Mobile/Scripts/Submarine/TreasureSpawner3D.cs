using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//보석 생성
public class TreasureSpawner3D : MonoBehaviour
{
  public int treasureAmount;
  public GameObject[] spawnPrefabs;

  Collider _spawnArea;
  List<GameObject> _spawnedList = new List<GameObject>();

  IEnumerator Start()
  {
    Timer.RoundEnd += OnRoundEnd;
    _spawnArea = GetComponent<Collider>();

    yield return SpawnNewTreasures(); //게임 시작시, 보석 생성
  }

  void OnDisable()
  {
    Timer.RoundEnd -= OnRoundEnd;
  }
    //공간에 랜덤생성
  private Vector3 GetRandomPointInCollider()
  {
    Bounds bounds = _spawnArea.bounds;
    return new Vector3(
        Random.Range(bounds.min.x, bounds.max.x),
        Random.Range(bounds.min.y, bounds.max.y),
        Random.Range(bounds.min.z, bounds.max.z)
    );
  }

  private IEnumerator SpawnNewTreasures()
  {
    yield return new WaitForEndOfFrame();
    GameObject dia;
    for (int i = 0; i < treasureAmount; i++)
    {
      dia = Instantiate(spawnPrefabs[Random.Range(0, spawnPrefabs.Length - 1)]); //오브젝트 생성
      dia.transform.SetParent(this.transform);
      dia.transform.position = GetRandomPointInCollider(); //랜덤 위치 값을 받아와서 생성
      _spawnedList.Add(dia); //리스트에 추가

      yield return null;
    }
    yield return new WaitForEndOfFrame();
  }
    //게임이 끝나면 보석 삭제
  private void OnRoundEnd()
  {
    if (_spawnedList.Count > 0)
    {
      for (int i = 0; i < _spawnedList.Count; i++)
      {
        Destroy(_spawnedList[i]);
      }
      _spawnedList.Clear(); //리스트 비움
    }
  }
}
