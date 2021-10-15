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
    private static bool ghostScared;
    private static bool ghostDead;


    void Start ()
    {
      backgroundMusic = GetComponent<AudioSource>();
      StartCoroutine(Main());
    }


    public static bool GhostScared
    {
        set { ghostScared = value; }
    }


    public static bool GhostDead
    {
        set { ghostDead = value; }
    }


    private void Update()
    {

        if (!backgroundMusic.isPlaying)
        {
            StartCoroutine(Main());
        }


            if (ghostScared == true)
        {
            //backgroundMusic.Stop();
            StopCoroutine(Main());

            if (backgroundMusic.clip == Normal)
            {
                backgroundMusic.Stop();
            }


            if (!backgroundMusic.isPlaying)
            {
                StartCoroutine(Scared());
            }
        }



        if (ghostDead == true)
        {
            StopCoroutine(Main());
            StopCoroutine(Scared());
          
            ghostScared = false;

            if (backgroundMusic.clip != OrcDead)
            {
                backgroundMusic.Stop();
            }
            if (!backgroundMusic.isPlaying)
            {
                StartCoroutine(Dead());
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





    public IEnumerator Dead()
    {
        backgroundMusic.clip = OrcDead;
        backgroundMusic.volume = 1;
        backgroundMusic.Play();
        yield return new WaitForSeconds(backgroundMusic.clip.length); ;
    }


        public IEnumerator Scared()
    {
        backgroundMusic.clip = OrcScared;
        backgroundMusic.volume =1;
        backgroundMusic.Play();
        yield return new WaitForSeconds(10);
        ghostScared = false;

    }


}
