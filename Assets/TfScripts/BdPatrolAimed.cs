using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class BdPatrolAimed : Action
{

    public AIController aiCtr;
    private float mSpeed;

    public override void OnStart()
    {
        aiCtr = this.GetComponent<AIController>();
        mSpeed = aiCtr.GetMoveSpeed();
    }


    public override TaskStatus OnUpdate()
    {

        if (aiCtr.GetEnemyTransform() == null)
        {
            return TaskStatus.Failure;
        }

        //瞄准
        if (!aiCtr.IsAimedEnemy())
        {
            aiCtr.RotateTurret(aiCtr.GetEnemyTransform().position);
            //barrier between tanks
            return TaskStatus.Running;
        }

        return TaskStatus.Success;
    }

    public override void OnTriggerEnter(Collider other)
    {
        Debug.Log("enemy");
        mSpeed = 0f - mSpeed;
    }


}
