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

        slime.SetVelocity(slime.moveSpeed * slime.facingDir,slime.jumpForce);
    }

    public override void Update()
    {
        base.Update();
        
        slime.animator.SetFloat("yVelocity",rb.velocity.y);
        
        if (triggerCalled && slime.IsGroundDetected())
            enemyStateMachine.ChangeState(slime.idleState);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
