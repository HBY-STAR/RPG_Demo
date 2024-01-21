using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBattleState : EnemyState
{
    private Slime slime;
    
    public SlimeBattleState(EnemyStateMachine enemyStateMachine, Enemy enemy, string ainmBoolName, Slime _slime) : base(enemyStateMachine, enemy, ainmBoolName)
    {
        slime = _slime;
    }
    
    public override void Enter()
    {
        base.Enter();
        
        if (PlayerManager.Instance.player.transform.position.x > slime.transform.position.x && slime.facingDir == -1)
            slime.Flip();
        else if(PlayerManager.Instance.player.transform.position.x < slime.transform.position.x && slime.facingDir == 1)
            slime.Flip();
        
        slime.SetVelocity(slime.moveSpeed * slime.facingDir,slime.jumpForce);
    }

    public override void Update()
    {
        base.Update();
        
        
        slime.animator.SetFloat("yVelocity",rb.velocity.y);
        
        if (((rb.velocity.y == 0) && triggerCalled) || slime.IsWallDetected())
        {
            if(slime.CanAttack())
                enemyStateMachine.ChangeState(slime.attackState);
            else
                enemyStateMachine.ChangeState(slime.idleState);
        }
            
    }

    public override void Exit()
    {
        base.Exit();
    }
}
