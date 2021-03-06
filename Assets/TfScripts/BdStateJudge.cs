﻿using UnityEngine;
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
       //敌人打不死我，或者我打的死敌人
        if (aiCtr.GetEnemyCurrentShellCount()*aiCtr.GetCurrentDamage() - aiCtr.GetCurrentHealth()<0 || aiCtr.GetCurrentShellCount()* aiCtr.GetCurrentDamage() - aiCtr.GetEnemyCurrentHealth()>=0)
        {
            if (!aiCtr.IsReloading())
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }




}
