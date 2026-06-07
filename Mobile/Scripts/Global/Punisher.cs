using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI; 
[RequireComponent(typeof(CanvasGroup))]
public class Punisher : MonoBehaviour
{
    [SerializeField, HideInInspector] CanvasGroup _penaltyOverlay;
    public Text RankOb;
    public Text ClassicOb;

    public Color Black;
    public Color Red; 

    private void OnValidate()
    {
        _penaltyOverlay = GetComponent<CanvasGroup>();
    }

    public void ExecutePenalty()
    {
        StopCoroutine("_ExecutePenalty");
        StartCoroutine("_ExecutePenalty");
    }

    IEnumerator _ExecutePenalty()
    {
        float value = 1;
        _penaltyOverlay.alpha = value;
        Red.a = value;
        if (GameManager.RankMode.Equals(true))
        {   
            RankOb.color = Red;
        }
        else
        {
            ClassicOb.color = Red;
        }
        while (value > 0.1f)
        {
            value -= Time.deltaTime * 7f;
            _penaltyOverlay.alpha = value;
            if (GameManager.RankMode.Equals(true))
            {
                Red.a = value;
                RankOb.color = Red;
            }
            else
            {
                Red.a = value;
                ClassicOb.color = Red;
            }
            yield return null;
        }
        //점수는 다시 검정색으로 만들어주기   
        Black.a = 1;
        if (GameManager.RankMode.Equals(true))
        {
            RankOb.color = Black;
        }
        else
        {
            ClassicOb.color = Black;
        }
    }
}