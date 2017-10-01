using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public PuzzleManager puzzleManager;
    public CanvasGroup gameOverPanelCanvasGroup;
    public string nextLevelName;
    public float gameOverFadeInMultiplier = 3f;

    public void Start()
    {
        gameOverPanelCanvasGroup.alpha = 0f;
        gameOverPanelCanvasGroup.interactable = false;
        gameOverPanelCanvasGroup.blocksRaycasts = false;

        puzzleManager.registerGameOverAction(action_activateScreen);
    }

    IEnumerator activateScreen()
    {
        for (; ; )
        {
            Debug.Log("onClick: fading");

            gameOverPanelCanvasGroup.alpha = Mathf.Lerp(gameOverPanelCanvasGroup.alpha, 1f, gameOverFadeInMultiplier * Time.deltaTime);
            Debug.Log("alpha: " + gameOverPanelCanvasGroup.alpha);
            if (gameOverPanelCanvasGroup.alpha > 0.95)
            {
                gameOverPanelCanvasGroup.interactable = true;
                gameOverPanelCanvasGroup.blocksRaycasts = true;
                yield break;
            }

            yield return new WaitForSeconds(0.01f);
        }
    }

    public void action_activateScreen()
    {
        StartCoroutine("activateScreen");
    }

    public void onClick_nextLevel()
    {
        Debug.Log("onClick");
        SceneManager.LoadScene(nextLevelName);
    }
}