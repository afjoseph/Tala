using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialScreenManager : MonoBehaviour {
	public TutorialScreen_puzzles puzzlesObject;
	private Animator animator;
	public Text tutorialText;

	void Start () {
		animator = GetComponent<Animator> ();
		puzzlesObject.registerOnAnimationCompletedEvent (onAnimationCompleted_puzzles_fade_in);
	}

	void onAnimationCompleted_puzzles_fade_in () {
		animator.SetTrigger ("revealText");
	}

	void onKeyReached_text_first () {
		tutorialText.text = "Welcome";
	}

	void onKeyReached_text_second () {
		tutorialText.text = "Sit somewhere comfortable";
	}

	void onKeyReached_text_third () {
		tutorialText.text = "Plug in your headphones";
	}

	void onKeyReached_text_fourth () {
		tutorialText.text = "Drag your finger slowly across the screen in a circular motion";
	}

	void onKeyReached_fade_out () {
		CameraFade.GetInstance ().FadeOut ();
	}
}