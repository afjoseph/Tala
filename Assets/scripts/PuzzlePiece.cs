using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: for now, this is fixed to vertically up and down
public enum MOTION
{
    DISPLACEMENT,
    ROTATION,
};

public enum MOVE_STATE
{
    STATIC,
    START_MOVEMENT,
    STOP_MOVEMENT,
    REACHED_GOAL
};

public class PuzzlePiece : MonoBehaviour
{
    public MOTION motion = MOTION.DISPLACEMENT;
    private float startPosition;
    public float endPosition;
    private float currentPosition;

    private float step = 0.0f;
    public float acceleration = 0.05f;
    public float deceleration = 0.3f;

    private MOVE_STATE moveState;
    private Action puzzleSolvedAction;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (motion == MOTION.DISPLACEMENT)
        {
            startPosition = transform.localPosition.y;
            currentPosition = startPosition;
            moveState = MOVE_STATE.STATIC;
        }
    }

    public void onAnimationCompleted()
    {
        Debug.Log("onAnimationCompleted");
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

    //It is assumed this would run every frame function
    public void toggleMovement(bool b)
    {
        if (moveState == MOVE_STATE.REACHED_GOAL)
        {
            return;
        }

        if (b)
        {
            moveState = MOVE_STATE.START_MOVEMENT;
        }
        else
        {
            moveState = MOVE_STATE.STOP_MOVEMENT;
        }
    }

    void Update()
    {
        switch (moveState)
        {
            case MOVE_STATE.STATIC:
                return;

            case MOVE_STATE.REACHED_GOAL:
                return;

            case MOVE_STATE.START_MOVEMENT:
                if (step < 1.0f)
                {
                    step = step + acceleration * Time.deltaTime;
                }
                break;

            case MOVE_STATE.STOP_MOVEMENT:
                if (step > 0.0f)
                {
                    step = step - deceleration * Time.deltaTime;
                }
                break;
        }

        switch (motion)
        {
            case MOTION.DISPLACEMENT:
                currentPosition = transform.localPosition.y;
                break;

            case MOTION.ROTATION:
                currentPosition = transform.eulerAngles.z;
                break;
        }

        var newPosition = 0.0f;
        if (Mathf.Abs(currentPosition - endPosition) <= 0.01f)
        { //If we reached our goal, just snap our current position there and raise a flag
            newPosition = endPosition;
            moveState = MOVE_STATE.REACHED_GOAL;
            puzzleSolvedAction.Invoke();
        }
        else
        { //else, just keep on smooth stepping
            newPosition = Mathf.SmoothStep(startPosition, endPosition, step);
        }

        switch (motion)
        {
            case MOTION.DISPLACEMENT:
                transform.localPosition = new Vector3(transform.position.x, newPosition, transform.position.z);
                break;

            case MOTION.ROTATION:
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, newPosition);
                break;
        }
    }
}