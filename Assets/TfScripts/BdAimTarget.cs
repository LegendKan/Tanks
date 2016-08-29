using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class BdAimTarget : Action {

	public AIController aiCtr;
    private float mSpeed;

    public override void OnStart(){ 
		aiCtr = this.GetComponent<AIController> ();
        mSpeed = aiCtr.GetMoveSpeed();
	}


	public override TaskStatus OnUpdate()
	{

        if (aiCtr.GetEnemyTransform() == null)
        {
            return TaskStatus.Failure;
        }
        //动态判断局势，跳出
        //劣势
        if (!(aiCtr.GetEnemyCurrentShellCount() * aiCtr.GetCurrentDamage() - aiCtr.GetCurrentHealth() < 0 || aiCtr.GetCurrentShellCount() * aiCtr.GetCurrentDamage() - aiCtr.GetEnemyCurrentHealth() >= 0))
        {
           // if (!aiCtr.IsReloading())
                return TaskStatus.Failure;
        }
        //有道具且近
        if (aiCtr.GetCurrentHealthTransform() != null && (this.transform.position - aiCtr.GetCurrentHealthTransform().position).sqrMagnitude <= (aiCtr.GetEnemyTransform().position - aiCtr.GetCurrentHealthTransform().position).sqrMagnitude)
        {
            return TaskStatus.Failure;
        }

        //瞄准
        if (!aiCtr.IsAimedEnemy()) {
            aiCtr.RotateTurret(aiCtr.GetEnemyTransform().position);
            //barrier between tanks
            if (aiCtr.IsAimed(aiCtr.GetEnemyTransform().position))
            {
                this.transform.RotateAround(aiCtr.GetEnemyTransform().position, Vector3.up, mSpeed * Time.deltaTime);
            }
            return TaskStatus.Running;
        }	

        return TaskStatus.Success;
    }

    public override void OnTriggerEnter(Collider other)
    {
        Debug.Log("enemy");
        mSpeed = 0f - mSpeed;
    }


}
