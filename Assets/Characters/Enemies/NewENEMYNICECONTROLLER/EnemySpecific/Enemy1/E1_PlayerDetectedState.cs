using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Build.Content;
using UnityEngine;

public class E1_PlayerDetectedState : PlayerDetectedState
{
    private Enemy1 enemy;
    private bool ground;
    public E1_PlayerDetectedState(Entity _entity, FiniteStateMachine _stateMachine, string _animBoolName, DataFor_PlayerDetectedState _stateData, Enemy1 _enemy) : base(_entity, _stateMachine, _animBoolName, _stateData)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        //entity.SetVelocity(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        ground = enemy.CheckGround();
        if(performCloseRangeAction && !enemy.isHopping && ground)
        {
            stateMachine.ChangeState(enemy.meleeAttackState);
        }
        else
        if (performLongRangeAction)
        {
            stateMachine.ChangeState(enemy.chargeState);
        }
        else
        if (!isPlayerInMaxAgrorange)
        {
            stateMachine.ChangeState(enemy.lookForPlayerState);
        }
        else 
        if(!isDetectingLedge)
        {
            // HIER KANN MAN BUG BEHEBEN IN DEM DER GEGNER DEN SPIELER AUF DER ANDEREN SEITE DER PLATTFORM ERKENNT

            //entity.Flip();
            //stateMachine.ChangeState(enemy.moveState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
