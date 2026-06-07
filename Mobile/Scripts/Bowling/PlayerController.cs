using UnityEngine;
using System.Collections;
using System;

namespace Bowling
{
  public class PlayerController : MonoBehaviour
  {
    public BezierCurve aimCurve;
    public float rotSpede;
    public float strength;
    public float pointerFollowSpeed = 25f;
    public LayerMask layerMask;
    public GameObject mainBall;
    public GameObject ghostBall;
    public GameObject bowlingBallPrefab;
    public BowlingBall CurrentBall { get; set; }

    Ray _ray { get { return Camera.main.ScreenPointToRay(Input.mousePosition); } }
    float _mouseInputValueY { get { return Input.mousePosition.y / Screen.height; } }
    float _mouseInputValueX { get { return Input.mousePosition.x / Screen.width; } }
    CameraController _camera;

    void Start()
    {
      Input.multiTouchEnabled = false;
      _camera = GetComponentInChildren<CameraController>();
    }

    void Update()
    {
      if (AppManager.Instance.gameRunning.Equals(true)
                && PopUpSystem.PopUpState.Equals(false))
        HandlePlayerInput();
    }

    private void HandlePlayerInput()
    {
      if (Input.GetMouseButtonDown(0))
      {
        RaycastHit hit;

        if (Physics.Raycast(_ray, out hit, 5f, layerMask))
        {
          if (hit.rigidbody.isKinematic)
          {
            ghostBall.SetActive(true); //
            ghostBall.transform.position = mainBall.transform.position + new Vector3(_ray.direction.x / 2f, 0f, -1f - (_mouseInputValueY * 2f));

            StartCoroutine(Aim());
          }
        }
      }
    }

    // Draws curve; ghost ball following, spinning according to mouse position
    // Instantiates balls & applies mouse delta as extra force forwards
    private IEnumerator Aim()
    {
      aimCurve.gameObject.SetActive(true);
      Vector3 mousePrevPos = Input.mousePosition;
      float mouseDelta = 0f;
      float rotValueX;

      while (ghostBall.activeSelf == true)
      {
        rotValueX = -(_mouseInputValueX - 0.5f);

        aimCurve.DrawCurve(new Vector3(-rotValueX / 5f, 0f, 0f));

        ghostBall.transform.position = Vector3.Lerp(ghostBall.transform.position, mainBall.transform.position + new Vector3(_ray.direction.x / 2f, 0f, -1f - (_mouseInputValueY * 2f)), pointerFollowSpeed * Time.deltaTime);
        ghostBall.transform.Rotate(Vector3.up, rotValueX * rotSpede * Time.deltaTime, Space.World);
        //마우스를 놓았을 때 , 볼링공을 생성해서 가짜 볼링공의 위치 값을 받아 굴려준다.
        if (Input.GetMouseButtonUp(0))
        {
          mouseDelta = (Input.mousePosition - mousePrevPos).magnitude;

          CurrentBall = Instantiate(bowlingBallPrefab, mainBall.transform.position + new Vector3(_ray.direction.x / 2f, 0f, -1f - (_mouseInputValueY * 2f)), ghostBall.transform.rotation).GetComponent<BowlingBall>();
          CurrentBall.Throw(transform.right, (ghostBall.transform.position - mainBall.transform.position) * strength, rotValueX * rotSpede);

          _camera.Target = CurrentBall.transform;
          _camera.Follow();

          aimCurve.gameObject.SetActive(false);
          StartCoroutine("_Intermission"); //메인볼을 안보이게 해주고 2초 후에 다시 보이도록 해준다.
          ghostBall.SetActive(false);
        }
        mousePrevPos = Input.mousePosition;
        yield return null;
      }
    }

    public void StopIntermission()
    {
      StopCoroutine("_Intermission");
      mainBall.SetActive(true);
    }
    //메인 볼, 2초 후에 재활성화
    private IEnumerator _Intermission()
    {
      mainBall.SetActive(false);
      float time = 0f;

      while (time < 2f)
      {
        time += Time.deltaTime;

        yield return null;
      }
      mainBall.SetActive(true);
    }

    public void ChangePosition()
    {
      StopCoroutine("_ChangePosition");
      StartCoroutine("_ChangePosition");
    }
    //메인 볼의 위치를 변경시킴
    private IEnumerator _ChangePosition()
    {
      Vector3 newPos = new Vector3(Mathf.Lerp(-1.2f, 1.2f, UnityEngine.Random.Range(0f, 1f)), transform.parent.position.y, transform.parent.position.z);

      while (Vector3.Distance(newPos, transform.parent.position) > 0.1f)
      {
        transform.parent.position = Vector3.Lerp(transform.parent.position, newPos, 5f * Time.deltaTime);
        yield return null;
      }
    }
  }
}