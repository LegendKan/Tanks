using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;

public class FollowMe : Action {
    public TankMovement m_TankMovement;
    public Transform m_TankTransform;
	// Use this for initialization
	void Start () {
	
	}

    public override TaskStatus OnUpdate()
    {
        m_TankMovement.MoveToPosition(m_TankTransform.position);
        return TaskStatus.Running;
    }
}
