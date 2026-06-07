using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHandBoundsManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // 좌-우
        if (transform.localPosition.z < 164f)
        {
            //transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 164f);
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 164f);
            //Debug.Log("z: " + transform.localPosition.z);
            //Debug.Log("왼쪽");
        }
        else if (transform.localPosition.z > 165f)
        {
            //transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 165f);
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 165f);
            //Debug.Log("z: " + transform.localPosition.z);
            //Debug.Log("오른쪽");
        }

        // 상-하
        if (transform.localPosition.y < 63f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, 63f, transform.localPosition.z);
            //Debug.Log("y: " + transform.localPosition.y);
            //Debug.Log("상");
        }
        else if (transform.localPosition.y > 64.5f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, 64.5f, transform.localPosition.z);
            //Debug.Log("y: " + transform.localPosition.y);
            //Debug.Log("하");
        }
    }
}
