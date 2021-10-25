using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitGame : MonoBehaviour
{
    public Button quitButton;

    public void Start()
    {
        quitButton = GameObject.Find("QuitGame").GetComponent<Button>();
        quitButton.onClick.AddListener(Quit);
    }


    public void Quit()
    {
        UnityEditor.EditorApplication.isPlaying = false; // stops game
    }

}
