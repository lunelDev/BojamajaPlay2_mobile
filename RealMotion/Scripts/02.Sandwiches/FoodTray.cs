using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodTray : MonoBehaviour
{
    public GameObject foodItem;
    public Transform TrayPos;
    Sandwiches.PlayerController _player;
    // public bool Active { get; set; }

    void Start()
    {
        _player = FindObjectOfType<Sandwiches.PlayerController>();
    }
    /*
        private void OnCollisionEnter(Collision collision)
        {
           /* GameObject col = collision.gameObject;
            if (col.tag.Equals("L_Hand") || col.tag.Equals("R_Hand"))
            {
                if (AppManager.Instance.gameRunning && OrderManager.CantHold.Equals(false))
                {
                    Sandwiches.PlayerController.CantTouch = true;
                    FoodItem foodItemInstance = Instantiate(foodItem).GetComponent<FoodItem>();
                    foodItemInstance.transform.position = transform.TransformPoint(Vector3.zero);
                    SoundManager.Instance.PlaySFX("15 pile material");
                    foodItemInstance.transform.position = Vector3.Lerp(foodItemInstance.transform.position, TrayPos.transform.position, 1f);

                    // _player.Grab(foodItemInstance);
                }
            }
        }*/
    private void OnCollisionExit(Collision collision)
    {

        GameObject col = collision.gameObject;

        if (AppManager.Instance.gameRunning && OrderManager.CantHold.Equals(false))
        {
            if (col.tag.Equals("L_Hand") || col.tag.Equals("R_Hand"))
            {
                Debug.Log(col);
                Sandwiches.PlayerController.CantTouch = true;
                FoodItem foodItemInstance = Instantiate(foodItem).GetComponent<FoodItem>();
                foodItemInstance.transform.position = transform.TransformPoint(Vector3.zero);
                SoundManager.Instance.PlaySFX("15 pile material");
                foodItemInstance.transform.position = Vector3.Lerp(foodItemInstance.transform.position, TrayPos.transform.position, 1f);

                // _player.Grab(foodItemInstance);
            }
        }
    }

}
