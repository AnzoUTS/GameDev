using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    private bool isBurger;
    public GameObject cherry;
    public static float x;
    private float randX;
    public float y;
    private const float centerX = 13.5f;
    private const float centerY = 14.0f;


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
        //x = Random.Range(-30.0f, 30.0f)+ centerX;
        x = Random.Range(-30.0f, 30.0f);


        //x = (centerX - randX)+centerX;


        Debug.Log("x Start "+ x);






        Instantiate(cherry, new Vector3(x+centerX, 5, 0), Quaternion.Euler(0, 0, 0));
        yield return new WaitForSeconds(10f);
        isBurger = false;

    }


    public static float Xvalue
    {
        get
        {
            return x;
        }
    }





}

