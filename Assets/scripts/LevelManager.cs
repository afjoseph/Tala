
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public PuzzleManager puzzleManager;
    public GameOverScreen gameOverScreen;

    public void Start()
    {
        puzzleManager.registerGameOverAction(action_activateScreen);
    }

    private void action_activateScreen()
    {
        
    }
}