using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseController : MonoBehaviour
{
    public UIDocument pauseUIDocument;
    public UIDocument mainMenuUIDocument;
    public GameObject pauseButton;
    public bool isPaused = false;
    public TimerController timerController;
    public ScoreManager scoreManager;
    public GameObject inputHandlerObject; // Referencia pra ativar e deativar o clique
    public CanvasGroup pauseCanvasGroup;

    private VisualElement pausePanel;
    private Button resumeButton;
    private Button restartButton;
    private Button quitButton;

    void Awake()
    {
    
        pauseUIDocument.rootVisualElement.style.display = DisplayStyle.None;
        mainMenuUIDocument.rootVisualElement.style.display = DisplayStyle.Flex;
        VisualElement root = pauseUIDocument.rootVisualElement;

        resumeButton = root.Q<Button>("ContinueButton");
        restartButton = root.Q<Button>("RestartButton");
        quitButton = root.Q<Button>("BackToMenuButton");
        pausePanel = root.Q<VisualElement>("PauseCanvas");

        if (resumeButton != null)
        {
            resumeButton.clicked += ResumeGame;
        }
        if (restartButton != null)
        {
            restartButton.clicked += RestartGame;
        }
        if (quitButton != null)
        {
            quitButton.clicked += QuitGameToMenu;
        }


        if (pauseButton != null)
        {
            CanvasGroup pauseCanvasGroup = pauseButton.GetComponent<CanvasGroup>();
            if (pauseCanvasGroup != null)
            {
                pauseCanvasGroup.alpha = 1f;
                pauseCanvasGroup.interactable = true;
                pauseCanvasGroup.blocksRaycasts = true;
            }
            pauseButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(TogglePause);
        }
    }

    void TogglePause()
    {
        if (!isPaused)
        {
            PauseGame();
        }
        
    }
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;

        if (pausePanel != null)
        {
            pauseUIDocument.rootVisualElement.style.display = DisplayStyle.Flex;
        }

        inputHandlerObject.SetActive(false);

        if (pauseButton != null)
        {
            CanvasGroup pauseCanvasGroup = pauseButton.GetComponent<CanvasGroup>();
            if (pauseCanvasGroup != null)
            {
                pauseCanvasGroup.alpha = 0f;
                pauseCanvasGroup.interactable = false;
                pauseCanvasGroup.blocksRaycasts = false;
            }
        }
    }

    public void ResumeGame()
    {
        Debug.Log("Resuming game...");

        isPaused = false;
        Time.timeScale = 1f;
        pauseUIDocument.rootVisualElement.style.display = DisplayStyle.None;

        if (pausePanel != null)
        {
            CanvasGroup pauseCanvasGroup = pauseButton.GetComponent<CanvasGroup>(); // Criar um CanvasGoup para o botão de pausa
            if (pauseCanvasGroup != null)
            {
                pauseCanvasGroup.alpha = 1f;
                pauseCanvasGroup.interactable = true;
                pauseCanvasGroup.blocksRaycasts = true;
            }
        }
    }

    public void RestartGame()
    {
        Debug.Log("Restarting game...");
        Time.timeScale = 1f;
        isPaused = false;
        pauseUIDocument.rootVisualElement.style.display = DisplayStyle.None;
        timerController.RestartTimer();
        timerController.StartTimer();
        scoreManager.ResetScore();
        scoreManager.ClearAllItems();


        CanvasGroup pauseCanvasGroup = pauseButton.GetComponent<CanvasGroup>(); // Criar um CanvasGoup para o botão de pausa
        if (pauseCanvasGroup != null)
        {
            pauseCanvasGroup.alpha = 1f;
            pauseCanvasGroup.interactable = true;
            pauseCanvasGroup.blocksRaycasts = true;
        }
    }

    public void QuitGameToMenu()
    {
        Debug.Log("ShowMainMenu chamado");

        pauseUIDocument.rootVisualElement.style.display = DisplayStyle.None;

        //uiManager.ShowMainMenu();
        Time.timeScale = 1f;
        timerController.StopTimer();
        scoreManager.ResetScore();
        scoreManager.ClearAllItems();

    }
}
