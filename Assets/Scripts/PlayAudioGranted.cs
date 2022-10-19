using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioGranted : MonoBehaviour
{
    public AudioClip granted;
    AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayGranted()
    {
        audioSource.PlayOneShot(granted, 1f);
    }



}
