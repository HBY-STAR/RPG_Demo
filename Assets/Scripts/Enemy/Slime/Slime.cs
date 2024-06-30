using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    [Header("Player dectected")]
    [SerializeField] protected Transform playerCheck;
    [SerializeField] protected float playerCheckDistance;
    [SerializeField] public LayerMask whatIsPlayer;
    
    [Header("Move info")]
    public float moveSpeed;
    public float jumpForce;

    [Header("Attack info")] 
    [SerializeField] public Transform attackCheck;
    [SerializeField] public float attackDistance;
    
    #region States
    public SlimeIdleState idleState { get; private set; }
    public SlimeMoveState moveState { get; private set; }
    public SlimeAttackState attackState { get; private set; }
    public SlimeBattleState battleState { get; private set; }
    public SlimeHitState hitState { get; private set; }
    
    #endregion
    
    protected override void Awake()
    {
        base.Awake();

        idleState = new SlimeIdleState(stateMachine,this,"Idle",this);
        moveState = new SlimeMoveState(stateMachine, this, "Move", this);
        attackState = new SlimeAttackState(stateMachine, this, "Attack", this);
        battleState = new SlimeBattleState(stateMachine, this, "Move", this);
        hitState = new SlimeHitState(stateMachine, this, "Hit", this);
    }

    protected override void Start()
    {
        base.Start();
        
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        
    }
    
    public bool IsPlayerDetected()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(playerCheck.position, playerCheckDistance, whatIsPlayer);

        if (playerCollider != null)
        {
            return true;
        }

        return false;
    }

    public bool CanAttack()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(attackCheck.position, attackDistance, whatIsPlayer);

        if (playerCollider != null)
        {
            return true;
        }

        return false;
    }
    

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        
        Gizmos.color = Color.cyan;


        Gizmos.DrawWireSphere(playerCheck.position, playerCheckDistance);
        Gizmos.DrawWireSphere(attackCheck.position, attackDistance);
    }

    public override void OnDamage()
    {
        base.OnDamage();
        stateMachine.ChangeState(hitState);

        if (stats.currentHeath <= 0)
        {
            Die();
        }
    }

    protected override void Die()
    {
        cd.enabled = false;
        rb.velocity = new Vector2(0, -10f);
    }
    
   
}
