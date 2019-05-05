using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float speed;
    private float moveVertical, moveHorizontal;
    public Sprite EnemyDown;
    public Sprite EnemyUp;
    public Sprite EnemyLeft;
    public Sprite EnemyRight;
    private SpriteRenderer spriterenderer;
    public static bool isUp, isLeft, isRight, isDown;
    public LayerMask Limit;
    private int hitPoints = 3;
    private bool isPause;
    public float offSet = 1f;
    private string facing;
    private int counter = 0;
    public Collider2D above, below, left, right;
    private int c = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();
        spriterenderer = GetComponent<SpriteRenderer>();
        if (spriterenderer.sprite == null)
        {
            spriterenderer.sprite = EnemyDown;
            //getMoving(-1, 0, false, true, false, false);
        }
        isDown = false;
        isLeft = true;
        isUp = false;
        isRight = false;
        isPause = GameController.isPause;
    }

    // Update is called once per frame
    void Update()
    {
        isPause = GameController.isPause;

        if (!isPause)
        {
            if (hitPoints < 3)
            {
                speed = 200f;
            }

            rb2d.velocity = Vector2.zero;

            /* getMoving(1, 0, false, false, false, true);
             print(c);
             c++;*/
            facing = CheckFacingDirection();

          //  print("facing: " + facing);

            if (facing == "Left")
                LeftMovements();
            else if (facing == "Up")
                UpMovements();
            else if (facing == "Right")
                RightMovements();
            else
                DownMovements();

            for (int i = 0; i < 3; i++)
            {
                print("--------------------------------");
            }
        }

    }
    // Need to have player check if he can move only when he is at position 5.0, 5.0 and not when its at 5.2, 6.3. ENemy attempt to go when change direction but it collides then goes to next avaialbe direction cause to walk in circles
    // Enemy is also detecting its left and right colliders one unit away earlier

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        /*  rb2d.velocity = Vector2.zero;

         /* getMoving(1, 0, false, false, false, true);
          print(c);
          c++;*/
        /* facing = CheckFacingDirection();

         print("facing: " + facing);

         if (facing == "Left")
             LeftMovements();
         else if (facing == "Up")
             UpMovements();
         else if (facing == "Right")
             RightMovements();
         else
             DownMovements();

         for (int i = 0; i < 3; i++)
         {
             print("--------------------------------");
         }*/

    }

    private void printValues()
    {
      //  print("Checking which direction there was no collissions");
       // print("Enemy Above: " + ColliderDetectorAbove.detected);
       // print("Enemy Below: " + ColliderDetectorBelow.detected);
       // print("Enemy Right: " + ColliderDetectorRight.detected);
      //  print("Enemy Left: " + ColliderDetectorLeft.detected);
    }
    private void LeftMovements()
    {
        //print("Left Movements");
       // printValues();

        //turn down if possible
        if (!ColliderDetectorBelow.detected)
            getMoving(0, -1, false, true, false, false);
        //else go left if possible
        else if (!ColliderDetectorLeft.detected)
            getMoving(-1, 0, false, false, true, false);
        //else go up if possible
        else if (!ColliderDetectorAbove.detected)
            getMoving(0, 1, true, false, false, false);
        //else go right
        else
            getMoving(1, 0, false, false, false, true);
    }

    private void UpMovements()
    {
      //  print("Up Movements");
      //  printValues();

        // turn left if possible
        if (!ColliderDetectorLeft.detected)
            getMoving(-1, 0, false, false, true, false);
        // else go up if possible
        else if (!ColliderDetectorAbove.detected)
            getMoving(0, 1, true, false, false, false);
        // else go right if possible
        else if (!ColliderDetectorRight.detected)
            getMoving(1, 0, false, false, false, true);
        // else go down
        else
            getMoving(0, -1, false, true, false, false);
    }
    private void RightMovements()
    {
       // print("Right Movements");
       // printValues();

        // turn up if possible
        if (!ColliderDetectorAbove.detected)
            getMoving(0, 1, true, false, false, false);
        // else go right if possible
        else if (!ColliderDetectorRight.detected)
            getMoving(1, 0, false, false, false, true);
        // else go down if possible
        else if (!ColliderDetectorBelow.detected)
            getMoving(0, -1, false, true, false, false);
        // else go left
        else
            getMoving(-1, 0, false, false, true, false);
    }

    private void DownMovements()
    {
        //print("Down Movements");
       //printValues();

        if (!ColliderDetectorRight.detected)
            getMoving(1, 0, false, false, false, true);
        else if (!ColliderDetectorBelow.detected)
            getMoving(0, -1, false, true, false, false);
        // else go left if possible
        else if (!ColliderDetectorLeft.detected)
            getMoving(-1, 0, false, false, true, false);
        // else go up 
        else
            getMoving(0, 1, true, false, false, false);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Projectile")
        {
            hitPoints--;
            if (hitPoints < 1)
            {
                Destroy(gameObject);
                GameController.totalScore += 1000;
            }
        }
        else if (collision.transform.tag == "Player")
        {
            if (GameController.activeGreaterFaith == false)
            {
                GameController.playerIsDead = true;
                Destroy(collision.gameObject);
            }
        }


    }


    private string CheckFacingDirection()
    {
        if (spriterenderer.sprite == EnemyLeft)
            return "Left";
        else if (spriterenderer.sprite == EnemyUp)
            return "Up";
        else if (spriterenderer.sprite == EnemyRight)
            return "Right";
        else
            return "Down";
    }

    void getMoving(float horizontal, float vertical, bool isUp, bool isDown, bool isLeft, bool isRight)
    {
        if (isDown)
        {
            spriterenderer.sprite = EnemyDown;
        }
        else if (isLeft)
        {
            spriterenderer.sprite = EnemyLeft;
        }
        else if (isUp)
        {
            spriterenderer.sprite = EnemyUp;
        }
        else if (isRight)
        {
            spriterenderer.sprite = EnemyRight;
        }


        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(horizontal, vertical);

       // print("moving " + movement);

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rb2d.AddRelativeForce(movement * speed);
        //transform.rotation = Quaternion.LookRotation(movement);
    }

}
