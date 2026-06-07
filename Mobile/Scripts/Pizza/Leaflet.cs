using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Leaflet : MonoBehaviour
{
  public enum Type { S, M, L }
  public Type type = Type.S;
   public int getScore =0;
  Pizza.OrderManager _queueManager;
  int _currentSpot = 5;
  Animator _animator;


  void Awake()
  {
    _queueManager = FindObjectOfType<Pizza.OrderManager>();
    _animator = GetComponentInChildren<Animator>();

        if (this.type.Equals(Type.S))
        {
            getScore = 300; 
        }
        else if (this.type.Equals(Type.M))
        {
            getScore = 600;
        }
        else if (this.type.Equals(Type.L))
        {
            getScore = 900;
        }
    }
  public void Despawn()
  {
    _animator.SetBool("Done", true);
    Destroy(gameObject, 1f);
  }

  public void MoveTo(int spot)
  {
    StopCoroutine("_MoveTo");
    StartCoroutine("_MoveTo", spot);
  }

  public void MoveUpOneSpot()
  {
    StopCoroutine("_MoveTo");
    StartCoroutine("_MoveTo", -1);
  }
//주문서 이동
  IEnumerator _MoveTo(int spot)
  {
    if (spot == _currentSpot) yield break;

    float time = 0f;
    float duration = 1f;
    _currentSpot = spot > -1 ? spot : --_currentSpot;

    while (time < duration)
    {
      if (time < duration * 0.8f) time += Time.deltaTime;
      else time += 0.5f * Time.deltaTime;

      transform.position = Vector3.Lerp(transform.position, _queueManager.Spots[_currentSpot], time / duration);
      yield return null;
    }
  }
}