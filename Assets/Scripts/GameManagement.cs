using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
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
    public List<Vector3> isWalkable3;
    //Vector3[] isWalkable3;








    // Start is called before the first frame update
    void Start()
    {
        List<Vector3> isWalkable = new List<Vector3>();
        List<GameObject> walkableArea = new List<GameObject>();
        Vector3[] isWalkable2;

        GameObject[] gameObjects;
        gameObjects = GameObject.FindGameObjectsWithTag("Item");

        foreach (GameObject item in gameObjects)
        {
            walkableArea.Add(item);
        }

       // Debug.Log("Area :" + walkableArea.Count);



        foreach (GameObject item in gameObjects)
        {
            isWalkable3.Add(item.transform.position);
        }

        //Debug.Log("isWalkable3 GMUP :" + isWalkable3.Count);
        /*
                foreach (Vector3 pos in isWalkable)
                {
                    Debug.Log("pos :" + pos);
                }
        */






    }


    // Update is called once per frame
    void Update()
    {


      //  Debug.Log("isWalkable GM3 :" + isWalkable3.Count);
 


    }

}

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

    }

}*/
