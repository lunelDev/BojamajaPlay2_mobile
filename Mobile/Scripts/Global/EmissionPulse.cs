using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissionPulse : MonoBehaviour
{
  public Material[] sharedMaterials;
  public float frequency = 1f;

  Color[] _colors;


  void Start()
  {
    StartCoroutine(Pulse());
    foreach (Material sharedMaterial in sharedMaterials)
    {
      sharedMaterial.EnableKeyword("_EMISSION");
    }
  }

  IEnumerator Pulse()
  {
    float value = 1f;
    _colors = new Color[sharedMaterials.Length];
    int i = 0;

    foreach (Material sharedMaterial in sharedMaterials)
    {
      _colors[i++] = sharedMaterial.GetColor("_EmissionColor");
    }

    while (true)
    {
      i = 0;
      value = (Mathf.Sin(Time.time * frequency) + 2f) / 3f;

      foreach (var mat in sharedMaterials)
      {
        _colors[i].r = value;
        mat.SetColor("_EmissionColor", _colors[i++]);
      }
      yield return null;
    }
  }
}
