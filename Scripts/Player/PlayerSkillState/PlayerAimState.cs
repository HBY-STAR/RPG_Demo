using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimState : PlayerState
{
    public PlayerAimState(PlayerStateMachine playerStateMachine, Player player, string ainmBoolName) : base(playerStateMachine, player, ainmBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        SkillManager.Instance.shurikanSkill.DotsActive(true);
    }

    public override void Update()
    {
        base.Update();
        
        player.SetVelocity(0,0);

        if (Input.GetKeyUp(KeyCode.Mouse2))
        {
            player.UnableShurikanImage();
            playerStateMachine.ChangeState(player.idleState);
        }

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        if(mousePosition.x > player.transform.position.x && player.facingDir == -1)
            player.Flip();
        else if(mousePosition.x < player.transform.position.x && player.facingDir == 1)
            player.Flip();

    }

    public override void Exit()
    {
        base.Exit();
        
        player.StartCoroutine("BusyFor", 0.3f);

    }
}
