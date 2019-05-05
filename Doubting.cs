using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doubting : MonoBehaviour
{
    private int hitPoints = 2;  //two hits to destroy
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if hit by projectile remove hp, then destroy if hit twice.
        if(collision.transform.tag == "Projectile")
        {
            hitPoints--;
            if(hitPoints < 1)
            {
                Destroy(gameObject);
            }
        }
    }
}
