using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artillery : MonoBehaviour
{
    private Tween tween;
    private Vector3 localPos;
    private Vector3 currentPos;
    private const float centerX = 13.5f;
    private const float centerY = -14.0f;
    private int x;
    private int y;
    private int direction;
    private Vector3 targetLocation;
    private float targetDistance;
    private int duration;

    void Start()
    {
        duration = Random.Range(5, 7); // max Exclusive for int
       // Debug.Log("duration " + duration);
        x = ArtilleryStrike.Xvalue;
        y = ArtilleryStrike.Yvalue;
        direction = ArtilleryStrike.Axis;
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
        x = ArtilleryStrike.Xvalue;
        y = ArtilleryStrike.Yvalue;
        direction = ArtilleryStrike.Axis;
        targetLocation = ArtilleryStrike.StrikeLocation;
        targetDistance = Vector3.Distance(transform.localPosition, targetLocation);
        Debug.Log(targetDistance);

        if (targetDistance <= 1.5)
        {
            StartCoroutine(Explode());
            Debug.Log("Destroy");
        }

        AddTween(transform, transform.localPosition, targetLocation, duration);
    }

    public void AddTween(Transform targetObject, Vector3 startPos, Vector3 endpos, float duration)
    {

        if (tween == null)
        {
            tween = new Tween(targetObject, startPos, endpos, Time.time, duration);
        }
    }

    IEnumerator Explode()
    {
        yield return new WaitForSecondsRealtime(3);
        Destroy(transform.gameObject);
    }
}
