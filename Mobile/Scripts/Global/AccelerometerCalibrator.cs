using UnityEngine;

public class AccelerometerCalibrator : MonoBehaviour
{
    //Quaternion은 회전을 표현하기 위해 사용됨
  public static Quaternion calibrationRotation { get; private set; }

  public static void Calibrate()
  {
    Vector3 offset = Input.acceleration; 
    //중심축을 기준으로 회전하고 싶은 방향 벡터를 설정 해준다.
    calibrationRotation = Quaternion.FromToRotation(offset, -Vector3.up);
  }
}