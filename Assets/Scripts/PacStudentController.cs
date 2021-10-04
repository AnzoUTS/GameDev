using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PacStudentController : MonoBehaviour
{
    KeyCode currentInput;
    KeyCode lastInput;
    //public GameObject PacStudent;
    private Vector3 lerpPos;
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
    List<GameObject> walkableArea;
    public List<Vector3> Walkable;
    [SerializeField]
   // private List<Vector3> isWalkable2;
   // private List<Vector3> isWalkable3;
    GameObject[] gameObjects;
    private GameObject gameManagement;
    private GameManagement gameScript;
    bool iswalkable;

    void Start()
    {
        KeyCode lastInput = KeyCode.None;
        anim = GetComponent<Animator>();
        speed = 0.5f;
        distance = 0;
        audio = GetComponent<AudioSource>();
        currentPos = new Vector3(1f, -1f, 0f);
        AddTween(transform, currentPos, new Vector3(1f, -1f, 0f), speed);
        transform.position = currentPos;

        List<Vector3> isWalkable = new List<Vector3>();

       // gameManagement = GameObject.Find("GameManagement");
       // gameScript = gameManagement.GetComponent<GameManagement>();
        //isWalkable3 = gameScript.isWalkable3;
       // Debug.Log("isWalkable #3: START" + isWalkable3.Count);


        //List<Vector3> isWalkable3 = new 
        List<GameObject> walkableArea = new List<GameObject>();

        gameObjects = GameObject.FindGameObjectsWithTag("Item");

        foreach (GameObject item in gameObjects)
        {
            walkableArea.Add(item);
        }

        Debug.Log("Area :" + walkableArea.Count);


        foreach (GameObject item in gameObjects)
        {
            Walkable.Add(item.transform.position);
        }






        Debug.Log("isWalkable2 Start:" + isWalkable.Count);
        //Debug.Log("isWalkable #3: start" + isWalkable3.Count);
    }

    private void FixedUpdate()
    {








        //WallCheck();
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

      //  Debug.Log("isWalkable2 Update:" + Walkable.Count);
      //  Debug.Log("isWalkable #3: update" + gameScript.isWalkable3.Count);

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
                //tween.Target.position = tween.EndPos;
                tween = null;
            }

        }

        if (currentInput == KeyCode.None || tween == null || distance == 0 )
        {
            int x = (int)Math.Round(localPos.x);
            int y = (int)Math.Round(localPos.y);


            Debug.Log("Last Input : " + lastInput + " Current Input : " + currentInput);

                if (iswalkable == true)
                {
                    Debug.Log("int x " + localPos.x + " : int y " + localPos.y);
                    Direction(lastInput);
                    currentInput = lastInput;
                } else
                {

                Direction(lastInput);

                }



                //else if (WallCheck() == true && lastInput != currentInput)
/*                else if (lastInput != currentInput)
                    {
                    Direction(lastInput);
                    currentInput = lastInput;
                    Debug.Log("----------------------Input change-------------------------");
                    Debug.Log("Distance: " + distance + " local pos :" + localPos + " EndPos: " + tween.EndPos);
                    Debug.Log(" Last Input : " + lastInput + " Current Input : " + currentInput);
                } else
                {
                //Direction(currentInput);
                Debug.Log("----------------------No Movement-------------------------");
                //Direction(KeyCode.None);
                }*/


            



        }


        if (Input.GetKeyDown(KeyCode.A)){
            lastInput = KeyCode.A;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            lastInput = KeyCode.D;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            lastInput = KeyCode.W;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            lastInput = KeyCode.S;
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


            if (Walkable.Contains(endpos))
            {
                iswalkable = true;
                
                if (tween == null)
                    {
                    tween = new Tween(targetObject, startPos, endpos, Time.time, duration);
                    }

            } else
        {
            iswalkable = false;
        }

    }
    



    public void Direction(KeyCode key)
    {
        int x = (int)Math.Round(localPos.x);
        int y = (int)Math.Round(localPos.y);
        float z = localPos.z;


        Debug.Log("Current Input : " + currentInput + " Last Input : " + lastInput);

        if (key == KeyCode.A)
        {       
                AddTween(transform, localPos, new Vector3(x-1, y, z), speed);
        }
        else if (key == KeyCode.D)
        {
                AddTween(transform, localPos, new Vector3(x+1, y, z), speed);
        }

        else if (key == KeyCode.W)
        {
                AddTween(transform, localPos, new Vector3(x, y+1, z), speed);
        }
        else if (key == KeyCode.S)
        {
                AddTween(transform, localPos, new Vector3(x, y-1, z), speed);
        }

        else if (key == KeyCode.None)
        {
            Debug.Log("wall hit "+ x + ":" + y + ":" + z);
        }




    }


}
