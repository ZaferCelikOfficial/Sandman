using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;
using System;

public class AnimationManager : LocalSingleton<AnimationManager>
{    
    Action endFunctionAction;
    bool isAnimationEnded;
    [Header("PlayerAnimations")]
    public AnimationClip idle;
    public AnimationClip playerWalk;
    public AnimationClip injuredLeftFootWalk;
    public AnimationClip injuredRightFootWalk;
    public AnimationClip leglessWalk;
    public float PlayAnim(AnimationClip clip, AnimancerComponent anim, float fade = 0.3f, float speed = 1, Action endAnimation = null)
    {
        var state = anim.Play(clip, fade);
        state.Speed = speed;

        isAnimationEnded = false;
        if (endAnimation != null)
        {
            endFunctionAction = endAnimation;
            state.Events.OnEnd = OnEndEvent;
        }
        return state.Duration / speed;
    }
    public void DoubleAnimation(AnimationClip clip, AnimationClip defaultClip, AnimancerComponent anim, float fade = 0.3f, float speed = 1, Action endAnimation = null)
    {
        var state = anim.Play(clip, fade);
        state.Speed = speed;
        state.Events.OnEnd = () => { PlayAnim(defaultClip, anim, 0.3f, 1f); };
    }
    private void OnEndEvent()
    {
        if (!isAnimationEnded)
        {
            isAnimationEnded = true;
            endFunctionAction();
        }
    }
}
