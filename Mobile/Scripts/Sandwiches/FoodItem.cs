using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//음식 재료 하나
public class FoodItem : MonoBehaviour
{
  public enum Type { W, B, L, T, C, E } //재료 종류
  public Type type = Type.W;

  Rigidbody _rigidbody;
  Collider _collider;


  void Start()
  {
    _rigidbody = GetComponent<Rigidbody>();
    _collider = GetComponent<Collider>();
  }
//올바른 재료를 놓았을 떄
  public void FloatIntoSandwich(Transform target)
  {
    StartCoroutine(_FloatIntoSandwich(target));
  }
    //오브젝트가 쟁반위에 자연스럽게 놓아지도록 함
  IEnumerator _FloatIntoSandwich(Transform target)
  {
    _rigidbody.useGravity = false;
    _rigidbody.isKinematic = true;
    _collider.enabled = false;
    float time = 0f;
    float duration = 0.2f;

    while (time < duration)
    {
      time += Time.deltaTime;
      transform.position = Vector3.Lerp(transform.position, target.position, time / duration);
      transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, time / duration);
      transform.localScale = Vector3.Lerp(transform.localScale, target.lossyScale, time / duration);

      if (time > duration * 0.5f && gameObject.layer != 0)
      {
        if (transform.childCount == 0)
          gameObject.layer = 0;
        else foreach (Transform t in transform) t.gameObject.layer = 0;
      }

      yield return null;
    }

    target.GetComponentInParent<Sandwich>().BuildNext();
    Destroy(gameObject); // 이 오브젝트는 사라지게 함
  }
    //잘못된 재료를 올렸을때, 바닥으로 떨어지게함
  public void Detach()
  {
    _rigidbody.isKinematic = false;
    _rigidbody.useGravity = true;

    _collider.enabled = true;

    Destroy(gameObject, 2f); //2초후 사라짐
  }

  public override bool Equals(object obj)
  {
    return obj.ToString() == type.ToString();
  }
  public override int GetHashCode()
  {
    return 0;
  }
}
