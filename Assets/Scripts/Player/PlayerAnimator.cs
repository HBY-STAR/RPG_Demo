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

    private void AttackTrigger()
    {
        Collider2D[] playerColliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackDistance, player.whatIsEnemy);

        foreach (var hit in playerColliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                hit.GetComponent<CharacterStats>().TakeDamage(player.stats.damage.GetValue());
                hit.GetComponent<Enemy>().OnDamage();
            }
        }
    }
    
    private void AirAttackTrigger()
    {
        Collider2D[] playerColliders = Physics2D.OverlapCircleAll(player.transform.position, player.airAttackDistance, player.whatIsEnemy);

        foreach (var hit in playerColliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                hit.GetComponent<CharacterStats>().TakeDamage((int)(player.stats.damage.GetValue()*player.airAttackStrength));
                hit.GetComponent<Enemy>().OnDamage();
            }
        }
    }
    
    private void ThrowShurikan()
    {
        SkillManager.Instance.shurikanSkill.CreateShurikan();
    }
}
