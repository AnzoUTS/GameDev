using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTime : MonoBehaviour
{

    private Text timeText;
    private float startTime;
    private float gameTime;
    private float hr;
    private float min;
    private float sec;

    void Start()
    {
        timeText = GetComponent<Text>();
        startTime = Time.time;
    }

    void Update()
    {

        //Debug.Log(gameTime);
        gameTime = Time.time - startTime;
        hr =  (gameTime / 3600f);
        min = (gameTime / 60f);
        sec = (gameTime % 60f);
        timeText.text = string.Format("GameTime {0:00}:{1:00}:{2:00}", hr, min, sec);

    }



}
