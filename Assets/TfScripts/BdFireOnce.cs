using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class BdFireOnce : Action {


	public AIController aiCtr;
    private float mSpeed;

    public override void OnStart(){ 
		aiCtr = this.GetComponent<AIController> ();
	}


	public override TaskStatus OnUpdate()
	{
        //瞄准了，就打一抢
        if (aiCtr.IsAimedEnemy() && aiCtr.IsEnemyAlive()) {
            aiCtr.Fire();
            return TaskStatus.Success;
        }


        if (aiCtr.GetEnemyTransform() == null)
        {
            return TaskStatus.Failure;
        }

        //瞄准
        aiCtr.RotateTurret(aiCtr.GetEnemyTransform().position);
        //barrier between tanks
        if (aiCtr.IsAimed(aiCtr.GetEnemyTransform().position) && !aiCtr.IsAimedEnemy())
        {
            this.transform.RotateAround(aiCtr.GetEnemyTransform().position, Vector3.up, mSpeed * Time.deltaTime);
        }
        return TaskStatus.Running;
        
	}


    public override void OnTriggerEnter(Collider other)
    {
        Debug.Log("yes");
        mSpeed = 0f - mSpeed;
    }


}
