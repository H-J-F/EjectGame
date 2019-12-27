using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using EjectGame.Tools;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    public GameObject gameOverAllUI;
    public GameObject gameOver;
    public GameObject victory;
    public CanvasGroup canvasGroup;

    void Start()
    {
        gameOverAllUI.SetActive(false);
        canvasGroup.alpha = 0;
    }

    public void ShowUI(bool isVictory)
    {
        gameOver.SetActive(!isVictory);
        victory.SetActive(isVictory);
        canvasGroup.alpha = 0;
        gameOverAllUI.SetActive(true);
        canvasGroup.DOFade(1f, 0.3f);
    }

    public void BackGame()
    {
        GameStateManager.instance.LoadNewScene(GameScenes.MainMenu);
    }
}