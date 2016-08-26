using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class BdFireOnce : Action {


	public AIController aiCtr;

	public override void OnStart(){ 
		aiCtr = this.GetComponent<AIController> ();
	}


	public override TaskStatus OnUpdate()
	{
		//瞄准
		if (!aiCtr.IsAimed(aiCtr.GetEnemyTransform().position)) {
			aiCtr.RotateTurret (aiCtr.GetEnemyTransform ().position);
			return TaskStatus.Running;
		}

		//敌人没死，就打一抢
		if (aiCtr.IsEnemyAlive())	
		{
			aiCtr.Fire ();	
		}
			
		return TaskStatus.Success;
	}


}
