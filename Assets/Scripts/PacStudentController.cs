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
    public AudioClip wall_FX;
    public AudioClip die_FX;
    private AudioSource pacaudio;
    private Animator OrcA;
    private Animator OrcB;
    private Animator OrcC;
    private Animator OrcD;
    private AudioSource music;
    public List<Vector3> Walkable;
    private GameObject[] gameObjects;
    public ParticleSystem dust;
    public ParticleSystem wallHit;
    private bool teleportL;
    private bool teleportR;
    private BoxCollider boxCollider;
    private int hitDirection;


    void Start()
    {
        teleportL = false;
        teleportR = false;
        boxCollider = GetComponent<BoxCollider>();
        hitDirection = 5;
        speed = 0.5f;
        lastInput = KeyCode.None;
        anim = GetComponent<Animator>();
        pacaudio = GetComponent<AudioSource>();
        OrcA = GameObject.Find("OrcA").GetComponent<Animator>();
        OrcB = GameObject.Find("OrcB").GetComponent<Animator>();
        OrcC = GameObject.Find("OrcC").GetComponent<Animator>();
        OrcD = GameObject.Find("OrcD").GetComponent<Animator>();
        currentPos = new Vector3(12f, -11f, 0f);
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
            if (!pacaudio.isPlaying)
            {
                pacaudio.clip = movement_FX;
                pacaudio.Play();

            }
        }

        if (tween != null)
        {
            float distance = Vector3.Distance(tween.Target.position, tween.EndPos);
            NormPos = (tween.EndPos - tween.StartPos).normalized;
          //  Debug.Log("Normalized  :" + NormPos + " || Animations : anim UP :" + anim.GetBool("up") + " anim RIGHT :" + anim.GetBool("right") + " anim DOWN :" + anim.GetBool("down") + " anim LEFT :" + anim.GetBool("left") + " || Distance : tween Target" + tween.Target.position + " tween EndPos" + tween.EndPos);

/*            if (NormPos.x != 0.0f || NormPos.y != 0.0f) // what is this for????
            {

            }*/

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
            // Debug.Log("Last Input : " + lastInput + " Current Input : " + currentInput);

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

/*        if (NormPos.x == 0.0f && NormPos.y == 1.0f)  // >0 ?
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
        }*/
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
        if (teleportL == true && NormPos.x == -1)
        {
            teleportL = false;
            AddTween(transform, new Vector3(27f, -14, 0), new Vector3(26f, -14, 0), speed);
            return true;
        }

        else if (teleportR == true && NormPos.x == +1)
        {
            teleportR = false;
            AddTween(transform, new Vector3(0, -14, 0), new Vector3(1f, -14, 0), speed);
            return true;
        }
        else
        {
            teleportL = false;
            teleportR = false;

            int x = (int)Math.Round(localPos.x);
            int y = (int)Math.Round(localPos.y);
            float z = localPos.z;

            if (key == KeyCode.A)
            {
                lerpPos = new Vector3(x - 1, y, z);
                if (MoveCheck(lerpPos))
                { 
                    anim.SetBool("left", true);
                    anim.SetBool("down", false);
                    anim.SetBool("up", false);
                    anim.SetBool("right", false);
                    hitDirection = 1;
                    boxCollider.center = new Vector3(-0.1f, 0, 0); // adjust box
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
                    anim.SetBool("right", true);
                    anim.SetBool("down", false);
                    anim.SetBool("left", false);
                    anim.SetBool("up", false);
                    hitDirection = 2;
                    boxCollider.center = new Vector3(0.1f, 0, 0); // adjust box
                    AddTween(transform, localPos, new Vector3(x + 1, y, z), speed);
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
                    anim.SetBool("up", true);
                    anim.SetBool("down", false);
                    anim.SetBool("left", false);
                    anim.SetBool("right", false);
                    hitDirection = 3;
                    boxCollider.center = new Vector3(0, 0.1f, 0); // adjust box
                    AddTween(transform, localPos, new Vector3(x, y + 1, z), speed);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (key == KeyCode.S)
            {
                lerpPos = new Vector3(x, y - 1, z);
                if (MoveCheck(lerpPos))
                {
                    anim.SetBool("down", true);
                    anim.SetBool("up", false);
                    anim.SetBool("left", false);
                    anim.SetBool("right", false);

                    hitDirection = 4;
                    boxCollider.center = new Vector3(0, -0.1f, 0); // adjust box
                    AddTween(transform, localPos, new Vector3(x, y - 1, z), speed);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (key == KeyCode.None)
            {
                return false;
            }


        }

        return false;
    }


    public bool MoveCheck(Vector3 lerp)
    {
        if (Walkable.Contains(lerp))
        {
            /*          if (transform.parent.gameObject.name == "Pellet")
                            Debug.Log("Pellet");*/
            // if pellet
            return true;
        }
        else
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
        }
        else
        {
            isMoving = false;
            dust.Stop();
        }
    }


    private void OnTriggerEnter(Collider trigger)
    {

        if (trigger.name == "TeleportL")
        {
            teleportL = true;
        }

        if (trigger.name == "TeleportR")
        {
            teleportR = true;
        }

        // Debug.Log("Trigger Enter: " + trigger.gameObject.name + " : " + trigger.gameObject.transform.position + " : Parent" + trigger.gameObject.transform.parent.name);

        if (trigger.gameObject.name.Contains("Pellet"))
        {
            Destroy(trigger.gameObject);
            GameManagement.Score += 10;
            pacaudio.clip = pellet_FX;
            pacaudio.Play();
        }

        if (trigger.gameObject.name.Contains("CherryBurger"))
        {
            Destroy(trigger.gameObject);
            GameManagement.Score += 100;
        }

        if (trigger.gameObject.CompareTag("Walls"))
        {
            pacaudio.clip = wall_FX;
            pacaudio.Play();
            WallHit();
        }

        if (trigger.gameObject.name.Contains("PowerFlash"))
        {
            Destroy(trigger.gameObject);
            AudioController.Music = true;
            GameManagement.ScaredTime = 10f;
            StartCoroutine(ScareGhosts());
        }


        if (trigger.gameObject.CompareTag("Enemy"))
        {
            pacaudio.clip = die_FX;
            pacaudio.Play();
            anim.SetBool("isDead", true);
            anim.SetBool("down", false);
            anim.SetBool("up", false);
            anim.SetBool("left", false);
            anim.SetBool("right", false);
            Debug.Log("DIE");
           // anim.SetBool("isDead", false);
            currentInput = KeyCode.None;
            lastInput = KeyCode.None;



        }


    }

    IEnumerator ScareGhosts()
    {
        GameManagement.Scared = true;
        OrcA.SetBool("isScared", true);
        OrcB.SetBool("isScared", true);
        OrcC.SetBool("isScared", true);
        OrcD.SetBool("isScared", true);
        yield return new WaitForSeconds(7f);
        OrcA.SetBool("isScared", false);
        OrcB.SetBool("isScared", false);
        OrcC.SetBool("isScared", false);
        OrcD.SetBool("isScared", false);
    }

    private void WallHit()
    {
        switch (hitDirection)
        {
            case 1:
                {
                    wallHit.transform.position = new Vector3(localPos.x - 0.5f, localPos.y, 0f);
                    wallHit.Play();
                    break;
                }
            case 2:
                {
                    wallHit.transform.position = new Vector3(localPos.x + 0.5f, localPos.y, 0f);
                    wallHit.Play();
                    break;
                }
            case 3:
                {
                    wallHit.transform.position = new Vector3(localPos.x, localPos.y + 0.5f, 0f);
                    wallHit.Play();
                    break;
                }
            case 4:
                {
                    wallHit.transform.position = new Vector3(localPos.x, localPos.y - 0.5f, 0f);
                    wallHit.Play();
                    break;
                }
            case 5:
                {
                    wallHit.Stop();
                    break;
                }
        }
    }
}