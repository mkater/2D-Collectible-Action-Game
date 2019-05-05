using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weakness : MonoBehaviour
{
   
    private Rigidbody2D rb2d;
    private float velocity;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); 
    }
    private void Update()
    {
        velocity = rb2d.velocity.magnitude; //measures its velocity in terms of how fast the rigidbody is moving.
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if the velocity is over a certain threshold, it destroys the enemy and player
        if (collision.transform.tag == "Enemy")
        {
            if (velocity > 4)
            {
                Destroy(collision.gameObject);
                //giving the player points if an enemy
                GameController.totalScore += 1000;
            }
        }
        else if (collision.transform.tag == "Player")
        {
            if (velocity > 4)
            {
                //and triggering all the isdead effects if the player dies.
                GameController.playerIsDead = true;
                Destroy(collision.gameObject);
            }
        }
    }
}
