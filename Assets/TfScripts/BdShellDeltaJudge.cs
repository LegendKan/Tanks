using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class BdShellDeltaJudge : Conditional {

	private int  deltaShellCount;
	public AIController aiCtr;

	public override void OnStart(){ 
		aiCtr = this.GetComponent<AIController> ();
	}


	public override TaskStatus OnUpdate()
	{

		deltaShellCount = aiCtr.GetCurrentShellCount () - aiCtr.GetEnemyCurrentShellCount ();
		if (deltaShellCount<0)	
		{
			return TaskStatus.Success;
		}

		return TaskStatus.Failure;
	}


}
