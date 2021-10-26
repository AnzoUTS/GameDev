using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStart : MonoBehaviour
{
    private Text start;
    private string[] countDown = { "3", "2", "1", "GO!" , ""};
    public GameObject ghostA;
    public GameObject ghostB;
    public GameObject ghostC;
    public GameObject ghostD;
    /*    GameObject ghostB;
        GameObject ghostC;
        GameObject ghostD;*/

    void Start()
    {
        //ghostA = GameObject.Find("OrcA");
/*        ghostB = GameObject.Find("OrcB");
        ghostC = GameObject.Find("OrcC");
        ghostD = GameObject.Find("OrcD");*/

        //ghostA.SetActive(false);
/*        ghostB.SetActive(false);
        ghostC.SetActive(false);
        ghostD.SetActive(false);*/

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
       // Instantiate(ghostA, new Vector3(12, -14, 0), Quaternion.Euler(0, 0, 0));
/*        Instantiate(ghostB, new Vector3(13, -14, 0), Quaternion.Euler(0, 0, 0));
        Instantiate(ghostC, new Vector3(14, -14, 0), Quaternion.Euler(0, 0, 0));
        Instantiate(ghostD, new Vector3(15, -14, 0), Quaternion.Euler(0, 0, 0));*/
/*        ghostB.SetActive(true);
        ghostC.SetActive(true);
        ghostD.SetActive(true);*/
        Time.timeScale = 1;
        gameObject.SetActive(false);
        



    }

    private IEnumerator StopTime()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(4f);
        //Time.timeScale = 1;

    }


}
