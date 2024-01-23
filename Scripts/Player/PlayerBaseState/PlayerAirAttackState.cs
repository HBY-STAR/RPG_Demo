using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirAttackState : PlayerState
{
    public PlayerAirAttackState(PlayerStateMachine playerStateMachine, Player player, string ainmBoolName) : base(playerStateMachine, player, ainmBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        player.SetVelocity(0,player.airAttackSpeed);
        
        player.EnableShurikanTrail();
    }

    public override void Exit()
    {
        player.UnableShurikanTrail();
        
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(rb.velocity.y == 0)
            playerStateMachine.ChangeState(player.idleState);
    }
}
