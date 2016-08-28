using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IsEnemyAlive : Conditional {
	public AIController aiCOntroller;

	// Use this for initialization
	public override void OnStart () {
		if(aiCOntroller==null){
			aiCOntroller = GetComponent<AIController> ();
		}
	}

	public override TaskStatus OnUpdate () {
		if(aiCOntroller.IsEnemyAlive()){
			return TaskStatus.Success;
		}
		else return TaskStatus.Failure;
	}
}
