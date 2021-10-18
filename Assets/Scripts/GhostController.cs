using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    private int lastTime;
    private float timer = -0.0f;
    private float speed;
    private float movement;
    private float duration;
    private bool isMoving;
    private bool canMove;

    private Vector3 currentPos;
    private Vector3 NormPos;
    private Vector3 localPos;
    private Vector3 lerpPos;
    public List<Vector3> Walkable;

    private Tween tween;
    private Animator anim;
    private GameObject[] gameObjects;
    private BoxCollider boxCollider;
    private GameManagement gameManagment;

    //private int direction;
    private int currentInput;
    private Vector3 lastPosition;

    private string enemyName;

    private Vector3 ghostPos;
    private Vector3 up;
    private Vector3 down;
    private Vector3 left;
    private Vector3 right;
    public List<Vector3> ghostOptions;
    int ghostThought;
    private Vector3 ghostLocation;
    private Vector3 direction;
    private Vector3 previousDirection;
    private Vector3 lastDirection;








    void Start()
    {
        speed = 2.5f;
        boxCollider = GetComponent<BoxCollider>();
        anim = GetComponent<Animator>();
        //currentPos = new Vector3(14f, -14f, 0f);
        //currentPos = new Vector3(5f, -1f, 0f);
        //transform.position = currentPos;
        ghostLocation = transform.position;
        gameObjects = GameObject.FindGameObjectsWithTag("Walkable");
        canMove = true;
        enemyName = transform.name;
        //Debug.Log(enemyName);
        foreach (GameObject item in gameObjects)
        {
            Walkable.Add(item.transform.position);
            //objectCount++;
        }

        NormPos = new Vector3(0, 1, 0);
        tween = null;

    }

    void Update()
    {
        duration = 1 / speed;
        localPos = transform.localPosition;

        if (tween != null)
        {
            float timeFraction = (Time.time - tween.StartTime) / tween.Duration;
            currentPos = Vector3.Lerp(tween.StartPos, tween.EndPos, timeFraction);
            transform.position = currentPos;
        }


        if (currentInput == 0 && enemyName == "OrcC")
        {
          //  currentInput = UnityEngine.Random.Range(1, 5);
        }

         movement = speed * Time.deltaTime;

        timer += Time.deltaTime;

        if ((int)timer > lastTime)
        {
            if (lastTime >= 0)
            {
                if (enemyName == "OrcC")
                {
                   // direction = UnityEngine.Random.Range(1, 5); // range 1-4
                }
                
                
             //   Debug.Log("ghost direction :" +direction);

                // Debug.Log("last time " + lastTime + " movement " + movement + "duration "+ duration);
            }
            lastTime = (int)timer;
        }

        if (tween != null)
        {
           
            float distance = Vector3.Distance(tween.Target.position, tween.EndPos);
            NormPos = (tween.EndPos - tween.StartPos).normalized;
            previousDirection = NormPos;

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


        if  (tween == null)
        {
            // Debug.Log("Last Input : " + lastInput + " Current Input : " + currentInput);

            if (canMove == true)
            {
                GhostBrain();
            }
        }


    }


/*    private void FixedUpdate()
    {
        duration = 1 / speed;
        localPos = transform.localPosition;

        if (tween != null)
        {
            float timeFraction = (Time.time - tween.StartTime) / tween.Duration;
            currentPos = Vector3.Lerp(tween.StartPos, tween.EndPos, timeFraction);
            transform.position = currentPos;
        }
    }*/

    public void Direction(Vector3 newDirection)
    {
/*        int x = (int)Math.Round(localPos.x);
        int y = (int)Math.Round(localPos.y);
        float z = localPos.z;*/


        int x = (int)Math.Round(lastPosition.x);
        int y = (int)Math.Round(lastPosition.y);
        float z = lastPosition.z;


        //Debug.Log(" new direction " + newDirection);


        if (newDirection == new Vector3(x-1, y + 1, z))
        {

                anim.SetBool("left", true);
                anim.SetBool("down", false);
                anim.SetBool("up", false);
                anim.SetBool("right", false);
        }
        else if (newDirection == new Vector3(x+1, y, z))
        {
                anim.SetBool("right", true);
                anim.SetBool("down", false);
                anim.SetBool("left", false);
                anim.SetBool("up", false);
        }
        else if (newDirection == new Vector3(x, y + 1, z))
    {
                anim.SetBool("up", true);
                anim.SetBool("down", false);
                anim.SetBool("left", false);
                anim.SetBool("right", false);
        }
        else if(newDirection == new Vector3(x, y - 1, z))
    {
                anim.SetBool("down", true);
                anim.SetBool("up", false);
                anim.SetBool("left", false);
                anim.SetBool("right", false);
        }
        else
        {
            Debug.Log("Ghost is confused : direction provided" + newDirection);
        }


    //AddTween(transform, localPos, newDirection, duration);


    }


    //private Vector3 GhostBrain()
    private void GhostBrain()
    {

        ghostOptions.Clear();


        // avoid rounding point errors
        int x = (int)Math.Round(localPos.x);
        int y = (int)Math.Round(localPos.y);
        float z = localPos.z;

        //ghostOptions.Clear();

        up = new Vector3(x, y + 1, z);
        down = new Vector3(x, y - 1, z);
        left = new Vector3(x - 1, y, z);
        right = new Vector3(x + 1, y, z);

        // Debug.Log("last Position " +lastPosition);

        // Debug.Log("up " + up + " down" + down + " left " + left + "  right " + right);

/*        if (NormPos.y == 0 && NormPos.x == 0)
        {
            NormPos = previousDirection;
        }*/


        if (lastDirection.y == 0 && lastDirection.x == 0)
        {
            lastDirection = new Vector3(0,1,0);
        }



        /*        if (NormPos.y == 1 && NormPos.x == 0) // up
                {

                    if (Walkable.Contains(up)) // up
                        ghostOptions.Add(up);

                    if (Walkable.Contains(left)) // left
                        ghostOptions.Add(left);

                    if (Walkable.Contains(right)) // right
                        ghostOptions.Add(right);
                }




                else if (NormPos.y == -1 && NormPos.x == 0) // down
                {

                    if (Walkable.Contains(down)) // down
                        ghostOptions.Add(down);

                    if (Walkable.Contains(left)) // left
                        ghostOptions.Add(left);

                    if (Walkable.Contains(right)) // right
                        ghostOptions.Add(right);
                }


                else if (NormPos.x == -1 && NormPos.y == 0) // left
                {

                    if (Walkable.Contains(up)) // up
                        ghostOptions.Add(up);

                    if (Walkable.Contains(down)) // down
                        ghostOptions.Add(down);

                    if (Walkable.Contains(left)) // left
                        ghostOptions.Add(left);



                else if (NormPos.x == 1 && NormPos.y == 0) // right
                {

                    if (Walkable.Contains(down)) // down
                        ghostOptions.Add(down);

                    if (Walkable.Contains(right)) // right
                        ghostOptions.Add(right);

                    if (Walkable.Contains(up)) // up
                        ghostOptions.Add(up);

                }*/
        if (lastDirection.y == 1 && lastDirection.x == 0) // up
        {

            if (Walkable.Contains(up)) // up
                ghostOptions.Add(up);

            if (Walkable.Contains(left)) // left
                ghostOptions.Add(left);

            if (Walkable.Contains(right)) // right
                ghostOptions.Add(right);

            moveGhost();
        }




        else if (lastDirection.y == -1 && lastDirection.x == 0) // down
        {

            if (Walkable.Contains(down)) // down
                ghostOptions.Add(down);

            if (Walkable.Contains(left)) // left
                ghostOptions.Add(left);

            if (Walkable.Contains(right)) // right
                ghostOptions.Add(right);

            moveGhost();
        }


        else if (lastDirection.x == -1 && lastDirection.y == 0) // left
        {

            if (Walkable.Contains(up)) // up
                ghostOptions.Add(up);

            if (Walkable.Contains(down)) // down
                ghostOptions.Add(down);

            if (Walkable.Contains(left)) // left
                ghostOptions.Add(left);

            moveGhost();

        }

        else if (lastDirection.x == 1 && lastDirection.y == 0) // right
        {

            if (Walkable.Contains(down)) // down
                ghostOptions.Add(down);

            if (Walkable.Contains(right)) // right
                ghostOptions.Add(right);

            if (Walkable.Contains(up)) // up
                ghostOptions.Add(up);

            moveGhost();

        }


        else
        {
            Debug.Log("confused!!!!!!!!!!!!!!!!!!!!" + previousDirection);

            Debug.Log("Transform " + transform + "localPos " + localPos + "direction " + direction + "(direction-lastDirection)" + (direction - lastDirection) + "(direction + lastDirection) " + (direction + lastDirection));

//            AddTween(transform, localPos, direction, duration);

            AddTween(transform, new Vector3(x, y, z), (direction + lastDirection), duration);
            /*
                        if (lastDirection.y == 1 && lastDirection.x == 0) // up
                        {

                            if (Walkable.Contains(up)) // up
                                ghostOptions.Add(up);

                            if (Walkable.Contains(left)) // left
                                ghostOptions.Add(left);

                            if (Walkable.Contains(right)) // right
                                ghostOptions.Add(right);

                            moveGhost();
                        }




                        else if (lastDirection.y == -1 && lastDirection.x == 0) // down
                        {

                            if (Walkable.Contains(down)) // down
                                ghostOptions.Add(down);

                            if (Walkable.Contains(left)) // left
                                ghostOptions.Add(left);

                            if (Walkable.Contains(right)) // right
                                ghostOptions.Add(right);
                            //if (Walkable.Contains(down) && lastPosition != down) // down
                            //    ghostOptions.Add(down);

                            moveGhost();
                        }


                        else if (lastDirection.x == -1 && lastDirection.y == 0) // left
                        {

                            if (Walkable.Contains(up)) // up
                                ghostOptions.Add(up);

                            if (Walkable.Contains(down)) // down
                                ghostOptions.Add(down);

                            if (Walkable.Contains(left)) // left
                                ghostOptions.Add(left);
                            *//*
                                                if (Walkable.Contains(right)) // right
                                                    ghostOptions.Add(right);
                            *//*


                            //if (Walkable.Contains(down) && lastPosition != down) // down
                            //    ghostOptions.Add(down);
                            moveGhost();
                        }



                        else if (lastDirection.x == 1 && lastDirection.y == 0) // right
                        {

                            if (Walkable.Contains(down)) // down
                                ghostOptions.Add(down);
                            *//*
                                                if (Walkable.Contains(left)) // left
                                                    ghostOptions.Add(left);*//*

                            if (Walkable.Contains(right)) // right
                                ghostOptions.Add(right);

                            if (Walkable.Contains(up)) // up
                                ghostOptions.Add(up);
                            moveGhost();
            */


            //}
        }







    }


    public void AddTween(Transform targetObject, Vector3 startPos, Vector3 endpos, float duration)
    {
        if (tween == null && canMove == true)
        {
            tween = new Tween(targetObject, startPos, endpos, Time.time, duration);
        }
    }



    private void moveGhost()
    {

        int x = (int)Math.Round(localPos.x);
        int y = (int)Math.Round(localPos.y);
        float z = localPos.z;

        ghostThought = UnityEngine.Random.Range(0, ghostOptions.Count);
        Debug.Log("Normal pos " + NormPos + "Valid options " + ghostOptions.Count + " lastPosition" + lastPosition + " localpos" + localPos + " ghostThought " + ghostThought);

        foreach (Vector3 option in ghostOptions)
        {
            Debug.Log("ghostArrayOptions " + option);

        }

        direction = ghostOptions[ghostThought];

        lastDirection = direction - new Vector3(x, y, z);
        Debug.Log("last direction : " + lastDirection);

        //previousDirection = NormPos;
        // Debug.Log("LASTpos " + lastPosition + "DIRECTION" + direction);

        //ghostOptions.Clear();

        AddTween(transform, new Vector3(x, y, z), direction, duration);

        //ghostOptions.Clear();
        //return direction;
    }


}
