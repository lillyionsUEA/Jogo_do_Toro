using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip background;
    public AudioClip bkgMusic;
    public AudioClip gameOverSound;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }
}
