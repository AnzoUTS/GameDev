using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private Animator anim;


    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        





    }



    private void OnTriggerEnter(Collider trigger)
    {

        if(GameManagement.Scared == true)
        {

            anim.SetBool("isDead", true);
            AudioController.GhostDead = true;
            GameManagement.Score += 300;

        }


    }


    }
