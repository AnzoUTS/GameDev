using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerMover : MonoBehaviour
{

    private Tween tween;
    private Vector3 localPos;
    private Vector3 currentPos;
    private const float centerX = 13.5f;
    private const float centerY = 14.0f;
    private const int MaxX = 30;
    private const int MaxY = -40;
    private float x;
    private float y;




    void Start()
    {
        x = CherryController.Xvalue;

    }



    private void FixedUpdate()
    {

        localPos = transform.localPosition;

        if (localPos.x < -45 || localPos.x > 45 || localPos.y < -32 || localPos.y > 15)
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
        
        AddTween(transform, transform.localPosition, new Vector3((-x)+centerX, -33, 0), 10);
    }

    public void AddTween(Transform targetObject, Vector3 startPos, Vector3 endpos, float duration)
    {
        
        if (tween == null)
        {
            Debug.Log("x Target " + endpos.x);
            tween = new Tween(targetObject, startPos, endpos, Time.time, duration);
        }
    }


}


