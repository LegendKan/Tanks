using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class AimTarget : Action {

	public AIController aiCOntroller;

	public SharedTransform targetTransform;
	//[Tooltip("If target is null then use the target position")]
	public SharedVector3 targetPosition;

	// Use this for initialization
	public override void OnStart () {
		if ((targetTransform == null || targetTransform.Value == null) && targetPosition == null)
		{
			Debug.LogError("Error: A MoveTowards target value is not set.");
			targetPosition = new SharedVector3(); // create a new SharedVector3 to prevent repeated errors
		}

		if(aiCOntroller==null){
			aiCOntroller = GetComponent<AIController> ();
		}
	}
	
	// Update is called once per frame
	public override TaskStatus OnUpdate () {
		var position = Target();
		if(aiCOntroller.IsAimed(aiCOntroller.GetEnemyTransform().position))
		{
			return TaskStatus.Success;
		}
		aiCOntroller.RotateTurret (aiCOntroller.GetEnemyTransform().position);
		return TaskStatus.Running;
	}

	// Return targetPosition if targetTransform is null
	private Vector3 Target()
	{
		if (targetTransform == null || targetTransform.Value == null)
		{
			return targetPosition.Value;
		}
		return targetTransform.Value.position;
	}
}
