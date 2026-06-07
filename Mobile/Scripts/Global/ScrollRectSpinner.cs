using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollRectSpinner : MonoBehaviour
{
    private ScrollRect scrollRect;
    private SceneLoader sceneLoader;

    public SceneIndex[] targets;
    public RectTransform target;
    public RectTransform content;
    public float spinMin = 25000f;
    public float spinMax = 100000f;


    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        sceneLoader = FindObjectOfType<SceneLoader>();

        targets = content.transform.GetComponentsInChildren<SceneIndex>();
        Spin();

        PlayerPrefs.DeleteAll();
    }
    void Update()
    {
        if (scrollRect.horizontalScrollbar.value >= 1f)
            ResetScrollview();
    }

    private void ResetScrollview()
    {
        Vector2 currVelocity = scrollRect.velocity;
        scrollRect.horizontalScrollbar.value = 0f;

        scrollRect.velocity = currVelocity;
    }
    public void Spin()
    {
        scrollRect.velocity = new Vector2(-UnityEngine.Random.Range(spinMin, spinMax), 0f);
        StartCoroutine(CheckSnap());
    }
    private IEnumerator CheckSnap()
    {
        while (Mathf.Abs(scrollRect.velocity.x) > 5f)
            yield return null;

        RectTransform closest = targets[0].GetComponent<RectTransform>();
        foreach (RectTransform test in content)
        {
            var scrollRectTargetPos = WorldToLocal(scrollRect.transform.position) - WorldToLocal(test.position);

            if (Vector2.Distance(scrollRect.transform.position, test.position) < Vector2.Distance(scrollRect.transform.position, closest.position))
                closest = test;

            yield return null;
        }

        StartCoroutine(SnapTo(closest));
    }

    private Vector2 WorldToLocal(Vector3 position)
    {
        return (Vector2)scrollRect.transform.InverseTransformPoint(position);
    }

    public IEnumerator SnapTo(RectTransform target)
    {
        while (Vector2.Distance(content.anchoredPosition, WorldToLocal(content.position) - WorldToLocal(target.position)) > 10f)
        {
            content.anchoredPosition = Vector2.Lerp(content.anchoredPosition, WorldToLocal(content.position) - WorldToLocal(target.position), Time.deltaTime * 10f);

            yield return null;
        }

        content.anchoredPosition = WorldToLocal(content.position) - WorldToLocal(target.position);

        FindObjectOfType<SceneLoader>().LoadScene(target.GetComponent<SceneIndex>().sceneIndex);
    }
}
