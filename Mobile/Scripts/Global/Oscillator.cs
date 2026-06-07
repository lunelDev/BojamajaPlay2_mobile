using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
  public enum Axis
  {
    X, Y, Z
  }
  public Axis axis;
  public float amplitude = 1f;
  public float frequency = 1f;
  public float value { get { return (Mathf.Sin(_myTime) + 1f) * 0.5f; } }

  Vector3 _startingPosition;
  float _myTime;
  float _myValue;


  private void Start()
  {
    _startingPosition = transform.position;
  }

  void Update()
  {
    _myTime += frequency * Time.deltaTime;
    _myValue = Mathf.Sin(_myTime) * amplitude;

    switch (axis)
    {
      case Axis.X:
        transform.position = new Vector3(_startingPosition.x + (_myValue * transform.lossyScale.x), transform.position.y, transform.position.z);
        break;
      case Axis.Y:
        transform.position = new Vector3(transform.position.x, _startingPosition.y + (_myValue * transform.lossyScale.y), transform.position.z);
        break;
      case Axis.Z:
        transform.position = new Vector3(transform.position.x, transform.position.y, _startingPosition.z + (_myValue * transform.lossyScale.z));
        break;
    }
  }
}
