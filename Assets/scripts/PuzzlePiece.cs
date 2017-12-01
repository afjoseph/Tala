using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour {
    public float accelerationMultiplier = 1.0f;
    public float decelerationMultiplier = -2.0f;

    private Action puzzleSolvedAction;

    private Animator animator;

    void Start () {
        animator = GetComponent<Animator> ();
    }

    public void onAnimationCompleted () {
        this.puzzleSolvedAction.Invoke ();
    }

    public void registerPuzzleSolvedAction (Action action) {
        this.puzzleSolvedAction = action;
    }

    public void toggleAnim (bool b) {
        animator.enabled = true;
        if (b) {
            animator.SetFloat ("speed", accelerationMultiplier);
        } else {
            animator.SetFloat ("speed", decelerationMultiplier);
        }
    }
}