using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemySelbstVersuch : MonoBehaviour
{
    public float maxHealth = 20;
    [SerializeField]
    private float currentHealth;

    public GameObject aliveV2;

    private bool isDead = false;

    public float speed;
    public Transform[] patrolPoints;
    public float waitTime = 1f;
    private int currentPointIndex = 0;
    private Rigidbody2D rb;
    private GameObject player;
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
        rb = aliveV2.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        currentHealth = maxHealth;
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        MoveAndShoot();
        //Die();
        isDead = false;
        
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

    private void Patrol()
    {
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
        if(!isDead)
        {
            
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
        aliveV2.SetActive(true);
        currentHealth = maxHealth;
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