using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public PuzzleManager puzzleManager;
    public string nextLevelName;
    public List<AnimatedEvent> animatedEvents;
    private Queue<AnimatedEvent> animatedEventsQueue = new Queue<AnimatedEvent>();
    private Animator animator;

    public void Start()
    {
        animator = GetComponent<Animator>();
        foreach (var animator in animatedEvents)
        {
            animatedEventsQueue.Enqueue(animator);
        }

        puzzleManager.registerGameOverAction(action_gameOver);
    }

    public void action_gameOver()
    {
        if (animatedEventsQueue.Count == 0)
        {
            activateGameOver();
        }
        else
        {
            startAnimatedEvent(animatedEventsQueue.Peek());
        }
    }

    public void startAnimatedEvent(AnimatedEvent animatedEvent)
    {
        animatedEvent.triggerAnimation();
        animatedEvent.registerAnimatedEventCompletedAction(action_onAnimatedEventCompleted);
    }

    public void action_onAnimatedEventCompleted()
    {
        Debug.Log("action_onAnimatedEventCompleted(): ");

        animatedEventsQueue.Dequeue();
        if (animatedEventsQueue.Count == 0)
        {
            activateGameOver();
            return;
        }

        startAnimatedEvent(animatedEventsQueue.Peek());
    }

    private void activateGameOver()
    {
        animator.SetTrigger("fadeIn");
    }

    public void onClick_nextLevel()
    {
        SceneManager.LoadScene(nextLevelName);
    }
}