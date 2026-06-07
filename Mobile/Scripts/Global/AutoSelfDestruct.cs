using UnityEngine;

public class AutoSelfDestruct : MonoBehaviour
{
  public float selfDestructInSeconds;
  void Start()
  {
    Destroy(gameObject, selfDestructInSeconds);
  }
//스크립트가 로드 되거나 값이 변경 될떄 함수를 호출
  void OnValidate()
  {
    //오브젝트의 파티클 지속시간이 지나면 삭제 되도록 함
    if (selfDestructInSeconds == 0)
      selfDestructInSeconds = GetComponent<ParticleSystem>().main.duration;
  }
}
