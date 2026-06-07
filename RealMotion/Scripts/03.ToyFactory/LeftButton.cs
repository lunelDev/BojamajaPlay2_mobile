using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Hotpot
{
    public class LeftButton : MonoBehaviour
    {
        public ConveyorBelt conveyorBelt;
        public BoxCollider leftBoxCollider;
        public BoxCollider rightBoxCollider;
        private void OnTriggerEnter(Collider other)
        {
            if (AppManager.Instance.gameRunning.Equals(true))
            {
                if (PopUpSystem.PopUpState.Equals(false))
                {
                    if (other.CompareTag("L_Hand") || other.CompareTag("R_Hand"))
                    {
                        if (conveyorBelt._FoodList.Count != 0)
                        {
                            leftBoxCollider.enabled = false;
                            rightBoxCollider.enabled = false;
                            conveyorBelt._FoodList[0].GetComponent<Food>().leftMove = true;
                            conveyorBelt._FoodList[0].GetComponent<Food>().MoveUP();
                            Invoke("EnableCollider", 0.5f);
                        }
                    }
                }
            }
        }

        public void EnableCollider()
        {
            leftBoxCollider.enabled = true;
            rightBoxCollider.enabled = true;
        }

    }
}