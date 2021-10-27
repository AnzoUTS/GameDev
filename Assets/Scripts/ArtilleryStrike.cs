using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryStrike : MonoBehaviour
{

    GameManagement gameManagement;
    public GameObject artillery;
    public List<Vector3> Walkable;
    private int walkCount;
    private int strikeChoice;
    private static Vector3 strikeLocation;
    private bool isStrike;
    public static int x;
    public static int y;
    private static int direction;
    private const float centerX = 13.5f;
    private const float centerY = -14.0f;
    public static int activeStrikes;


    public AudioClip hit;
    private AudioSource hitAudio;
    private static bool hitTarget;


    /*    private GameObject item;
        private Vector3 currentPos;
        //private Tween activeTween;
        //public List<Tween> activeTweens;
        private List<GameObject> itemList;
        public List<Tween> activeTweens = new List<Tween>();*/

    private void Start()
    {
        gameManagement = GameObject.Find("GameManagement").GetComponent<GameManagement>();
        hitAudio = GetComponent<AudioSource>();
        Walkable = gameManagement.Walkable;
        walkCount = Walkable.Count;
        
        hitTarget = false;
        isStrike = false;



    }

    void Update()
    {
        Debug.Log("HITTARGET " + hitTarget);

        if (!isStrike && GameManagement.StartMovement == true && activeStrikes <2 && AudioController.Music == true)
        {
            isStrike = true;
            StartCoroutine(Incomming());
        }



        if (!hitAudio.isPlaying && activeStrikes <2)
        {

            StartCoroutine(Explosion());

 /*           hitAudio.clip = hit;
            hitAudio.Play();
            hitAudio.loop = false;*/
            //hitTarget = false;

        }

        /*        if (activeTweens.Count > 0)
                {

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


                }*/


    }

    private IEnumerator Incomming()
    {
        activeStrikes++;
        

        x = Random.Range(-20, 20);
        y = Random.Range(-20, 20);
        direction = Random.Range(1, 5); // range 1-4

        strikeChoice = UnityEngine.Random.Range(0, walkCount);
        // Debug.Log("choice "+ strikeChoice);

        strikeLocation = Walkable[strikeChoice];
        Debug.Log("Strike Location " + strikeLocation);

        switch (direction)
        {
            case 1:
                {
                    Instantiate(artillery, new Vector3(x + centerX, 5, 0), Quaternion.Euler(0, -120, 0));
                    break;
                }

            case 2:
                {
                    Instantiate(artillery, new Vector3(40, y + centerY, 0), Quaternion.Euler(0, -120, 0));
                    break;
                }

            case 3:
                {
                    Instantiate(artillery, new Vector3(-x + centerX, -33, 0), Quaternion.Euler(0, -120, 0));
                    break;
                }

            case 4:
                {
                    Instantiate(artillery, new Vector3(-15, -y + centerY, 0), Quaternion.Euler(0, -120, 0));
                    break;
                }
        }

        yield return new WaitForSeconds(2);
        isStrike = false;
    }

    public static int Xvalue
    {
        get
        {
            return x;
        }
    }

    public static int Yvalue
    {
        get
        {
            return y;
        }
    }
    public static int Axis
    {
        get
        {
            return direction;
        }
    }


    public static int ActiveStrikes
    {
        set
        {
            activeStrikes = value;
        }
        get
        {
            return activeStrikes;
        }
    }


    public static bool HitTarget
    {
        set
        {
            hitTarget = value;
        }
        get
        {
            return hitTarget;
        }
    }



    public static Vector3 StrikeLocation
    {

        set
        {
            strikeLocation = value;
        }
        get
        {
            return strikeLocation;
        }
    }


    public IEnumerator Explosion()
    {
        hitAudio.clip = hit;
        hitAudio.Play();
        

        yield return new WaitForSeconds(hitAudio.clip.length);
        hitAudio.loop = false;
    }








        /*
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

            }*/








    }
