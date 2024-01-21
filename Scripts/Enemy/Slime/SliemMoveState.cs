using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMoveState : EnemyState
{
    private Slime slime;
    
    public SlimeMoveState(EnemyStateMachine enemyStateMachine, Enemy enemy, string ainmBoolName, Slime _slime) : base(enemyStateMachine, enemy, ainmBoolName)
    {
        slime = _slime;
    }
    
    public override void Enter()
    {
        base.Enter();

        startTimer = 1f;
        slime.SetVelocity(slime.moveSpeed * slime.facingDir,slime.jumpForce);
    }

    public override void Update()
    {
        base.Update();

        startTimer -= Time.deltaTime;
        
        slime.animator.SetFloat("yVelocity",rb.velocity.y);
        
        if((rb.velocity.y == 0) && triggerCalled || slime.IsWallDetected())
            enemyStateMachine.ChangeState(slime.idleState);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
