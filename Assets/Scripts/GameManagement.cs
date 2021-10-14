using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagement : MonoBehaviour
{
    private GameObject ghostTime;
    private static int score;
    private static float scaredTime;
    private static bool scared;
    public static int lives;

    private void Start()
    {
        ghostTime = GameObject.Find("GhostTime");
        ghostTime.SetActive(false);
        score = 0;
        scaredTime = 0;
        lives = 3;
    }


    private void Update()
    {
        if(scared == true)
        {
            ghostTime.SetActive(true);
            ScaredTime -= Time.deltaTime;
            // Debug.Log(ScaredTime);
            if (ScaredTime <= 0)
            {
                ghostTime.SetActive(false);
                scared = false;
            }
        }
    }



/*    public void PacLife()
    {
        lives--;
        Debug.Log("PacLife :" +lives);
        if (lives == 0)
        {
            Debug.Log("GameOver");
        }

    }*/

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





}

/*    int PlayerPrefs.GetInt(string key);
    float PlayerPrefs.GetFloat(string key);
    string PlayerPrefs.GetString ((string key);
    void PlayerPrefs.SetInt (string key, int value);
    void PlayerPrefs.SetFloat(string key, float value);
    void PlayerPrefs.SetString (string key, string value);
    bool PlayerPrefs.HasKey (string key);
    void PlayerPrefs.DeleteKey (string key);
    void PlayerPrefs.DeleteAll();
  */

//}