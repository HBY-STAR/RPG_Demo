using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected EnemyStateMachine enemyStateMachine;
    protected Enemy enemy;

    protected Rigidbody2D rb;

    private string ainmBoolName;
    protected float startTimer;

    protected bool triggerCalled;
    
    public EnemyState(EnemyStateMachine enemyStateMachine, Enemy enemy, string ainmBoolName)
    {
        this.enemyStateMachine = enemyStateMachine;
        this.enemy = enemy;
        this.ainmBoolName = ainmBoolName;
    }
    
    public virtual void Enter()
    {
        enemy.animator.SetBool(ainmBoolName, true);
        rb = enemy.rb;
        triggerCalled = false;
    }

    public virtual void Update()
    {
        startTimer -= Time.deltaTime;
    }

    public virtual void Exit()
    {
        enemy.animator.SetBool(ainmBoolName, false);
    }

    public virtual void AnimationFinished()
    {
        triggerCalled = true;
    }
    
}
