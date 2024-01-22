using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region Components

    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }
    
    public Collider2D cd { get; private set; }
    public CharacterStats stats { get; private set; }

    #endregion
    
    [Header("Collision check")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    
    public int facingDir { get; private set; } = 1;
    protected bool _facingRight = true;

    public System.Action OnFilped;

    protected virtual void Awake()
    {
    }

    protected virtual void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<Collider2D>();
        stats = GetComponent<CharacterStats>();
    }

    protected virtual void Update()
    {
    }
    
    public virtual void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.velocity = new Vector2(xVelocity, yVelocity);
        FlipController(xVelocity);
    }

    #region Filp

    public virtual void Flip()
    {
        facingDir = facingDir * (-1);
        _facingRight = !_facingRight;
        transform.Rotate(0, 180, 0);
        if(OnFilped != null)
            OnFilped();
    }

    public virtual void FlipController(float x)
    {
        if (x > 0 && !_facingRight)
            Flip();
        else if (x < 0 && _facingRight)
            Flip();
    }

    #endregion
    
    #region GroundWallCheck
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position,
            new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position,
            new Vector3(wallCheck.position.x + wallCheckDistance * facingDir, wallCheck.position.y));
    }
    
    public bool IsGroundDetected() =>
        Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

    public bool IsWallDetected() =>
        Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);
    
    #endregion

}
