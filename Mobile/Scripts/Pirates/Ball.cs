using UnityEngine;
using System.Collections;
using System;

namespace Pirates
{
  public class Ball : MonoBehaviour
  {
    public GameObject boardsExplosion;
    public GameObject hitExplosion;
    public GameObject waterSplash;
    public float power;

    Rigidbody _rigidbody;
    MeshRenderer _meshRenderer;
    AudioSource _audioSource;


    void Awake()
    {
      SetInitialReferences();
      StartCoroutine("_Dissolve");
      _rigidbody.AddForce(transform.forward * power, ForceMode.Impulse);
      _rigidbody.AddForce(transform.up * power * 0.06f, ForceMode.Impulse);
      _rigidbody.AddTorque(transform.position + UnityEngine.Random.insideUnitSphere * 10f);
    }

    void SetInitialReferences()
    {
      _rigidbody = GetComponent<Rigidbody>();
      _meshRenderer = GetComponent<MeshRenderer>();
      _audioSource = GetComponent<AudioSource>();
    }

    private IEnumerator _Dissolve()
    {
      float value = 1f;
      Color currentColor = _meshRenderer.material.GetColor("_Color");

      yield return new WaitForSeconds(10f);

      while (value > 0f)
      {
        value -= Time.deltaTime * 2f;

        currentColor.a = value;
        _meshRenderer.material.SetColor("_Color", currentColor);

        yield return null;
      }

      Destroy(gameObject);
    }

    void OnCollisionEnter(Collision _col)
    {
      GameObject go = _col.gameObject;
    //배를 맞췄을 떄,
      if (go.layer == LayerMask.NameToLayer("Buildings"))
      {
        Hit(_col.GetContact(0).point);
        if (go != null)
          go.GetComponent<PirateShip>().Kill();
      }
      else if (go.layer == 0)
      {
        Miss();
      }

      _meshRenderer.enabled = false;
      GetComponent<Collider>().enabled = false;
      GetComponent<Rigidbody>().useGravity = false;
      GetComponent<Rigidbody>().velocity = Vector3.zero;

      Destroy(gameObject, 2f); //2초후 소멸
    }
    //배를 맞췄을 때, 파티클 + 사운드
    public void Hit(Vector3 pos)
    {
      Instantiate(hitExplosion, pos, Quaternion.identity);
      Instantiate(boardsExplosion, pos, Quaternion.identity);
      _audioSource.PlayOneShot(SoundManager.Instance.GetClipByName("lowHit"));
    }
    //배를 맞추지 못했을 떄
    public void Miss()
    {
      var w = Instantiate(waterSplash);
      w.transform.position = transform.position + new Vector3(0f, -3f, 0f);
      _audioSource.PlayOneShot(SoundManager.Instance.GetClipByName("deepsplash"));
    }
  }
}