using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class ReloadingAndRound : Action
{
    public AIController aiCtr;

    public override void OnStart()
    {
        aiCtr = this.GetComponent<AIController>();
    }


    public override TaskStatus OnUpdate()
    {
        if (!aiCtr.IsReloading())
            aiCtr.ReloadClip();
        if (aiCtr.GetCurrentShellCount() == 8 && !aiCtr.IsReloading())
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }
}
