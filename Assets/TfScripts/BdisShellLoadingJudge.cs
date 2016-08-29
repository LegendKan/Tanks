using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class BdisShellLoadingJudge : Action
{

    public AIController aiCtrl;


    public override void OnStart()
    {
        aiCtrl = this.GetComponent<AIController>();
    }

    public override TaskStatus OnUpdate()
    {
        if (aiCtrl.GetCurrentShellCount() != 0 && !aiCtrl.IsReloading())
        {
            return TaskStatus.Failure;
        }

        //有道具且近
        if (aiCtrl.GetCurrentHealthTransform() != null && (this.transform.position - aiCtrl.GetCurrentHealthTransform().position).sqrMagnitude <= (aiCtrl.GetEnemyTransform().position - aiCtrl.GetCurrentHealthTransform().position).sqrMagnitude)
        {
            return TaskStatus.Failure;
        }

        aiCtrl.ReloadClip();
        return TaskStatus.Running;
    }

}
