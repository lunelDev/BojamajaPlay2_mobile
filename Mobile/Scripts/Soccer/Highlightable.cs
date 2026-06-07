using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlightable : MonoBehaviour
{
  public Material highlightMaterial;
  public float duration = 0.5f;
  public float delay = 0f;
  [Tooltip("X = Min; Y = Max")]
  public Vector2 alphaBounds;

  Renderer[] _renderers;
  Material _instancedMaterial;


  void Awake()
  {
    _renderers = GetComponentsInChildren<Renderer>();
  }

  void OnEnable()
  {
    foreach (var renderer in _renderers)
    {
      var materials = renderer.sharedMaterials.ToList();

      _instancedMaterial = Instantiate(highlightMaterial);
      materials.Add(_instancedMaterial);

      renderer.materials = materials.ToArray();
    }
  }

  void OnDisable()
  {
    foreach (var renderer in _renderers)
    {
      var materials = renderer.sharedMaterials.ToList();

      materials.Remove(_instancedMaterial);

      renderer.materials = materials.ToArray();
    }
  }

  void OnDestroy()
  {
    // Destroy material instances
    Destroy(_instancedMaterial);
  }

  public void Highlight(bool highlight)
  {
    StopCoroutine("_Highlight");
    StartCoroutine("_Highlight", highlight == true ? alphaBounds.y : alphaBounds.x);
  }

  private IEnumerator _Highlight(float alpha)
  {
    yield return new WaitForSeconds(delay);

    float time = 0f;

    Color startColor, endColor;
    startColor = endColor = _instancedMaterial.GetColor("_TintColor");
    endColor.a = alpha;

    while (time < duration)
    {
      time += Time.deltaTime;

      _instancedMaterial.SetColor("_TintColor", Color.Lerp(startColor, endColor, time / duration));

      yield return null;
    }
  }
}
