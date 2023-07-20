using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatAudio : MonoBehaviour
{
    public AudioSource combatAudio;

    public void PlayCombatButton()
        { 
         combatAudio.Play();
        }
}
