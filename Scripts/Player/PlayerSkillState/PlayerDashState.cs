using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(PlayerStateMachine playerStateMachine, Player player, string ainmBoolName) : base(playerStateMachine, player, ainmBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        startTimer = player.dashDuration;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        
        player.SetVelocity(player.dashSpeed * player.dashDir ,rb.velocity.y);

        if (startTimer < 0)
        {
            if (player.IsGroundDetected())
            {
                playerStateMachine.ChangeState(player.idleState);
            }
            else
            {
                player.SetVelocity(player.dashSpeed*player.dashDir/4,rb.velocity.y);
                playerStateMachine.ChangeState(player.airState);
            }
            
        }
    }
}
