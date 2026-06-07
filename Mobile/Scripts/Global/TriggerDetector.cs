using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class TriggerDetector : MonoBehaviour
{
  public UnityEvent OnTrigger = null;
  public string detectableLayerName = "Default";

  Collider _collider;


  void OnValidate()
  {
    if ((_collider = GetComponent<Collider>()).isTrigger == false)
      _collider.isTrigger = true;
  }

  void OnTriggerEnter(Collider col)
  {
    if (col.gameObject.layer == LayerMask.NameToLayer(detectableLayerName))
    {
      if (OnTrigger != null) OnTrigger.Invoke();
      col.gameObject.layer = 0;
    }
  }
}
