using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(PlayerStateMachine playerStateMachine, Player player, string ainmBoolName) : base(playerStateMachine, player, ainmBoolName)
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
        
        if (xInput !=0 )
            player.SetVelocity(xInput*0.8f,rb.velocity.y);
        
        
        if(player.IsWallDetected())
            playerStateMachine.ChangeState(player.wallSlideState);

        if (player.IsGroundDetected())
            playerStateMachine.ChangeState(player.idleState);
    }
}
