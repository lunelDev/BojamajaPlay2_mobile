using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
  public float delay = 0f;
  public float duration = 0.5f; //트레이 시간

    //총알의 트레이 표현
  LineRenderer _lineRenderer;
  void Awake()
  {
    _lineRenderer = GetComponent<LineRenderer>();
  }
    
  public void Init(Transform target)
  {
    _lineRenderer.SetPosition(1, transform.InverseTransformPoint(target.position));
    StartCoroutine(Dissolve());
  }
    //트레이 표현
  private IEnumerator Dissolve()
  {
    yield return new WaitForSeconds(delay);
    float time = 0f;
    Color startStartColor = _lineRenderer.endColor, startEndColor = _lineRenderer.startColor;
    Color endStartColor = _lineRenderer.endColor, endEndColor = _lineRenderer.endColor;
    startEndColor.a = 0f;
    endEndColor.a = 0f;

    while (time < duration)
    {
      time += Time.deltaTime;
      _lineRenderer.startColor = Color.Lerp(startStartColor, startEndColor, time / duration);
      _lineRenderer.endColor = Color.Lerp(endStartColor, endEndColor, time / duration);
      yield return null;
    }
    Destroy(gameObject);
  }
}
