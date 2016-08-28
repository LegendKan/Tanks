using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class isBulletHigher : Conditional
{
    public AIController aiCtl;
    public override void OnStart()
    {
        if (aiCtl == null)
        {
            aiCtl = GetComponent<AIController>();
        }
    }

    public override TaskStatus OnUpdate()
    {
        if (aiCtl.GetEnemyCurrentShellCount() <= aiCtl.GetCurrentShellCount())
        {
            return TaskStatus.Success;
        }
        else return TaskStatus.Failure;
    }
}
