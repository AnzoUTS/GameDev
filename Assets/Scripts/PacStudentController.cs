using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PacStudentController : MonoBehaviour
{
    KeyCode currentInput;
    KeyCode lastInput;
    private bool keyInput;
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
    Vector3 lerpPos;

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

        if (currentInput == KeyCode.None || tween == null)
        {
            int x = (int)Math.Round(localPos.x);
            int y = (int)Math.Round(localPos.y);


            /*       if (currentInput == KeyCode.None)
                   {
                       currentInput = lastInput;
                   }
        */


            Debug.Log("Last Input : " + lastInput + " Current Input : " + currentInput + " iswalkable :" + iswalkable + " : KeyInput : " + keyInput);

            if (Direction(lastInput))
            {
                Direction(lastInput);
                currentInput = lastInput;
            }
/*            else if (iswalkable == true && keyInput == true)
            {
                Direction(lastInput);
                currentInput = lastInput;
                keyInput = false;
            }*/
            else
            {

                if (Direction(lastInput))
                {
                    Direction(lastInput);

                }else
                {
                    //Direction(lastInput);
                    Direction(currentInput);
                    keyInput = false;
                }



            }

          






        }


        if (Input.GetKeyDown(KeyCode.A)){

            keyInput = true;
            lastInput = KeyCode.A;

            if (currentInput == KeyCode.None)
            {
                currentInput = KeyCode.A;
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            keyInput = true;
            lastInput = KeyCode.D;
            if (currentInput == KeyCode.None)
            {
                currentInput = KeyCode.D;
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            keyInput = true;
            lastInput = KeyCode.W;
            if (currentInput == KeyCode.None)
            {
                currentInput = KeyCode.W;
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            keyInput = true;
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
        validMove();

        IEnumerator validMove()
        {
            while (Walkable.Contains(endpos))
            {
                iswalkable = true;
            }
            yield return null;
        }


        if (Walkable.Contains(endpos))
        {
            if (tween == null)
            {
                tween = new Tween(targetObject, startPos, endpos, Time.time, duration);
            }
            //iswalkable = true;
            iswalkable = false;
        }
        else
        {
            iswalkable = true;
            //iswalkable = false;
            //Direction(currentInput);
            //currentInput = lastInput;
        }


/*        if (tween == null)
            {
                tween = new Tween(targetObject, startPos, endpos, Time.time, duration);
            }
*/
        


    }
    



    public bool Direction(KeyCode key)
    {
        int x = (int)Math.Round(localPos.x);
        int y = (int)Math.Round(localPos.y);
        float z = localPos.z;


        Debug.Log("Current Input : " + currentInput + " Last Input : " + lastInput);

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
            Debug.Log("Lerp True");
            return true;
        } else
        {
            Debug.Log("Lerp False");
            return false;
        }
    }


}
