using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCatchState : PlayerState
{
    private Transform shurikan;
    
    public PlayerCatchState(PlayerStateMachine playerStateMachine, Player player, string ainmBoolName) : base(playerStateMachine, player, ainmBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        shurikan = player.shurikan.transform;
        
        if(shurikan.position.x > player.transform.position.x && player.facingDir == -1)
            player.Flip();
        else if(shurikan.position.x < player.transform.position.x && player.facingDir == 1)
            player.Flip();

        rb.velocity = new Vector2(1f * -player.facingDir, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();
        
        if(triggerCalled)
            playerStateMachine.ChangeState(player.idleState);
    }

    public override void Exit()
    {
        base.Exit();
        
        player.EnableShurikanImage();
        
        player.StartCoroutine("BusyFor", 0.1f);
    }
}
