using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSound : MonoBehaviour
{
    public AudioSource AmbientMusic;

    void Update()
    {
        if (!AmbientMusic.isPlaying)
        {
            AmbientMusic.Play();
        }
    }
}
