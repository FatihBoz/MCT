using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource src;
    public AudioClip pickUp;
    public AudioClip menuMusic;
    public AudioClip gameMusic;
    public AudioClip lost;
    public AudioClip won;
    public AudioClip click;


    public void PickUp()
    {
        src.clip = pickUp;
        src.Play();
    }
    public void MenuMusic()
    {
        src.clip = menuMusic;
        src.Play();
    }
    public void GameMusic()
    {
        src.clip = gameMusic;
        src.Play();
    }
    public void Lost()
    {
        src.clip = lost;
        src.Play();
    }
    public void Won()
    {
        src.clip = won;
        src.Play();
    }
    public void Click()
    {
        src.clip = click;
        src.Play();
    }

}
