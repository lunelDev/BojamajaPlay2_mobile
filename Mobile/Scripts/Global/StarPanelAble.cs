using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPanelAble : MonoBehaviour
{
    private void OnEnable()
    {
        SoundManager.Instance.PlayStar();
    }
}
