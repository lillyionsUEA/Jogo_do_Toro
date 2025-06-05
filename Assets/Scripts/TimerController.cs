using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class TimerController : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private Image sliderObject;

    [Header("Sound Settings")]
    public AudioSource audioSource;
    public AudioClip timerEndSound;


    float time;
    bool startTimer;
    public float timeLimit = 60f;
    float multiplierFactor;
    public bool TimerRunning { get { return startTimer; } }
    public event System.Action OnTimerEnd;

    void Update()
    {
        if (!startTimer) return;

        time -= Time.deltaTime;

        if (time <= 0f)
        {
            time = 0f;
            startTimer = false;

            if (audioSource != null && timerEndSound != null)
            {
                audioSource.PlayOneShot(timerEndSound);
            }
            
            OnTimerEnd?.Invoke();
            Debug.Log("Timer ended!");
            ScoreManager.Instance.CheckAndSaveHighScore();
            
        }

        timerText.text = Mathf.CeilToInt(time).ToString();
        sliderObject.fillAmount = time * multiplierFactor;
    }

    void Start()
    {
        timerText.text = timeLimit.ToString();
        time = timeLimit;
        startTimer = false;
        sliderObject.fillAmount = time * multiplierFactor;
        ScoreManager.Instance.UpdateHighScoreText();
    }

    public void StartTimer()
    {
        multiplierFactor = 1f / timeLimit;
        startTimer = true;

        sliderObject.fillAmount = time * multiplierFactor;
    }

    public void StopTimer()
    {
        if (startTimer)
        {
            startTimer = false;
        }
    }

    public void RestartTimer()
    {
        time = timeLimit; // Reset the time to the time limit
        multiplierFactor = 1f / timeLimit; // Calculate the multiplier factor based on the time limit
        startTimer = true; // Restart the timer
        timerText.text = time.ToString(); // Update the timer text to show the reset time
        sliderObject.fillAmount = time * multiplierFactor; // Reset the slider fill amount

        Debug.Log("Timer restarted!");
        Debug.Log("Time scale" + Time.timeScale);
    }
}
