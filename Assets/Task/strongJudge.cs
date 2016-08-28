using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class strongJudge : Conditional {
    public AIController aiCtl;
    private float weTankJudge;
    private float theirTankJudge;
    private float theirTankToprop;
    private float weTankToprop;

    public override void OnStart()
    {
        if(aiCtl == null)
        aiCtl = GetComponent<AIController>();
        weTankJudge = aiCtl.GetCurrentHealth() - aiCtl.GetEnemyCurrentShellCount() * aiCtl.GetShellDamage();
        theirTankJudge = aiCtl.GetEnemyCurrentHealth() - aiCtl.GetCurrentShellCount() * aiCtl.GetShellDamage();
        weTankToprop = Vector3.Distance(aiCtl.GetEnemyTransform().position, aiCtl.GetCurrentHealthTransform().position);
        theirTankToprop = Vector3.Distance(aiCtl.GetTransform().position, aiCtl.GetCurrentHealthTransform().position);
    }

    public override TaskStatus OnUpdate()
    {
        if (weTankJudge > 0)
        {
            if (theirTankJudge / weTankJudge <= 0.4 || weTankToprop+20 < theirTankToprop)
                return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
