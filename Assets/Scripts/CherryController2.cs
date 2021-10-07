using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryController2 : MonoBehaviour
{
    public List<GameObject> Burgers;
    public GameObject[] cherry;
    private bool isBurger;
    //rivate GameObject cherry;
    private Tween tween;
    private Vector3 localPos;
    private Vector3 currentPos;



    void Start()
    {
        //Burgers.Add(transform.gameObject, transform.position, new Vector3(-35, -14, 0), 2);
        //Burgers.Add(Instantiate(cherry[0], new Vector3(35, -14, 0), Quaternion.Euler(0, 0, 0)));
        AddTween(cherry[0].transform, transform.localPosition, new Vector3(-35, -14, 0), 2);
        isBurger = false;
    }


    void Update()
    {
  
        if (!isBurger)
        {
            isBurger = true;
            StartCoroutine(CherryBurger());
        }
    }



    private void FixedUpdate()
    {
        localPos = transform.localPosition;

        if (tween != null)
        {
            float timeFraction = (Time.time - tween.StartTime) / tween.Duration;
            currentPos = Vector3.Lerp(tween.StartPos, tween.EndPos, timeFraction);
            transform.position = currentPos;
        }
    }





    private IEnumerator CherryBurger()
    {

        yield return new WaitForSeconds(3f);
        Burgers.Add(Instantiate(cherry[0], new Vector3(35, -14, 0), Quaternion.Euler(0, 0, 0)));
        AddTween(cherry[0].transform, localPos, new Vector3(-35, -14,  0), 2);
        yield return new WaitForSeconds(3f);
        Destroy(Burgers[0]);
        isBurger = false;

    }



    public void AddTween(Transform targetObject, Vector3 startPos, Vector3 endpos, float duration)
    {
        if (tween == null)
        {
            tween = new Tween(targetObject, startPos, endpos, Time.time, duration);
        }
    }





}

