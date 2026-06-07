using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyFactoryRealMotion : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        GameObject col = collision.gameObject;
        Debug.Log("------------------------15");
        ToyPart colToyPart = col.GetComponent<ToyPart>();
        if (AppManager.Instance.gameRunning.Equals(true))
        {
            if (col.gameObject.layer.Equals(16))
                    {
                        Debug.Log("------------------------4");
                        col.gameObject.transform.parent = null;
                        Debug.Log(col.gameObject.transform.parent);
                        if (!ToyFactory.PlayerController.Instance.holdBool)
                        {
                            ToyFactory.PlayerController.Instance.holdBool = true;
                            colToyPart.Active = true;
                        }
                       ToyFactory.PlayerController.Instance.CheckForDragUp(colToyPart); //물건을 집어올림
                    }
        }
        
    }
}
