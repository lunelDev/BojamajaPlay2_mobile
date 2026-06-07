using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class CollisionDetector : MonoBehaviour
{
  public UnityEvent OnCollision = null;
  public string detectableLayerName = "Default";

  Collider _collider;
  void OnValidate()
  {
    if ((_collider = GetComponent<Collider>()).isTrigger == true)
      _collider.isTrigger = false;
  }
    //충돌 처리 
  void OnCollisionEnter(Collision col)
  {
    if (col.gameObject.layer == LayerMask.NameToLayer(detectableLayerName))
    {
      if (OnCollision != null) OnCollision.Invoke();
    }
  }
}
