using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SaturationInterpolator : MonoBehaviour
{
  public float speed = 5f;

  PostProcessVolume _volume;
  ColorGrading _colorGrading;


  void Awake()
  {
    _volume = GetComponent<PostProcessVolume>();

    _colorGrading = _volume.profile.GetSetting<ColorGrading>();
  }

  void OnEnable()
  {
    _colorGrading.saturation.value = 0f;

    StartCoroutine(Fade());
  }

  IEnumerator Fade()
  {
    while (_colorGrading.saturation.value > -99f)
    {
      _colorGrading.saturation.value = Mathf.Lerp(_colorGrading.saturation.value, -100f, speed * Time.deltaTime);
      yield return null;
    }
    _colorGrading.saturation.value = -100f;
  }
}
