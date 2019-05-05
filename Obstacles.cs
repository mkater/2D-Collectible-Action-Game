using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    //the brown rocks
    private int hitPoints = 3;
    private Rigidbody2D rb2d;
    private float velocity;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();    
    }
    private void Update()
    {
        //velocity is equal to the rigidbody magnitude velocity
        velocity = rb2d.velocity.magnitude;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Projectile")
        {
            hitPoints--;
            if (hitPoints < 1)
            {
                Destroy(gameObject);
            }
        }
        else if (collision.transform.tag == "Enemy")
        {
            if (velocity > 4)
            {
                //destroys enemy, gives player points
                Destroy(collision.gameObject);
                GameController.totalScore += 1000;
            }
        }
        else if(collision.transform.tag == "Player")
        {
            if (velocity > 4)
            {
                //destroys player
                GameController.playerIsDead = true;
                Destroy(collision.gameObject);
            }
        }
    }
}
