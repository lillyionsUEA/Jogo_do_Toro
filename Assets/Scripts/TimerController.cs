using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.Universal;
using UnityEngine.SocialPlatforms.Impl;

public class TimerController : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private Image sliderObject;

    [Header("Sound Settings")]
    public AudioSource audioSource;
    public AudioClip timerEndSound;

    [Header("Slow Motion Settings")]
    public float slowMotionFactor = 0.5f; // Fator de desaceleração
    public float slowMotionDuration = 3f; // Duração da desaceleração
    [SerializeField] private ScriptableRendererFeature freezeFullScreen;
    [SerializeField] private Material _material;


    float time;
    bool startTimer;
    private bool isSlowMotionActive = false;

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
        freezeFullScreen.SetActive(false);
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

    public void ActivateSlowMotion()
    {
        if (!isSlowMotionActive)
        {
            isSlowMotionActive = true;
            StartCoroutine(SlowDownTime());
        }
    }

    private IEnumerator SlowDownTime()
    {
        if (freezeFullScreen != null)
        {
            freezeFullScreen.SetActive(true); // Ativa o VFX
        }
        else
        {
            Debug.LogWarning("Freeze VFX not assigned!");
        }

        isSlowMotionActive = true;

        // Reduz a velocidade do tempo
        Time.timeScale = slowMotionFactor;

        // Espera a duração do efeito
        yield return new WaitForSecondsRealtime(slowMotionDuration);

        // Restaura o tempo
        Time.timeScale = 1f;
        isSlowMotionActive = false;
        if (freezeFullScreen != null)
        {
            freezeFullScreen.SetActive(false); // Desativa o VFX
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
