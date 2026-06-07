using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Sandwiches
{
  public class PlayerController : MonoBehaviour
  {
    public float pointerFollowSpeed;
   Camera _cam;
    void Start()
    {
      Input.multiTouchEnabled = false;
      _cam = Camera.main;
    }
        public void Grab(FoodItem foodItem)
    {
        
      StopCoroutine("_Hold");
      StartCoroutine("_Hold", foodItem);
    }

    private IEnumerator _Hold(FoodItem foodItem)
    {
      while (Input.GetMouseButton(0) && AppManager.Instance.gameRunning )
      {

                foodItem.transform.position =
          Vector3.Lerp(
            foodItem.transform.position,
            _cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _cam.WorldToScreenPoint(foodItem.transform.position).z)),
            pointerFollowSpeed * Time.deltaTime);

        CodeWrapper.AbsoluteZ(foodItem.transform, 0.5703665f, Space.World);

             //   Debug.Log("foodItem.transform.position : " + foodItem.transform.position);
        yield return null;
      }

      foodItem.Detach();
    }
  }
}