using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    private int _comboCounter = 0 ;
    private float _comboWindows = 0.3f;
    private float _lastTimeAttacked;
    
    public PlayerAttackState(PlayerStateMachine playerStateMachine, Player player, string ainmBoolName) : base(playerStateMachine, player, ainmBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        player.EnableSwordTrail();
        
        if (_comboCounter > 2 || Time.time > _comboWindows + _lastTimeAttacked)
            _comboCounter = 0;

        float attackDir = player.facingDir;

        if (xInput != 0)
        {
            if (xInput > 0)
                attackDir = 1;
            else
                attackDir = -1;
        }
        
        player.animator.SetInteger("ComboCounter",_comboCounter);
        
        player.SetVelocity(player.attackMovement[_comboCounter].x*attackDir,player.attackMovement[_comboCounter].y);

    }

    public override void Update()
    {
        base.Update();
        
        if (_comboCounter == 2)
        {
            float attackDir = player.facingDir;

            if (xInput != 0)
            {
                if (xInput > 0)
                    attackDir = 1;
                else
                    attackDir = -1;
            }
            
            player.SetVelocity(player.attackMovement[_comboCounter].x*attackDir,player.attackMovement[_comboCounter].y);
        }
        
        if(triggerCalled)
            playerStateMachine.ChangeState(player.idleState);
    }

    public override void Exit()
    {
        player.UnableSwordTrail();

        
        _comboCounter++;
        _lastTimeAttacked = Time.time;

        player.StartCoroutine("BusyFor", 0.2f);
        
        base.Exit();
        
    }
}