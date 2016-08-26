using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class isBloodJudge : Conditional
{
    public AIController aiCtrl;

    public override void OnStart()
    {
        aiCtrl = GetComponent<AIController>();
    }
    public override TaskStatus OnUpdate()
    {
        if (aiCtrl.GetCurrentHealth() > aiCtrl.GetEnemyCurrentHealth() + 300)
            return TaskStatus.Success;
        else return TaskStatus.Failure;
    }
}
