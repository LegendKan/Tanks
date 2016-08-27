using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class seekToHealthBag : Action
{

    public AIController aiCtrl;

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

    }


    public override TaskStatus OnUpdate()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < aiCtrl.GetShellRange())
        {
            return TaskStatus.Success;
        }

        if (aiCtrl.GetCurrentHealthTransform() != null)
        {
            navMeshAgent.destination = aiCtrl.GetCurrentHealthTransform().position;
        }
        return TaskStatus.Running;
    }

    public override void OnEnd()
    {
        navMeshAgent.enabled = false;
    }



}
