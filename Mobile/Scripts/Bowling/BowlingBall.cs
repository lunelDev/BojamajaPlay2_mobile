using UnityEngine;
using System.Collections;

//생성 되는 볼링공
public class BowlingBall : MonoBehaviour
{
    public bool hitPin = false;
    public GameObject confetti;
    public float spinDuration;
    //  public CameraController _camCtrl; 
    Vector3 _referencePos;
    Rigidbody _rigidbody;
    MeshRenderer _meshRenderer;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
        FindObjectOfType<CameraController>().MissAnyway();

    }
    void Start()
    {
        Destroy(gameObject, 10f); //볼링공 10초 뒤에 사라짐
    }

    void OnCollisionEnter(Collision _col)
    {
        if (_col.gameObject.layer == LayerMask.NameToLayer("Buildings")) //볼링공이 핀에 맞았을 때 
        {
            if (!hitPin)
            {
                hitPin = true; //핀이 맞았다!
                SoundManager.Instance.PlaySFX("bowling-strike");
                Confetti();
                FindObjectOfType<PinSpawner>().EvaluatePins(2f);
                FindObjectOfType<CameraController>().Hit(); //카메라의 위치를 hitCameraPosition위치까지 이동 시킴
                StopCoroutine("_ApplyContinuousModifiers");
            }
        }
        //벽에 부딪히면 오브젝트 사라지게함 
        if (_col.gameObject.layer == LayerMask.NameToLayer("wall"))
        {
            FindObjectOfType<CameraController>().MissAnyway();
            Destroy(this.gameObject, 2f);
        }
    }

    //볼링공을 회전값 만큼 힘을 더해 던짐
    public void Throw(Vector3 refPos, Vector3 forceVector, float rotValue)
    {
        SoundManager.Instance.PlaySFX("bowling-ball", 2f);

        _referencePos = refPos;

        _rigidbody.AddForce(forceVector, ForceMode.Impulse);

        StartCoroutine("_ApplyContinuousModifiers", rotValue);
    }

    private IEnumerator _ApplyContinuousModifiers(float rotValue)
    {
        float val = rotValue;
        float time = 0f;

        while (time < spinDuration)
        {
            time += Time.deltaTime;
            val = Mathf.Lerp(rotValue, 0f, time / spinDuration);

            _rigidbody.AddForce(_referencePos * val / 2 * Time.deltaTime, ForceMode.Force);
            transform.Rotate(Vector3.up, rotValue * Time.deltaTime, Space.World);

            yield return null;
        }
    }
    //볼링공이 충돌 했을떄 파티클 생성
    public void Confetti()
    {
        Instantiate(confetti, transform.position, Quaternion.identity);
    }
}