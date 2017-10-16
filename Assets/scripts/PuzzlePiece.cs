using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    public float endPosition;
    public float accelerationMultiplier = 1.0f;
    public float decelerationMultiplier = -2.0f;

    private Action puzzleSolvedAction;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void onAnimationCompleted()
    {
        this.puzzleSolvedAction.Invoke();
    }

    public void registerPuzzleSolvedAction(Action action)
    {
        this.puzzleSolvedAction = action;
    }

    public void toggleAnim(bool b)
    {
        if (b)
        {
            animator.SetFloat("speed", 1.0f);
        }
        else
        {
            animator.SetFloat("speed", -2.0f);
        }
    }
}