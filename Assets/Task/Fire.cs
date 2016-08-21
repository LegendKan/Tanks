using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Fire : Action {

	public AIController aiCOntroller;

	// Use this for initialization
	public override void OnStart () {
		if(aiCOntroller==null){
			aiCOntroller = GetComponent<AIController> ();
		}
	}
	
	// Update is called once per frame
	public override TaskStatus OnUpdate () {
		if(aiCOntroller.GetFireRemaining()>0)
		{
			return TaskStatus.Failure;
		}
		aiCOntroller.Fire ();
		return TaskStatus.Success;
	}
}
