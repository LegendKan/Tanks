﻿using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class follow15m : Action
{

    public AIController aiCtrl;

    //nav
    private NavMeshAgent navMeshAgent;
    public float offsetDistance = 0.1f;


    public override void OnAwake()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();

    }


    public override void OnStart()
    {
        aiCtrl = this.GetComponent<AIController>();

        offsetDistance = aiCtrl.GetShellRange();

        navMeshAgent.speed = aiCtrl.GetMoveSpeed();
        navMeshAgent.angularSpeed = aiCtrl.GetBodyRotateSpeed();
        navMeshAgent.enabled = true;
        navMeshAgent.destination = aiCtrl.GetEnemyTransform().position;
        navMeshAgent.stoppingDistance = 10.0f;
    }


    public override TaskStatus OnUpdate()
    {
        if (!aiCtrl.IsAimed(aiCtrl.GetEnemyTransform().position))
        {
            aiCtrl.RotateTurret(aiCtrl.GetEnemyTransform().position);
        }
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance == 10.0f)
        {
            return TaskStatus.Success;
        }

        if (aiCtrl.GetEnemyTransform() != null)
        {
            navMeshAgent.destination = aiCtrl.GetEnemyTransform().position;
        }
        return TaskStatus.Running;
    }

    public override void OnEnd()
    {
        navMeshAgent.enabled = false;
    }



}
