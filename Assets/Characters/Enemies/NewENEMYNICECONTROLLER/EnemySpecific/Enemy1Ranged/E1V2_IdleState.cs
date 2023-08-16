using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1V2_IdleState : IdleState
{

    private Enemy1Ranged enemy;
    public E1V2_IdleState(Entity _entity, FiniteStateMachine _stateMachine, string _animBoolName, DataFor_IdleState _stateData, Enemy1Ranged _enemy) : base(_entity, _stateMachine, _animBoolName, _stateData)
    {
        this.enemy = _enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();



        if (isPlayerInMinAgrorange)
        {
            stateMachine.ChangeState(enemy.playerDetectedState);
        }
        else
        if (isIdleTimeOver)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
