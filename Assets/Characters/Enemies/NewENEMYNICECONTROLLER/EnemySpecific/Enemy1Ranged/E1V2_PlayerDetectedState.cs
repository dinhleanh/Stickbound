using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1V2_PlayerDetectedState : PlayerDetectedState
{

    private Enemy1Ranged enemy;
    public E1V2_PlayerDetectedState(Entity _entity, FiniteStateMachine _stateMachine, string _animBoolName, DataFor_PlayerDetectedState _stateData, Enemy1Ranged enemy) : base(_entity, _stateMachine, _animBoolName, _stateData)
    {
        this.enemy = enemy;
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
