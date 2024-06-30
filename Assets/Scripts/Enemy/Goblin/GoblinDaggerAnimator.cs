using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinDaggerAnimator : MonoBehaviour
{
    private GoblinDagger goblinDagger => GetComponentInParent<GoblinDagger>();

    private void AnimationTrigger()
    {
        goblinDagger.AnimationTrigger();
    }
    
    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(goblinDagger.attackCheck.position, goblinDagger.attackDistance, goblinDagger.whatIsPlayer);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null)
            {
                hit.GetComponent<CharacterStats>().TakeDamage(goblinDagger.stats.damage.GetValue());
            }
        }
    }
}
