using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class BdWithinDistanceJudge : Conditional {

	public AIController aiCtr;

	public override void OnStart(){ 
		aiCtr = this.GetComponent<AIController> ();
	}


	public override TaskStatus OnUpdate()
	{
		if (aiCtr.IsAimed(aiCtr.GetEnemyTransform().position)) {
			aiCtr.RotateTurret (aiCtr.GetEnemyTransform ().position);
		}
	
		if (aiCtr.GetDistanceWithEnemy()<=aiCtr.GetShellRange()) {
			
			return TaskStatus.Success;
		}
        
		return TaskStatus.Failure;

	}


}
