using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class BdFire : Conditional {


	public AIController aiCtr;
	private float mSpeed;

    int i = 0;

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

        this.transform.RotateAround(aiCtr.GetEnemyTransform().position,Vector3.up,mSpeed*Time.deltaTime);
		return TaskStatus.Success;
	}


	public override void OnTriggerEnter(Collider other)
	{
		Debug.Log ("no");
		mSpeed = 0f-mSpeed;
	}

}
