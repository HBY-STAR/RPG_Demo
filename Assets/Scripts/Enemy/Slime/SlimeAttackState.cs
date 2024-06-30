using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttackState : EnemyState
{
    private Slime slime;
    
    public SlimeAttackState(EnemyStateMachine enemyStateMachine, Enemy enemy, string ainmBoolName, Slime _slime) : base(enemyStateMachine, enemy, ainmBoolName)
    {
        slime = _slime;
    }
    
    public override void Enter()
    {
        base.Enter();
        
        slime.SetVelocity(0,rb.velocity.y);
        
        if (PlayerManager.Instance.player.transform.position.x > slime.transform.position.x && slime.facingDir == -1)
            slime.Flip();
        else if(PlayerManager.Instance.player.transform.position.x < slime.transform.position.x && slime.facingDir == 1)
            slime.Flip();
        
        startTimer = 2f;
    }

    public override void Update()
    {
        base.Update();
        
        if (PlayerManager.Instance.player.transform.position.x > slime.transform.position.x && slime.facingDir == -1)
            slime.Flip();
        else if(PlayerManager.Instance.player.transform.position.x < slime.transform.position.x && slime.facingDir == 1)
            slime.Flip();

        startTimer -= Time.deltaTime;

        if (startTimer < 0 && triggerCalled)
        {
            if (!slime.CanAttack())
            {
                enemyStateMachine.ChangeState(slime.battleState);
            }
        }
           
          
    }

    public override void Exit()
    {
        base.Exit();
    }
}
