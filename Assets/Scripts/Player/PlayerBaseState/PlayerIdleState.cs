using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(PlayerStateMachine playerStateMachine, Player player, string ainmBoolName) : base(playerStateMachine, player, ainmBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(0,rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(xInput != 0 && !player.isBusy)
            playerStateMachine.ChangeState(player.moveState);
    }
}
