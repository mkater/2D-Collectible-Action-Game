using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Murmurings : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //just takes one hit to destroy.
        if(collision.transform.tag == "Projectile")
        {
            Destroy(gameObject);
        }
    }
}
