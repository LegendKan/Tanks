using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class BdPropJudge : Conditional {

	//private int  deltaShellCount;
	public AIController aiCtr;

	public override void OnStart(){ 
		aiCtr = this.GetComponent<AIController> ();
	}


	public override TaskStatus OnUpdate()
	{

		//有道具，且距离小于敌方距离
		if (aiCtr.GetCurrentHealthTransform () != null && (this.transform.position-aiCtr.GetCurrentHealthTransform ().position).sqrMagnitude<(aiCtr.GetEnemyTransform().position-aiCtr.GetCurrentHealthTransform ().position).sqrMagnitude) {
			return TaskStatus.Success;
		}
   
		return TaskStatus.Failure;

	}

}
