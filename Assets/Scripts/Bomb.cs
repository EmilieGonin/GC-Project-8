using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] AudioSource _bombSound;

    public void PlaySound()
    {
        _bombSound?.Play();
    }
}
