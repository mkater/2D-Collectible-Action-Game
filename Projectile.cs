using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float velocity = 5f;
    public float velocityY = 0f;
    private float projectileLifetime = 0.75f;
    private float upGrade = 0.5f;
    public static int upgradeCount = 0;   //when the upgrade is collected increase this value.
    private float upGradeTotal;
    Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //resets upgrades to 0 if player dies
     
        upGradeTotal = upGrade * (upgradeCount *1.0f);
        rb2d.velocity = new Vector2(velocity, velocityY);
        //projectile lasts longer with upgrades
        Destroy(gameObject, (projectileLifetime+upGradeTotal));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //projectile destroyed when it hits anything not a player or other projectile.
        if (collision.transform.tag != "Projectile" && collision.transform.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}
