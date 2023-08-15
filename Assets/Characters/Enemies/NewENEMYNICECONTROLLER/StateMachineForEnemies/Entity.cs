using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
//using UnityEditor.U2D.Path;
using UnityEngine;

public class Entity : MonoBehaviour
{
    // Things that every Enemy has!!!
   // Set velocity oder isGorunded usw... Dinge die jeder Enemy haben sollte

    public FiniteStateMachine stateMachine;

    public DataFor_Entity entityData;
    public int facingDirection {  get; private set; }
    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }
    public GameObject aliveGO { get; private set; }

    public AnimationToStateMachine atsm { get; private set; }

    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Transform ledgeCheck;

    [SerializeField]
    private Transform playerCheck;

    [SerializeField]
    private Transform groundCheck;

    [SerializeField]
    private Transform playerBehindCheck;



    public bool isHopping = false;

    [SerializeField]
    private float currentHealth;

    private float currentStunResistance;

    private float lastDamageTime;

    public int lastDamageDirection { get; private set; }    
    

    private Vector2 velocityWorkspace;


    protected bool isStunned;

    protected bool isDead;


    private GameManager gameManager;


   







    public virtual void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        
        facingDirection = 1;

        currentHealth = entityData.maxHealth;
        currentStunResistance = entityData.stunResistance;

        aliveGO = transform.Find("Alive").gameObject; // GAME OBJECT MUSS AUCH ALIVE HEI?EN!!!!!
        rb = aliveGO.GetComponent<Rigidbody2D>(); 
        anim = aliveGO.GetComponent <Animator>();
        atsm = aliveGO.GetComponent<AnimationToStateMachine>();
        

        stateMachine = new FiniteStateMachine();
    }

    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();


        anim.SetFloat("yVelocity", rb.velocity.y);

        
        if (Time.time >= lastDamageTime + entityData.stunRecoveryTime)
        {
            ResetStunResistance();
        }

        if (gameManager.respawn)
        {
            currentHealth = entityData.maxHealth;
            
        }

       
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    public virtual void SetVelocity(float _velocity)
    {
        velocityWorkspace.Set(facingDirection * _velocity, rb.velocity.y); // vll hier fehler!
        rb.velocity = velocityWorkspace;
    }

    // Für Knockback hernehmen
    public virtual void SetVelocity(float velocity,Vector2 angle, int direction)
    {
        angle.Normalize();

        velocityWorkspace.Set(angle.x * velocity * direction, angle.y * velocity);
        rb.velocity = velocityWorkspace;
    }

    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, aliveGO.transform.right,entityData.wallCheckDistance, entityData.whatisGround);
    }

    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down,entityData.ledgeCheckDistance,entityData.whatisGround);
    }

    public virtual bool CheckGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position,entityData.groundCheckRadius,entityData.whatisGround);
    }

    public virtual bool CheckPlayerInMinAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right,entityData.minAgroDistance,entityData.whatIsPlayer);
    }

    public virtual bool CheckPlayerInMaxAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.maxAgroDistance, entityData.whatIsPlayer);

    }

    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.Raycast(playerCheck.position,aliveGO.transform.right,entityData.closeRangeActionDistance,entityData.whatIsPlayer);
    }

    public virtual bool CheckPlayerBehind()
    {
        return Physics2D.Raycast(playerBehindCheck.position, aliveGO.transform.right *-1, entityData.playerBehindCheckDistance, entityData.whatIsPlayer);
    }

    public virtual void ResetStunResistance()
    {
        isStunned = false;
        currentStunResistance = entityData.stunResistance;
    }

    public virtual void DamageHop(float velocity)
    {
        isHopping = true;
        velocityWorkspace.Set(rb.velocity.x, velocity);
        rb.velocity = velocityWorkspace;
    }

    public virtual void Damage(AttackDetails attackDetails)
    {
        lastDamageTime = Time.time;

        currentHealth -= attackDetails.damageAmount;
        currentStunResistance -= attackDetails.stunDamageAmount;

        DamageHop(entityData.damageHopSpeed);
        //isHopping = false;

        Instantiate(entityData.hitParticle, aliveGO.transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));

        if(attackDetails.position.x>aliveGO.transform.position.x)
        {
            lastDamageDirection = -1;
        }
        else
        {
            lastDamageDirection = 1;
        }

        if(currentStunResistance <= 0f)
        {
            isStunned = true;
        }

        if(currentHealth <= 0f)
        {
            isDead = true;
        }

    }

    



    public virtual void Respawn()
    {
        if(gameManager.respawn && aliveGO.activeInHierarchy == false)
        {
            isDead = false;
            aliveGO.SetActive(true);
            currentHealth = entityData.maxHealth;
            currentStunResistance = entityData.stunResistance;
        }
    }

    public virtual float WhatIsCurrentHealth()
    {
        return currentHealth;
    }

    public virtual void Flip()
    {
        facingDirection *= -1;
        aliveGO.transform.Rotate(0f, 180f, 0f);
    }

    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * facingDirection*entityData.wallCheckDistance));
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down  * entityData.ledgeCheckDistance));
        Gizmos.DrawLine(playerBehindCheck.position, playerBehindCheck.position + (Vector3)(Vector2.left * facingDirection * entityData.playerBehindCheckDistance));

        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.closeRangeActionDistance),0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.minAgroDistance), 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.maxAgroDistance), 0.2f);

        
    }



    
}
