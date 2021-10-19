using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagement : MonoBehaviour
{
    private static int score;
    private static float scaredTime;
    private static bool scared;
    private static bool recovery;
    public static int lives;
    private string finalTime;
    private string highScore;
    private string bestTime;
    private DateTime previousTime;
    private DateTime currentTime;
    private GameObject[] walkableGameObjects;
    private GameObject gameOver;
    private GameObject ghostTime;
    private int startingPellets; 
    private int previousBest;
    private static int pellets;
    public AudioClip gameOverClip;
    AudioSource gameOverMusic;

    private void Start()
    {
        highScore = PlayerPrefs.GetString("HighScore");
        previousBest = int.Parse(highScore);
        bestTime = PlayerPrefs.GetString("FastestTime");
        ghostTime = GameObject.Find("GhostTime");
        walkableGameObjects = GameObject.FindGameObjectsWithTag("Walkable");
        gameOver = GameObject.Find("GameOver");
        gameOver.SetActive(false);
        ghostTime.SetActive(false);
        score = 0;
        scaredTime = 0;
        lives = 3;
        gameOverMusic = GetComponent<AudioSource>();
        gameOverMusic.clip = gameOverClip;

        foreach (GameObject obj in walkableGameObjects)
        {
            if (obj.name.Contains("Pellet"))
            {
                startingPellets++;
            }
        }
        pellets = startingPellets-1;
        Debug.Log("Starting Pellets :" + pellets);
    }

    private void Update()
    {
        if(scared == true)
        {
            ghostTime.SetActive(true);
            ScaredTime -= Time.deltaTime;
            //Debug.Log("ScardTime" + scaredTime);
            if (ScaredTime <= 3)
            {
                scared = false;
                recovery = true;
               // ghostTime.SetActive(false);          
            }
        }

        if (recovery == true)
        {
            ScaredTime -= Time.deltaTime;

            if (ScaredTime <= 0)
            {    
                recovery = false;
                ghostTime.SetActive(false);
            }
        }




        if (lives == 0 || pellets ==0 )
        {
            finalTime = GameTime.finalTime;
            GameResults();
        }
    }

    public static int Pellets
    {
        set { pellets = value; }
        get { return pellets; }
    }

    public static int Life
    {
        set { lives = value; }
        get { return lives; }
    }

    public static int Score
    {
        set { score = value; }
        get { return score; }
    }

    public static float ScaredTime
    {
        set { scaredTime = value; }
        get { return scaredTime; }
    }

    public static bool Scared
    {
        set { scared = value; }
        get { return scared; }
    }


    public static bool Recovery
    {
        set { recovery = value; }
        get { return recovery; }
    }

    public void GameResults()
    {
        gameOver.SetActive(true);
        Invoke("StartScreen", 3f);

        Debug.Log("PREF bestscore " + highScore + " PREF best time " + bestTime);
        Debug.Log("gamescore " + score + " gametime " + finalTime);
        
        if (score > previousBest) {
            PlayerPrefs.SetString("FastestTime", finalTime);
            PlayerPrefs.SetString("HighScore", score.ToString());
            Debug.Log("New High Score");
        } 

        if (score == previousBest)

        {
            var cultureInfo = new CultureInfo("en-AU");
            previousTime = DateTime.ParseExact(bestTime, "hh:mm:ss", cultureInfo);
            currentTime = DateTime.ParseExact(finalTime, "hh:mm:ss", cultureInfo);
            Debug.Log("oldtime" + previousTime + " newtime " + currentTime);

            if ( currentTime < previousTime)
            {
                PlayerPrefs.SetString("FastestTime", finalTime);
                Debug.Log("New Fastest Time");
            }     
        }
    }

    private void StartScreen()
    {
            SceneManager.LoadScene("StartScene");
    }
    
}
