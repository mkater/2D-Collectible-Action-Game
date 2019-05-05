using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static bool playerIsDead = false;    //player is alive at first
    public static bool activeGreaterFaith = false;  //no active faith
    public static float greaterFaithTime = 10.0f;   //lasts for 10 seconds when active
    public static int playerLives = 5;  //player has 5 lives
    public static int bibleCount = 0;   //keeps track of bibles collected
    public static int mannacount = 0;   //manna collected
    public static int totalScore = 0;   //the total score
    public static int questionsCount = 0;   //questions collected
    public static int questionsRequired = 5;    //always 5 per level
    public static int mannaRequired;    //differs per level
    public static int theLevel; 
    public static bool loseLife = false;
    public static bool levelComplete = false;  
    public GameObject exit; //the exit to each level
    private bool playerWantstoRestart = false;  //restarting level
    private bool playerWantsNewGame = false;    //restarting game from beginning
    private bool playerWinsGame = false;    //has beaten the game
    Scene scene;
    public static bool isPause; //game is paused
    GameObject go;
    public SpriteRenderer wastedIcon;   //player has died icon
    public SpriteRenderer gameOverIcon; //player out of lives icon
    public SpriteRenderer playerBeatsTheGame;   //player beaten the game icon
    public int timer = 300;

    // Start is called before the first frame update
    void Start()
    {
        //nothing active at start
        exit.SetActive(false);
        wastedIcon.enabled = false;
        gameOverIcon.enabled = false;
        playerBeatsTheGame.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //gets the active scene
        scene = SceneManager.GetActiveScene();
        if(playerWinsGame == true)
        {
            //if player wins game, pauses the game displays winning icon and restarts if they want to
            isPause = true;
            playerBeatsTheGame.enabled = true;
            playerWantsNewGame = true;
        }
        if(playerWantstoRestart == true && Input.GetKeyDown("space"))
        {
            //player has died and wants to restart, disables all icons and reloads current scene.
            isPause = false;
            wastedIcon.enabled = false;
            playerWantstoRestart = false;
            gameOverIcon.enabled = false;
            SceneManager.LoadScene(scene.name);
        }
        if(playerWantsNewGame == true && Input.GetKeyDown("space"))
        {
            //player wants to restart the game, disables all icons and reloads level one
            isPause = false;
            gameOverIcon.enabled = false;
            playerWantsNewGame = false;
            playerWinsGame = false;
            SceneManager.LoadScene("LevelOne");
        }
        if(playerIsDead == true)//if player dies
        {
            Projectile.upgradeCount = 0;
            playerController.fireRate = 0.5f;
            playerLives--;//takes away a life
            isPause = true;
            if (playerLives < 1)//game over
            {
                gameOverIcon.enabled = true;
                playerWantsNewGame = true;
                totalScore = 0; //resets lives, the score, and biblecount
                bibleCount = 0;
                playerLives = 5;
            }
            else 
            {
                wastedIcon.enabled = true;  //if they die once score etc. not reset
                playerWantstoRestart = true;
            }
            //after any death or restart these are set
            mannacount = 0;
            questionsCount = 0;
            playerIsDead = false;
           
        }//end of if player is dead
        //the levels and their specific manna required.
       if(scene.name == "LevelOne")
        {
            theLevel = 1;
            mannaRequired = 82;
        }
       else if(scene.name == "LevelTwo")
        {
            theLevel = 2;
            mannaRequired = 29; 
        }
       else if (scene.name == "LevelThree")
        {
            theLevel = 3;
            mannaRequired = 86;
        }
       else if (scene.name == "LevelFour")
        {
            theLevel = 4;
            mannaRequired = 4;
        }
       else if (scene.name == "LevelFive")
        {   
            theLevel = 5;
            mannaRequired = 70;
        }

       //if they reach requirement for beating level
       if(mannacount >= mannaRequired && questionsCount >= questionsRequired)
        {
            //exit appears
            exit.SetActive(true);
        }
       //keeping track of if greater faith is active and for how long
        if (activeGreaterFaith == true)
        {
             greaterFaithTime -= Time.deltaTime;
        }
        if (HeadsUpDisplay.timeleft < 0)
        {
            GameController.playerIsDead = true;
            HeadsUpDisplay.timeleft = 300;
        }

        if (greaterFaithTime < 0)
        {
            activeGreaterFaith = false;
            greaterFaithTime = 10f;
        }
        //when they actually cross onto the exit
        if(playerController.beatTheLevel == true)
        {
            totalScore += (HeadsUpDisplay.timeleft * 10);//they get points based on how many seconds left
            HeadsUpDisplay.timeleft = 0;
            //loads the next level
            if (scene.name == "LevelOne")
            {
                SceneManager.LoadScene("LevelTwo");
            }
            else if (scene.name == "LevelTwo")
            {
                SceneManager.LoadScene("LevelThree");
            }
            else if (scene.name == "LevelThree") 
            {
                SceneManager.LoadScene("LevelFour");
            }
            else if (scene.name == "LevelFour")
            {
                SceneManager.LoadScene("LevelFive");
            }
            else if (scene.name == "LevelFive")
            {
                //player wins game, triggers all the stuff that it entails
                playerWinsGame = true;
            }
            //questions and manna and time reset to starting values when restarting level
            questionsCount = 0;
            mannacount = 0;
            HeadsUpDisplay.timeleft = 300;
            playerController.beatTheLevel = false;
        }
        if (Input.GetKey(KeyCode.P))
        {
            //pauses the game
            isPause = true;
            Time.timeScale = 0;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            //turns off is pause
            isPause = false;
            Time.timeScale = 1;
        }
    }
}
