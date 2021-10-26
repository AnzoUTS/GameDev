using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScene : MonoBehaviour
{

    public void Start()
    {
        Debug.Log("StartScreen");
    }




    public void loadStartScene()
    {
        if (Time.timeScale == 1)
        {
            SceneManager.LoadScene("StartScene");
            Time.timeScale = 0;


            SceneManager.UnloadSceneAsync("Scene1"); // annoying, but seems to prevent a rare error
     
            //SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);
            

        }
    }




    public void loadLevel(string scene)
    {
        Time.timeScale = 0;
        GameManagement.StartMovement = false;
        SceneManager.LoadScene(scene);
    }

/*    public void unloadLevel(string scene)
    {
        {
            SceneManager.UnloadSceneAsync(scene); // cleanly 
        }
    }*/

}
