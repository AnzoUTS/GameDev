using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    private bool isBurger;
    public GameObject cherry;
    public static int x;
    public static int y;
    private static int direction;
    private const float centerX = 13.5f;
    private const float centerY = -14.0f;

    void Update()
    {
        if (!isBurger)
        {
            isBurger = true;
            StartCoroutine(CherryBurger());
        }
    }

    private IEnumerator CherryBurger()
    {
        x = Random.Range(-20, 20);
        y = Random.Range(-20, 20);
        direction = Random.Range(1, 5); // max Exclusive for int

        switch (direction)
        {
            case 1: 
                {
                    Instantiate(cherry, new Vector3(x + centerX, 5, 0), Quaternion.Euler(0, 0, 0));
                  //  Debug.Log("x Start " + (x + centerX) + "|| y Start " + 5 + "|| axis :" + axis);
                    break;
                }

            case 2:
                {
                    Instantiate(cherry, new Vector3(40, y + centerY, 0), Quaternion.Euler(0, 0, 0));
                 //   Debug.Log("x Start " + 40 + "|| y Start " + (y + centerY) + "|| axis :" + axis);
                    break;
                }

            case 3:
                {
                    Instantiate(cherry, new Vector3(-x + centerX, -33, 0), Quaternion.Euler(0, 0, 0));
                //    Debug.Log("x Start " + (-x + centerX) + "|| y Start " + -33 + "|| axis :" + axis);
                    break;
                }

            case 4:
                {
                    Instantiate(cherry, new Vector3(-15, -y + centerY, 0), Quaternion.Euler(0, 0, 0));
                //    Debug.Log("x Start " + -15 + "|| y Start " +  (-y + centerY) + "|| axis :" + axis);
                    break;
                }
        }

        yield return new WaitForSeconds(10f);
        isBurger = false;
    }

    public static int Xvalue
    {
        get
        {
            return x;
        }
    }

    public static int Yvalue
    {
        get
        {
            return y;
        }
    }

    public static int Axis
    {
        get
        {
            return direction;
        }
    }

}

