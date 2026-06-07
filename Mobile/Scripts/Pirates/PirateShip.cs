using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateShip : MonoBehaviour
{
    public float buoyancyRate;
    public float buoyancyAmplitude;
    public float knotRate;
    public float growthSpeed;
    public GameObject deadTwin;
    public Vector3 X { get; set; }
    public Vector3 Z { get; set; }
    public uint Flag { get; set; }

    bool _rotating { get; set; }
    bool _lastRotatedLeft = true;
    Vector3 _ogScale;
    float _time;
    float _fireCooldown;
    List<PirateCannon> _leftSideCannons;
    List<PirateCannon> _rightSideCannons;
    [SerializeField, HideInInspector] List<PirateCannon> _cannons;


    void OnValidate()
    {
        _cannons = GetComponentsInChildren<PirateCannon>().ToList();
    }

    void Start()
    {
        _ogScale = transform.localScale;
        StartCoroutine(Grow());

        _leftSideCannons = new List<PirateCannon>(_cannons.GetRange(0, 3));
        _rightSideCannons = new List<PirateCannon>(_cannons.GetRange(3, 3));

        transform.Rotate(new Vector3(0f, 135f, 0f), Space.World);

        _time = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        if (_time > 90f * Mathf.Deg2Rad && _time < 270f * Mathf.Deg2Rad)
        {
            transform.Rotate(new Vector3(0f, 180f, 0f), Space.Self);
            _lastRotatedLeft = false;
        }

        transform.position = Vector3.Lerp(X, Z, (Mathf.Sin(_time) + 1f) * 0.5f);

        foreach (var c in _cannons)
            c.enabled = true;
        transform.GetChild(0).gameObject.SetActive(true);
        GetComponent<MeshRenderer>().enabled = true;
    }

    void Update()
    {
        if (AppManager.Instance.gameRunning.Equals(true))
        {
            MoveTime();

            transform.position = Vector3.Lerp(X, Z, (Mathf.Sin(_time) + 1f) * 0.5f);
            X += new Vector3(0f, Mathf.Sin(buoyancyRate * Time.time) * buoyancyAmplitude, 0f) * Time.deltaTime;
            Z += new Vector3(0f, Mathf.Sin(buoyancyRate * Time.time) * buoyancyAmplitude, 0f) * Time.deltaTime;

            if (!_rotating)
            {
                _fireCooldown += Time.deltaTime;
                if (_fireCooldown > 12f)
                {
                    _fireCooldown = 0f;
                    StartCoroutine(Fire());
                }
            }
            if (Vector3.Distance(transform.position, X) < 5f && !_lastRotatedLeft)
            {
                _lastRotatedLeft = true;
                StartCoroutine(_Rotate()); //left
            }
            if (Vector3.Distance(transform.position, Z) < 5f && _lastRotatedLeft)
            {
                _lastRotatedLeft = false;
                StartCoroutine(_Rotate()); //right
            }

            if (_lastRotatedLeft)
            { foreach (var c in _rightSideCannons) c.Aim(); }
            else
            { foreach (var c in _leftSideCannons) c.Aim(); }
        }

    }

    private void MoveTime()
    {
        _time += Time.deltaTime / (X - Z).magnitude * knotRate;
    }
    //해적선이 포탄에 맞았을 때,
    public void Kill()
    {
        var birds = GetComponentsInChildren<lb_Bird>();
        var birdController = FindObjectOfType<lb_BirdController>();

        foreach (var bird in birds)
            bird.transform.parent = birdController.transform;

        for (int i = 1; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        birdController.UpdateTargets();

        foreach (var bird in birds)
            bird.FlyAway();

        transform.parent.GetComponent<PirateShipSpawner>().RemoveFlag(Flag);

        DataManager.Instance.scoreManager.Add(800); //800점 추가
        Instantiate(deadTwin, transform.position, transform.rotation, transform);

        GetComponent<Collider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        Destroy(gameObject, 5f);
        this.enabled = false;
    }

    private IEnumerator _Rotate()
    {
        _rotating = true;

        float _time = 0f;
        float _duration = 5f;
        float _angleSnapshot = transform.localEulerAngles.y;
        float _endAngle = transform.localEulerAngles.y - (_lastRotatedLeft ? 180f : -180f);

        while (_time < _duration)
        {
            _time += Time.deltaTime;
            transform.localEulerAngles = new Vector3(0f, Mathf.Lerp(_angleSnapshot, _endAngle, _time / _duration), 0f);
            yield return null;
        }

        _rotating = false;
        transform.localEulerAngles = new Vector3(0f, _endAngle, 0f);
    }

    private IEnumerator Fire()
    {
        List<PirateCannon> cannonsToFire = new List<PirateCannon>(_lastRotatedLeft ? _rightSideCannons : _leftSideCannons);
        PirateCannon loadedCannon;

        for (int i = 2; i >= 0; i--)
        {
            loadedCannon = cannonsToFire[Random.Range(0, i)];
            loadedCannon.Fire();
            GetComponent<AudioSource>().Play();
            cannonsToFire.Remove(loadedCannon);

            yield return new WaitForSeconds(Random.Range(0.05f, 0.3f));
        }
    }

    IEnumerator Grow()
    {
        float size = _ogScale.x / 2f;

        while (size < _ogScale.x)
        {
            size += growthSpeed * Time.deltaTime;
            transform.localScale = new Vector3(size, size, size);

            yield return new WaitForFixedUpdate();
        }

        transform.localScale = _ogScale;
    }
}
