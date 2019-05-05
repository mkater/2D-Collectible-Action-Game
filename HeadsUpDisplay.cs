using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class HeadsUpDisplay : MonoBehaviour
{
    public Text levelText, scoreText, biblesFoundText, mannaFoundText, questionsFoundText, livesText, timeText;
    private int score, level, biblesFound, mannaFound, questionsFound, lives; 
    public static int timeleft;
    private Timer time;
    public SpriteRenderer greaterFaithIcon;
    private bool activateIcon = true;

    // Start is called before the first frame update
    void Start()
    {
        timeleft = 300;
        StartCoroutine("TimerCountdown");

        level = 1;
        questionsFound = 0;
       
        //time.Equals(30000);

        // switch for valling function later
        //levelText.text = "Level: " + level.ToString();
        //displays all the appropriate variables below in the HUD on the bottom of the screen.
        updateDisplay(levelText, "Level:", GameController.theLevel);
        updateDisplay(scoreText, "Score:", GameController.totalScore);
        updateDisplay(biblesFoundText, "Bibles:", GameController.bibleCount);
        updateDisplayLong(mannaFoundText, "Manna:", GameController.mannacount, GameController.mannaRequired);
        updateDisplayLong(questionsFoundText, "Questions:", GameController.questionsCount, GameController.questionsRequired);
        updateDisplay(livesText, "Lives:", GameController.playerLives);
        updateDisplay(timeText, "Time:", timeleft);

    }

    // Update is called once per frame
    void Update()
    {
        //displays the specified things, keeps track of changes in update
        updateDisplay(levelText, "Level:", GameController.theLevel);
        updateDisplay(scoreText, "Score:", GameController.totalScore);
        updateDisplay(biblesFoundText, "Bibles:", GameController.bibleCount);
        updateDisplayLong(mannaFoundText, "Manna:", GameController.mannacount, GameController.mannaRequired);
        updateDisplayLong(questionsFoundText, "Questions:", GameController.questionsCount, GameController.questionsRequired);
        updateDisplay(livesText, "Lives:", GameController.playerLives);
        updateDisplay(timeText, "Time:", timeleft);
        activateIcon = !activateIcon;
        //used to flicker the greaterFaith icon in the HUD when it has less than 5 seconds left
        if(GameController.activeGreaterFaith && GameController.greaterFaithTime < 5)
        {
            greaterFaithIcon.enabled = activateIcon;
        }
        else if (GameController.activeGreaterFaith)
        {
            greaterFaithIcon.enabled = true;
        }
        else
        {
            greaterFaithIcon.enabled = false;
        }

    }

    
    // In each function, call "levelText.text = "Level: " + level.ToString()" to update values displayed

    void updateDisplay(Text name, string word, int value)
    {
        name.text = word + " " + value.ToString();
    }
    void updateDisplayLong(Text name, string word, int value, int required)
    {
        name.text = word + " " + value.ToString() + "/" + required.ToString();
    }

    //decreases the timer.
    IEnumerator TimerCountdown()
    {
        Debug.Log("time left" + timeleft);
        while (true && !GameController.isPause)
        {
            yield return new WaitForSeconds(1);
            timeleft--;
        }
       
        //player dies if time reaches 0
    }
    bool checkIfTimeLeft()
    {
        if(timeleft < 0)
        {
            return false;
        }
        return true;
    }
    
}
