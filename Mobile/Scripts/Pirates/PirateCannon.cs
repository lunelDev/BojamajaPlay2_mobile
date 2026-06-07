using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateCannon : MonoBehaviour
{
  public GameObject cannonBallPrefab;

  [SerializeField, HideInInspector] MeshRenderer _meshRenderer;
  [SerializeField, HideInInspector] Transform _gunPoint;


  void OnValidate()
  {
    _meshRenderer = GetComponentInChildren<MeshRenderer>();
    _gunPoint = transform.GetChild(1);
  }

  void Start()
  {
    _meshRenderer.enabled = true;
  }
    //해적선의 포탄
  public void Fire()
  {
    Instantiate(cannonBallPrefab, _gunPoint.position, _gunPoint.rotation);
  }

  public void Aim()
  {
    Vector3 dir = Camera.main.transform.position - transform.position;
    dir.y += 15f;
    transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
  }
}
