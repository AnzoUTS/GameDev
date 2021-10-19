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
    private bool ghostArea;
    private Vector3 currentPos;
    private Vector3 NormPos;
    private Vector3 localPos;
    public List<Vector3> Walkable;
    public List<Vector3> GhostArea;
    public List<Vector3> GhostAreaExitA;
    private Tween tween;
    private Animator anim;
    private GameObject[] gameObjects;
    private BoxCollider boxCollider;
    private GameManagement gameManagment;
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
    private Vector3 startingPos;
    private Vector3 pacPosition;
    public  bool isAlive;
    private bool isScared;
    private bool isRecovery;
    private float targetDistance;
    PacStudentController pacman;



/*    public static bool IsAlive
    {
        set { isAlive = value; }
        get { return isAlive; }
    }
*/


    void Start()
    {

        pacman = gameObject.GetComponent<PacStudentController>(); // access pacman script
        speed = 1.7f;
        boxCollider = GetComponent<BoxCollider>();
        anim = GetComponent<Animator>();
        ghostLocation = transform.position;
        gameObjects = GameObject.FindGameObjectsWithTag("Walkable");
        canMove = true;
        enemyName = transform.name;
        //Debug.Log(enemyName);
        foreach (GameObject item in gameObjects)
        {
            Walkable.Add(item.transform.position);
            if (item.name.Contains("GhostArea"))
                GhostArea.Add(item.transform.position);
            if (item.name.Contains("ExitA"))
                GhostAreaExitA.Add(item.transform.position);
        }

        Debug.Log("ghost options at start "+ ghostOptions.Count);

        if (enemyName == "OrcA")
        {
            startingPos = new Vector3(12f, -14f, 0f);

        }

        if (enemyName == "OrcB")
        {
            startingPos = new Vector3(13f, -14f, 0f);

        }

        if (enemyName == "OrcC")
        {
            startingPos = new Vector3(14f, -14f, 0f);
           
        }

        isAlive = true;

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



         movement = speed * Time.deltaTime;

        timer += Time.deltaTime;

        if ((int)timer > lastTime)
        {
            if (lastTime >= 0)
            {
        
            }
            lastTime = (int)timer;
        }

        if (tween != null)
        {
           
            float distance = Vector3.Distance(tween.Target.position, tween.EndPos);
            NormPos = (tween.EndPos - tween.StartPos).normalized;
            previousDirection = NormPos;

          //  Debug.Log("Normalized  :" + NormPos + " || Animations : anim UP :" + anim.GetBool("up") + " anim RIGHT :" + anim.GetBool("right") + " anim DOWN :" + anim.GetBool("down") + " anim LEFT :" + anim.GetBool("left") + " || Distance : tween Target" + tween.Target.position + " tween EndPos" + tween.EndPos);

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
            if (canMove == true && isAlive)
            {
                if (localPos == startingPos)
                {
/*                    ghostOptions.Clear();
                    ghostArea = true;
                    tween = null;
                    Debug.Log("localPos triggered");*/
                }


                GhostBrain();
            }

            else if (!isAlive)
            {
                
                AddTween(transform, localPos, startingPos, 10); // send to ghost area


            }
        }


        if (GameManagement.Scared)
        {
            isScared = true;
        }

        if (GameManagement.Recovery)
        {
            isScared = false;
            isRecovery = true;
        }
        if (!GameManagement.Scared && !GameManagement.Recovery)
        {
            isScared = false;
            isRecovery = false;
        }




        if (isAlive)

        //if (!GameManagement.Scared && isAlive)
        {
            if (NormPos == new Vector3(-1, 0, 0))
            {
                anim.SetBool("left", true);
                anim.SetBool("down", false);
                anim.SetBool("up", false);
                anim.SetBool("right", false);
                anim.SetBool("isDead", false);
            }
            if (NormPos == new Vector3(1, 0, 0))
            {
                anim.SetBool("right", true);
                anim.SetBool("down", false);
                anim.SetBool("left", false);
                anim.SetBool("up", false);
                anim.SetBool("isDead", false);
            }
            if (NormPos == new Vector3(0, 1, 0))
            {
                anim.SetBool("up", true);
                anim.SetBool("down", false);
                anim.SetBool("left", false);
                anim.SetBool("right", false);
                anim.SetBool("isDead", false);
            }
            if (NormPos == new Vector3(0, -1, 0))
            {
                anim.SetBool("down", true);
                anim.SetBool("up", false);
                anim.SetBool("left", false);
                anim.SetBool("right", false);
                anim.SetBool("isDead", false);
            }

            if (isScared)
            {
                anim.SetBool("up", false);
                anim.SetBool("down", false);
                anim.SetBool("left", false);
                anim.SetBool("right", false);
                anim.SetBool("isScared", true);
                anim.SetBool("isDead", false);
                // Invoke("Recovery", 7f);
            }
            if (isRecovery)
            {
                anim.SetBool("up", false);
                anim.SetBool("down", false);
                anim.SetBool("left", false);
                anim.SetBool("right", false);
                anim.SetBool("isScared", false);
                anim.SetBool("isRecovery", true);
                anim.SetBool("isDead", false);
                // Invoke("Recovery", 7f);
            }
        }
        else if (!isAlive)
        {
            anim.SetBool("down", false);
            anim.SetBool("up", false);
            anim.SetBool("left", false);
            anim.SetBool("right", false);
            anim.SetBool("isScared", false);
            anim.SetBool("isRecovery", false);
            anim.SetBool("isDead", true);
        }
        else
        {

            Debug.Log("unkown State!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!" + GameManagement.Recovery + " " + GameManagement.Scared + " " + isAlive);

            /*            anim.SetBool("up", true);
                        anim.SetBool("down", false);
                        anim.SetBool("up", false);
                        anim.SetBool("left", false);
                        anim.SetBool("right", false);
                        anim.SetBool("isScared", false);
                        anim.SetBool("isRecovery", false);
                        anim.SetBool("isDead", false);*/
        }


       // Debug.Log(enemyName + " isAlive " + isAlive);

    }



    private void GhostBrain()
    {
        ghostOptions.Clear();
        // avoid rounding point errors
        int x = (int)Math.Round(localPos.x);
        int y = (int)Math.Round(localPos.y);
        float z = localPos.z;

        up = new Vector3(x, y + 1, z);
        down = new Vector3(x, y - 1, z);
        left = new Vector3(x - 1, y, z);
        right = new Vector3(x + 1, y, z);

        if (lastDirection.y == 0 && lastDirection.x == 0)
        {
            lastDirection = new Vector3(0,1,0);
        }


        if (isAlive == false)
        {
            Debug.Log("isalive False");
            AddTween(transform, new Vector3(x, y, z), startingPos, 10); // send to ghost area

        }


        if (ghostArea == false)
        {

            if (lastDirection.y == 1 && lastDirection.x == 0) // up
            {
                if (Walkable.Contains(up) && !GhostArea.Contains(up)) // up
                    ghostOptions.Add(up);

                if (Walkable.Contains(left) && !GhostArea.Contains(left)) // left
                    ghostOptions.Add(left);

                if (Walkable.Contains(right) && !GhostArea.Contains(right)) // right
                    ghostOptions.Add(right);

                moveGhost();
            }


            else if (lastDirection.y == -1 && lastDirection.x == 0) // down
            {
                if (Walkable.Contains(down) && !GhostArea.Contains(down)) // down
                    ghostOptions.Add(down);

                if (Walkable.Contains(left) && !GhostArea.Contains(left)) // left
                    ghostOptions.Add(left);

                if (Walkable.Contains(right) && !GhostArea.Contains(right)) // right
                    ghostOptions.Add(right);

                moveGhost();
            }


            else if (lastDirection.x == -1 && lastDirection.y == 0) // left
            {
                if (Walkable.Contains(up) && !GhostArea.Contains(up)) // up
                    ghostOptions.Add(up);

                if (Walkable.Contains(down) && !GhostArea.Contains(down)) // down
                    ghostOptions.Add(down);

                if (Walkable.Contains(left) && !GhostArea.Contains(left)) // left
                    ghostOptions.Add(left);

                moveGhost();
            }

            else if (lastDirection.x == 1 && lastDirection.y == 0) // right
            {
                if (Walkable.Contains(down) && !GhostArea.Contains(down)) // down
                    ghostOptions.Add(down);

                if (Walkable.Contains(right) && !GhostArea.Contains(right)) // right
                    ghostOptions.Add(right);

                if (Walkable.Contains(up) && !GhostArea.Contains(up)) // up
                    ghostOptions.Add(up);

                moveGhost();
            }

        }
        if (ghostArea == true)
        {

            if (GhostAreaExitA.Contains(down)) // down
                ghostOptions.Add(down);

            if (GhostAreaExitA.Contains(right)) // right
                ghostOptions.Add(right);

            if (GhostAreaExitA.Contains(left)) // left
                ghostOptions.Add(left);

            if (GhostAreaExitA.Contains(up)) // up
                ghostOptions.Add(up);

           // Debug.Log("ghost area!!!!!!!!!!");

            moveGhost();
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

        if (ghostOptions.Count == 0)
            //if (ghostOptions.Count == 0 && ghostArea == true)
            {
         //   Debug.Log("Ghost has no other option - Ghost area True");

            if (Walkable.Contains(up)) // up
                ghostOptions.Add(up);

            if (Walkable.Contains(down)) // down
                ghostOptions.Add(down);

            if (Walkable.Contains(right)) // right
                ghostOptions.Add(right);

            if (Walkable.Contains(left)) // left
                ghostOptions.Add(left);

            if (Walkable.Contains(up)) // up
                ghostOptions.Add(up);
        }

/*        if (ghostOptions.Count == 0 && ghostArea == false)
        {
            Debug.Log("Ghost has no other option - Ghost Area False");

            if (Walkable.Contains(up) && !GhostArea.Contains(up)) // up
                ghostOptions.Add(up);

            if (Walkable.Contains(left) && !GhostArea.Contains(left)) // left
                ghostOptions.Add(left);

            if (Walkable.Contains(right) && !GhostArea.Contains(right)) // right
                ghostOptions.Add(right);

            if (Walkable.Contains(down) && !GhostArea.Contains(down)) // down
                ghostOptions.Add(down);
        }*/


        if (enemyName == "OrcC" && ghostArea == false)
        {

            ghostThought = UnityEngine.Random.Range(0, ghostOptions.Count);
            //Debug.Log("Normal pos " + NormPos + "Valid options " + ghostOptions.Count + " lastPosition" + lastPosition + " localpos" + localPos + " ghostThought " + ghostThought);

            foreach (Vector3 option in ghostOptions)
            {
     //           Debug.Log("Ghost Randomised Options " + option);
            }

            direction = ghostOptions[ghostThought];

        }

        if (enemyName == "OrcB" || ghostArea == true)
        //  if (enemyName == "OrcB" || (enemyName == "OrcC" && ghostArea == true))
        {


           // Debug.Log("Normal pos " + NormPos + "Valid options " + ghostOptions.Count + " lastPosition" + lastPosition + " localpos" + localPos);

            foreach (Vector3 option in ghostOptions)
            {
                /*                Debug.Log("Ghost Defence Options " + option);
                                pacPosition = PacStudentController.PacPosition;
                                float pacDistance = Vector3.Distance(option, pacPosition);
                                Debug.Log("Ghost option distance " + pacDistance);
                                if (ghostOptions.Count == 1)
                                {
                                    Debug.Log("direction option 1" + option);
                                    direction = option;
                                    targetDistance = pacDistance;
                                }
                                else if (targetDistance > pacDistance)
                                {
                                        direction = option;
                                        Debug.Log("direction option multi" + option);
                                } else
                                {
                                    direction = ghostOptions[0];
                                    Debug.Log("direction option last direction" + direction);
                                }*/




  //              Debug.Log("Ghost Defence Options " + option);
                pacPosition = PacStudentController.PacPosition;
                float pacDistance = Vector3.Distance(option, pacPosition);
        //        Debug.Log("Ghost option distance " + pacDistance);
                if (ghostOptions.Count == 1)
                {
       //             Debug.Log("direction option 1" + option);
                    direction = option;
                    targetDistance = pacDistance;
                }
                else if (targetDistance > pacDistance)
                {
                    direction = option;
   //                 Debug.Log("direction option multi" + option);
                }
                else
                {
                    direction = ghostOptions[0];
      //              Debug.Log("direction option last direction" + direction);
                }



            }

      //      Debug.Log("ORC B ghost direction decision" + direction);

        }

        if (enemyName == "OrcA" || (GameManagement.Scared || GameManagement.Recovery))
        {

     
                //Debug.Log("Normal pos " + NormPos + "Valid options " + ghostOptions.Count + " lastPosition" + lastPosition + " localpos" + localPos + " ghostThought " + ghostThought);

                foreach (Vector3 option in ghostOptions)
                {
          //          Debug.Log("Ghost Defence Options " + option);
                    pacPosition = PacStudentController.PacPosition;
                    float pacDistance = Vector3.Distance(option, pacPosition);
           //         Debug.Log("Ghost option distance " + pacDistance);
                /*                if (ghostOptions.Count == 1)
                                {
                                    direction = option;
                                    targetDistance = pacDistance;
                                }
                                else
                                {
                                    if (pacDistance > targetDistance)
                                    {
                                        direction = option;
                                    }
                                }*/

                    if (ghostOptions.Count == 1)
                    {
            //            Debug.Log("direction option 1" + option);
                        direction = option;
                        targetDistance = pacDistance;
                    }
                    else if (targetDistance < pacDistance)
                    {
                        direction = option;
                //        Debug.Log("direction option multi" + option);
                    }
                    else
                    {
                        direction = ghostOptions[0];
              //          Debug.Log("direction option last direction" + direction);
                    }
                }

        //    Debug.Log("ORC A ghost direction decision" + direction);
        }

            lastDirection = direction - new Vector3(x, y, z);
          //  Debug.Log("ghost area " + ghostArea + "DECISION" + direction + "Last direction" + lastDirection + "Valid options " + ghostOptions.Count + " lastPosition" + lastPosition + " localpos" + localPos + " ghostThought " + ghostThought + " new Vector3(x, y, z)" + new Vector3(x, y, z) + "direction " + direction);

            AddTween(transform, new Vector3(x, y, z), direction, duration);
        

    }


    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.name.Contains("PacStudent"))
        {
           if(GameManagement.Scared || GameManagement.Recovery)
           {
                Debug.Log(enemyName + "is Dead");
                isAlive = false;
                AudioController.GhostDead=+1;
                GameManagement.Score += 300;
                /*     CancelInvoke("Recovery");
                       CancelInvoke("NormalState");*/
                //     StartCoroutine(EnemyDead()); // Removed from 80% Section           
           }

        }

