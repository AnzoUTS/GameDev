using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTime : MonoBehaviour
{

    private Text timeText;
    private string time;
    private float startTime;
    private float gameTime;
    private float hr;
    private float min;
    private float sec;
    public static string finalTime;

    void Start()
    {
        timeText = GetComponent<Text>();
        startTime = Time.time;
    }

    void Update()
    {

        if (GameManagement.Life > 0 && GameManagement.Pellets > 0)
        {
            gameTime = Time.time - startTime;
            TimeSpan timeSpan = TimeSpan.FromSeconds(gameTime);
            time = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
            timeText.text = "GameTime : " + time;

    /*        //Debug.Log(gameTime);
            gameTime = Time.time - startTime;
            hr = (gameTime / 3600f);
            min = (gameTime / 60f);
            sec = (gameTime % 60f);

            time = string.Format("{0:00}:{1:00}:{2:00}", hr, min, sec);
            timeText.text = "GameTime : " + time;*/
        }
        else
        {
            /*         TimeSpan timeSpan = TimeSpan.FromSeconds(gameTime);
                        string timeText = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);*/
            finalTime = time;
            //finalTime = string.Format("{0:00}:{1:00}:{2:00}", hr, min, sec);
        }
    }

    public static string FinalTime
    {
        get { return finalTime; }
    }



}
