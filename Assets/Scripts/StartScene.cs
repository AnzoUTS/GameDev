using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScene : MonoBehaviour
{





    public void loadLevel(string scene)
    {
        if (Time.timeScale == 1)
        {
            SceneManager.LoadScene(scene);
        }
    }



}
