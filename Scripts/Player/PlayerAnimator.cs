using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }
    
    private void ThrowShurikan()
    {
        SkillManager.Instance.shurikanSkill.CreateShurikan();
    }
}
