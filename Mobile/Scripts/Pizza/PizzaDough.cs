using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Diagnostics;

public class PizzaDough : MonoBehaviour
{
    public float size { get { return _animator.GetCurrentAnimatorStateInfo(0).normalizedTime; } }
    public GameObject cloudPoof;
    public GameObject failParticle;

    GameObject _fakePizza;
    Vector3 _servePos = new Vector3(1.542591f, -0.579461f, 5.673782f);
    Vector3 _serveParticle = new Vector3(1.681f, 1.042f, 5.673782f);
    Animator _animator;
    Rigidbody _rigidbody;
    Vector3 _spawn;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _spawn = transform.position;
    }
    //피자 사이즈 늘이는 애니메이션
    public void Knead(float buildUp)
    {
        _animator.Play("Knead", -1, buildUp);
    }
    //피자 완성 됬을 때, 상자 포장 애니메이션
    public void Serve(GameObject fakePizza)
    {
        _fakePizza = fakePizza;
        _fakePizza.transform.parent.GetComponent<Animator>().SetBool("Bueno", false);
        StartCoroutine(FloatIntoPizzaBox());
    }
    //피자 만드는데 실패 했을 경우, 피자를 떨군다
    public void ThrowAway()
    {
        _rigidbody.useGravity = true;
        _rigidbody.isKinematic = false;
        Instantiate(failParticle, this.gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject, 2f);
    }
    
    public void ReSet()
    {
        transform.position = _spawn;
        _rigidbody.useGravity = false;
        _rigidbody.isKinematic = true;
        _animator.Play("Knead", -1, 0);

        _fakePizza.GetComponent<MeshRenderer>().enabled = false;
        GetComponentInChildren<MeshRenderer>().enabled = true;
    }

    IEnumerator FloatIntoPizzaBox()
    {
        _fakePizza.GetComponent<MeshRenderer>().enabled = false;

        var list = new List<Vector3>();
        var points = 10;

        for (int i = 0; i < points + 1; i++)
        {
            list.Add(BezierCurve.QuadBezier(i / (float)points,
                                            transform.position,
                                            transform.position + (_servePos - transform.position) * 0.5f + new Vector3(0f, 3f, 0f),
                                            _servePos));
        }

        float time = 0f;
        float duration = 0.05f;

        for (int j = 0; j < points; j++)
        {
            while (time < duration)
            {
                time += Time.deltaTime;
                transform.position = Vector3.Lerp(list[j], list[j + 1], time / duration);
                yield return null;
            }
            time = 0f;
        }

        SoundManager.Instance.PlaySFX("18 packaged pizza", 1.5f);
        SoundManager.Instance.PlaySFX("20 packaged pizza");
        var cloud = Instantiate(cloudPoof, _serveParticle, Quaternion.identity);
        cloud.transform.localScale /= 2f;
        _fakePizza.GetComponent<MeshRenderer>().enabled = true;
        _fakePizza.transform.parent.GetComponent<Animator>().SetBool("Bueno", true);
        GetComponentInChildren<MeshRenderer>().enabled = false;
    }
}