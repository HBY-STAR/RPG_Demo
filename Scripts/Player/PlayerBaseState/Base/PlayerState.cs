using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine playerStateMachine;
    protected Player player;

    protected Rigidbody2D rb;
    protected float xInput;

    private string ainmBoolName;
    protected float startTimer;

    protected bool triggerCalled;
 

    public PlayerState(PlayerStateMachine playerStateMachine, Player player, string ainmBoolName)
    {
        this.playerStateMachine = playerStateMachine;
        this.player = player;
        this.ainmBoolName = ainmBoolName;
    }

    public virtual void Enter()
    {
        player.animator.SetBool(ainmBoolName, true);
        rb = player.rb;
        triggerCalled = false;
    }

    public virtual void Update()
    {
        startTimer -= Time.deltaTime;
        
        xInput = Input.GetAxisRaw("Horizontal");
    }

    public virtual void Exit()
    {
        player.animator.SetBool(ainmBoolName, false);
    }

    public virtual void AnimationFinished()
    {
        triggerCalled = true;
    }

    
}
