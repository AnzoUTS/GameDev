using System.Collections;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource audio;
    public AudioClip Intro;
    public AudioClip Normal;
    public AudioClip OrcScared;
    public AudioClip OrcDead;
    public static AudioClip musicClip;
    public static Coroutine MusicStart;
    private static bool ghostScared; 

    public IEnumerator Start()
    {
        audio = GetComponent<AudioSource>();
        audio.clip = Normal;
        audio.loop = true;
        audio.volume = 0.5f;
        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);
/*        audio.clip = Normal;
        audio.Play();*/
    }


    public static bool Music
    {
        set { ghostScared = value; }
        get { return false; }
    }

    private void Update()
    {
        if (ghostScared == true)
        {
            ghostScared = false;
            StartCoroutine(Scared());
        }
    }

    public IEnumerator Scared()
    {
        audio.Stop();
        audio.clip = OrcScared;
        audio.volume =1;
        audio.Play();
        yield return new WaitForSeconds(10);
        audio.clip = Normal;
        audio.Play();
    }


}
