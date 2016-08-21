using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class CanFire : Conditional
{
	public AIController aiController;

	// Use this for initialization
	public override void OnStart () {
		if(aiController==null){
			aiController = GetComponent<AIController> ();
		}
	}

	public override TaskStatus OnUpdate()
	{
		if(aiController.GetFireRemaining()>0f)
		{
			return TaskStatus.Failure;
		}
		return TaskStatus.Success;
	}
}