using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{
  [HideInInspector] public LineRenderer lineRenderer;
  [Range(2, 20)] public int positionCount = 12;
  public Transform endPoint;
  public float midPointFactor = 0.5f;

  Vector3 _inverseEndpointPos;

    // 이 함수는 검사기에서 스크립트가 로드되거나 값이 변경되면 호출됩니다(편집기에서만 호출됨).
    void OnValidate()
  {
    lineRenderer = GetComponent<LineRenderer>();

    lineRenderer.positionCount = positionCount;
    lineRenderer.SetPosition(positionCount - 1, Vector3.zero);
  }

    // 끝점이 시작점 앞에 렌더링되기 때문에 역순으로 그리기
    //첫 번째 점과 마지막 점을 제외한 모든 점을 반복하고 Lerp 값 적용
    public void DrawCurve(Vector3 midPointOffset)
  {
    _inverseEndpointPos = transform.InverseTransformPoint(endPoint.position);
    lineRenderer.SetPosition(0, _inverseEndpointPos);

    for (int i = 1; i < positionCount - 1; i++)
    {
      lineRenderer.SetPosition(i,
        QuadBezier(i / (float)(positionCount - 1),
          _inverseEndpointPos,                                   // end point
          _inverseEndpointPos * midPointFactor + midPointOffset, // mid point
          Vector3.zero));                                       // start point
    }
  }

  public static Vector3 QuadBezier(float t, Vector3 p1, Vector3 p2, Vector3 p3)
  {
    float u = 1 - t;

    Vector3 p = u * u * p1;
    p += 2 * u * t * p2;
    p += t * t * p3;

    return p;
  }
}
