using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class BdLoadingShell : Action {

    public AIController aiCtrl;


    public override void OnStart()
    {
        aiCtrl = this.GetComponent<AIController>();
    }

    public override TaskStatus OnUpdate()
    {
       // Debug.Log(aiCtrl.GetCurrentShellCount());
        if (aiCtrl.GetCurrentShellCount() == 0 && !aiCtrl.IsReloading())
        {
            aiCtrl.ReloadClip();
            return TaskStatus.Success;
        }

        return TaskStatus.Running;
    }

    }
