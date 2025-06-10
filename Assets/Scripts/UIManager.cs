using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public UIDocument mainMenuUIDocument;
    public GameObject hudUI;
    public UIDocument pauseUIDocument;
    public UIDocument resultUIDocument;
    public CanvasGroup hudCanvasGroup;

    public TimerController timerController;
    public ScoreManager scoreManager;
    public GameObject inputHandlerObject;

    public void ShowMainMenu()
    {
        mainMenuUIDocument.rootVisualElement.style.display = DisplayStyle.Flex;
        MenuController menuController = FindObjectOfType<MenuController>();
        if (menuController != null)
        {
            menuController.SetUpMenu();
        }

        HideHUD();
        pauseUIDocument.rootVisualElement.style.display = DisplayStyle.None;
        resultUIDocument.rootVisualElement.style.display = DisplayStyle.None;
    }

    /*public void StartGame()
    {
        mainMenuUIDocument.rootVisualElement.style.display = DisplayStyle.None;
        ShowHUD();
        pauseUIDocument.rootVisualElement.style.display = DisplayStyle.None;
        resultUIDocument.rootVisualElement.style.display = DisplayStyle.None;

        Time.timeScale = 1f;
        timerController.StartTimer();
        scoreManager.ResetScore();
        scoreManager.ClearAllItems();
        inputHandlerObject.SetActive(true); // Ativar clique de novo
    }*/

    /*public void RestartGame()
    {
        Time.timeScale = 1f;

        mainMenuUIDocument.rootVisualElement.style.display = DisplayStyle.None;
        ShowHUD();
        pauseUIDocument.rootVisualElement.style.display = DisplayStyle.None;
        resultUIDocument.rootVisualElement.style.display = DisplayStyle.None;

        timerController.RestartTimer();
        timerController.StartTimer();
        scoreManager.ResetScore();
        scoreManager.ClearAllItems();
        inputHandlerObject.SetActive(true); // Ativar clique de novo
    }*/

    public void ShowHUD()
    {
        hudCanvasGroup.alpha = 1f;
        hudCanvasGroup.interactable = true;
        hudCanvasGroup.blocksRaycasts = true;
    }
    public void HideHUD()
    {
        hudCanvasGroup.alpha = 0f;
        hudCanvasGroup.interactable = false;
        hudCanvasGroup.blocksRaycasts = false;
    }
}
