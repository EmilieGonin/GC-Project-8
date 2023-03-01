using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public AudioSource BombSound;

    void Start()
    {
        //Make an explosion sound when a bomb is revealed
        if (BombSound != null)
        {
            BombSound.Play();
        }
    }
}
