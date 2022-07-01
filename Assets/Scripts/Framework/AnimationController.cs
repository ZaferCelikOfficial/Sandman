using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;
using System;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AnimancerComponent))]

public class AnimationController : MonoBehaviour
{
    AnimancerComponent animancer;
    Action endFunctionAction;
    bool isAnimationEnded;

    private void Awake()
    {
        animancer = GetComponent<AnimancerComponent>();
    }

    public float PlayAnim(AnimationClip clip, float fade = 0.3f,float speed = 1, Action endAnimation = null)
    {
        var state = animancer.Play(clip, fade);
        state.Speed = speed;

        isAnimationEnded = false;
        if (endAnimation != null)
        {
            endFunctionAction = endAnimation;
            state.Events.OnEnd = OnEndEvent;
        }
        return state.Duration / speed;
    }

    private void OnEndEvent()
    {
        if (!isAnimationEnded)
        {
            isAnimationEnded = true;
            endFunctionAction();
        }
    }

    public AnimancerComponent GetAnimancer()
    {
        return animancer;
    }
}
