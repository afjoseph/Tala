using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedEvent : MonoBehaviour
{
    public String triggerName;
    private Animator animator;
    private Action animatedEventCompleted;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void registerAnimatedEventCompletedAction(Action action)
    {
        this.animatedEventCompleted = action;
    }

    public void onAnimatedEventCompleted()
    {
        animatedEventCompleted.Invoke();
    }

    public void triggerAnimation()
    {
        animator.SetTrigger(triggerName);
    }
}