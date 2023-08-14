using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Ranged : Entity
{
    public E1V2_MoveState moveState {  get; private set; }
    public E1V2_IdleState idleState { get; private set; }

    public E1V2_PlayerDetectedState playerDetectedState { get; private set; }


    [SerializeField]
    private DataFor_MoveState moveStateData;

    [SerializeField]
    private DataFor_IdleState idleStateData;

    [SerializeField]
    private DataFor_PlayerDetectedState playerDetectedStateData;


    public override void  Start()
    {
        base.Start();


        moveState = new E1V2_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new E1V2_IdleState(this,stateMachine, "idle",idleStateData, this);
        playerDetectedState = new E1V2_PlayerDetectedState(this,stateMachine,"playerDetected",playerDetectedStateData, this);

        stateMachine.Initialize(moveState);
    }

    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);
    }

    public override void Respawn()
    {
        base.Respawn();
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }

    public override void Update()
    {
        base.Update();
    }
}
