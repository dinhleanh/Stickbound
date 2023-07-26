using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    [SerializeField] private bool combatEnabled;

    private bool gotInput;
    private bool isAttacking;
    private bool isFirstAttack;

    [SerializeField] private float inputTimer, attack1Radius, attack1Damage;

    [SerializeField] private Transform attack1HitBoxPosition;

    [SerializeField] private LayerMask whatIsDamageable;

    private float lastInputTime = Mathf.NegativeInfinity;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("canAttack", combatEnabled);
    }

    private void Update()
    {
        CheckCombatInput();
        CheckAttacks();
        FinishAttack1(); // NORMAL NICHT IM SKRIPT DRINNEN NUR ZU VERANSCHAULICHUNG - UNity MESSAGES ZU ANIMATOR
    }

    private void CheckCombatInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("lol attack1");
            if (combatEnabled)
            {

                //Attempt Combat
                gotInput = true;
                lastInputTime = Time.time;
            }
        }
    }

    private void CheckAttacks()
    {
        if (gotInput)
        {
            // perform Attack1
            if (!isAttacking)
            {
                gotInput = false;
                isAttacking = true;
                isFirstAttack = !isFirstAttack; // Alternate between animations
                anim.SetBool("attack1", true);
                anim.SetBool("firstAttack", isFirstAttack);
                anim.SetBool("isAttacking", isAttacking);

            }

        }

        if (Time.time >= lastInputTime + inputTimer)
        {
            gotInput = false;
        }
    }


    private void CheckAttackHitBox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attack1HitBoxPosition.position, attack1Radius, whatIsDamageable);

        foreach (Collider2D collider in detectedObjects)
        {
            collider.transform.parent.SendMessage("Damage", attack1Damage);
            // Instantiate hit particle
        }
    }

    private void FinishAttack1()
    {

        isAttacking = false;
        anim.SetBool("isAttacking", isAttacking);
        anim.SetBool("attack1", false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attack1HitBoxPosition.position, attack1Radius);
    }
}
