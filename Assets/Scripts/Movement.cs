using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject PacStudent;
    public GameObject Cube;
    private Vector3 currentPos;
    public Tween tween;
    private float speed;
    //private ArrayList path;
    int path;


    // Start is called before the first frame update
    void Start()
    {
        speed = 2.5f;
        currentPos = new Vector3(-11f, 14f, 0f);
        int i;
        int path = 1;

        //PacStudent.transform.position = currentPos;
        Cube.transform.position = currentPos;

        //AddTween(PacStudent.transform, PacStudent.transform.position, new Vector3(0.0f, 13.0f, 0.0f), 0.5f);
        AddTween(Cube.transform, Cube.transform.position, new Vector3(-6.0f, 14.0f, 0.0f), speed);



        Debug.Log("POS" + PacStudent.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        switch (path)
        {
            case 1:
                AddTween(Cube.transform, Cube.transform.position, new Vector3(-6.0f, 14.0f, 0.0f), speed);
                break;
            case 2:
                AddTween(Cube.transform, Cube.transform.position, new Vector3(-6.0f, 10.0f, 0.0f), speed);
                break;
            case 3:
                AddTween(Cube.transform, Cube.transform.position, new Vector3(-11.0f, 10.0f, 0.0f), speed);
                break;
            case 4:
                AddTween(Cube.transform, Cube.transform.position, new Vector3(-11.0f, 14.0f, 0.0f), speed);
                path = 0;
                break;

        }

        

        Debug.Log(tween.Target + ":"+ tween.StartPos + ":" + tween.EndPos + ":" + tween.Duration);
        Debug.Log(tween.Target.position + ":" + tween.EndPos + ":" + currentPos);

        if (Vector3.Distance(tween.Target.position, tween.EndPos) >= 0.01)
        {
        float timeFraction = (Time.time - tween.StartTime) / tween.Duration;
            currentPos = Vector3.Lerp(tween.StartPos, tween.EndPos, timeFraction);
            tween.Target.position = currentPos;
            Debug.Log("GO");
        }
       else
        {
            tween.Target.position = tween.EndPos;
            tween = null;
            Debug.Log("NO");
            path++;
            //AddTween(Cube.transform, Cube.transform.position, new Vector3(-6.0f, 10.0f, 0.0f), speed);

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
