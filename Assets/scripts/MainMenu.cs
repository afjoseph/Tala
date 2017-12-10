using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    private Animator animator;

    void Start () {
        animator = GetComponent<Animator> ();
    }

    void Update () {
        if (Input.GetKeyDown (KeyCode.Escape)) {
            onBackPressed_hideAbout ();
        }
    }

    // Use this for initialization
    void onAnimationCompleted_fade () {
        animator.SetTrigger ("doMove");
    }

    // Update is called once per frame
    void onAnimationCompleted_move () {
        animator.SetTrigger ("doSplit");
    }

    void onAnimationCompleted_split () {
        animator.SetTrigger ("doRevealText");
    }

    public void onClick_startGame () {
        CameraFade.GetInstance ().FadeOut ();
    }

    public void onClick_displayAbout () {
        animator.SetBool ("displayAbout", true);
    }

    public void onBackPressed_hideAbout () {
        animator.SetBool ("displayAbout", false);
    }
}