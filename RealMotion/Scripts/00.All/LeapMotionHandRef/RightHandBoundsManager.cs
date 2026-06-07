using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandBoundsManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // 좌-우
        if (transform.localPosition.z > -168f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -168f);
            //Debug.Log("z: " + transform.localPosition.z);
            //Debug.Log("왼쪽");
        }
        //this.transform.position
        else if (transform.localPosition.z < -169f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -169f);
            //Debug.Log("z: " + transform.localPosition.z);
            //Debug.Log("오른쪽");
        }

        // 상-하
        if (transform.localPosition.y > -63.5f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -63.5f, transform.localPosition.z);
            //Debug.Log("y: " + transform.localPosition.y);
            //Debug.Log("상");
        }
        else if (transform.localPosition.y < -65f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -65f, transform.localPosition.z);
            //Debug.Log("y: " + transform.localPosition.y);
            //Debug.Log("하");
        }
    }
}