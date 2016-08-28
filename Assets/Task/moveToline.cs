using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class moveToline : Action {
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
        navMeshAgent.speed = aiCtrl.GetMoveSpeed();
        navMeshAgent.angularSpeed = aiCtrl.GetBodyRotateSpeed();
        navMeshAgent.enabled = true;
        navMeshAgent.destination = new Vector3(2*aiCtrl.GetTransform().position.x - aiCtrl.GetEnemyTransform().position.x, 2 * aiCtrl.GetTransform().position.y - aiCtrl.GetEnemyTransform().position.y, 2 * aiCtrl.GetTransform().position.z - aiCtrl.GetEnemyTransform().position.z);

    }


    public override TaskStatus OnUpdate()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f)
        {
            return TaskStatus.Success;
        }

        if (aiCtrl.GetEnemyTransform() != null)
        {
            navMeshAgent.destination = new Vector3(2 * aiCtrl.GetTransform().position.x - aiCtrl.GetEnemyTransform().position.x, 2 * aiCtrl.GetTransform().position.y - aiCtrl.GetEnemyTransform().position.y, 2 * aiCtrl.GetTransform().position.z - aiCtrl.GetEnemyTransform().position.z);
        }
        return TaskStatus.Running;
    }

    public override void OnEnd()
    {
        navMeshAgent.enabled = false;
    }
}
