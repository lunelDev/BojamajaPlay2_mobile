using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieRealMotion : MonoBehaviour
{
    public Gun leftGun;
    public Gun rightGun;

    public int damage; //총알의 데미지 

    private void OnCollisionEnter(Collision collision)
    {
        if (AppManager.Instance.gameRunning.Equals(true)
            && Zombies.PlayerController.Instance._shootingAllowed.Equals(true))
        {
            if (this.gameObject.tag.Equals("L_Hand"))
            {
                leftGun.Shoot();
            }
            else if (this.gameObject.tag.Equals("R_Hand"))
            {
                rightGun.Shoot();
            }

        }
    }

}
