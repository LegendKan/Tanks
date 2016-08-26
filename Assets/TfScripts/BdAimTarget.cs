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
		Transform ts = this.transform.FindChild ("TankTurret");
		this.transform.FindChild ("TankTurret").LookAt (aiCtr.GetEnemyTransform ().position);

		if (aiCtr.IsAimed(aiCtr.GetEnemyTransform().position)){
			return TaskStatus.Success;
		}

		if (aiCtr.GetEnemyTransform()==null) {
			return TaskStatus.Failure;
		}

		return TaskStatus.Running;
	}
}
