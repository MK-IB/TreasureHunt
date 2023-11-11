using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsController : MonoBehaviour
{
    public static SoundsController instance;

    public AudioSource mainAudioSource, bgSource;

    public AudioClip swoosh, arrowOnEnemy, arrowOnWall, UiClick;

    private void Awake()
    {
        instance = this;
    }

    public void PlaySound(AudioClip clip)
    {
        mainAudioSource.PlayOneShot(clip);
    }
}
