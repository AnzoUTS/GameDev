using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStart : MonoBehaviour
{
    private Text start;
    private string[] countDown = { "3", "2", "1", "GO!" , ""};

    void Start()
    {

        start = GetComponent<Text>();
        StartCoroutine(Go());
        StartCoroutine(StopTime());
    }

    private IEnumerator Go()
    {
        start.text = (string)countDown.GetValue(0);
        yield return new WaitForSecondsRealtime(1f);
        start.text = (string)countDown.GetValue(1);
        yield return new WaitForSecondsRealtime(1f);
        start.text = (string)countDown.GetValue(2);
        yield return new WaitForSecondsRealtime(1f);
        start.text = (string)countDown.GetValue(3);
        yield return new WaitForSecondsRealtime(1f);
        AudioController.Music = true;
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    private IEnumerator StopTime()
    {
        Time.timeScale = 0;
        yield return new WaitForSeconds(4f);
        Time.timeScale = 1;
    }


}
