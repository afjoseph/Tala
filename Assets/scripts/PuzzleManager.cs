using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public List<PuzzlePiece> puzzlePieces = new List<PuzzlePiece>();
    private Queue<PuzzlePiece> queue = new Queue<PuzzlePiece>();
    public GestureController gestureController;

    //TODO: turn this into a game state where the state manager would broadcast a GAME_OVER event
    private Action gameOverAction;

    [NonSerialized]
    public bool isGameWon = false;

    private void action_gestureStart()
    {
        Debug.Log("action_gestureStart(): ");

        if (queue.Count == 0)
        {
            return;
        }

        Debug.Log(queue.Peek());
        startSolvingPuzzlePiece(queue.Peek());
    }

    private void action_gestureBreak()
    {
        Debug.Log("action_gestureBreak(): ");

        if (queue.Count == 0)
        {
            return;
        }

        stopSolvingPuzzlePiece(queue.Peek());
    }

    private void action_currentPuzzleSolved()
    {
        Debug.Log("action_currentPuzzleSolved(): ");

        queue.Dequeue();
        if (queue.Count == 0)
        {
            activateGameOver();
            return;
        }

        startSolvingPuzzlePiece(queue.Peek());
    }

    private void startSolvingPuzzlePiece(PuzzlePiece puzzlePiece)
    {
        Debug.Log("startSolvingPuzzlePiece(): ");
        puzzlePiece.toggleMovement(true);
        puzzlePiece.registerPuzzleSolvedAction(action_currentPuzzleSolved);
    }

    private void stopSolvingPuzzlePiece(PuzzlePiece puzzlePiece)
    {
        Debug.Log("stopSolvingPuzzlePiece(): ");
        puzzlePiece.toggleMovement(false);
    }

    void Start()
    {
        gestureController.registerGestureBreakAction(action_gestureBreak);
        gestureController.registerGestureStartAction(action_gestureStart);

        foreach (var puzzlePiece in puzzlePieces)
        {
            queue.Enqueue(puzzlePiece);
        }
    }

    public void registerGameOverAction(Action action)
    {
        gameOverAction = action;
    }

    private void activateGameOver()
    {
        Debug.Log("Game is won");
        isGameWon = true;
        gameOverAction.Invoke();
    }
}