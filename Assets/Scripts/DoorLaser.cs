using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLaser : MonoBehaviour
{
    public AudioSource AudioFone;
    public AudioSource AudioOff;

    public void Off()
    {
        AudioFone.Stop();
        AudioOff.Play();
    }
}
