using UnityEngine;

public class GyroRotate : MonoBehaviour
{
  public enum RotateAxis
  {
    x, y, z
  }
  public enum Type
  {
    lerp,
    delta,
    deltaClamp,
    literal,
    literalClamp,
  }
  public RotateAxis rotateAxis;
  public Type type = Type.lerp;
  public float limit;
  public float speed = 3f;
  public bool switchDirection;

  Quaternion _referenceRotation;
  Quaternion _deviceRotation;
  Quaternion _eliminationOfXY;
  Quaternion _rotationZ;
  Vector3 _startRot;
  float _rot;
  float _deltaRot;


  void Awake()
  {
    DeviceRotation.InitGyro();
  }

  void Start()
  {
    if (Screen.sleepTimeout != SleepTimeout.NeverSleep)
      Screen.sleepTimeout = SleepTimeout.NeverSleep;
  }

  void OnEnable()
  {
    _startRot = transform.localEulerAngles;
  }

  void Update()
  {
    _rotationZ = GetGyroRotation();
    switch (type)
    {
      case Type.lerp:
        {
        }
        break;
      case Type.delta:
        {
          if (_rotationZ.eulerAngles.z < 180f)
            _deltaRot = _rotationZ.eulerAngles.z / 180f;
          else
            _deltaRot = ((360f - _rotationZ.eulerAngles.z) / 180f) * -1f;

          _deltaRot *= speed * (switchDirection ? -1 : 1);
          switch (rotateAxis)
          {
            case RotateAxis.x:
              transform.localEulerAngles += new Vector3(_deltaRot, 0f, 0f);
              break;
            case RotateAxis.y:
              transform.localEulerAngles += new Vector3(0f, _deltaRot, 0f);
              break;
            case RotateAxis.z:
              transform.localEulerAngles += new Vector3(0f, 0f, _deltaRot);
              break;
          }
        }
        break;
      case Type.deltaClamp:
        {
          float angle = _rotationZ.eulerAngles.z;
          if (angle < 180f)
          {
            angle = ClampLHS();
            _deltaRot = angle / limit;
          }
          else if (angle > 180f)
          {
            angle = ClampRHS();
            _deltaRot = ((360f - angle) / limit) * -1f;
          }

          _deltaRot *= speed * (switchDirection ? -1 : 1);
          switch (rotateAxis)
          {
            case RotateAxis.x:
              transform.localEulerAngles += new Vector3(_deltaRot, 0f, 0f);
              break;
            case RotateAxis.y:
              transform.localEulerAngles += new Vector3(0f, _deltaRot, 0f);
              break;
            case RotateAxis.z:
              transform.localEulerAngles += new Vector3(0f, 0f, _deltaRot);
              break;
          }
        }
        break;
      case Type.literal:
        {
          _rot = _rotationZ.eulerAngles.z * speed;
          switch (rotateAxis)
          {
            case RotateAxis.x:
              transform.localEulerAngles = new Vector3(_rotationZ.eulerAngles.z, 0f, 0f);
              break;
            case RotateAxis.y:
              transform.localEulerAngles = new Vector3(0f, _rotationZ.eulerAngles.z, 0f);
              break;
            case RotateAxis.z:
              transform.localEulerAngles = new Vector3(0f, 0f, _rotationZ.eulerAngles.z); ;
              break;
          }
          transform.localEulerAngles *= switchDirection ? -1 : 1;
        }
        break;
      case Type.literalClamp:
        {
          float angle = _rotationZ.eulerAngles.z;
          if (angle < 180f)
            angle = ClampLHS();
          else if (angle > 180f)
            angle = ClampRHS();

          switch (rotateAxis)
          {
            case RotateAxis.x:
              transform.localEulerAngles = new Vector3(angle, 0f, 0f);
              break;
            case RotateAxis.y:
              transform.localEulerAngles = new Vector3(0f, angle, 0f);
              break;
            case RotateAxis.z:
              transform.localEulerAngles = new Vector3(0f, 0f, angle);
              break;
          }
          transform.localEulerAngles *= switchDirection ? -1 : 1;
        }
        break;
    }

  }

  private float ClampLHS()
  {
    return Mathf.Clamp(_rotationZ.eulerAngles.z, 0, limit);
  }

  private float ClampRHS()
  {
    return Mathf.Clamp(_rotationZ.eulerAngles.z, 360f - limit, 360f);
  }

  private Quaternion GetGyroRotation()
  {
    _referenceRotation = Quaternion.identity;
    _deviceRotation = DeviceRotation.Get();
    _eliminationOfXY = Quaternion.Inverse(
        Quaternion.FromToRotation(_referenceRotation * Vector3.forward,
                                  _deviceRotation * Vector3.forward)
    );
    return _eliminationOfXY * _deviceRotation;
  }
}