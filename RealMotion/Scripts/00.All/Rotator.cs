using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
  public Space space;
  public Vector3 axis = new Vector3(0f, 1f, 0f);
  public float speed = 1f;

  void Update()
  {
    transform.Rotate(axis * speed * Time.deltaTime, space);
  }
}
