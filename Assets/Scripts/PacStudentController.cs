using System;
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
    private bool keyPress;
    private float distance;

    void Start()
    {
        KeyCode lastInput = KeyCode.None;
        anim = GetComponent<Animator>();
        speed = 0.5f;
        distance = 0;
        audio = GetComponent<AudioSource>();
        currentPos = new Vector3(1f, -1f, -1f);
        AddTween(PacStudent.transform, currentPos, new Vector3(1f, -1f, -1f), speed);
        PacStudent.transform.position = currentPos;
    }

    private void FixedUpdate()
    {
        localPos = PacStudent.transform.localPosition;
        
        if (tween != null)
        {
            float timeFraction = (Time.time - tween.StartTime) / tween.Duration;
            currentPos = Vector3.Lerp(tween.StartPos, tween.EndPos, timeFraction);
            PacStudent.transform.position = currentPos;
        }
    }


    void Update()
    {

        if (tween != null)
        {

            float distance = Vector3.Distance(tween.Target.position, tween.EndPos);
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

           if (distance > 0)
            {
                tween.Target.position = currentPos;
            }
            else
            {
                tween.Target.position = tween.EndPos;
                tween = null;
            }

        }

        if (lastInput == KeyCode.None || tween == null)
        {

            int x = (int)Math.Round(localPos.x); //(int)Math.Round(2.5555);
            int y = (int)Math.Round(localPos.y);
            float z = localPos.z;

            if (keyPress == true)
            {
                lastInput = currentInput;
                Debug.Log("KeyChange - local " + localPos + " : lastkey " + lastInput);

                Debug.Log("int x " + localPos.x + " : int y " + localPos.y);

                Debug.Log("int x " + x + " : int y " + y);

                if (lastInput == KeyCode.A)
                {
                    AddTween(PacStudent.transform, localPos, new Vector3(x-1, y, z), speed);
                }else if (lastInput == KeyCode.D)
                {
                    AddTween(PacStudent.transform, localPos, new Vector3(x+1, y, z), speed);
                } else if (lastInput == KeyCode.W)
                {
                    AddTween(PacStudent.transform, localPos, new Vector3(x, y+1, z), speed);
                } else if (lastInput == KeyCode.S)
                {
                    AddTween(PacStudent.transform, localPos, new Vector3(x, y-1, z), speed);
                }

                keyPress = false;
                currentInput = KeyCode.None;
            } else
            {
                if (lastInput == KeyCode.A)
                {
                    AddTween(PacStudent.transform, localPos, new Vector3(x-1, y, z), speed);
                }
                else if (lastInput == KeyCode.D)
                {
                    AddTween(PacStudent.transform, currentPos, new Vector3(x+1 ,y, z), speed);
                }
                else if (lastInput == KeyCode.W)
                {
                    AddTween(PacStudent.transform, localPos, new Vector3(x, y+1, z), speed);
                }
                else if (lastInput == KeyCode.S)
                {
                    AddTween(PacStudent.transform, localPos, new Vector3(x, y-1, z), speed);
                }
            }

        }


        if (Input.GetKeyDown(KeyCode.A)){
            currentInput = KeyCode.A;
            keyPress = true;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            currentInput = KeyCode.D;
            keyPress = true;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            currentInput = KeyCode.W;
            keyPress = true;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            currentInput = KeyCode.S;
            keyPress = true;
        }


        if (NormPos.x == 0.0f && NormPos.y == 1.0f)  // >0 ?
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
            tween = new Tween(targetObject, startPos, endpos, Time.time, duration);
        } else
        {
           // tween = new Tween(targetObject, localPos, localPos, Time.time, duration);
        }

    }
}
