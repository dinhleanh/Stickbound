using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FlyingEnemySelbstVersuch : MonoBehaviour
{
    public float maxHealth = 20;
    [SerializeField]
    private float currentHealth;

    public GameObject aliveV2;

    private bool isDead = false;

    public Animator animator;


    private bool isFlying;
    private bool isAttacking;




    public float footstepDelay = 0.2f; // Die Verzögerung zwischen den Fußschritten
    private float lastFootstepTime;


    public float SoundRange = 20f;







    public float speed;
    public Transform[] patrolPoints;
    public float waitTime = 1f;
    private int currentPointIndex = 0;
    private Rigidbody2D rb;
    private GameObject player;
    private PlayerStats playerStats;
    public LayerMask WhatIsPlayer;
    public LayerMask WhatIsGround;
    public float ShootingRange = 7f;
    public GameObject bullet;
    public Transform bulletPos;
    private float timeSinceLastShot = 0f;

    private bool isPlayerVisible = false;

    private float patrolTimer = 0f;

    private void Awake()
    {

        aliveV2 = transform.Find("AliveV2").gameObject;
        animator = aliveV2.GetComponent<Animator>();
        rb = aliveV2.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<PlayerStats>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        PlaySoundInRange();
        timeSinceLastShot += Time.deltaTime;
        if (playerStats.currentHealth > 0f)
        {
            MoveAndShoot();
            animator.SetBool("attacking", isAttacking); // Setze die Attack-Animation im Animator
        }
        else
        {
            isAttacking = false; // Zurücksetzen der isAttacking-Variable
            animator.SetBool("attacking", false); // Deaktivieren der Attack-Animation
        }
    }

    private void MoveAndShoot()
    {
        float distance = Vector2.Distance(aliveV2.transform.position, player.transform.position);
        //isPlayerVisible = false;

        if (distance < ShootingRange)
        {
            Vector2 directionToPlayer = (player.transform.position - aliveV2.transform.position).normalized;

            RaycastHit2D hitWall = Physics2D.Raycast(rb.position, directionToPlayer, ShootingRange, WhatIsGround);
            RaycastHit2D hitPlayer = Physics2D.Raycast(rb.position, directionToPlayer, ShootingRange, WhatIsPlayer);
            
            if (hitPlayer.collider != null && timeSinceLastShot >= 1f)
            {
                
                if (!hitWall || hitPlayer.distance < hitWall.distance)
                {
                    Shoot();
                    timeSinceLastShot = 0f;
                    isPlayerVisible = true;
                }
                else
                {
                    isPlayerVisible = false;
                }
            }
        }
        else
        {
            isPlayerVisible = false;
        }

        if (isPlayerVisible)
        {
            Vector2 directionToPlayer = (player.transform.position - aliveV2.transform.position).normalized;
            FlipEnemy(directionToPlayer);

            // Stand still in the air
            rb.velocity = Vector2.zero;
            //Patrol();
        }
        else
        {
            Patrol();
        }
    }



    public void PlaySoundInRange()
    {
        Vector2 directionToPlayer = (player.transform.position - aliveV2.transform.position).normalized;

        RaycastHit2D hitPlayer = Physics2D.Raycast(rb.position, directionToPlayer, SoundRange, WhatIsPlayer);

        bool isInSoundRange;

        if (hitPlayer.collider != null)
        {
            isInSoundRange = true;
        }
        else
        {
            isInSoundRange = false;
        }



        if (isDead)
        {
            AudioManager.Instance.MuteSound("Enemy2Idle");
        }
        else
        {
            AudioManager.Instance.UnmuteSound("Enemy2Idle");
        }


        if (Time.time - lastFootstepTime >= footstepDelay)
        {
            if(!isDead)
            {
                if (isInSoundRange)
                {
                    AudioManager.Instance.UnmuteSound("Enemy2Idle");
                    AudioManager.Instance.PlaySound("Enemy2Idle");
                }
                else
                {
                    AudioManager.Instance.MuteSound("Enemy2Idle");
                }
                lastFootstepTime = Time.time; // Aktualisiere den Zeitstempel
            }
            
        }



       



    }






    private void Patrol()
    {
        isAttacking = false;
        isFlying = true;

        

        
        animator.SetBool("flying",isFlying);
        Vector2 targetPosition = patrolPoints[currentPointIndex].position;

        if (Vector2.Distance(aliveV2.transform.position, targetPosition) > 0.1f)
        {
            patrolTimer = 0f; // Reset the patrol timer
            Vector2 direction = (targetPosition - (Vector2)aliveV2.transform.position).normalized;
            rb.velocity = direction * speed;

            // Nur drehen, wenn der Spieler nicht sichtbar ist
            if (!isPlayerVisible)
            {
                FlipEnemy(direction);
            }
        }
        else
        {
            patrolTimer += Time.deltaTime;

            if (patrolTimer >= waitTime)
            {
                patrolTimer = 0f;
                currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
            }

            rb.velocity = Vector2.zero;

            // Nur drehen, wenn der Spieler nicht sichtbar ist
            if (!isPlayerVisible)
            {
                FlipEnemy(rb.transform.localScale); // Flip direction if waiting
            }
        }
    }

    private void Shoot()
    {
        isFlying = false;
        
        if(!isDead)
        {
            isAttacking = true;
            animator.SetBool("attacking", isAttacking);
            Instantiate(bullet, bulletPos.position, Quaternion.identity);
        }
        
    }

    private void FlipEnemy(Vector2 direction)
    {
        bool shouldFlip = (direction.x > 0 && !isFacingRight) || (direction.x < 0 && isFacingRight);

        if (shouldFlip)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = aliveV2.transform.localScale;
            localScale.x *= -1;
            aliveV2.transform.localScale = localScale;
        }
    }

    private bool isFacingRight = true;



    public void Respawn()
    {
        
        currentHealth = maxHealth;
        aliveV2.transform.position = patrolPoints[0].position;
        aliveV2.SetActive(true);
        Patrol();
        isDead = false;
    }

    public void Damage (AttackDetails details)
    {
        currentHealth -= details.damageAmount;
        
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        aliveV2.gameObject.SetActive(false);
    }










    private void OnDrawGizmos()
    {
        foreach (Transform patrolPoint in patrolPoints)
        {
            Gizmos.DrawWireSphere(patrolPoint.transform.position, 0.5f);
        }

        Gizmos.DrawWireSphere(transform.position, ShootingRange);
    }
}