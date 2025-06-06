using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource inGameSource;
    [SerializeField] AudioSource rainSource;

    [Header("Audio Clips")]
    public AudioClip background;
    public AudioClip rainSound;
    //public AudioClip bkgMusic;
    //public AudioClip gameOverSound;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }
    public void PlayRainSound()
    {
        if (rainSource != null && rainSound != null)
        {
            rainSource.clip = rainSound;
            rainSource.loop = true;
            rainSource.Play();
        }
    }
    public void StopRainSound()
    {
        if (rainSource != null)
        {
            rainSource.Stop();
        }
    }
}
