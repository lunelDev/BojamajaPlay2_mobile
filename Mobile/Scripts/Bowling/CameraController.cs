using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CameraController : MonoBehaviour
{
    public Transform Target { get; set; }
    public float missWaitTime = 0.5f;
    public Transform hitCameraPosition;

    Transform _camera;
    Quaternion _startingRotation;


    void Start()
    {
        _camera = GetComponent<Camera>().transform;
       /// Debug.Log("_camera : " + _camera.transform.localPosition);
        _startingRotation = transform.rotation;
    }
    //카메라의 위치를 시작위치로 보낸다
    public void Begin()
    {
        StopAllCoroutines();
        StartCoroutine(_Begin());
    }
    private IEnumerator _Begin()
    {
        FindObjectOfType<Bowling.PlayerController>().ChangePosition(); // 다시 시작할 때, 공의 위치를 바꿈
        while (Vector3.Distance(_camera.localPosition, new Vector3(0f, -0.2f, 0f)) > 0.1f)
        {
            //_camera.localPosition = Vector3.Lerp(_camera.localPosition, Vector3.zero, 5f * Time.deltaTime);
            _camera.localPosition = Vector3.Lerp(_camera.localPosition, new Vector3(0f, -0.2f,0f), 5f * Time.deltaTime); 
            transform.rotation = Quaternion.Lerp(transform.rotation, _startingRotation, 5f * Time.deltaTime);

            yield return null;
        }
    }
    //카메라가 볼링공을 따라가도록 함
    public void Follow()
    {
        StopAllCoroutines();
        StartCoroutine(_Follow());
    }
    private IEnumerator _Follow()
    {
        while (true)
        {
            _camera.position = Vector3.Lerp(_camera.position, new Vector3(Target.position.x, _camera.position.y, Target.position.z),2* Time.deltaTime);
            yield return null;
        }
    }
    //볼링공을 굴렸을 때 카메라 따라감
    public void Hit()
    {
        StopAllCoroutines();
        StartCoroutine(_Hit());
    }
    private IEnumerator _Hit()
    {
        float time = 0f;
        while (time < 1f)
        {
            time += Time.deltaTime;
            _camera.position = Vector3.Lerp(_camera.position, hitCameraPosition.position, 2f * Time.deltaTime);

            yield return null;
        }

        StartCoroutine(_Begin()); // 처음시작 위치로 돌려줌
    }
    //볼링핀이 아무것도 맞지 않았을 때
    public void Miss()
    {
        if (Target.GetComponent<BowlingBall>().hitPin == false)
        {
            StopAllCoroutines();
            StartCoroutine(_Miss());
        }
    }
    public void MissAnyway()
    {
        StopAllCoroutines();
        StartCoroutine(_Miss());
    }
    //카메라의 움직임을 볼링공의 방향을 보게함, 
    private IEnumerator _Miss()
    {
        float time = 0f;
        while (time < missWaitTime)
        {
            time += Time.deltaTime;
            transform.LookAt(Target, Vector3.up);

            yield return null;
        }
        StartCoroutine(_Begin()); //처음 위치로 복귀
    }

}
