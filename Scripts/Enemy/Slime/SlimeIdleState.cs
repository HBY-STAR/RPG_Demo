using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeIdleState : EnemyState
{
    private Slime slime;
    
    public SlimeIdleState(EnemyStateMachine enemyStateMachine, Enemy enemy, string ainmBoolName, Slime _slime) : base(enemyStateMachine, enemy, ainmBoolName)
    {
        slime = _slime;
    }
    
    public override void Enter()
    {
        base.Enter();
        
        slime.SetVelocity(0,rb.velocity.y);
        
        if(slime.IsWallDetected() || !slime.IsGroundDetected())
            slime.Flip();

        startTimer = 2f;
    }

    public override void Update()
    {
        base.Update();

        startTimer -= Time.deltaTime;

        if (startTimer < 0)
        {
            if (slime.IsPlayerDetected())
            {
                enemyStateMachine.ChangeState(slime.battleState);
            }
            else
            {
                enemyStateMachine.ChangeState(slime.moveState);
            }
            
        }
            
    }

    public override void Exit()
    {
        base.Exit();
    }
    
}
