using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class BdAimTargetJugde : Conditional {

	public AIController aiCtr;

	public override void OnStart(){ 
		aiCtr = this.GetComponent<AIController> ();
	}


	public override TaskStatus OnUpdate()
	{	
		if (aiCtr.IsAimed(aiCtr.GetEnemyTransform().position))	
		{
			return TaskStatus.Success;
		}

		return TaskStatus.Failure;
	}




}
