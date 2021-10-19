using System.Collections;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public AudioClip Intro;
    public AudioClip Normal;
    public AudioClip OrcScared;
    public AudioClip OrcDead;
    public static AudioClip musicClip;
    //private static bool ghostScared;
   // private static int ghostDead;
    private static bool music;

    void Start ()
    {
      backgroundMusic = GetComponent<AudioSource>();
      music = false;
    }

    /*    public static bool GhostScared
        {
            set { ghostScared = value; }
        }*/

    public static bool Music
    {
        set { music = value; }
    }

    /*    public static int GhostDead
        {
            set { ghostDead = value; }
            get { return ghostDead; }
        }*/

    private void Update()
    {

        if (GameManagement.deadGhostCount == 0 && backgroundMusic.clip == OrcDead)
        {
            backgroundMusic.Stop();
            Debug.Log("stoping dead audio");
        }



        if (!backgroundMusic.isPlaying && music)
        {
            StartCoroutine(Main());
        }
            if (GameManagement.Scared || GameManagement.Recovery)
            {
                StopCoroutine(Main());

                if (backgroundMusic.clip != OrcScared)
                {
               // StopCoroutine(Dead());
                backgroundMusic.Stop();
                }

                if (!backgroundMusic.isPlaying)
                {
                StartCoroutine(Scared());
                }
            }

   /*     if ((!GameManagement.Scared || !GameManagement.Recovery) && backgroundMusic.clip == OrcScared)
        {
            backgroundMusic.Stop();
        }*/

            if (GameManagement.deadGhostCount > 0)
            {

                backgroundMusic.Stop();
                StopCoroutine(Main());
                StopCoroutine(Scared());
          
                //ghostScared = false;

                if (backgroundMusic.clip != OrcDead)
                {
                    backgroundMusic.Stop();
                }
                if (!backgroundMusic.isPlaying)
                {

                    backgroundMusic.clip = OrcDead;
                    backgroundMusic.volume = 1;
                    backgroundMusic.Play();

                    //StartCoroutine(Dead());
                }

            }


       // Debug.Log("background" + " scard " + ghostScared + " dead " + ghostDead);

    }

    public IEnumerator Main()
    {
        backgroundMusic.clip = Normal;
        backgroundMusic.loop = true;
        backgroundMusic.volume = 0.5f;
        backgroundMusic.Play();
        yield return new WaitForSeconds(backgroundMusic.clip.length);
    }

/*    public IEnumerator Dead()
    {
        backgroundMusic.clip = OrcDead;
        backgroundMusic.volume = 1;
        backgroundMusic.Play();
        yield return new WaitForSeconds(5); ;
        backgroundMusic.Stop();
    }*/

    public IEnumerator Scared()
    {
        backgroundMusic.clip = OrcScared;
        backgroundMusic.volume =1;
        backgroundMusic.Play();
        yield return new WaitForSeconds(10);
       // ghostScared = false;

    }
}
