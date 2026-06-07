using UnityEngine;
using System.Collections;
using System;

namespace Soccer
{

    //생성된 축구공
  public class Ball : MonoBehaviour
  {
    public GameObject confetti; //골 넣었을 때 , 파티클
    public float spinDuration; //스핀 지속시간
    Vector3 _referencePos;
    Rigidbody _rigidbody;
    MeshRenderer _meshRenderer;
    float _timeSinceHit;
    

        void Awake()
    {
      SetInitialReferences();
      StartCoroutine("_Dissolve");
    }

    private void Update()
    {
      _timeSinceHit += Time.deltaTime;
    }

    void SetInitialReferences()
    {
      _rigidbody = GetComponent<Rigidbody>();
      _meshRenderer = GetComponent<MeshRenderer>();
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
     
        void OnCollisionEnter(Collision col)
    {
      StopCoroutine("_ApplyContinuousModifiers");
      if (_timeSinceHit > 0.1f)
      {
        _timeSinceHit = 0f;
        SoundManager.Instance.PlaySFX("ball hit ground 01", Mathf.Clamp(_rigidbody.velocity.magnitude * 0.1f, 0, 0.9f));
      }
    }

    private void DisableKinematic()
    {
      _rigidbody.isKinematic = false;
    }
        //공을 굴렸을 때 
    public void Throw(Vector3 refPos, Vector3 forceVector, float strength, float rotValue)
    {
      DisableKinematic(); //물리 작용을 제거 한다.
      _referencePos = refPos;

      _rigidbody.AddForce((forceVector) * strength, ForceMode.Impulse);

      SoundManager.Instance.PlaySFX("12 kick", 2f);
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

        _rigidbody.AddForce(_referencePos * val / 3 * Time.deltaTime, ForceMode.Force);
        transform.Rotate(Vector3.up, rotValue * Time.deltaTime, Space.World);  //회전값 주기 

                yield return null;
      }
    }

    public void Confetti()
    {
      DataManager.Instance.scoreManager.Add(500); //공을 넣었을 때 500점
      SoundManager.Instance.PlaySFX("cheer", 1.5f);
      SoundManager.Instance.PlaySFX("05 goal", 2f);
      Instantiate(confetti, transform.position, Quaternion.identity);
    }
  }
}