using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class BdFire : Conditional {


	public AIController aiCtr;
	private float mSpeed;

	public override void OnStart(){ 
		aiCtr = this.GetComponent<AIController> ();
		mSpeed = aiCtr.GetMoveSpeed ();
	}


	public override TaskStatus OnUpdate()
	{

        //瞄准了，就打一抢
        if (aiCtr.IsAimedEnemy() && aiCtr.IsEnemyAlive())
        {
            aiCtr.Fire();
        }
        //没弹药了
        if (aiCtr.GetCurrentShellCount()<=0) {
			
			aiCtr.ReloadClip ();
			return TaskStatus.Failure;
		}

		return TaskStatus.Success;
	}

}
