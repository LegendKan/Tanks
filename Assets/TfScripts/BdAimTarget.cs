using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class BdAimTarget : Action {

	public AIController aiCtr;
    private float mSpeed;

    public override void OnStart(){ 
		aiCtr = this.GetComponent<AIController> ();
	}


	public override TaskStatus OnUpdate()
	{
        //瞄准
        if (!aiCtr.IsAimedEnemy()) {
            aiCtr.RotateTurret(aiCtr.GetEnemyTransform().position);
            //barrier between tanks
            if (aiCtr.IsAimed(aiCtr.GetEnemyTransform().position)  )
            {
                this.transform.RotateAround(aiCtr.GetEnemyTransform().position, Vector3.up, mSpeed * Time.deltaTime);
            }
            return TaskStatus.Running;
        }	

		if (aiCtr.GetEnemyTransform()==null) {
			return TaskStatus.Failure;
		}

        return TaskStatus.Success;
    }

    public override void OnTriggerEnter(Collider other)
    {
        Debug.Log("yes");
        mSpeed = 0f - mSpeed;
    }


}
