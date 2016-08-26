using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class ReloadingAndRound : Action
{
    private NavMeshAgent agent;
    public AIController aiCtr;
    private float mSpeed;

    public override void OnStart()
    {
        agent = GetComponent<NavMeshAgent>();
        aiCtr = this.GetComponent<AIController>();
        mSpeed = aiCtr.GetMoveSpeed();
    }


    public override TaskStatus OnUpdate()
    { 
        if (aiCtr.GetCurrentShellCount() == 0 && !aiCtr.IsReloading())
        {
            aiCtr.ReloadClip();

        }
        if (aiCtr.GetCurrentShellCount() != 0 && !aiCtr.IsReloading())
        {
            return TaskStatus.Success;
        }
        agent.speed = aiCtr.GetMoveSpeed();
        agent.angularSpeed = aiCtr.GetBodyRotateSpeed();
        agent.enabled = true;
        agent.destination = Vector3.zero;


        return TaskStatus.Running;
    }


    public override void OnTriggerEnter(Collider other)
    {
        Debug.Log("yes");
        mSpeed = 0f - mSpeed;
    }


}
