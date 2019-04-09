using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataManagement2 : MonoBehaviour
{
    Text scoreText;
    Text finalScoreDisplay;

    private float timer;
    public static int displayScore;
    public static bool gameState;
    public GameObject scoreObj;
    public GameObject finalScore;
    public GameObject[] endScreen;
    public GameObject[] inGameUI;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        endScreen = GameObject.FindGameObjectsWithTag("ShowOnEnd");
        inGameUI = GameObject.FindGameObjectsWithTag("ShowInGame");
    }

    // Update is called once per frame
    void DisplayScoreInGame()
    {
        scoreText = scoreObj.GetComponent<Text>();
        scoreText.text = displayScore.ToString();     
    }

    void DisplayFinalScore()
    {
        finalScoreDisplay = finalScore.GetComponent<Text>();
        finalScoreDisplay.text = "Score : " + displayScore.ToString();

    }

  

    public void showPaused()
    {
        foreach (GameObject g in endScreen)
        {
            g.SetActive(true);
        }

        foreach (GameObject g in inGameUI)
        {
            g.SetActive(false);
        }
    }

    public void hidePaused()
    {
        foreach (GameObject g in endScreen)
        { 
            g.SetActive(false);
        }

        foreach (GameObject g in inGameUI)
        {
            g.SetActive(true);
        }
    }

    void Update()
    {
        gameState = BallController.isDead;
        displayScore = BallController.score;

        if (gameState == true)
        {
            if (Time.timeScale == 1)
            {
                DisplayFinalScore();
                Time.timeScale = 0;
                showPaused();
            }
        }

        if (gameState == false)
        {
                Time.timeScale = 1;
                DisplayScoreInGame();
                hidePaused();
        }

    }
}
