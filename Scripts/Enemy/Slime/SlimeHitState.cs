using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeHitState : EnemyState
{
    private Slime slime;
    
    public SlimeHitState(EnemyStateMachine enemyStateMachine, Enemy enemy, string ainmBoolName, Slime _slime) : base(enemyStateMachine, enemy, ainmBoolName)
    {
        slime = _slime;
    }
    
    public override void Enter()
    {
        base.Enter();

        rb.velocity = new Vector2(slime.moveSpeed * slime.facingDir * (-1),slime.jumpForce/1.5f);

        startTimer = 0.5f;
    }

    public override void Update()
    {
        base.Update();
        
        startTimer -= Time.deltaTime;
        
        if (startTimer < 0)
        {
            enemyStateMachine.ChangeState(slime.battleState);
        }
            
    }

    public override void Exit()
    {
        base.Exit();
    }
}
