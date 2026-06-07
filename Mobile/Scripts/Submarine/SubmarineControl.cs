using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineControl : MonoBehaviour
{
    public float maxSpeed;
    public float acceleration;
    public float hitInterval = 1.5f;
    public Punisher penalty;
    public AccelerometerControl[] lazyAccelerometers;

    Rigidbody _rigidbody;
    bool _engineRunning;
    float _timeSinceHit;


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        AccelerometersEnabled(false);
    }

    void FixedUpdate()
    {
        Move();
        transform.localEulerAngles = new Vector3(0f, transform.localEulerAngles.y, 0f);
    }

    private void Update()
    {
        _timeSinceHit += Time.deltaTime;
    }

    void OnCollisionEnter(Collision col)
    {
        //잠수함이 부딪혔을때, 점수 감소!
        if (col.gameObject.layer == 0 && _timeSinceHit >= hitInterval)
        {
            _timeSinceHit = 0;
            SoundManager.Instance.PlaySFX("lowHit", 0.1f);
            SoundManager.Instance.PlaySFX("underwater-impact", 3.5f);
            SoundManager.Instance.PlaySFX("ScoreDown");

            penalty.ExecutePenalty();
            DataManager.Instance.scoreManager.Subtract(100); //점수 100감소
        }
    }

    public void AccelerometersEnabled(bool state)
    {
        foreach (var a in lazyAccelerometers) a.enabled = state;
    }
    //앞으로 이동
    private void Move()
    {
        if (_engineRunning && _rigidbody.velocity.magnitude < maxSpeed)
        {
            _rigidbody.AddForce(transform.GetChild(0).forward * acceleration);
        }
    }
    //게임 시작되면 잠수함 움직이게 함
    public void StartEngine()
    {
        _engineRunning = true;
        AccelerometersEnabled(true);
    }

    public void StopEngine(bool crash = false)
    {
        _engineRunning = false;
        AccelerometersEnabled(false);
    }
    /*
  private void OnGUI()
  {
    GUIStyle guiStyle = new GUIStyle();
    guiStyle.fontSize = 40;
    GUI.Label(new Rect(100f, Screen.height - 200f, 300f, 100f), "<b><color=white>Velocity: " + _rigidbody.velocity.magnitude + "</color></b>", guiStyle);
  }*/
}