using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuideLine : MonoBehaviour
{
    //public static GuideLine Instance { get; private set; }

    //private void Awake()
    //{
    //    if (Instance != null)
    //        Destroy(this);
    //    else Instance = this;
    //}

    private void OnDisable()
    {
        StopCoroutine(_FadeInOut());
    }

    private void OnEnable()
    {
        StartCoroutine(_FadeInOut());
    }

    IEnumerator _FadeInOut()
    {
        Image img = GetComponent<Image>();
        Color color = img.color;
        float changeValueSpeed = 2f;
        bool fadeIn = true;

        color.a = 0f;

        while (true)
        {
            if (color.a < 1 && fadeIn)
            {
                color.a += Time.deltaTime * changeValueSpeed;
                img.color = color;
                if (color.a > 1)
                {
                    fadeIn = false;
                }
            }
            else
            {
                color.a -= Time.deltaTime * changeValueSpeed;
                img.color = color;
                if (color.a < 0)
                {
                    fadeIn = true;
                }
            }
            yield return null;
        }

    }
}