using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PirateShipSpawner : MonoBehaviour
{
  public GameObject piratePrefab;
  public Vector2 minMax;
  [Range(1, 32)] public int maxPirateShips;

  Vector3 _p, _x, _z;
  uint _occupiedMask = 0b_0000_0000_0000_0000_0000_0000_0000_0000; // empty byte
  Dictionary<uint, float> _lookupTable = new Dictionary<uint, float>();
  float _time;

  [SerializeField, HideInInspector] uint[] _refFlags;
  [SerializeField, HideInInspector] Collider _collider;
  [SerializeField, HideInInspector] lb_BirdController _birdController;


  void OnValidate()
  {
    _collider = GetComponent<Collider>();
    _birdController = FindObjectOfType<lb_BirdController>();
  }

  void Start()
  {
    // create a unique flag for each lane 
    _refFlags = Enumerable.Range(0, maxPirateShips) // create enumerable the size of max pirate ship limit
                          .Select(i => (uint)1 << i) // shift every bit left by index value; same as { i => (uint)Mathf.Pow(2, i) }
                          .ToArray();

    int j = 0;
    foreach (uint i in _refFlags)
    {
      _lookupTable.Add(i, Mathf.Lerp(minMax.x, minMax.y, j / (float)(maxPirateShips - 1)));
      j++;
    }

    _p = _x = _z = transform.position;
    _p += new Vector3(-_collider.bounds.extents.x, -4.1f, -_collider.bounds.extents.z);
    _x += new Vector3(-_collider.bounds.extents.x, -4.1f, _collider.bounds.extents.z);
    _z += new Vector3(_collider.bounds.extents.x, -4.1f, -_collider.bounds.extents.z);
  }

  public void StartSpawner()
  {
    StopCoroutine("_Spawner");
    StartCoroutine("_Spawner");
  }

  public void StopSpawner()
  {
    StopCoroutine("_Spawner");
  }

  private IEnumerator _Spawner()
  {
    _time = 0f;
    yield return new WaitUntil(() => AppManager.Instance.gameRunning.Equals(true));
    while (AppManager.Instance.gameRunning.Equals(true))
    {
      _time += Time.deltaTime;
      if (_time > 2f)
      {
        _time = 0f;
        if (maxPirateShips > transform.childCount)
          SpawnPirateShip();
        _birdController.UpdateTargets();
      }
      yield return null;
    }
  }
    //해적선 생성
  private void SpawnPirateShip()
  {
    uint[] availableFlags = _refFlags.Where(flag => (flag & ~_occupiedMask) == flag).ToArray(); // select all flags wich are unoccupied
    uint targetFlag = availableFlags[Random.Range(0, availableFlags.Length)]; // pick random from unoccupied
    _occupiedMask |= targetFlag; // mark flag as occupied

    float t = _lookupTable[targetFlag];
        //5초 후에 사라짐
    PirateShip pirateShip = Instantiate(piratePrefab).GetComponent<PirateShip>();
    pirateShip.transform.parent = transform;
    pirateShip.Flag = targetFlag;
    pirateShip.X = Vector3.Lerp(_p, _x, t);
    pirateShip.Z = Vector3.Lerp(_p, _z, t);
    pirateShip.enabled = true;
  }

  public void RemoveFlag(uint flag)
  {
    _occupiedMask &= ~flag; // remove flag from mask
    if (transform.childCount == 5)
      _time = -2f;
  }
}
