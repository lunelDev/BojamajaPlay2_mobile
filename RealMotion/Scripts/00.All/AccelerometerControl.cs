using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AccelerometerControl : MonoBehaviour
{
  public enum Motion
  {
    Pitch, Roll
  }
  public enum Axis
  {
    X, Y, Z
  }
  public Motion motion = Motion.Roll;
  public Axis affectedAxis = Axis.Y;
  public float speed = 3f;
  [Range(0f, 180f)]
  public float clampDegrees;
  public bool switchDirection;
  public bool literal;

  Dictionary<string, Action> _actionTable = new Dictionary<string, Action>();
  float _angle { get { return affectedAxis == Axis.X ? transform.localEulerAngles.x : affectedAxis == Axis.Y ? transform.localEulerAngles.y : transform.localEulerAngles.z; } }
  Vector3 _calibAccel;
  string _config;

  Vector3 _deltaEuler = Vector3.zero;
  Vector3 _literalEuler = Vector3.zero;
  Vector3 _clampedEuler = Vector3.zero;


  void Start()
  {
    if (Screen.sleepTimeout != SleepTimeout.NeverSleep)
      Screen.sleepTimeout = SleepTimeout.NeverSleep;

    SetupLookupTable();

    _config = motion.ToString()[0] + affectedAxis.ToString();
  }

  void FixedUpdate()
  {
    _calibAccel = AccelerometerCalibrator.calibrationRotation * Input.acceleration;

    if (literal)
    {
      _actionTable["L" + _config]();

      transform.localEulerAngles = switchDirection ? -_literalEuler : _literalEuler;
    }
    else
    {
      _actionTable["D" + _config]();
      if (switchDirection) _deltaEuler *= -1;

      transform.localEulerAngles += _deltaEuler * speed;
      if (clampDegrees > 0f)
      {
        if (_angle < 180f)
        {
          _actionTable[_config[1] + "R"]();
        }
        else if (_angle > 180f)
        {
          _actionTable[_config[1] + "L"]();
        }
        transform.localEulerAngles = _clampedEuler;
      }
    }
  }

  private void SetupLookupTable()
  {
    // for delta

    _actionTable.Add("D" + Motion.Pitch.ToString()[0] + Axis.X.ToString(), () => XVector(ref _deltaEuler, _calibAccel.z)); // DPX
    _actionTable.Add("D" + Motion.Pitch.ToString()[0] + Axis.Y.ToString(), () => YVector(ref _deltaEuler, _calibAccel.z)); // DPY
    _actionTable.Add("D" + Motion.Pitch.ToString()[0] + Axis.Z.ToString(), () => ZVector(ref _deltaEuler, _calibAccel.z)); // DPZ

    _actionTable.Add("D" + Motion.Roll.ToString()[0] + Axis.X.ToString(), () => XVector(ref _deltaEuler, _calibAccel.x)); // DRX
    _actionTable.Add("D" + Motion.Roll.ToString()[0] + Axis.Y.ToString(), () => YVector(ref _deltaEuler, _calibAccel.x)); // DRY
    _actionTable.Add("D" + Motion.Roll.ToString()[0] + Axis.Z.ToString(), () => ZVector(ref _deltaEuler, _calibAccel.x)); // DRZ

    // for literal

    _actionTable.Add("L" + Motion.Pitch.ToString()[0] + Axis.X.ToString(), () => XVector(ref _literalEuler, _calibAccel.z * clampDegrees)); // LPX
    _actionTable.Add("L" + Motion.Pitch.ToString()[0] + Axis.Y.ToString(), () => YVector(ref _literalEuler, _calibAccel.z * clampDegrees)); // LPY
    _actionTable.Add("L" + Motion.Pitch.ToString()[0] + Axis.Z.ToString(), () => ZVector(ref _literalEuler, _calibAccel.z * clampDegrees)); // LPZ

    _actionTable.Add("L" + Motion.Roll.ToString()[0] + Axis.X.ToString(), () => XVector(ref _literalEuler, _calibAccel.x * clampDegrees)); // LRX
    _actionTable.Add("L" + Motion.Roll.ToString()[0] + Axis.Y.ToString(), () => YVector(ref _literalEuler, _calibAccel.x * clampDegrees)); // LRY
    _actionTable.Add("L" + Motion.Roll.ToString()[0] + Axis.Z.ToString(), () => ZVector(ref _literalEuler, _calibAccel.x * clampDegrees)); // LRZ

    // for clamping

    _actionTable.Add(Axis.X.ToString() + "R", () => XVector(ref _clampedEuler, ClampRHS(transform.localEulerAngles.x))); // XR
    _actionTable.Add(Axis.X.ToString() + "L", () => XVector(ref _clampedEuler, ClampLHS(transform.localEulerAngles.x))); // XL

    _actionTable.Add(Axis.Y.ToString() + "R", () => YVector(ref _clampedEuler, ClampRHS(transform.localEulerAngles.y))); // YR
    _actionTable.Add(Axis.Y.ToString() + "L", () => YVector(ref _clampedEuler, ClampLHS(transform.localEulerAngles.y))); // YL

    _actionTable.Add(Axis.Z.ToString() + "R", () => ZVector(ref _clampedEuler, ClampRHS(transform.localEulerAngles.z))); // ZR
    _actionTable.Add(Axis.Z.ToString() + "L", () => ZVector(ref _clampedEuler, ClampLHS(transform.localEulerAngles.z))); // ZL
  }

  void XVector(ref Vector3 vector, float value)
  {
    if (literal)
      vector.x = Mathf.LerpAngle(vector.x, value, speed * Time.deltaTime);
    else
      vector.x = value;
  }
  void YVector(ref Vector3 vector, float value)
  {
    if (literal)
      vector.y = Mathf.LerpAngle(vector.y, value, speed * Time.deltaTime);
    else
      vector.y = value;
  }
  void ZVector(ref Vector3 vector, float value)
  {
    if (literal)
      vector.z = Mathf.LerpAngle(vector.z, value, speed * Time.deltaTime);
    else
      vector.z = value;
  }
  float ClampLHS(float rot) => Mathf.Clamp(rot, 360f - clampDegrees, 360f);
  float ClampRHS(float rot) => Mathf.Clamp(rot, 0, clampDegrees);

  // private void OnGUI()
  // {
  //   GUIStyle guiStyle = new GUIStyle();
  //   guiStyle.fontSize = 40;
  //   GUI.Label(new Rect(100f, 100f, 300f, 100f), "<b><color=white>INPUT - \t" + Input.acceleration.ToString("f2") + "</color></b>", guiStyle);
  //   // GUI.Label(new Rect(100f, 200f, 300f, 100f), "<color=white>OFFST - \t" + (-offset).ToString("f2") + "</color>", guiStyle);
  //   GUI.Label(new Rect(100f, 300f, 300f, 100f), "<b><color=white>CALIB - \t" + calibAccel.ToString("f2") + "</color></b>", guiStyle);
  // }
}