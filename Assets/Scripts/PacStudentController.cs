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
    public List<Vector3> isWalkable;
    [SerializeField]
    private List<Vector3> isWalkable2;
    GameObject[] gameObjects;



    private void Awake()
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
        
        //List<Vector3> isWalkable2 = new List<Vector3>();
        List<GameObject> walkableArea = new List<GameObject>();

            gameObjects = GameObject.FindGameObjectsWithTag("Item");

            foreach (GameObject item in gameObjects)
            {
                walkableArea.Add(item);
            }

            Debug.Log("Area :" + walkableArea.Count);



            foreach (GameObject item in gameObjects)
            {
                isWalkable2.Add(item.transform.position);
            }

            Debug.Log("isWalkable2: Awake" + isWalkable2.Count);



/*        if (isWalkable.Contains(new Vector3(2, -1, 0)))
        {

            Debug.Log("YEAHJASSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS");
        }
        else
        {
            Debug.Log("NOOOOOOSSSSSSSSSSSSSSSSSSSSSSSSSS");
        }*/



    }







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

        /*        List<Vector3> isWalkable = new List<Vector3>();
                List<GameObject> walkableArea = new List<GameObject>();

                gameObjects = GameObject.FindGameObjectsWithTag("Item");

                foreach (GameObject item in gameObjects)
                {
                    walkableArea.Add(item);
                }

                Debug.Log("Area :"+walkableArea.Count);



                foreach (GameObject item in gameObjects)
                {
                    isWalkable.Add(item.transform.position);
                }

                Debug.Log("isWalkable :" + isWalkable.Count);*/

               foreach (Vector3 pos in isWalkable2)
                {
            int i = 1;    
                    Debug.Log("pos :" + i + " ||" + pos);
            i++;
                }
       

        /*        if (isWalkable.Contains(new Vector3(2, -1, 0)))
                {

                    Debug.Log("YEAHJASSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS");
                }
                else
                {
                    Debug.Log("NOOOOOOSSSSSSSSSSSSSSSSSSSSSSSSSS");
                }
        */


        Debug.Log("isWalkable2 Start:" + isWalkable2.Count);
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

        Debug.Log("isWalkable2 Update:" + isWalkable2.Count);

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

                if (WallCheck() == false)

                {
                    Debug.Log("int x " + localPos.x + " : int y " + localPos.y);
                    Direction(lastInput);
                    currentInput = lastInput;
                }
                //else if (WallCheck() == true && lastInput != currentInput)
                else if (lastInput != currentInput)
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
                }


            



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






        if (isWalkable.Contains(localPos))
        {

            Debug.Log("YEAHJASSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS");
        }
        else
        {
            Debug.Log("NOOOOOOSSSSSSSSSSSSSSSSSSSSSSSSSS");
        }



    }


    public void AddTween(Transform targetObject, Vector3 startPos, Vector3 endpos, float duration)
    {

        Debug.Log(endpos);

        /*        foreach (Vector3 pos in isWalkable)
                {
                    Debug.Log("pos :" +pos);
                }*/
        //if (isWalkable.Contains(endpos))
            if (isWalkable2.Contains(endpos))
            //if (walkableArea.Contains(endpos))
            // if (isWalkable.Contains(Vector3(2,-1,0)))
            //f (isWalkable.Find(endpos))
            {
            Debug.Log("Walkable Area!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!DDDDDDDDDDDDDDDDDDDDDDDDDDDD!!!!!!!!!!!!!!!!!!!ASWDASDASDASDASDASASD!!!!!!!!!!!!");



            /*       if (isWalkable2.Contains(endpos))
                   //if (walkableArea.Contains(endpos))
                   // if (isWalkable.Contains(Vector3(2,-1,0)))
                   //f (isWalkable.Find(endpos))
                   {
                       Debug.Log("Walkable Area!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!###################################!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                   }
           */


            if (tween == null)
            {
                tween = new Tween(targetObject, startPos, endpos, Time.time, duration);
            }
            else
            {
                // tween = new Tween(targetObject, localPos, localPos, Time.time, duration);
            }

        }

    }
    



    public void Direction(KeyCode key)
    {
        int x = (int)Math.Round(localPos.x);
        int y = (int)Math.Round(localPos.y);
        float z = localPos.z;


        Debug.Log("Current Input : " + lastInput + " Last Input : " + currentInput);

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
           // Direction(KeyCode.None);
            //AddTween(transform, localPos, new Vector3(x, y, z), speed);
            //AddTween(transform, localPos, localPos, speed);
            Debug.Log("wall hit "+ x + ":" + y + ":" + z);
        }




    }




    // public static bool Raycast Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layerMask = DefaultRaycastLayers
    bool WallCheck() // an invisible ray (line) that is cast (drawn) from one point to another in 3D space
    {
        int x = (int)Math.Round(localPos.x);
        int y = (int)Math.Round(localPos.y);
        int z = (int)Math.Round(localPos.z);

        RaycastHit hitInfo;

/*        RaycastHit up;
        RaycastHit down;
        RaycastHit left;
        RaycastHit right;

        bool rayup = Physics.Raycast(localPos, new Vector3(0,1,0), out up, 1f);
        bool raydown = Physics.Raycast(localPos, new Vector3(0,-1, 0), out down, 1f);
        bool rayleft = Physics.Raycast(localPos, new Vector3(1, 0, 0), out left, 1f);
        bool rayright = Physics.Raycast(localPos, new Vector3(-1, 0, 0), out right, 1f);

        if (rayup || raydown || rayleft || rayright == true)
        {
            if (up.transform.gameObject.CompareTag("Walls"))
            {
                Debug.Log("WALL UP FOUND !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                return true;
            }
            if (down.transform.gameObject.CompareTag("Walls"))
            {
                Debug.Log("WALL DOWN FOUND !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                return true;
            }
            if (left.transform.gameObject.CompareTag("Walls"))
            {
                Debug.Log("WALL LEFT FOUND !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                return true;
            }
            if (right.transform.gameObject.CompareTag("Walls"))
            {
                Debug.Log("WALL RIGHT FOUND !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                return true;

            }
            return false;
        }

        return false;*/

        bool raycastHits = Physics.Raycast(localPos, NormPos, out hitInfo, 1f);
        Debug.Log("Normal :"+ NormPos);



        if (raycastHits == true)
        {
            //Debug.Log("Raycast Hit!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!: " + hitInfo.transform.gameObject.name + " : " +hitInfo.transform.gameObject.tag);

            if (hitInfo.transform.gameObject.CompareTag("Walls"))
            {
               // Debug.Log("WALL FOUND !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                return true;
            }
            else
            {
              // Debug.Log("Raycast FALSE !!");
                return false;
            }
        }
        else
        {
           // Debug.Log("Raycast FALSE !!");
            return false;
        }

       
    }


    /*    // public static bool Raycast Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layerMask = DefaultRaycastLayers
        public void 4DWallCheck() // an invisible ray (line) that is cast (drawn) from one point to another in 3D space
        {
            int x = (int)Math.Round(localPos.x);
            int y = (int)Math.Round(localPos.y);
            int z = (int)Math.Round(localPos.z);

            RaycastHit hitInfo;

            bool raycastHits = Physics.Raycast(localPos, NormPos, out hitInfo, 1f);
            Debug.Log("Normal :" + NormPos);



            if (raycastHits == true)
            {
                //Debug.Log("Raycast Hit!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!: " + hitInfo.transform.gameObject.name + " : " +hitInfo.transform.gameObject.tag);

                if (hitInfo.transform.gameObject.CompareTag("Walls"))
                {
                    // Debug.Log("WALL FOUND !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

                }
            }


        }
    */



/*    public bool Walk(Vector3 walk)
    {

        if (isWalkable.Contains(walk))
        {

            return true;
        }
        else
        {
            return false;
        }

    }*/


}
