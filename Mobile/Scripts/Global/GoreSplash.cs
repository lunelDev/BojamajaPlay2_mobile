using UnityEngine;

public class GoreSplash : MonoBehaviour
{
  public float splashForce = 100f;
  public float splashForceFluctuation = 100f;
  public float rotationForce = 50f;
  public float rotationForceFluctuation = 50f;
  public float maxPercentAngleDivergenceFromUp = 20f;
  Collider _col;


  void Start()
  {
    maxPercentAngleDivergenceFromUp *= 0.01f;
  }

  public void RandomForceAndTorque(Transform obj)
  {
    _col = obj.GetComponent<Collider>();

    Vector3 randomBoundsPoint = new Vector3(Random.Range(_col.bounds.center.x - _col.bounds.extents.x, _col.bounds.center.x + _col.bounds.extents.x), transform.position.y, transform.position.z); //find random x and z coordinate on the collider
    Vector3 dirUp = new Vector3(randomBoundsPoint.x, randomBoundsPoint.y + 2f, randomBoundsPoint.z) - randomBoundsPoint; //get up direction from randomBoundsPoint
    Vector3 randomCoord = randomBoundsPoint + (Vector3)Random.insideUnitCircle; //get a random point in a sphere around the randomBoundsPoint
    float angle = Vector3.Angle(dirUp, randomCoord - randomBoundsPoint);
    Vector3 randomDir = Vector3.RotateTowards(dirUp, randomCoord - randomBoundsPoint, angle * maxPercentAngleDivergenceFromUp * Mathf.Deg2Rad, 0f); //get the final direction from up direction to random direction

    obj.rotation = Random.rotation;
    obj.GetComponent<Rigidbody>().AddTorque(randomDir.normalized * Random.Range(rotationForce - rotationForceFluctuation, rotationForce + rotationForceFluctuation));
    obj.GetComponent<Rigidbody>().AddForce(randomDir.normalized * Random.Range(splashForce - (angle * 0.5f) - splashForceFluctuation, splashForce - (angle * 0.5f) + splashForceFluctuation));
  }

  public void AssistedForceAndTorque(Transform obj, Vector3 direction)
  {
    _col = obj.GetComponent<Collider>();

    Vector3 newForce = new Vector3(transform.position.x, transform.position.y, transform.position.z + 5f) - (direction * 10f);

    obj.rotation = Random.rotation;
    obj.GetComponent<Rigidbody>().AddTorque(newForce * splashForce);
    obj.GetComponent<Rigidbody>().AddForce(newForce * splashForce);
  }

  public void RandomTorque(Transform obj, Vector3 vector)
  {
    _col = obj.GetComponent<Collider>();
    Vector3 randomBoundsPoint = new Vector3(Random.Range(_col.bounds.center.x - _col.bounds.extents.x, _col.bounds.center.x + _col.bounds.extents.x), transform.position.y, transform.position.z);

    Vector3 dir = randomBoundsPoint - vector;

    obj.GetComponent<Rigidbody>().AddTorque(dir * rotationForce);
  }
}
