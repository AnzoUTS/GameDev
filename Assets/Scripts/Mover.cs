using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    //public GameObject PacStudent;
    //public GameObject Cube;
    public GameObject Pac2;
    public Vector3 currentPos;
    public Vector3 position;
    public Vector3 NormPos;
    public Tween tween;
    private float speed;
    //private ArrayList path;
    int path;
    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {

        anim = GetComponent<Animator>();
        speed = 2.5f;
        currentPos = new Vector3(-11f, 14f, 0f);
        path = 1;
    
        //PacStudent.transform.position = currentPos;
        //Cube.transform.position = currentPos;
        Pac2.transform.position = currentPos;
        AddTween(Pac2.transform, Pac2.transform.position, new Vector3(-6.0f, 14.0f, 0.0f), speed);

        //AddTween(PacStudent.transform, PacStudent.transform.position, new Vector3(0.0f, 13.0f, 0.0f), 0.5f);
        //AddTween(Cube.transform, Cube.transform.position, new Vector3(-6.0f, 14.0f, 0.0f), speed);



        //Debug.Log("POS" + PacStudent.transform.position);
    }

    // Update is called once per frame
    void Update()
    {

        NormPos = (tween.EndPos - tween.StartPos).normalized;
        Debug.Log("Normalized  :" + (tween.EndPos - tween.StartPos).normalized);


        /*        switch (path)
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
        */
        // Debug.Log("trans: "+Pac2.transform);
        //  Debug.Log(path);

        /*        switch (path)
                {
                    case 1:
                        AddTween(Pac2.transform, Pac2.transform.position, new Vector3(-6.0f, 14.0f, 0.0f), speed);
                        break;
                    case 2:
                        AddTween(Pac2.transform, Pac2.transform.position, new Vector3(-6.0f, 10.0f, 0.0f), speed);
                        break;
                    case 3:
                        AddTween(Pac2.transform, Pac2.transform.position, new Vector3(-11.0f, 10.0f, 0.0f), speed);
                        break;
                    case 4:
                        AddTween(Pac2.transform, Pac2.transform.position, new Vector3(-11.0f, 14.0f, 0.0f), speed);
                        path = 0;
                        break;

                }*/


        Debug.Log("DISTANCE : tween Target" + tween.Target.position + "tween EndPos" + tween.EndPos);

        // Debug.Log(tween.Target + ":"+ tween.StartPos + ":" + tween.EndPos + ":" + tween.Duration);
        //Debug.Log(tween.Target.position + ":" + tween.EndPos + ":" + currentPos);

        if (Vector3.Distance(tween.Target.position, tween.EndPos) >= 0.01)
        {
        float timeFraction = (Time.time - tween.StartTime) / tween.Duration;
            currentPos = Vector3.Lerp(tween.StartPos, tween.EndPos, timeFraction);
            tween.Target.position = currentPos;
        }
       else
        {
            tween.Target.position = tween.EndPos;
            tween = null;
            Debug.Log("Change Pos");
            path++;
            //AddTween(Cube.transform, Cube.transform.position, new Vector3(-6.0f, 10.0f, 0.0f), speed);

        }


        switch (path)
        {
            case 1:
                AddTween(Pac2.transform, Pac2.transform.localPosition, new Vector3(-6.0f, 14.0f, 0.0f), speed);
                break;
            case 2:
                AddTween(Pac2.transform, Pac2.transform.localPosition, new Vector3(-6.0f, 10.0f, 0.0f), speed);
                break;
            case 3:
                AddTween(Pac2.transform, Pac2.transform.localPosition, new Vector3(-11.0f, 10.0f, 0.0f), speed);
                break;
            case 4:
                AddTween(Pac2.transform, Pac2.transform.localPosition, new Vector3(-11.0f, 14.0f, 0.0f), speed);
                path = 0;
                break;

        }

        //PacStudent.transform.position = currentPos;
        //Cube.transform.position = currentPos;
        Pac2.transform.position = currentPos;
        // position = Pac2.transform.position;
        position = GetEndPos();
       // Debug.Log("positionGET:" + position);


        //Debug.Log("POS" + PacStudent.transform.position + "POSCube" + Cube.transform.position);
        // Debug.Log("Mover POS " + currentPos + "Mover ENDPOS: "+tween.EndPos );


       
       
        //  Debug.Log("X :" + position.x);
        // Debug.Log("Y :" + position.y);
        Debug.Log("anim UP :" + anim.GetBool("up") + " anim RIGHT :" + anim.GetBool("right")+ " anim DOWN:" + anim.GetBool("down") + " anim LEFT :" + anim.GetBool("left"));
        // Debug.Log("Y :" + anim);


        if (NormPos.x == 0.0f && NormPos.y == 1.0f)
        {
            anim.SetBool("up", true);
        }
        else
        {
            anim.SetBool("up", false);
        }


        if (NormPos.x == 1.0f && NormPos.y == 0.0f)
        {
            anim.SetBool("right", true);
        }
        else
        {
            anim.SetBool("right", false);
        }

        if (NormPos.x == 0.0f && NormPos.y == -1.0f)
        {
            anim.SetBool("down", true);
        }
        else
        {
            anim.SetBool("down", false);
        }


        if (NormPos.x == -1.0f && NormPos.y == 0.0f)
        {
            anim.SetBool("left", true);
        }
        else
        {
            anim.SetBool("left", false);
        }

        if (Input.GetKey(KeyCode.Return))
        {
            anim.SetBool("die", true);
        }






        /*
         * 
                if (directionVector.x == 1 && directionVector.y == 0)
                {
                    animator.Play(rightAnim);
                }
                else if (directionVector.x == -1 && directionVector.y == 0)
                {
                    animator.Play(leftAnim);
                }
                else if (directionVector.x == 0 && directionVector.y == 1)
                {
                    animator.Play(upAnim);
                }
                else if (directionVector.x == 0 && directionVector.y == -1)
                {
                    animator.Play(downAnim);
                }
        */




    }


    public void AddTween(Transform targetObject, Vector3 startPos, Vector3 endpos, float duration)
    {

        if (tween == null)
        {
            Debug.Log("Add Tween");
            tween = new Tween(targetObject, startPos, endpos, Time.time, duration);
        }

    }

    public Vector3 GetEndPos()
    {
        return tween.EndPos;
    }



}
