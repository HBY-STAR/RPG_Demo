using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinDaggerHitState : EnemyState
{
    private GoblinDagger goblinDagger;

    public GoblinDaggerHitState(EnemyStateMachine enemyStateMachine, Enemy enemy, string ainmBoolName, GoblinDagger goblinDagger) : base(enemyStateMachine, enemy, ainmBoolName)
    {
        this.goblinDagger = goblinDagger;
    }

    public override void Enter()
    {
        base.Enter();
        
        rb.velocity = new Vector2(goblinDagger.moveSpeed * goblinDagger.facingDir * (-1),1.5f);

        startTimer = 0.5f;
    }

    public override void Update()
    {
        base.Update();
        
        startTimer -= Time.deltaTime;
        
        if (startTimer < 0)
        {
            enemyStateMachine.ChangeState(goblinDagger.battleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
