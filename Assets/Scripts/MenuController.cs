using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

public class MenuController : MonoBehaviour
{
    public UIDocument uiMenuDocument;
    public TimerController timerController;
    public ScoreManager scoreManager;
    public GameObject inputHandlerObject;
    public UIDocument resultUIDocument;

    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip menuMusic;

    private Button playButton;
    private Button creditsButton;
    private Button closeCreditsButton;
    private Button quitButton;
    private VisualElement creditsPanel;
    private VisualElement menuPanel;

    void Awake()
    {
        var root = uiMenuDocument.rootVisualElement;
        uiMenuDocument.rootVisualElement.style.display = DisplayStyle.Flex;
        ShowMusicMenu();

        playButton = root.Q<Button>("PlayButton");
        creditsButton = root.Q<Button>("CreditsButton");
        closeCreditsButton = root.Q<Button>("BackToMenuButton");
        quitButton = root.Q<Button>("QuitButton");
        creditsPanel = root.Q<VisualElement>("CreditsCanvas");
        menuPanel = root.Q<VisualElement>("MainMenuCanvas");

        if (playButton != null)
        {
            playButton.clicked += OnPlayClicked;
        }
        if (creditsButton != null)
        {
            creditsButton.clicked += ShowCredits;
        }
        if (closeCreditsButton != null)
        {
            closeCreditsButton.clicked += HideCredits;
        }
        if (quitButton != null)
        {
            quitButton.clicked += QuitGame;
        }
        if (creditsPanel != null)
        {
            creditsPanel.style.display = DisplayStyle.None;
        }
    }

    public void SetUpMenu()
    {
        uiMenuDocument.rootVisualElement.style.display = DisplayStyle.Flex;
        resultUIDocument.rootVisualElement.style.display = DisplayStyle.None;
        ShowMusicMenu();
        TimerController timerController = FindObjectOfType<TimerController>();
        if (timerController != null)
        {
            timerController.StopTimer();
            timerController.RestartTimer();
            TimerController.startTimer = false;
            timerController.audioSource.Stop();
            timerController.rainSource.Stop();
        }

    }
    
    // Mostrar o menu e tocar a música
    public void ShowMusicMenu()
    {
        if (uiMenuDocument.rootVisualElement.style.display == DisplayStyle.Flex)
        {
            if (audioSource != null && menuMusic != null)
            {
                audioSource.clip = menuMusic;
                audioSource.loop = true;
                audioSource.Play();
            }
        }
    }

    public void OnPlayClicked()
    {

        uiMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
        resultUIDocument.rootVisualElement.style.display = DisplayStyle.None;
        
        Time.timeScale = 1f;
        timerController.StartTimer();
        scoreManager.ResetScore();
        scoreManager.ClearAllItems();
        inputHandlerObject.SetActive(true);

        if (audioSource != null && menuMusic != null)
        {
            audioSource.Stop();
        }

        CanvasGroup hudCanvasGroup = FindObjectOfType<UIManager>().hudCanvasGroup;
        if (hudCanvasGroup != null)
        {
            hudCanvasGroup.alpha = 1f;
            hudCanvasGroup.interactable = true;
            hudCanvasGroup.blocksRaycasts = true;
        }

        CanvasGroup pauseCanvasGroup = FindObjectOfType<PauseController>().pauseCanvasGroup;
        if (pauseCanvasGroup != null)
        {
            pauseCanvasGroup.alpha = 1f;
            pauseCanvasGroup.interactable = true;
            pauseCanvasGroup.blocksRaycasts = true;
        }
    }

        public void ShowCredits()
        {
            if (creditsPanel != null)
            {
                Debug.Log("Credits button clicked.");
                creditsPanel.style.display = DisplayStyle.Flex;
            }
            else
            {
                Debug.LogWarning("Credits panel not found.");
            }
        }

        public void HideCredits()
        {
            if (creditsPanel != null)
                creditsPanel.style.display = DisplayStyle.None;
        }
        public void QuitGame()
        {
            Application.Quit();
            Debug.Log("Fechando jogo...");

            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
    }
