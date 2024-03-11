using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinDaggerIdleState : EnemyState
{
    private GoblinDagger goblinDagger;

    public GoblinDaggerIdleState(EnemyStateMachine enemyStateMachine, Enemy enemy, string ainmBoolName, GoblinDagger goblinDagger) : base(enemyStateMachine, enemy, ainmBoolName)
    {
        this.goblinDagger = goblinDagger;
    }

    public override void Enter()
    {
        base.Enter();
        
        goblinDagger.SetVelocity(0,rb.velocity.y);

        startTimer = 3f;
    }

    public override void Update()
    {
        base.Update();
        
        startTimer -= Time.deltaTime;

        if (startTimer < 0)
        {
            if (goblinDagger.IsPlayerDetected())
            {
                enemyStateMachine.ChangeState(goblinDagger.battleState);
            }
            else
            {
                enemyStateMachine.ChangeState(goblinDagger.moveState);
            }
            
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
