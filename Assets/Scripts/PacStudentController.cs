using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PacStudentController : MonoBehaviour
{
    private KeyCode currentInput;
    private KeyCode lastInput;
    private Vector3 currentPos;
    private Vector3 NormPos;
    private Vector3 localPos;
    private Vector3 lerpPos;
    private float speed;
    bool isMoving;
    private Tween tween;
    private Animator anim;
    public AudioClip movement_FX;
    public AudioClip pellet_FX;
    public AudioClip wallt_FX;
    private AudioSource audio;
    public List<Vector3> Walkable;
    private GameObject[] gameObjects;
    public ParticleSystem dust;


    void Start()
    {
        speed = 0.5f;
        lastInput = KeyCode.None;
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        currentPos = new Vector3(1f, -1f, 0f);
        transform.position = currentPos;
        gameObjects = GameObject.FindGameObjectsWithTag("Walkable");
        foreach (GameObject item in gameObjects)
        {
            Walkable.Add(item.transform.position);
        }
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














        StartCoroutine(IsMoving());

        if (isMoving)
        {
            if (!audio.isPlaying)
            {
                audio.clip = movement_FX;
                audio.Play();

            }
        }

        if (tween != null)
        {
            float distance = Vector3.Distance(tween.Target.position, tween.EndPos);
            NormPos = (tween.EndPos - tween.StartPos).normalized;
           // Debug.Log("Normalized  :" + NormPos + " || Animations : anim UP :" + anim.GetBool("up") + " anim RIGHT :" + anim.GetBool("right") + " anim DOWN :" + anim.GetBool("down") + " anim LEFT :" + anim.GetBool("left") + " || Distance : tween Target" + tween.Target.position + " tween EndPos" + tween.EndPos);

            if (NormPos.x != 0.0f || NormPos.y != 0.0f)
            {

            }

            if (distance > 0)
            {
                tween.Target.position = currentPos;
            }
            else
            {
                tween = null;

            }

        }

        if (currentInput == KeyCode.None || tween == null)
        {
            Debug.Log("Last Input : " + lastInput + " Current Input : " + currentInput);

            if (Direction(lastInput))
            {
                currentInput = lastInput;
            }

            else
            {
                if (Direction(lastInput))
                {
                }
                else
                {
                    Direction(currentInput);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            lastInput = KeyCode.A;
            if (currentInput == KeyCode.None)
            {
                currentInput = KeyCode.A;
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            lastInput = KeyCode.D;
            if (currentInput == KeyCode.None)
            {
                currentInput = KeyCode.D;
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            lastInput = KeyCode.W;
            if (currentInput == KeyCode.None)
            {
                currentInput = KeyCode.W;
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            lastInput = KeyCode.S;
            if (currentInput == KeyCode.None)
            {
                currentInput = KeyCode.S;
            }
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


}


    public void AddTween(Transform targetObject, Vector3 startPos, Vector3 endpos, float duration)
    {
            if (tween == null)
            {
                tween = new Tween(targetObject, startPos, endpos, Time.time, duration);
            }
    }
    
    public bool Direction(KeyCode key)
    {
        int x = (int)Math.Round(localPos.x);
        int y = (int)Math.Round(localPos.y);
        float z = localPos.z;

        if (key == KeyCode.A)
        {
            lerpPos = new Vector3(x - 1, y, z);
            if (MoveCheck(lerpPos))
            {
                AddTween(transform, localPos, new Vector3(x - 1, y, z), speed);
                return true;
            }
            else
            {
                return false;
            }    
        }
        else if (key == KeyCode.D)
        {
            lerpPos = new Vector3(x + 1, y, z);
            if (MoveCheck(lerpPos))
            {
                AddTween(transform, localPos, new Vector3(x+1, y, z), speed);
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (key == KeyCode.W)
        {
            lerpPos = new Vector3(x, y + 1, z);
            if (MoveCheck(lerpPos))
            {
                AddTween(transform, localPos, new Vector3(x, y+1, z), speed);
            return true;
        }
        else
        {
            return false;
        }
    }
        else if (key == KeyCode.S)
        {
            lerpPos = new Vector3(x, y -1, z);
            if (MoveCheck(lerpPos))
            {
                AddTween(transform, localPos, new Vector3(x, y-1, z), speed);
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (key == KeyCode.None)
        {
            Debug.Log("KeyNone "+ x + ": " + y + ": " + z);
            return false;
        }
        return false;
    }


    public bool MoveCheck(Vector3 lerp)
    {
        if (Walkable.Contains(lerp))
        {
/*            if (transform.parent.gameObject.name == "Pellet")
                Debug.Log("Pellet");*/
            // if pellet
            return true;
        } else
        {
            return false;
        }
    }


    private IEnumerator IsMoving()
    {
        Vector3 startPos = gameObject.transform.position;
        yield return new WaitForSeconds(0.2f);
        Vector3 lastPos = transform.position;
        if (startPos.x != lastPos.x || startPos.y != lastPos.y)
        {
            isMoving = true;
            dust.Play();
   
        } else
        {
            isMoving = false;
            dust.Stop();
            audio.Stop();
        }
    }


    private void OnTriggerEnter(Collider trigger)  // added both OnEnter and OnExit as instructions slightly confusing.
    {

        Debug.Log("Trigger Enter: " + trigger.gameObject.name + " : " + trigger.gameObject.transform.position + " : Parent" + trigger.gameObject.transform.parent.name);

        audio.clip = pellet_FX;
        audio.Play();

        // First attempt
        //GameObject x = GameObject.Find("TriggerBox");
        //Debug.Log("Trigger Exit: " + x.name + " : " + x.transform.position);
    }



}
