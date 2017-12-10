using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScreen_puzzles : MonoBehaviour {
    Action onAnimatedCompletedAction;

	void onAnimationCompleted_puzzles_fade_in () {
        this.onAnimatedCompletedAction.Invoke ();
	}

    public void onAnimationCompleted () {
    }

	public void registerOnAnimationCompletedEvent(Action action) {
		this.onAnimatedCompletedAction = action;
	}

}