/*        if (trigger.name.Contains("PacStudent"))
        {
            if (!GameManagement.Scared && !GameManagement.Recovery && isAlive)
            {
                pacman = gameObject.GetComponent<PacStudentController>();
                pacman.PacDeath(); // access pacman script   
                Debug.Log("PacKill");
            }

        }*/


        if (trigger.name.Contains("GhostArea"))
        {
            ghostArea = true;
            isAlive = true;
            // AudioController.GhostDead=+1;
            //       Debug.Log("GhostArea = True");

            // Debug.Log(enemyName + "is Alive thanks to" + trigger.name);
            /*            anim.SetBool("up", true);
                        anim.SetBool("down", false);
                        anim.SetBool("up", false);
                        anim.SetBool("left", false);
                        anim.SetBool("right", false);
                        anim.SetBool("isScared", false);
                        anim.SetBool("isRecovery", false);
                        anim.SetBool("isDead", false);
            */
        }

        if (!trigger.name.Contains("GhostArea"))
        {
            ghostArea = false;
      //      Debug.Log("GhostArea = False");
        }


    }
/*    void Recovery()
    {
        anim.SetBool("isRecovery", true);
        anim.SetBool("isScared", false);
        Invoke("NormalState", 3f);
    }

    void NormalState()
    {
        anim.SetBool("isRecovery", false);
        anim.SetBool("isDead", false);
        anim.SetBool("up", true);
    }*/


/*    IEnumerator EnemyDead() // only used for 80% Section 
    {
        CancelInvoke("Recovery");
        CancelInvoke("NormalState");

        anim.SetBool("isScared", false);
        anim.SetBool("isRecovery", false);
        yield return new WaitForSeconds(15);
        AudioController.GhostDead = false;
        isAlive = true;
        anim.SetBool("isDead", false);
        anim.SetBool("up", true);
    }*/


}
