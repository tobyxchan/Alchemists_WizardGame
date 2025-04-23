using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    void Awake()
    {
        var src = GetComponent<AudioSource>();
        src.loop = true;
        src.playOnAwake = true;  // or call src.Play() below
        src.Play(); // this is the only Play method that respects loop
    }
}