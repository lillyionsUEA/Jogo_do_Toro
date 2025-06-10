using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource rainSource;

    [Header("Audio Clips")]
    public AudioClip background;
    public AudioClip rainSound;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }
}
