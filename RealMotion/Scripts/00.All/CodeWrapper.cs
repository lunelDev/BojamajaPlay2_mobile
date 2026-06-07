using UnityEngine;

public static class CodeWrapper
{
  public static void AbsoluteX(Transform transform, float x, Space space = Space.World)
  {
    if (space == Space.World) transform.position = new Vector3(x, transform.position.y, transform.position.z);
    else transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
  }
  public static void AbsoluteY(Transform transform, float y, Space space = Space.World)
  {
    if (space == Space.World) transform.position = new Vector3(transform.position.x, y, transform.position.z);
    else transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
  }
  public static void AbsoluteZ(Transform transform, float z, Space space = Space.World)
  {
    if (space == Space.World) transform.position = new Vector3(transform.position.x, transform.position.y, z);
    else transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, z);
  }
}