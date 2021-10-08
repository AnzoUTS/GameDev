using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    private bool isBurger;
    public GameObject cherry;
    public static int x;
    public static int y;
    private static int axis;
    private const float centerX = 13.5f;
    private const float centerY = -14.0f;
    private Object burgerMaker; 



    void Start()
    {
        x = Random.Range(-30, 30);
        y = Random.Range(-30, 30);
        axis = Random.Range(1, 4);



    }



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
     
        x = Random.Range(-30, 30);
        y = Random.Range(-30, 30);
        axis = Random.Range(1, 5); // check random max for int (5)

        switch (axis)
        {
            case 1: 
                {
                    Instantiate(cherry, new Vector3(x + centerX, 5, 0), Quaternion.Euler(0, 0, 0));
                    break;
                }

            case 2:
                {
                    Instantiate(cherry, new Vector3(40, y + centerY, 0), Quaternion.Euler(0, 0, 0));
                    break;
                }

            case 3:
                {
                    Instantiate(cherry, new Vector3(-x + centerX, -33, 0), Quaternion.Euler(0, 0, 0));
                    break;
                }

            case 4:
                {
                    Instantiate(cherry, new Vector3(-15, -y + centerY, 0), Quaternion.Euler(0, 0, 0));
                    break;
                }









        }


        Debug.Log("x Start "+ x + "|| y Start " + y + "|| axis :" + axis);


   



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
            return axis;
        }
    }



}

