using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("up", false);
        anim.SetBool("down", false);
        anim.SetBool("left", true);
        anim.SetBool("right", false);


    }


    void Update()
    {
        
        if (GameManagement.Scared == true)
        {
           // ghostScared = true;
            anim.SetBool("up", false);
            anim.SetBool("down", false);
            anim.SetBool("left", false);
            anim.SetBool("right", false);
            anim.SetBool("isScared", true);
           // Invoke("Recovery",7f);


        }

        Debug.Log("GM Scared" + GameManagement.Scared + " || Animations : anim UP :" + anim.GetBool("up") + " anim RIGHT :" + anim.GetBool("right") + " anim DOWN :" + anim.GetBool("down") + " anim LEFT :" + anim.GetBool("left") + " || scared " + anim.GetBool("isScared") + " is recovery "+anim.GetBool("isRecovery") + " is DEad" + anim.GetBool("isDead"));


    }



    private void OnTriggerEnter(Collider trigger)
    {

        if(GameManagement.Scared == true)
        {
            anim.SetBool("isScared", false);
            anim.SetBool("isRecovery", false);
            anim.SetBool("isDead", true);


            AudioController.GhostDead = true;
            GameManagement.Score += 300;
            CancelInvoke("Recovery");
            CancelInvoke("NormalState");
            StartCoroutine(EnemyDead());
        }

    }


    IEnumerator EnemyDead()
    {
        anim.SetBool("isScared", false);
        anim.SetBool("isRecovery", false);
        yield return new WaitForSeconds(5);
        anim.SetBool("isDead", false);
        AudioController.GhostDead = false;

    }


    void Recovery()
    {

        anim.SetBool("isRecovery", true);
        anim.SetBool("isScared", false);
        Invoke("NormalState", 3f);
    }

    void NormalState()
    {

        anim.SetBool("isDead", false);
        anim.SetBool("up", true);

    }


    }
