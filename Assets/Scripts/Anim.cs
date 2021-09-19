using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim: MonoBehaviour
{
    public Vector3 Pac;
    public Vector3 position;
    public Vector3 endposition;
    private Animator anim;
    public Tween tween;
    public Mover pos;
    //private Vector3 pos;


    // Start is called before the first frame update
    void Start()
    {
       anim = GetComponent<Animator>();
        //Pac = pos.currentPos;
        position = pos.position;
        //endposition = pos.GetEndPos();



    }

    // Update is called once per frame
    void Update()
    {
/*        endposition = pos.position;


        Debug.Log("PACX :" +Pac.x);
        Debug.Log("PACY :" + Pac.y);
        Debug.Log("posAnim :" + position);
        Debug.Log("GetAnim :" + endposition);*/

        /*        Debug.Log(Pac.transform.position.x);
                Debug.Log(Pac.transform.position.y);
                Debug.Log(Pac.transform.position.z);
                Debug.Log(Pac.transform.position);*/




/*        if (Input.GetKey(KeyCode.UpArrow))
        {
            anim.SetBool("up", true);
        }

        if (Pac.x <= 0f)
        {
            anim.SetBool("right", true);
        }


        if (Input.GetKey(KeyCode.DownArrow))
        {
            anim.SetBool("down", true);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            anim.SetBool("left", true);
        }

        if (Input.GetKey(KeyCode.Return))
        {
            anim.SetBool("die", true);
        }
*/
        // Debug.Log("anim Pos ::" + position.currentPos);
       // Debug.Log("local Pos ::" + Pac.transform.localPosition + "LocaX:" + Pac.transform.localPosition.x + "LocaY:" + Pac.transform.localPosition.y);
        //Debug.Log("local Pos X::" + Pac.transform.localPosition);
        //Debug.Log("endPos:" + tween.Target);
        //Debug.Log("LocalNormPos:" + Pac.transform.localPosition.normalized);
        //Debug.Log("LocaX:" + Pac.transform.localPosition.x + "LocaY:" + Pac.transform.localPosition.y);

    }
/*
    private Vector3 GetCurrentPos()
    {
        //return currentPos;
    }*/
}
