using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    KeyCode lastInput;
    KeyCode currentInput;
    public GameObject PacStudent;
    private Vector3 currentPos;
    private Vector3 NormPos;
    private Vector3 localPos;
    private Tween tween;
    private float speed;
    private Animator anim;
    public AudioClip Movement;
    private AudioSource audio;
    int path;



    void Start()
    {

        anim = GetComponent<Animator>();
        speed = 2f;

        audio = GetComponent<AudioSource>();
        //path = 1;

        // ### 90% Band - START 
        // currentPos = new Vector3(-11f, 14f, 0f);
        // AddTween(PacStudent.transform, PacStudent.transform.position, new Vector3(-6.0f, 14.0f, 0.0f), speed);
        // ### 90% Band - END 

        currentPos = new Vector3(1f, -1f, 0f);
        AddTween(PacStudent.transform, currentPos, new Vector3(2f, -1f, 0f), 0f);
        PacStudent.transform.position = currentPos;

    }

    private void FixedUpdate()
    {
        localPos = PacStudent.transform.localPosition;

        float timeFraction = (Time.time - tween.StartTime) / tween.Duration;
        currentPos = Vector3.Lerp(tween.StartPos, tween.EndPos, timeFraction);
        PacStudent.transform.position = currentPos;
    }


    void Update()
    {
        NormPos = (tween.EndPos - tween.StartPos).normalized;
        //Debug.Log("Normalized  :" + NormPos + " || Animations : anim UP :" + anim.GetBool("up") + " anim RIGHT :" + anim.GetBool("right") + " anim DOWN :" + anim.GetBool("down") + " anim LEFT :" + anim.GetBool("left") + " || Distance : tween Target" + tween.Target.position + " tween EndPos" + tween.EndPos);

        if (NormPos.x != 0.0f || NormPos.y != 0.0f)
        {
            if (!audio.isPlaying)
            {
                audio.clip = Movement;
                audio.Play();

            }
        }

        if (Vector3.Distance(tween.Target.position, tween.EndPos) >= 0.01)
        {
            tween.Target.position = currentPos;
        }
        else
        {
            tween.Target.position = tween.EndPos;
            //tween = null;
            //path++;

        }

        //localPos = PacStudent.transform.localPosition;


        Debug.Log("LocalPos :" +localPos + " : CurrentPos :" + currentPos);
 //       Debug.Log("CurrentPos :" + currentPos);

        if (Input.GetKeyDown(KeyCode.A)){
            AddTween(PacStudent.transform, localPos, new Vector3(localPos.x-1, localPos.y, localPos.z), speed);
            Debug.Log("KeyPress  : A");
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            AddTween(PacStudent.transform, localPos, new Vector3(localPos.x + 1, localPos.y, localPos.z), speed);
            Debug.Log("KeyPress  : D");
        }


        if (Input.GetKeyDown(KeyCode.W))
        {
            AddTween(PacStudent.transform, localPos, new Vector3(localPos.x, localPos.y -1, localPos.z), speed);
            Debug.Log("KeyPress  : W");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            AddTween(PacStudent.transform, localPos, new Vector3(localPos.x, localPos.y+1, localPos.z), speed);
            Debug.Log("KeyPress  : S");
        }





        switch (path)
        {

            case 1:
                AddTween(PacStudent.transform, PacStudent.transform.localPosition, new Vector3(6.0f, -1.0f, -1.0f), speed);
                break;
            case 2:
                AddTween(PacStudent.transform, PacStudent.transform.localPosition, new Vector3(6.0f, -5.0f, -1.0f), speed);
                break;
            case 3:
                AddTween(PacStudent.transform, PacStudent.transform.localPosition, new Vector3(1.0f, -5.0f, -1.0f), speed);
                break;
            case 4:
                AddTween(PacStudent.transform, PacStudent.transform.localPosition, new Vector3(1.0f, -1.0f, -1.0f), speed);
                //path = 0;
                break;

        }

        //PacStudent.transform.position = currentPos;  // moved to fixed update

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

    }


    public void AddTween(Transform targetObject, Vector3 startPos, Vector3 endpos, float duration)
    {

        if (tween == null)
        {
            //Debug.Log("Add Tween");
            tween = new Tween(targetObject, startPos, endpos, Time.time, duration);
        }

    }
}
