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

    public List<string> deadGhosts;
    private static string ghostName;
    public static int deadGhostCount;

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

        //timer += Time.deltaTime;

        /*        if ((int)timer > lastTime)
                {
                    if (lastTime >= 0)
                    {
                       // Debug.Log("last time " + lastTime + " movement " + movement + "duration "+ duration);
                    }
                    lastTime = (int)timer;
                }
          */



        if (Scared == true)
        {
            Recovery = false;
            ghostTime.SetActive(true);
            ScaredTime -= Time.deltaTime;
      //      Debug.Log("GhostTime Scared:");

            if (ScaredTime <= 3)
            {
                Scared = false;
                Recovery = true;
       //         Debug.Log("GhostTime Recovery:");
            }
        }

        if (Recovery == true)
        {
            ScaredTime -= Time.deltaTime;

            if (ScaredTime <= 0)
            {    
                Recovery = false;
                ghostTime.SetActive(false);
             //   Debug.Log("GhostTime Off:");
            }
        }


        if (lives == 0 || pellets ==0 )
        {
            finalTime = GameTime.finalTime;
            GameResults();
        }

/*
        if (ghostName != null)
        {
            DeadGhost(ghostName);
        }*/
    
    
    
    
    
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



/*    public string Ghost
    {
        set { GhostName = value; }
        get { return GhostName; }
    }*/
/*    public static int DeadGhostCount
    {
        set { deadGhostCount = value; }
        get { return deadGhostCount; }
    }*/





   // public static string GhostName { get => ghostName; set => ghostName = value; }

    public void DeadGhost(string ghostName)
    {
        //Ghost = ghostName; // this ghost instance
        deadGhosts.Add(ghostName);
        Debug.Log(" Ghost added :" + ghostName + " Total ghosts :" + deadGhosts.Count);
        //ghostName = null;
    }


    public void AliveGhost(string ghostName)
    {
       // this.Ghost = ghostName; // this ghost instance
        deadGhosts.Remove(ghostName);
        Debug.Log(" Ghost :" + ghostName + " Removed Total ghosts :" + deadGhosts.Count);
        //ghostName = null;
    }


    public int GhostCount()
    {
        return deadGhosts.Count;
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
