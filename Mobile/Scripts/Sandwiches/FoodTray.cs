using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodTray : MonoBehaviour
{
    public GameObject foodItem;

    Sandwiches.PlayerController _player;


    void Start()
    {
        _player = FindObjectOfType<Sandwiches.PlayerController>();
    }

    void OnMouseDown()
    {
        if (AppManager.Instance.gameRunning && OrderManager.CantHold.Equals(false) && PopUpSystem.PopUpState.Equals(false))
        {

            FoodItem foodItemInstance = Instantiate(foodItem).GetComponent<FoodItem>();
            foodItemInstance.transform.position = transform.TransformPoint(Vector3.zero);

            SoundManager.Instance.PlaySFX("15 pile material");
            _player.Grab(foodItemInstance);
        }
    }
}
