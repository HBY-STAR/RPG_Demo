using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    private bool jumpOnce;
    public PlayerAirState(PlayerStateMachine playerStateMachine, Player player, string ainmBoolName) : base(playerStateMachine, player, ainmBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        jumpOnce = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        
        player.animator.SetFloat("yVelocity",rb.velocity.y);

        player.DashController();
        
        if (xInput !=0 )
            player.SetVelocity(xInput*0.8f,rb.velocity.y);
        
        if (Input.GetKeyDown(KeyCode.Mouse0) && player.canAirAttack)
            playerStateMachine.ChangeState(player.airAttackState);
        
        if (Input.GetKeyDown(KeyCode.Space) && !jumpOnce)
        {
            rb.velocity = new Vector2(0, player.jumpForce);
            jumpOnce = true;
        }
        
        if(player.IsWallDetected())
            playerStateMachine.ChangeState(player.wallSlideState);

        if (player.IsGroundDetected())
            playerStateMachine.ChangeState(player.idleState);
    }
}
