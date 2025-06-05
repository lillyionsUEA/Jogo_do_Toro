using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

public class ResultScreenController : MonoBehaviour
{
    public TimerController timerController;
    public UIDocument uIDocument;
    public GameObject inputHandlerObject;
    public UIManager uiManager;

    private Label ScoreLabel;
    private Label highScoreLabel;
    private Button restartButton;
    private Button exitButton;

    void Awake()
    {
        var root = uIDocument.rootVisualElement;
        root.style.display = DisplayStyle.None;

        ScoreLabel = root.Q<Label>("ResultText");
        highScoreLabel = root.Q<Label>("HighScoreText");

        restartButton = root.Q<Button>("ReplayButton");
        exitButton = root.Q<Button>("CloseButton");

        if (restartButton != null)
        {
            restartButton.clicked += RestartGame;
        }
        if (exitButton != null)
        {
            exitButton.clicked += QuitGameToMenu;
        }

        gameObject.SetActive(true);
    }

    void OnEnable()
    {
        if (timerController != null)
            timerController.OnTimerEnd += ShowResult;
        Debug.Log("Subscribed on OnTimerEnd event.");
    }
    void OnDisable()
    {
        if (timerController != null)
            timerController.OnTimerEnd -= ShowResult;
    }
    public void ShowResult()
    {
        //gameObject.SetActive(true);

        var root = uIDocument.rootVisualElement;
        root.style.display = DisplayStyle.Flex;

        ScoreLabel.text = ScoreManager.Instance.GetScore().ToString();
        ScoreManager.Instance.CheckAndSaveHighScore();
        highScoreLabel.text = "High Score: " + ScoreManager.Instance.GetHighScore().ToString();
        inputHandlerObject.SetActive(false);
        Debug.Log("ShowResults called.");
    }

    public void HideResult()
    {
        var root = uIDocument.rootVisualElement;
        root.style.display = DisplayStyle.None;
        //gameObject.SetActive(false);
        inputHandlerObject.SetActive(true);
    }

    public void RestartGame()
    {
        //uiManager.RestartGame();
        timerController.RestartTimer();
        timerController.StartTimer();
        ScoreManager.Instance.ResetScore();
        ScoreManager.Instance.ClearAllItems();
        inputHandlerObject.SetActive(true); // Ativar clique de novo
        HideResult();
    }
    public void QuitGameToMenu()
    {
        uiManager.ShowMainMenu();
        
        Time.timeScale = 1f;
        timerController.StopTimer();
        ScoreManager.Instance.ResetScore();
        ScoreManager.Instance.ClearAllItems();
        Debug.Log("MainMenu called");
    }
}

