using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAudio : MonoBehaviour
{
    public AudioSource buttonAudio;

    public void PlayButton()
    {
        buttonAudio.Play();
    }
}
