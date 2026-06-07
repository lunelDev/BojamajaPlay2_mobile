using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe_PlayerCtrl : MonoBehaviour
{
    public Camera cam;
    public LayerMask layerMask;
    RaycastHit hit;
    Ray ray;
    //
    public Animator broken;
    public GameObject ClearParticle;

    public static Pipe_PlayerCtrl Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else Instance = this;
        ray = cam.ScreenPointToRay(Input.mousePosition);
    }
    //밸브가 터졌을 때, 흔들리는 애니메이션 실행
    public void BrokenShanking()
    {
        StartCoroutine(BrokenShanking_());
    }
    IEnumerator BrokenShanking_()
    {
        broken.SetBool("Broken", true);
        yield return new WaitForSeconds(1f);
        broken.SetBool("Broken", false);
        yield return null;
    }
  
}
