using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Transitioner : MonoBehaviour
{
  public float time = 5f;

  Image _image;
  float _temp = 0f;
  float _percent = 0f;


  void OnEnable()
  {
    _temp = 0f;
  }

  void Start()
  {
    _image = GetComponent<Image>();
  }

  void Update()
  {
    _temp += Time.deltaTime;
    if (_temp > time)
    {
      _temp = time;
    }

    _percent = _temp / time;
    _image.fillAmount = Mathf.LerpAngle(0f, 1f, _percent);
  }
}
