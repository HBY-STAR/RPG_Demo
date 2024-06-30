using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(PlayerStateMachine playerStateMachine, Player player, string ainmBoolName) : base(playerStateMachine, player, ainmBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        
        player.DashController();
        
        if (Input.GetKeyDown(KeyCode.Mouse2) && HasNoShurikan() && player.IsGroundDetected() )
            playerStateMachine.ChangeState(player.aimState);
        if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected() )
            playerStateMachine.ChangeState(player.jumpState);
        if (Input.GetKeyDown(KeyCode.Mouse0) && player.IsGroundDetected() )
            playerStateMachine.ChangeState(player.attackState);
        
        if(!player.IsGroundDetected())
            playerStateMachine.ChangeState(player.airState);
    }

    private bool HasNoShurikan()
    {
        if (!player.shurikan)
        {
            return true;
        }
        player.shurikan.GetComponent<ShurikanSkillController>().ReturnShurikan();
        return false;
    }
}
