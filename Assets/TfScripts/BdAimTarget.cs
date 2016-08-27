using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class BdAimTarget : Action {

	public AIController aiCtr;


	public override void OnStart(){ 
		aiCtr = this.GetComponent<AIController> ();
	}


	public override TaskStatus OnUpdate()
	{
        //瞄准
        if (!aiCtr.IsAimedEnemy()) {
            aiCtr.RotateTurret(aiCtr.GetEnemyTransform().position);
            return TaskStatus.Running;
        }	

		if (aiCtr.GetEnemyTransform()==null) {
			return TaskStatus.Failure;
		}

        return TaskStatus.Success;
    }
}
