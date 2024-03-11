using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinDaggerBattleState : EnemyState
{
    private GoblinDagger goblinDagger;

    public GoblinDaggerBattleState(EnemyStateMachine enemyStateMachine, Enemy enemy, string ainmBoolName, GoblinDagger goblinDagger) : base(enemyStateMachine, enemy, ainmBoolName)
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
        
        if (PlayerManager.Instance.player.transform.position.x > goblinDagger.transform.position.x && goblinDagger.facingDir == -1)
            goblinDagger.Flip();
        else if(PlayerManager.Instance.player.transform.position.x < goblinDagger.transform.position.x && goblinDagger.facingDir == 1)
            goblinDagger.Flip();
        
        if(goblinDagger.CanAttack() && goblinDagger.IsGroundDetected())
            enemyStateMachine.ChangeState(goblinDagger.attackState);
        
        goblinDagger.SetVelocity(goblinDagger.moveSpeed * goblinDagger.facingDir,0);

    }

    public override void Exit()
    {
        base.Exit();
    }
}
