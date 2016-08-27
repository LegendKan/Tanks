using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class BdStateJudge : Conditional
{

    public AIController aiCtr;

    public override void OnStart()
    {
        aiCtr = this.GetComponent<AIController>();
    }


    public override TaskStatus OnUpdate()
    {
       //敌人打不死我，我打的死敌人
        if (aiCtr.GetEnemyCurrentShellCount()*aiCtr.GetShellDamage()-aiCtr.GetCurrentHealth()<=0 || aiCtr.GetCurrentShellCount()*aiCtr.GetShellDamage()-aiCtr.GetEnemyCurrentHealth()>=0)
        {
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }




}
