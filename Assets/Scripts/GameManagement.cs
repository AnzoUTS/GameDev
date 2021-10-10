using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagement : MonoBehaviour
{
    private GameObject HUD;
    private static int score;

    public static int Score
    {
        set { score = value; } 
        get { return score; }
    }



    private void Start()
    {
        score = 0;
    }

    private void Update()
    {

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













}