using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyPart : MonoBehaviour
{
    public enum Type { B, P, R, S, T } //장난감의 종류 
    public Type type;
    public int part;
    public GameObject toyPrefab; //완성된 장난감
    public GameObject cloudPoof; //성공 파티클
    public GameObject deathCloud; //실패 파티클

    Rigidbody _rigidbody;
    Collider _collider;

    ToyFactory.PlayerController Player;
    public bool Active { get; set; }


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();

        Player = ToyFactory.PlayerController.Instance;
    }

    void OnMouseDown()
    {
        if (_rigidbody.useGravity == false)
        {
            if (AppManager.Instance.gameRunning.Equals(true))
            {
                Player.CheckForDragUp(this); //물건을 집어올림
            }
         
        
        }
         
    }

    void OnCollisionEnter(Collision col)
    {
        if (AppManager.Instance.gameRunning.Equals(true))
        {
            Player.CheckWhetherToRelease(this); //
            if (col.gameObject.layer == LayerMask.NameToLayer("Buildings") && this.Active)
            {
                ToyPart toyPiece = col.transform.GetComponent<ToyPart>();
                //같은 type이고 part가 다른경우,
                if (toyPiece.Equals(this.type) && this.part != toyPiece.part)
                {
                    Player.Bueno(toyPrefab); //완성된 장난감 생성
                    Instantiate(cloudPoof, col.transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity); //성공한 파티클
                    SoundManager.Instance.PlaySFX("12 assembly success", 1.5f);
                    SoundManager.Instance.PlaySFX("impact wrench", 2.5f);
                    SoundManager.Instance.PlaySFX("socket wrench", 2f);
                }
                else
                {
                    Instantiate(deathCloud, col.transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
                    DataManager.Instance.scoreManager.Subtract(200);//점수 감점
                    SoundManager.Instance.PlaySFX("error", 2f);
                    SoundManager.Instance.PlaySFX("17 assembly failure", 3f);
                    SoundManager.Instance.PlaySFX("ScoreDown");

                    // SoundManager.Instance.PlaySFX("tools clacking", 1f);
                }

                Destroy(col.transform.gameObject);
            }

            Destroy(this.gameObject);
        }

    }

    public void Detach(Vector3 vec)
    {
        _rigidbody.isKinematic = false;
        _rigidbody.useGravity = true;

        _collider.enabled = true;
        //_collider.isTrigger = true;

        AddForce(vec * 20f);

        Destroy(gameObject, 2f);
    }

    public void AddForce(Vector3 force)
    {
        _rigidbody.AddForce(force, ForceMode.VelocityChange);
    }

    public override bool Equals(object obj)
    {
        return obj.ToString() == type.ToString();
    }
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
