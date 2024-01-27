using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Timers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class Player : Entity
{
    [Header("Attack info")] 
    [SerializeField] public Vector2[] attackMovement;
    [SerializeField] public Transform attackCheck;
    [SerializeField] public float attackDistance;
    [SerializeField] public LayerMask whatIsEnemy;
    [SerializeField] public float airAttackDistance;
    [SerializeField] public float airAttackSpeed;
    [SerializeField] public float airAttackStrength;
    public bool canAirAttack;

    [Header("Move info")] 
    public float moveSpeed = 4f;
    public float jumpForce = 8f;

    [Header("Dash info")] public float dashSpeed = 25f;
    public float dashDuration = 0.2f;
    public float dashDir;
    public ParticleSystem dashGhostEffect;
    
    #region States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerAttackState attackState { get; private set; }
    public PlayerAimState aimState { get; private set; }
    public PlayerCatchState catchState { get; private set; }
    
    public PlayerAirAttackState airAttackState { get; private set; }

    #endregion
    
    #region Sword_Shurikan_info
    public Transform sword { get; private set; }
    public TrailRenderer swordTrailRenderer { get; private set; }
    public Transform shurikan_image { get; private set; }
    public SpriteRenderer shurikan_image_sprite_render { get; private set; }
    
    public List<TrailRenderer> shurikan_image_trail = new List<TrailRenderer>();
    
    #endregion


    protected override void Awake()
    {
        base.Awake();
        
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(stateMachine, this, "Idle");
        moveState = new PlayerMoveState(stateMachine, this, "Move");
        jumpState = new PlayerJumpState(stateMachine, this, "Jump");
        airState = new PlayerAirState(stateMachine, this, "Air");
        dashState = new PlayerDashState(stateMachine, this, "Dash");
        wallSlideState = new PlayerWallSlideState(stateMachine, this, "WallSlide");
        attackState = new PlayerAttackState(stateMachine, this, "Attack");
        aimState = new PlayerAimState(stateMachine, this, "ShurikenAim");
        catchState = new PlayerCatchState(stateMachine, this, "ShurikenCatch");
        airAttackState = new PlayerAirAttackState(stateMachine, this, "AirAttack");
    }


    protected override void Start()
    {
        base.Start();
        
        // get sword and shurikan
        for (int i = 0; i < animator.GetComponent<SpriteSkin>().rootBone.childCount; i++)
        {
            Transform childBone = animator.GetComponent<SpriteSkin>().rootBone.GetChild(i);
            if (childBone.name == "sword")
            {
                sword = childBone;
            }
            if (childBone.name == "shurikan")
            {
                shurikan_image = childBone;
            }
        }
        
        shurikan_image_sprite_render = shurikan_image.GetComponent<SpriteRenderer>();
        swordTrailRenderer = sword.GetComponentInChildren<TrailRenderer>();

        for (int i = 0; i < shurikan_image.childCount; i++)
        {
            shurikan_image_trail.Add(shurikan_image.GetChild(i).GetComponentInChildren<TrailRenderer>());
        }

        EnableShurikanImage();
        UnableSwordTrail();
        UnableShurikanTrail();
        
        // dash ghost
        dashGhostEffect = GetComponent<ParticleSystem>();
        dashGhostEffect.Stop();       

        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        
        stateMachine.currentState.Update();
    }

    public override void SetVelocity(float xVelocity, float yVelocity)
    {
        base.SetVelocity(xVelocity * moveSpeed, yVelocity);
    }

    #region ShurikanPrefabController
    public GameObject shurikan { get; private set; }

    public void ThrowShurikan(GameObject newShurikan)
    {
        if(!shurikan)
            shurikan = newShurikan;
    }

    public void CatchShurikan()
    {
        if (shurikan)
        {
            stateMachine.ChangeState(catchState);
            Destroy(shurikan); 
        }
          
    }

    #endregion
    
    #region SwordTrailController

    public void EnableSwordTrail()
    {
        if(swordTrailRenderer)
            swordTrailRenderer.enabled = true;
    }
    
    public void UnableSwordTrail()
    {
        if(swordTrailRenderer)
            swordTrailRenderer.enabled = false;
    }
    
   
    
    #endregion

    #region ShurikanImageController

    public void EnableShurikanImage()
    {
        if (shurikan_image_sprite_render)
        {
            shurikan_image_sprite_render.enabled = true;
            canAirAttack = true;
        }
    }
    
    public void UnableShurikanImage()
    {
        if (shurikan_image_sprite_render)
        {
            shurikan_image_sprite_render.enabled = false;
            canAirAttack = false;
        }
    }

    public void EnableShurikanTrail()
    {
        if (shurikan_image_trail.Count == 4)
        {
            foreach (var trail in shurikan_image_trail)
            {
                trail.enabled = true;
            }
        }
    }
    
    public void UnableShurikanTrail()
    {
        if (shurikan_image_trail.Count == 4)
        {
            foreach (var trail in shurikan_image_trail)
            {
                trail.enabled = false;
            }
        }
    }

    #endregion

    #region isBusy
    public bool isBusy { get; private set; }

    public IEnumerator BusyFor(float seconds)
    {
        isBusy = true;

        yield return new WaitForSeconds(seconds);

        isBusy = false;
    }

    #endregion

    public void AnimationTrigger()
    {
        stateMachine.currentState.AnimationFinished();
    }


    public void DashController()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && SkillManager.Instance.dashSkill.CanUseSkill())
        {
            SkillManager.Instance.dashSkill.UseSkill();
            dashDir = Input.GetAxisRaw("Horizontal");

            if (dashDir == 0)
                dashDir = facingDir;
            else if (dashDir < 0)
                dashDir = -1;
            else
                dashDir = 1;

            stateMachine.ChangeState(dashState);
        }
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        
        Gizmos.DrawWireSphere(attackCheck.position, attackDistance);
        Gizmos.DrawWireSphere(transform.position, airAttackDistance);
    }
}