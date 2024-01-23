using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public EnemyStateMachine stateMachine;

    protected override void Awake()
    {
        base.Awake();

        stateMachine = new EnemyStateMachine();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        
        stateMachine.currentState.Update();
    }
    
    public void AnimationTrigger()
    {
        stateMachine.currentState.AnimationFinished();
    }
    
    public virtual void OnDamage()
    {
    }
}
