using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
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
    Vector3[] isWalkable2;








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

        Debug.Log("Area :" + walkableArea.Count);



        foreach (GameObject item in gameObjects)
        {
            isWalkable.Add(item.transform.position);
        }

        Debug.Log("isWalkable :" + isWalkable.Count);
        /*
                foreach (Vector3 pos in isWalkable)
                {
                    Debug.Log("pos :" + pos);
                }
        */

        /*        if (isWalkable.Contains(new Vector3(2, -1, 0)))
                {

                    Debug.Log("YEAHJASSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS");
                }
                else
                {
                    Debug.Log("NOOOOOOSSSSSSSSSSSSSSSSSSSSSSSSSS");
                }

        */





    }
}

    // Update is called once per frame
/*    void Update()
    {
*/


/*        if (Walk(new Vector3(2, -1, 0)))
        {
            Debug.Log("FUCK YEAH!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        }



    }*/



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
