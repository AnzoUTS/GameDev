using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artillery : MonoBehaviour
{
    private Tween tween;
    private Vector3 localPos;
    private Vector3 currentPos;
   // private const float centerX = 13.5f;
   // private const float centerY = -14.0f;
    private int x;
    private int y;
    private int direction;
    private Vector3 targetLocation;
    private float targetDistance;
    private int duration;
    public ParticleSystem fire;
    public ParticleSystem fireTail;
    private bool fireTailPlay;
    private bool firePlay;
    public AudioClip hit;
    private AudioSource hitAudio;
    private bool hitTarget;

/*
    [SerializeField]
    private GameObject item;
    private ArtilleryStrike tweener;
    [SerializeField]
    private List<GameObject> itemList = new List<GameObject>();
*/




    void Start()
    {
        duration = Random.Range(5, 7); // max Exclusive for int
        fireTailPlay =false;
        firePlay = false;
        hitTarget = false;
        hitAudio = GetComponent<AudioSource>();
        targetLocation = ArtilleryStrike.StrikeLocation;

/*        tweener = GetComponent<ArtilleryStrike>();
        itemList.Add(item);*/

    }

    private void FixedUpdate()
    {
        targetDistance = Vector3.Distance(transform.localPosition, targetLocation);

        if (tween != null)
        {
            if (targetDistance > 0.4f)
            {
                float timeFraction = (Time.time - tween.StartTime) / tween.Duration;
                currentPos = (timeFraction * timeFraction * timeFraction) *
                                    (targetLocation - tween.StartPos) + tween.StartPos;

                transform.position = currentPos;
            }
            else
            {
                tween = null;
                transform.position = targetLocation;
            }
        }
    }

    void Update()
    {
        if (!fireTailPlay)
        {
            //fireTail.Play();
        }

        if (!firePlay)
        {
            fire.Play();
        }

        if (targetDistance == 0 && !hitTarget)
        {
            
/*            if (!hitAudio.isPlaying)
            {
                hitAudio.clip = hit;
                hitAudio.Play();
                Debug.Log("Play1 " + targetDistance);
            }*/
            StartCoroutine(Explode());    
        }

        AddTween(this.transform, this.transform.localPosition, targetLocation, duration);
        //tweener.AddTween(this.transform, this.transform.localPosition, targetLocation, duration);
    }

    public void AddTween(Transform targetObject, Vector3 startPos, Vector3 endpos, float duration)
    {
        if (tween == null)
        {
            tween = new Tween(targetObject, startPos, endpos, Time.time, duration);
        }
    }

    IEnumerator Explode()
    {
        //hitTarget = true;
        /*        if (!hitAudio.isPlaying)
                {
                    hitAudio.clip = hit;
                    hitAudio.Play();
                    Debug.Log("Play2");
                }*/
        ArtilleryStrike.ActiveStrikes--;
        ArtilleryStrike.HitTarget = true;
        yield return new WaitForSecondsRealtime(1.8f);
        ArtilleryStrike.HitTarget = false;
        yield return new WaitForSecondsRealtime(5);
       
        Debug.Log("Active Strikes " +ArtilleryStrike.ActiveStrikes);
        Destroy(this.transform.gameObject);
    }
}
