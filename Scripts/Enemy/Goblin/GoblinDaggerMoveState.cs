using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinDaggerMoveState : EnemyState
{
    private GoblinDagger goblinDagger;

    public GoblinDaggerMoveState(EnemyStateMachine enemyStateMachine, Enemy enemy, string ainmBoolName, GoblinDagger goblinDagger) : base(enemyStateMachine, enemy, ainmBoolName)
    {
        this.goblinDagger = goblinDagger;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        
        goblinDagger.SetVelocity(goblinDagger.moveSpeed*goblinDagger.facingDir,0);
        
        if(goblinDagger.IsWallDetected() || !goblinDagger.IsGroundDetected())
            goblinDagger.Flip();
        
        if (goblinDagger.IsPlayerDetected())
        {
            enemyStateMachine.ChangeState(goblinDagger.battleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
