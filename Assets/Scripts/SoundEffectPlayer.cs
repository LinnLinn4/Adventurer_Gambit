using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectPlayer : MonoBehaviour
{
    public AudioSource src;
    public AudioClip winSound, loseSound, wheelSound;
    // Start is called before the first frame update
    public void playWinSfx()
    {
        src.clip = winSound;
        src.Play();
    }

    public void playLoseSfx()
    {
        src.clip = loseSound;
        src.Play();
    }

    public void playWheelSfx()
    {
        src.clip = wheelSound;
        src.Play();
    }
}
