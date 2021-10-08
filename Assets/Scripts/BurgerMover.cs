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
    private int destroyX;
    private int destroyY;




    void Start()
    {
        Debug.Log("Burger Created");
        x = CherryController.Xvalue;
        y = CherryController.Yvalue;
        axis = CherryController.Axis;

    }



    private void FixedUpdate()
    {

        localPos = transform.localPosition;

        if (localPos.x == destroyX || localPos.y == destroyY)
        {
            Debug.Log("Burger Destroyed");
            Destroy(transform.gameObject);


        }



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

        switch (axis)
        {
            case 1:
                {
                    AddTween(transform, transform.localPosition, new Vector3((-x) + centerX, -33, 0), 10);
                    destroyX = -x + 13;
                    destroyY = -32;
                    break;
                }

            case 2:
                {
                    AddTween(transform, transform.localPosition, new Vector3(-15, (-y) + centerY, 0), 8);
                    destroyX = -14;
                    destroyY = -y - 13;
                    break;
                }
            case 3:
                {
                    AddTween(transform, transform.localPosition, new Vector3((x) + centerX, 5, 0), 10);
                    destroyX = x + 13 - 1;
                    destroyY =  4;
                    break;
                }

            case 4:
                {
                    AddTween(transform, transform.localPosition, new Vector3(40, (y) + centerY, 0), 8);
                    destroyX = 39;
                    destroyY = y-13;
                    break;
                }

        }


        
    }

    public void AddTween(Transform targetObject, Vector3 startPos, Vector3 endpos, float duration)
    {
        
        if (tween == null)
        {
            Debug.Log("x Target " + endpos.x + "|| y Target " + endpos.y);
            tween = new Tween(targetObject, startPos, endpos, Time.time, duration);
        }
    }


}


