using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(PlayerStateMachine playerStateMachine, Player player, string ainmBoolName) : base(playerStateMachine, player, ainmBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        rb.velocity = new Vector2(rb.velocity.x, player.jumpForce);
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
        
        if (rb.velocity.y < 0)
            playerStateMachine.ChangeState(player.airState);
    }
}
