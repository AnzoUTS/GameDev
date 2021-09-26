using System.Collections;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    // Audio Clips 
    public AudioClip Intro;
    public AudioClip Main;
    public AudioClip Movement;
   

    IEnumerator Start()
    {
        AudioSource audio = GetComponent<AudioSource>();

        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);
        audio.clip = Main;
        audio.Play();
    }


}
