using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;
#region Enums
public enum CharacterAnimationState
{
    idle,
    walkNormal,
    walkInjuredLeft,
    walkInjuredRight,
    walkLegless,    
}
#endregion

public class PlayerAnimationController : LocalSingleton<PlayerAnimationController>
{
    #region variables
    public CharacterAnimationState characterAnimationState;

    [SerializeField] AnimancerComponent anim;
    #endregion

    #region Unity
    void Start()
    {
        Idle();
    }
    #endregion

    #region AnimationPlayers
    void Idle()
    {
        characterAnimationState = CharacterAnimationState.idle;
        AnimationManager.Instance.PlayAnim(AnimationManager.Instance.idle, anim, 0.3f, 1);        
    }
    public void PlayIdle()
    {
        if (characterAnimationState != CharacterAnimationState.idle)
        {
            characterAnimationState = CharacterAnimationState.idle;
            AnimationManager.Instance.PlayAnim(AnimationManager.Instance.idle, anim, 0.3f, 1);
        }
    }
    public void PlayWalkNormal()
    {
        if (characterAnimationState != CharacterAnimationState.walkNormal)
        {
            characterAnimationState = CharacterAnimationState.walkNormal;
            AnimationManager.Instance.PlayAnim(AnimationManager.Instance.playerWalk, anim, 0.3f, 1f);
        }
    }
    public void PlayWalkInjuredLeft()
    {
        if (characterAnimationState != CharacterAnimationState.walkInjuredLeft)
        {
            characterAnimationState = CharacterAnimationState.walkInjuredLeft;
            AnimationManager.Instance.PlayAnim(AnimationManager.Instance.injuredLeftFootWalk, anim, 0.3f, 1f);
        }
    }
    public void PlayWalkInjuredRight()
    {
        if (characterAnimationState != CharacterAnimationState.walkInjuredRight)
        {
            characterAnimationState = CharacterAnimationState.walkInjuredRight;
            AnimationManager.Instance.PlayAnim(AnimationManager.Instance.injuredRightFootWalk, anim, 0.3f, 1f);
        }
    }
    public void PlayWalkLegless()
    {
        if (characterAnimationState != CharacterAnimationState.walkLegless)
        {
            characterAnimationState = CharacterAnimationState.walkLegless;
            AnimationManager.Instance.PlayAnim(AnimationManager.Instance.leglessWalk, anim, 0.3f, 1);
        }
    }
    #endregion
}
