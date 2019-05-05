using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerController : MonoBehaviour
{

    public static bool beatTheLevel = false;
    private Rigidbody2D rb2d;
    public GameObject mainCamera;
    public GameObject ProjectileLeft;
    public GameObject ProjectileRight;
    public GameObject ProjectileUp;
    public GameObject ProjectileDown;
    public float speed;
    //four sprites for the four positions of Moses
    public Sprite MosesDown;
    public Sprite MosesUp;
    public Sprite MosesLeft;
    public Sprite MosesRight;
    private SpriteRenderer spriterenderer;
    private int direction;
    Vector2 projectileSpawnPoint;
    public static float fireRate = 0.5f;   //change this value to increase the firing rate.
    float nextFire = 0f;
    private bool isPause;

    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();
        spriterenderer = GetComponent<SpriteRenderer>();
        if(spriterenderer.sprite == null)
        {
            //starts as MosesDown at the beginning
            spriterenderer.sprite = MosesDown;
        }

        isPause = GameController.isPause;
    }

    private void Update()
    {
        //camera follows the player, except for the clamping done in camerascripts
        mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -20);

        isPause = GameController.isPause;

        if (!isPause)
        {
            //sets the sprite and the direction of movement in the 4 directions
            if (Input.GetKey(KeyCode.UpArrow))
            {
                spriterenderer.sprite = MosesUp;
                direction = 1;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                spriterenderer.sprite = MosesDown;
                direction = 2;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                spriterenderer.sprite = MosesRight;
                direction = 3;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                spriterenderer.sprite = MosesLeft;
                direction = 4;
            }

            //fires if space is held or pressed and if the firing rate allows it.
            if (Input.GetKeyDown("space") || Input.GetKey("space"))
            {
                if (Time.time > nextFire)
                {
                    nextFire = Time.time + fireRate;
                    fire();
                }
            }
        }
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {

        if (!isPause)
        {
            //resets to 0 at every update to avoid him from gliding after input released
            rb2d.velocity = Vector2.zero;
            //Store the current horizontal input in the float moveHorizontal.
            float moveHorizontal = Input.GetAxis("Horizontal");

            //Store the current vertical input in the float moveVertical.
            float moveVertical = Input.GetAxis("Vertical");

            //Use the two store floats to create a new Vector2 variable movement.
            Vector2 movement = new Vector2(moveHorizontal, moveVertical);

            //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
            rb2d.AddRelativeForce(movement * speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //for picking up the various objects
        if(collision.gameObject.CompareTag("Authority"))
        {
            collision.gameObject.SetActive(false);
            Projectile.upgradeCount++;
        }
        else if(collision.gameObject.CompareTag("WordPickUp"))
        {
            collision.gameObject.SetActive(false);
            //increases firing rate
            if(fireRate > 0.2f)
            {
                fireRate -= 0.1f;
            }
            //resets to default if player dies
           
        }
        else if(collision.gameObject.CompareTag("Bible"))
        {
            collision.gameObject.SetActive(false);
            GameController.bibleCount++;
            //you get an extra life for every 10 bibles you pick up
            if(GameController.bibleCount % 10 == 0)
            {
                GameController.playerLives++;
            }
        }
        else if(collision.gameObject.CompareTag("Manna"))
        {      
                collision.gameObject.SetActive(false);
                GameController.mannacount++;
            //getting manna gives you points
                GameController.totalScore += 100;
        }
        else if(collision.gameObject.CompareTag("GreaterFaith"))
        {
            collision.gameObject.SetActive(false);
            GameController.activeGreaterFaith = true;
            
        }
        else if(collision.gameObject.CompareTag("Questions"))
        {
            collision.gameObject.SetActive(false);
            GameController.questionsCount++;
        }
        else if(collision.gameObject.CompareTag("Exit"))
        {
            beatTheLevel = true;
        }
    }

    void fire()
    {
        //fires a projectile depending on what direction Moses is facing either up, left, down, and right with the appropriate object that has its own velocity to ensure it goes the right way.
        projectileSpawnPoint = transform.position;
        if (direction == 1)
        {
            projectileSpawnPoint += new Vector2(0f, 0.9f);
            Instantiate(ProjectileUp, projectileSpawnPoint, Quaternion.identity);
        }
        else if (direction == 2)
        {
            projectileSpawnPoint += new Vector2(0f, -0.9f);
            Instantiate(ProjectileDown, projectileSpawnPoint, Quaternion.identity);
        }
        else if (direction == 3)
        {
            projectileSpawnPoint += new Vector2(+0.9f, 0f);
            Instantiate(ProjectileRight, projectileSpawnPoint, Quaternion.identity);
        }
        else if (direction == 4)
        {
            projectileSpawnPoint += new Vector2(-0.9f, 0f);
            Instantiate(ProjectileLeft, projectileSpawnPoint, Quaternion.identity);
        }
    }
}
