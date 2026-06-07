using UnityEngine;

//디바이스 회전 값( 자이로 )
public static class DeviceRotation
{
  public static bool HasGyroscope { get { return SystemInfo.supportsGyroscope; } }

    //  HasGyroscope가 true이면 장치의 회전값을 받아오고 false일 경우 회전 값을 받지 않는다
    public static Quaternion Get()
  {
    return HasGyroscope ? ReadGyroscopeRotation() : Quaternion.identity;
  }

  public static void InitGyro()
  {
    if (HasGyroscope)
    {
      Input.gyro.enabled = true;                // enable the gyroscope
      Input.gyro.updateInterval = 0.0167f;    // set the update interval to it's highest value (60 Hz)
    }
  }
//장치의 회전값 
  private static Quaternion ReadGyroscopeRotation()
  {
    return new Quaternion(0.5f, 0.5f, -0.5f, 0.5f) * Input.gyro.attitude * new Quaternion(0, 0, 1, 0);
  }
}