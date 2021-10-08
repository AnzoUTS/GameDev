using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerMover : MonoBehaviour
{

    private Tween tween;
    private Vector3 localPos;
    private Vector3 currentPos;
    private const float centerX = 13.5f;
    private const float centerY = -14.0f;
    private int x;
    private int y;
    private int axis;
    private float destroyX1;
    private float destroyY1;
    private float destroyX2;
    private float destroyY2;




    void Start()
    {


        destroyX1 = -99;
        destroyX2 = 99;
        destroyY1 = -99;
        destroyY2 = 99;

        Debug.Log("Burger Created");
        x = CherryController.Xvalue;
        y = CherryController.Yvalue;
        axis = CherryController.Axis;

    }



    private void FixedUpdate()
    {

        localPos = transform.localPosition;

        if (tween != null)
        {
            float timeFraction = (Time.time - tween.StartTime) / tween.Duration;
            currentPos = Vector3.Lerp(tween.StartPos, tween.EndPos, timeFraction);
            transform.position = currentPos;
        }
    }

    void Update()
    {
        x = CherryController.Xvalue;
        y = CherryController.Yvalue;
        axis = CherryController.Axis;



        if (localPos.x <= destroyX1 || localPos.x >= destroyX2 || localPos.y <= destroyY1 || localPos.y >= destroyY2)
        {
            Debug.Log("Burger Destroyed");
            Destroy(transform.gameObject);


        }


        switch (axis)
        {
            case 1:
                {

                    /*                  2 -14 -30 40
                                        destroyX1 = -17;
                                        destroyX2 = 44;
                                        destroyY1 = -30;
                                        destroyY2 = 99;*/

                    // --> down
                    destroyX1 = -17;
                    destroyX2 = 44;
                    destroyY1 = -30; 
                    destroyY2 = 99;// main
                    AddTween(transform, transform.localPosition, new Vector3((-x) + centerX, -33, 0), 10);
    

                    break;
                }

            case 2:
                {
                    // --> left
                    destroyX1 = -13; // main
                    destroyX2 = 44;
                    destroyY1 = -30;
                    destroyY2 = 2;
                    AddTween(transform, transform.localPosition, new Vector3(-15, (-y) + centerY, 0), 8);
   

                    break;
                }
            case 3:
                {
                    // --> up
                    destroyX1 = -17;
                    destroyX2 = 44;
                    destroyY1 = -99; // main
                    destroyY2 = 2; 
                    AddTween(transform, transform.localPosition, new Vector3((x) + centerX, 5, 0), 10);
                 
                    break;
                }

            case 4:
                {
                    // --> right
                    destroyX1 = -99;// main
                    destroyX2 = 40; 
                    destroyY1 = -30;
                    destroyY2 = 2;
                    AddTween(transform, transform.localPosition, new Vector3(45, (y) + centerY, 0), 8);
           

                    break;
                }

        }


        
    }

    public void AddTween(Transform targetObject, Vector3 startPos, Vector3 endpos, float duration)
    {
        
        if (tween == null)
        {
            //Debug.Log("x Target " + endpos.x + "|| y Target " + endpos.y);
            Debug.Log("x1 " + destroyX1 + "|| x2 " + destroyX2 + "|| y1 " + destroyY1 + "|| y2 " + destroyY2 + "x Target " + endpos.x + "|| y Target " + endpos.y);
            tween = new Tween(targetObject, startPos, endpos, Time.time, duration);
        }
    }


}


