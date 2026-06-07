using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PirateBall : MonoBehaviour
{
    public GameObject brokenWall;
    public GameObject explosion;
    public GameObject smoke;

    Transform _player;


    void Start()
    {
        _player = FindObjectOfType<Pirates.PlayerController>().transform;
        Instantiate(smoke, transform.position, Quaternion.identity);
        StartCoroutine("InterpolatePosition");
    }

    IEnumerator InterpolatePosition()
    {
        if (AppManager.Instance.gameRunning.Equals(true))
        {
            var list = new List<Vector3>();
            var randX = Random.Range(-6f, 6f);
            var randY = (Random.Range(0, 2) == 0 ? -1 : 1) * 3f;
            for (int i = 0; i < 21; i++)
            {
                list.Add(BezierCurve.QuadBezier(i / 20f,
                                                transform.position,
                                                transform.position + (_player.position - transform.position) * 0.5f + new Vector3(0f, 18f, 0f),
                                                _player.position + _player.TransformVector(new Vector3(randX, randY, +4f))));
            }

            float time = 0f;
            float duration = 0.125f;

            for (int j = 0; j < 20; j++)
            {
                while (time < duration)
                {
                    time += Time.deltaTime;
                    transform.position = Vector3.Lerp(list[j], list[j + 1], time / duration);
                    yield return null;
                }
                time = 0f;
            }
            // Instantiate(explosion, transform.position, Quaternion.identity);
            var vfx = Instantiate(brokenWall, transform.position, Quaternion.identity);
            vfx.transform.localScale /= 8f;
            vfx = Instantiate(explosion, transform.position, Quaternion.identity);
            vfx.transform.localScale /= 8f;

            GetComponent<AudioSource>().Play();

            GetComponent<Collider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;

            _player.GetComponent<Pirates.PlayerController>().penaltyPanel.ExecutePenalty();
            DataManager.Instance.scoreManager.Subtract(50);
            SoundManager.Instance.PlaySFX("ScoreDown");
            Destroy(gameObject, 2f);
        }
       
    }

}
