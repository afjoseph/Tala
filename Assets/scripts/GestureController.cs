using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GESTURE_STATE
{
    STATE_STATIC,
    STATE_MOVING,
};

public enum DIRECTION
{
    NULL,
    CW,
    CCW
};

public class GestureController : MonoBehaviour
{
    public PuzzleManager puzzleManager;
    private Vector2 currPos = Vector2.zero;
    private Vector2 prevPos = Vector2.zero;

    public float maxDelay = 0.1f;

    private GESTURE_STATE _gestureState;
    public GESTURE_STATE gestureState
    {
        get { return _gestureState; }
    }

    private Action gestureBreakAction;
    private Action gestureStartAction;
    public float gestureRadius;
    private DIRECTION currentDirection;
    private bool isReceivingUserTouchInput = false;
    private bool didFireStartGestureAction = false;

    void Start()
    {
        // Input.simulateMouseWithTouches = true;

        resetGesture();
        StartCoroutine("updateGesture");
    }

    void Update()
    {
        if (puzzleManager.isGameWon)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            // Debug.Log("mouse button down");
            isReceivingUserTouchInput = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            // Debug.Log("mouse button up");
            breakGesture();
        }
    }

    IEnumerator updateGesture()
    {
        for (; ; )
        {
            // If the user is not touching the screen, do nothing
            if (!isReceivingUserTouchInput)
            {
                yield return new WaitForSeconds(maxDelay);
                continue;
            }

            // Calculate the current touch position in screen space
            Vector2 inputPos = new Vector2();
            Vector2 screenPos = new Vector2();
            Vector2 centerPos = new Vector2();
            inputPos.x = Input.mousePosition.x;
            inputPos.y = Camera.main.pixelHeight - Input.mousePosition.y;
            screenPos = Camera.main.ScreenToWorldPoint(new Vector3(inputPos.x, inputPos.y, Camera.main.nearClipPlane));
            // Debug.Log ("screen pos: " + screenPos);

            // We need the previous screen touch position and the current one
            prevPos = currPos;
            currPos = screenPos;

            // Get the vectors from the center of the screen
            var v1 = currPos - centerPos;
            var v2 = prevPos - centerPos;
            // Debug.Log ("v1: [" + v1 + "] && v2: [" + v2 + "]");

            // Calculate sign (Cross product) and the dot product
            var sign = 0.0f;
            var dot = 0.0f;

            // If this was the first run, prevPos would be zero and it would give incorrect calculations, 
            //      just set the direction to CW and the dot to greater than one just to pass the first run
            // Debug.Log("prevPos: " + prevPos);
            // Debug.Log("currPos: " + currPos);
            if (prevPos == Vector2.zero)
            {
                sign = -1;
                dot = 1;
            }
            else
            {
                sign = Mathf.Sign(v1.x * v2.y - v1.y * v2.x);
                dot = Vector2.Dot(v1, v2);
            }
            // Debug.Log("dot product: " + dot + " | sign: " + sign);

            //Assign direction
            switch (currentDirection)
            {
                case DIRECTION.NULL:
                    break;

                case DIRECTION.CW:
                    // If this was still CW, the user is still gesturing in the correct direction. 
                    //      Else, the user must've broken the gesture
                    if (sign > 0)
                    {
                        breakGesture();
                        yield return new WaitForSeconds(maxDelay);
                        continue;
                    }
                    break;
                case DIRECTION.CCW:
                    // If this was still CCW, the user is still gesturing in the correct direction. 
                    //      Else, the user must've broken the gesture
                    if (sign < 0)
                    {
                        breakGesture();
                        yield return new WaitForSeconds(maxDelay);
                        continue;
                    }
                    break;
            }

            // So far, the gesture is still moving in a proper direction, check the dot product for a break in gesture
            if (dot < 0.0f || dot > this.gestureRadius)
            {
                // Debug.Log("Breaking gesture 3");
                breakGesture();
                yield return new WaitForSeconds(maxDelay);
                continue;
            }

            // If we reached here, its confirmed that we are moving with no breaks
            currentDirection = sign < 0 ? DIRECTION.CW : DIRECTION.CCW;
            _gestureState = GESTURE_STATE.STATE_MOVING;
            if (!didFireStartGestureAction)
            {
                startGesture();
            }
            // Debug.Log("currentDirection: [" + currentDirection.ToString() + "] && gestureState: [" + gestureState.ToString() + "]");
            yield return new WaitForSeconds(maxDelay);
        }
    }

    private void startGesture()
    {
        gestureStartAction.Invoke();
        didFireStartGestureAction = true;
    }

    private void breakGesture()
    {
        //If we were moving, send a callback that the gesture was broken
        if (gestureState == GESTURE_STATE.STATE_MOVING)
        {
            gestureBreakAction.Invoke();
        }
        resetGesture();
    }

    private void resetGesture()
    {
        isReceivingUserTouchInput = false; //The user has to lift his finger and retouch again
        didFireStartGestureAction = false;
        currPos = Vector2.zero;
        prevPos = Vector2.zero;
        _gestureState = GESTURE_STATE.STATE_STATIC;
        currentDirection = DIRECTION.NULL;
    }

    public void registerGestureBreakAction(Action action)
    {
        this.gestureBreakAction = action;
    }

    public void registerGestureStartAction(Action action)
    {
        this.gestureStartAction = action;
    }
}