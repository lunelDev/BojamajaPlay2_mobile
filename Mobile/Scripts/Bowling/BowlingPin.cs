using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingPin : MonoBehaviour
{
    public bool standing = true;

    MeshRenderer _meshRenderer;
    PinSpawner _pinSpawner;
    Rigidbody _rigidbody;
    Collider _collider;
    Transform _pivot;
    bool _touched;


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _pivot = transform.GetChild(0);
        _pinSpawner = GetComponentInParent<PinSpawner>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Balls") || collision.gameObject.layer == LayerMask.NameToLayer("Buildings"))
        {
            if (!_touched)
                StartCoroutine("_CheckTippingAngle");
        }
    }
    public float angle;
    private IEnumerator _CheckTippingAngle()
    {
        _touched = true;
      
        Vector3 pinEuler;

        while (standing)
        {
            pinEuler = new Vector3(transform.localEulerAngles.x, 0f, transform.localEulerAngles.z);
            angle = Quaternion.Angle(Quaternion.identity, Quaternion.Euler(pinEuler));
            //볼링 핀의 각도에 따라 넘어졌는지 아닌지를 파악하고 넘어진 것을 체크
            if (angle > 10f)
            {
                StartCoroutine(SquareSinFade());
                standing = false;
                _pinSpawner.FellPin(this);
            }
            yield return null;
        }
    }
    //넘어진 핀의 색에 변화를 준다
    private IEnumerator SquareSinFade()
    {
        yield return new WaitForSeconds(1f);
        float timeline = 0f;
        Color positiveColor, negativeColor;
        positiveColor = negativeColor = _meshRenderer.material.GetColor("_Color");
        negativeColor.a = 0.2f;

        while (true)
        {
            timeline += Time.deltaTime * 25f;
            _meshRenderer.material.SetColor("_Color", Mathf.Sign(Mathf.Sin(timeline)) > 0 ? positiveColor : negativeColor);

            yield return null;
        }
    }
}
