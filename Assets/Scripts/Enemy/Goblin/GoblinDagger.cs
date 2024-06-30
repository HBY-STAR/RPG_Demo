using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinDagger : Enemy
{
    [Header("Player dectected")]
    [SerializeField] protected Transform playerCheck;
    [SerializeField] protected float playerCheckDistance;
    [SerializeField] public LayerMask whatIsPlayer;
    
    [Header("Move info")]
    public float moveSpeed;

    [Header("Attack info")] 
    [SerializeField] public Transform attackCheck;
    [SerializeField] public float attackDistance;
    
    #region States
    public GoblinDaggerIdleState idleState { get; private set; }
    public GoblinDaggerMoveState moveState { get; private set; }
    public GoblinDaggerAttackState attackState { get; private set; }
    public GoblinDaggerHitState hitState { get; private set; }
    public GoblinDaggerBattleState battleState { get; private set; }
    
    #endregion
    
    protected override void Awake()
    {
        base.Awake();

        idleState = new GoblinDaggerIdleState(stateMachine, this, "Idle", this);
        moveState = new GoblinDaggerMoveState(stateMachine, this, "Move", this);
        attackState = new GoblinDaggerAttackState(stateMachine, this, "Attack", this);
        hitState = new GoblinDaggerHitState(stateMachine, this, "Hit", this);
        battleState = new GoblinDaggerBattleState(stateMachine, this, "Move", this);
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
