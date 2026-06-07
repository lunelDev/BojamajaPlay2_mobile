using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StonDestroy : MonoBehaviour
{
    //돌맹이 떨어지고 조금 있다 사라지게 함
    private void Start()
    {
        Destroy(this.gameObject, 3f);
    }
}
