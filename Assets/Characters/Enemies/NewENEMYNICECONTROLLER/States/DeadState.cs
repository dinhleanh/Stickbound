using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class DeadState : State
{
    protected DataForDeadState stateData;
    GameManager gameManager;
    //public Transform lol;
    
    public DeadState(Entity _entity, FiniteStateMachine _stateMachine, string _animBoolName, DataForDeadState _stateData) : base(_entity, _stateMachine, _animBoolName)
    {
        this.stateData = _stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
    }

    public override void Enter()
    {
        base.Enter();

        GameObject.Instantiate(stateData.deathBloodParticle, entity.aliveGO.transform.position,stateData.deathBloodParticle.transform.rotation);
        GameObject.Instantiate(stateData.deathChunkParticle, entity.aliveGO.transform.position, stateData.deathChunkParticle.transform.rotation);


        //lol = entity.aliveGO.transform;
        //GameObject.Destroy(entity.gameObject);
        
        //entity.aliveGO.SetActive(false);

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
