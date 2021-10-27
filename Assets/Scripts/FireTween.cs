using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTween : MonoBehaviour
{
    private GameObject item;
    private Vector3 currentPos;
    //private Tween activeTween;
    //public List<Tween> activeTweens;
    private List<GameObject> itemList;
    public List<Tween> activeTweens = new List<Tween>();

    void Start()
    {
       
    }

    void Update()
    {


        if (activeTweens.Count > 0)
        {

            // -------------------------------------------------------------------------------------------
            // #### Linear Interpolation
            // -------------------------------------------------------------------------------------------
            /*            if (Vector3.Distance(activeTween.Target.position, activeTween.EndPos) > 0.1f)
                        {
                            float timeFraction = (Time.time - activeTween.StartTime) / activeTween.Duration;
                            currentPos = Vector3.Lerp(activeTween.StartPos, activeTween.EndPos, timeFraction);
                            activeTween.Target.position = currentPos;


                        }
                        else
                        {
                            activeTween.Target.position = activeTween.EndPos;
                            activeTween = null;
                        }
            */


            // ------------------------------------------------------------------------------------------
            // #### Cubic Easing-In Interpolation
            // ------------------------------------------------------------------------------------------
            /*                if (Vector3.Distance(activeTween.Target.position, activeTween.EndPos) > 0.1f)
                                    {
                                        float timeFraction = (Time.time - activeTween.StartTime) / activeTween.Duration;
                                        currentPos = (timeFraction * timeFraction * timeFraction) *
                                                     (activeTween.EndPos - activeTween.StartPos) + activeTween.StartPos;

                                        activeTween.Target.position = currentPos;
                                    }
                                    else
                                    {
                                        activeTween.Target.position = activeTween.EndPos;
                                        activeTween = null;*/

            //#############  End  90% BAND


            for (int i = activeTweens.Count - 1; i >= 0; i--)

            {
                if (Vector3.Distance(activeTweens[i].Target.position, activeTweens[i].EndPos) > 0.1f)
                {
                    float timeFraction = (Time.time - activeTweens[i].StartTime) / activeTweens[i].Duration;
                    currentPos = (timeFraction * timeFraction * timeFraction) *
                                 (activeTweens[i].EndPos - activeTweens[i].StartPos) + activeTweens[i].StartPos;

                    activeTweens[i].Target.position = currentPos;
                    Debug.Log(activeTweens[i].Target.position);
                }
                else
                {
                    activeTweens[i].Target.position = activeTweens[i].EndPos;
                    activeTweens.RemoveAt(i);

                }
            }


        }


    }


    public bool AddTween(Transform targetObject, Vector3 startPos, Vector3 endpos, float duration)
    {

        if (TweenExists(targetObject) == false)
        {
            Debug.Log("Add Active Tween");
            activeTweens.Add(new Tween(targetObject, startPos, endpos, Time.time, duration));
            return true;
        }

        return false;
    }



    public bool TweenExists(Transform target)
    {
        // First attempt
        // bool x = activeTweens.Exists(t => t.Target == target);
        // Debug.Log("Tween Exists :" + x);
        // return x;

        foreach (Tween activeTween in activeTweens)
        {
            if (activeTween.Target.transform == target)
            {
                return true;
            }
        }
        return false;

    }


    // #############   90% BAND  ####
    /*    public void AddTween(Transform targetObject, Vector3 startPos, Vector3 endpos, float duration)
        {
            Debug.Log("Add Tween");

            if (activeTween == null)
            {

                Debug.Log("Add Active Tween");
                activeTween = new Tween(targetObject, startPos, endpos, Time.time, duration);
            }
        }*/
    //#############   90% BAND  ####
}
