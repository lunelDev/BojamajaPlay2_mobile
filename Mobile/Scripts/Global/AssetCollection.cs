using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssetCollection : MonoBehaviour
{
    [Tooltip("Best to worst")]
    public Sprite[] assets;
    public Image imageComponent;
    
    
    void Start()
    {
        imageComponent = GetComponent<Image>();
    }
}
