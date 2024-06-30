using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAnimator : MonoBehaviour
{
    private Slime slime => GetComponentInParent<Slime>();

    private void AnimationTrigger()
    {
        slime.AnimationTrigger();
    }
    
    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(slime.attackCheck.position, slime.attackDistance, slime.whatIsPlayer);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null)
            {
                hit.GetComponent<CharacterStats>().TakeDamage(slime.stats.damage.GetValue());
            }
        }
    }
}
