using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinDaggerAttackState : EnemyState
{
    private GoblinDagger goblinDagger;

    public GoblinDaggerAttackState(EnemyStateMachine enemyStateMachine, Enemy enemy, string ainmBoolName, GoblinDagger goblinDagger) : base(enemyStateMachine, enemy, ainmBoolName)
    {
        this.goblinDagger = goblinDagger;
    }

    public override void Enter()
    {
        base.Enter();
        
        goblinDagger.SetVelocity(0,rb.velocity.y);
        
        if (PlayerManager.Instance.player.transform.position.x > goblinDagger.transform.position.x && goblinDagger.facingDir == -1)
            goblinDagger.Flip();
        else if(PlayerManager.Instance.player.transform.position.x < goblinDagger.transform.position.x && goblinDagger.facingDir == 1)
            goblinDagger.Flip();
        
        startTimer =     1f;
    }

    public override void Update()
    {
        base.Update();
        
        if (PlayerManager.Instance.player.transform.position.x > goblinDagger.transform.position.x && goblinDagger.facingDir == -1)
            goblinDagger.Flip();
        else if(PlayerManager.Instance.player.transform.position.x < goblinDagger.transform.position.x && goblinDagger.facingDir == 1)
            goblinDagger.Flip();

        startTimer -= Time.deltaTime;

        if (startTimer < 0 && triggerCalled)
        {
            if (!goblinDagger.CanAttack())
            {
                enemyStateMachine.ChangeState(goblinDagger.battleState);
            }
        }
           
          
    }

    public override void Exit()
    {
        base.Exit();
    }
}
