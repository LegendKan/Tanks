using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;

public class FollowTarget : Action
{
    //[Tooltip("The speed of the agent")]
    public SharedFloat speed;
    //[Tooltip("The agent has arrived when the square magnitude is less than this value")]
    public SharedFloat arriveDistance = 0.1f;
    //[Tooltip("Should the agent be looking at the target position?")]
    public SharedBool lookAtTarget = true;
    //[Tooltip("Max rotation delta if lookAtTarget is enabled")]
    public SharedFloat maxLookAtRotationDelta;
    //[Tooltip("The transform that the agent is moving towards")]
    public SharedTransform targetTransform;
    //[Tooltip("If target is null then use the target position")]
    public SharedVector3 targetPosition;

    public override void OnStart()
    {
        if ((targetTransform == null || targetTransform.Value == null) && targetPosition == null)
        {
            Debug.LogError("Error: A MoveTowards target value is not set.");
            targetPosition = new SharedVector3(); // create a new SharedVector3 to prevent repeated errors
        }
    }

    public override TaskStatus OnUpdate()
    {
        var position = Target();
        // Return a task status of success once we've reached the target
        if (Vector3.SqrMagnitude(transform.position - position) < arriveDistance.Value)
        {
            return TaskStatus.Success;
        }

        //record the position of the tank in the last frame
        Vector3 oldPosition = transform.position;

        // We haven't reached the target yet so keep moving towards it
        transform.position = Vector3.MoveTowards(transform.position, position, speed.Value * Time.deltaTime);
		//transform.position = GetComponent<NavMeshAgent>().nextPosition;
		if (lookAtTarget.Value)
        {
            //find out the next position returned by the navigation mesh
            //Vector3 nextNavPosition = GetComponent<NavMeshAgent>().nextPosition;
            //Forward to the new position
			transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(transform.position - oldPosition), maxLookAtRotationDelta.Value);
        }
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

    // Reset the public variables
    public override void OnReset()
    {
        arriveDistance = 0.1f;
        lookAtTarget = true;
    }

}