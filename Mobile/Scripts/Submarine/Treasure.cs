using UnityEngine;

namespace Submarine
{
  public class Treasure : MonoBehaviour
  {
    public int points; // 보석 점수
    public float rotationSpeed;


    void OnTriggerEnter(Collider col)
    {
            //플레이어가 보석과 충돌 햇을 떄
      if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
      {
        DataManager.Instance.scoreManager.Add(points);
        SoundManager.Instance.PlaySFX("13 Get jewelry");

        Destroy(this.gameObject);
      }
    }
    void Update()
    {
      transform.Rotate(transform.forward, rotationSpeed * Time.deltaTime, Space.World);
    }
  }
}