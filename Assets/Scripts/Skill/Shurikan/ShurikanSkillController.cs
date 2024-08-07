using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikanSkillController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private Collider2D cd;
    private Player player;

    private bool canRotate = true;
    private bool isReturning=false;
    private float returnSpeed = 30f;
    
    private int shurikanDamage = 3;

    
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<Collider2D>();
    }

    public void SetUpShurikan(Vector2 dir,float gravityScale, Player player)
    {
        this.player = player;
        rb.velocity = dir;
        rb.gravityScale = gravityScale;
    }

    public void ReturnShurikan()
    {
        rb.isKinematic = false;
        rb.gravityScale = 0;
        transform.parent = null;
        isReturning = true;
    }

    private void Update()
    {        
        if(canRotate)
            Rotate();

        if (isReturning)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position,
                returnSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, player.transform.position) < 0.2f)
            {
                player.CatchShurikan();
            }
        }
        
    }
    
    private void Rotate()
    {
        transform.Rotate(0f, 0f, 10f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckDamage(collision);
        
        if (isReturning)
            return;

        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            canRotate = false;
            rb.isKinematic = true;
        
            transform.localScale = Vector3.one;
        
            rb.constraints = RigidbodyConstraints2D.FreezeAll;

            transform.localScale = Vector3.one;
        }
    }

    private void CheckDamage(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            if (rb.velocity.x != 0 || isReturning)
            {
                collision.GetComponent<CharacterStats>().TakeDamage(player.stats.damage.GetValue()*shurikanDamage);
                collision.GetComponent<Enemy>().OnDamage();
            }
        }
    }
}