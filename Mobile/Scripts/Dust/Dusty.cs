using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dusty : MonoBehaviour
{
    public GameObject dustParticle;
    public GameObject dustTrail;
    public int points;
    public float flySpeed = 15f;
    public float growthSpeed = 10f;
    public float floatSpeed = 3f;
    public float floatAmplitude = 0.01f;
    public GameObject largeMesh; // stage 0
    public GameObject mediumMesh; // stage 1
    public GameObject smallMesh; // stage 2

    Rigidbody _rigidbody;
    Collider _collider;
    MobSpawner _spawner;
    Vector3 _scale;
    Vector3 _destination;
    public int _currStage = 0;
    void Awake()
    {
        _scale = transform.localScale;

        transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().receiveShadows = false;
    }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _spawner = transform.parent.GetComponent<MobSpawner>();

        StartCoroutine(Grow());
    }
 
    void Update()
    {
        transform.localPosition += new Vector3(0f, Mathf.Sin(Time.timeSinceLevelLoad * floatSpeed) * floatAmplitude, 0f);
    }
    private void OnMouseDown()
    {
        if (AppManager.Instance.gameRunning && PopUpSystem.PopUpState.Equals(false))
        {
            SoundManager.Instance.PlaySFX("02  dusting", 2f);
            Stage(++_currStage);
        }
    }
    public void _stageUp()
    {
        _currStage += 1;
        Stage(_currStage);
    }

    //먼지의 크기 
    private void Stage(int stage)
    {
        var dust = Instantiate(dustParticle, transform.position, Quaternion.identity);
        SoundManager.Instance.PlaySFX("08 The sound of dust getting smaller");
        if (stage > 2)
        {
            dust.transform.localScale *= 10f;
            SoundManager.Instance.PlaySFX("13 the sound of dust disappearing");
            Perish(); //먼지 사라짐
            return;
        }
        if (stage == 1)
        {
            largeMesh.SetActive(false);
            mediumMesh.SetActive(true);
            dust.transform.localScale *= 20f;
            dustTrail.transform.localScale *= 0.5f;
        }
        else if (stage == 2)
        {
            mediumMesh.SetActive(false);
            smallMesh.SetActive(true);
            dust.transform.localScale *= 15f;
            dustTrail.transform.localScale *= 0.5f;
        }
    }

    IEnumerator Fly()
    {
        _destination = _spawner.GetRandomPointInCollider();

        Vector3 dir = _destination - transform.position; // get dir vector
        dir = new Vector3(dir.x, 0f, dir.z); // remove y
        Quaternion targetDir = Quaternion.LookRotation(dir, Vector3.up); // actual rotation (now Z faces the direction, however our model's forward axis is X)
        targetDir = Quaternion.AngleAxis(-90f, Vector3.up) * targetDir; // our dust model's axis is not correct, we rotate so that X axis faces destination

        while (Vector3.Distance(transform.position, _destination) > 1f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetDir, 2f * Time.deltaTime);
            _rigidbody.AddForce((_destination - transform.position).normalized * flySpeed, ForceMode.Force);

            yield return new WaitForFixedUpdate();
        }
        StartCoroutine(Idle());
    }

    IEnumerator Idle()
    {
        float duration = Random.Range(1f, 2f);

        while (duration > 0f)
        {
            duration -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        StartCoroutine(Fly());
    }

    IEnumerator Grow()
    {
        Vector3 newScale = Vector3.zero;
        float size = 0f;

        while (size < _scale.x)
        {
            size += growthSpeed * Time.deltaTime;
            newScale = new Vector3(size, size, size);

            transform.localScale = newScale;

            yield return new WaitForFixedUpdate();
        }
        transform.localScale = _scale;

        StartCoroutine(Fly());
    }

    public void Perish()
    {
        DataManager.Instance.scoreManager.Add(points); //점수 추가
        _spawner.RemoveFromPool(this.gameObject); //리스트안에서 지워줌
        Destroy(gameObject); //먼지 지움
    }
}
