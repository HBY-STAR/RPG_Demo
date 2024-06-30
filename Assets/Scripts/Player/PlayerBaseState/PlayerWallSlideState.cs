using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(PlayerStateMachine playerStateMachine, Player player, string ainmBoolName) : base(playerStateMachine, player, ainmBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        
        if(rb.velocity.y<0)
            player.SetVelocity(0,rb.velocity.y*0.3f);


        if (Input.GetKeyDown(KeyCode.Space) && player.IsWallDetected())
        {
            if (xInput > 0 && player.facingDir < 0 || xInput < 0 && player.facingDir > 0)
            {
                player.SetVelocity(player.jumpForce/10 * player.facingDir*(-1f),rb.velocity.y);
                playerStateMachine.ChangeState(player.jumpState);
            }
            else if (xInput > 0 && player.facingDir > 0 || xInput < 0 && player.facingDir < 0 || xInput == 0)
            {
                rb.velocity = new Vector2(0, player.jumpForce);
            }
        }
        
        if(player.IsGroundDetected())
            playerStateMachine.ChangeState(player.idleState);
        if(!player.IsWallDetected())
            playerStateMachine.ChangeState(player.airState);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
