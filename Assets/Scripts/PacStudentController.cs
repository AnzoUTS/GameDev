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

    void Start()
    {

        anim = GetComponent<Animator>();
        speed = 0.5f;

        audio = GetComponent<AudioSource>();

        currentPos = new Vector3(1f, -1f, 0f);
        AddTween(PacStudent.transform, currentPos, new Vector3(1f, -1f, 0f), speed);
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
                tween = null;
                //path++;

            }

        }

        if (Input.GetKeyDown(KeyCode.A)){
            AddTween(PacStudent.transform, localPos, new Vector3(localPos.x-1, localPos.y, localPos.z), speed);
            Debug.Log("KeyPress  : A " + localPos);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            AddTween(PacStudent.transform, currentPos, new Vector3(localPos.x + 1, localPos.y, localPos.z), speed);
            Debug.Log("KeyPress  : D " + localPos + " : " + (float)localPos.x + 1f);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            AddTween(PacStudent.transform, localPos, new Vector3(localPos.x, localPos.y+1, localPos.z), speed);
            Debug.Log("KeyPress  : W " + localPos);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            AddTween(PacStudent.transform, PacStudent.transform.localPosition, new Vector3(localPos.x, localPos.y-1, localPos.z), speed);
            Debug.Log("KeyPress  : S " + localPos);
        }


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
       // if (tween != null)
        {
            Debug.Log("Add Tween " +targetObject + " : " +  startPos + " : " + endpos );
            tween = new Tween(targetObject, startPos, endpos, Time.time, duration);
        } else
        {
            Debug.Log("Add Tween NULL " + targetObject + " : " + startPos + " : " + endpos);
        }

    }
}
