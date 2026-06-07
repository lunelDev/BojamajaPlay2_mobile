using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
  public Transform gunpoint;
  public GameObject cannonBallPrefab;
  public GameObject smokePrefab;

  bool _ready = true;
  [SerializeField, HideInInspector] Highlightable _highlightable;
  [SerializeField, HideInInspector] Animator _animator;



  public bool Ready { get { return _ready; } set { _ready = value; _highlightable.Highlight(!value); } }


  void OnValidate()
  {
    _highlightable = GetComponent<Highlightable>();
    _animator = GetComponentInChildren<Animator>();
  }

  public void Aim(Vector3 target)
  {
    Vector3 dir = target - transform.position;
    dir = new Vector3(dir.x, Input.mousePosition.y / Screen.height * 10f, dir.z);
    transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
  }

  public void Fire()
  {
    if (Ready)
      StartCoroutine("_Fire");
  }

  private IEnumerator _Fire()
  {
    Ready = false;
    SoundManager.Instance.PlaySFX("cannon1", 0.75f);
    Instantiate(cannonBallPrefab, gunpoint.position, gunpoint.rotation);
    Smoke();
    _animator.SetTrigger("Fire");
    yield return new WaitForSeconds(1f);
    Ready = true;
  }

  private void Smoke()
  {
    Instantiate(smokePrefab, gunpoint.position, Quaternion.identity);
  }
}
