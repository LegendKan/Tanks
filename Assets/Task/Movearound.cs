using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Movearound : Action {
    public AIController aiCtl;
    private NavMeshAgent agent;
    public override void OnAwake()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }
    public override void OnStart()
    {
        aiCtl = GetComponent<AIController>();
        agent.speed = aiCtl.GetMoveSpeed();
        agent.angularSpeed = aiCtl.GetBodyRotateSpeed();
        agent.enabled = true;
        agent.destination = aiCtl.GetCurrentHealthTransform().position;
    }
    public override TaskStatus OnUpdate()
    {
        if (!agent.pathPending && agent.remainingDistance < 2)
        {
            return TaskStatus.Success;
        }

        if (aiCtl.GetCurrentHealthTransform() != null)
        {
            agent.destination = aiCtl.GetCurrentHealthTransform().position;
        }
        return TaskStatus.Running;
    }

    public override void OnEnd()
    {
        agent.enabled = false;
    }
}
