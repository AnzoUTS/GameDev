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

        if (GameManagement.Life > 0)
        {
            //Debug.Log(gameTime);
            gameTime = Time.time - startTime;
            hr = (gameTime / 3600f);
            min = (gameTime / 60f);
            sec = (gameTime % 60f);
            time = string.Format("{0:00}:{1:00}:{2:00}", hr, min, sec);
            timeText.text = "GameTime : " + time;
        }
        else
        {
            finalTime = string.Format("{0:00}:{1:00}:{2:00}", hr, min, sec);
        }
    }

    public static string FinalTime
    {
        get { return finalTime; }
    }



}
