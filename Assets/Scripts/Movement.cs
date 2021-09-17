using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject PacStudent;
    public GameObject Cube;
    private Vector3 currentPos;
    public Tween tween;

    // Start is called before the first frame update
    void Start()
    {
        currentPos = new Vector3(-11f, 14f, 0f);
        //PacStudent.transform.position = currentPos;
        Cube.transform.position = currentPos;

        //AddTween(PacStudent.transform, PacStudent.transform.position, new Vector3(0.0f, 13.0f, 0.0f), 0.5f);
        AddTween(Cube.transform, Cube.transform.position, new Vector3(0.0f, 14.0f, 0.0f), 11.5f);

        Debug.Log("POS" + PacStudent.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(tween.Target + ":"+ tween.StartPos + ":" + tween.EndPos + ":" + tween.Duration);
        Debug.Log(tween.Target.position + ":" + tween.EndPos + ":" + currentPos);

        if (Vector3.Distance(tween.Target.position, tween.EndPos) > 0.1f)
        {
        float timeFraction = (Time.time - tween.StartTime) / tween.Duration;
            currentPos = Vector3.Lerp(tween.StartPos, tween.EndPos, timeFraction);
            tween.Target.position = currentPos;
            Debug.Log("GO");
        }
       else
        {
            tween.Target.position = tween.EndPos;
            //tween = null;
            Debug.Log("NO");

       }

        //PacStudent.transform.position = currentPos;
        Cube.transform.position = currentPos;

        Debug.Log("POS" + PacStudent.transform.position + "POSCube" + Cube.transform.position);

    }


    public void AddTween(Transform targetObject, Vector3 startPos, Vector3 endpos, float duration)
    {

        if (tween == null)
        {
            Debug.Log("Add Tween");
            tween = new Tween(targetObject, startPos, endpos, Time.time, duration);
        }

    }





}
