using UnityEngine;

public class AccelerometerCalibrator : MonoBehaviour
{
    //Quaternion은 회전을 표현하기 위해 사용됨
  public static Quaternion calibrationRotation { get; private set; }

  public static void Calibrate()
  {
        //Input.acceleration은 3차원 공간에 있는 장치의 마지막 측정된 선형 가속도(읽기 전용)
        //
    Vector3 offset = Input.acceleration; 
    //중심축을 기준으로 회전하고 싶은 방향 벡터를 설정 해준다.
    calibrationRotation = Quaternion.FromToRotation(offset, -Vector3.up);
  }
}