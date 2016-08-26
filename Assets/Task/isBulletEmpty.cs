using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class isBulletEmpty : Conditional
{

    public AIController aiCtr;

    public override void OnStart()
    {
        aiCtr = this.GetComponent<AIController>();
    }


    public override TaskStatus OnUpdate()
    {
        //随时瞄准
        if (aiCtr.IsAimed(aiCtr.GetEnemyTransform().position))
        {
            aiCtr.RotateTurret(aiCtr.GetEnemyTransform().position);
        }

        if (aiCtr.GetCurrentShellCount() == 0)
        {
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;

    }


}

