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
		//瞄准
		if (!aiCtr.IsAimed(aiCtr.GetEnemyTransform().position)) {
			aiCtr.RotateTurret (aiCtr.GetEnemyTransform ().position);
			return TaskStatus.Running;
		}

		//敌人没死，就打
		if (aiCtr.IsEnemyAlive())	
		{
			aiCtr.Fire ();	
		}
		//没弹药了
		if (aiCtr.GetCurrentShellCount()<=0) {
			
			aiCtr.ReloadClip ();
			return TaskStatus.Failure;
		}

		//一边换弹药，一边围绕敌人运动运动
		this.transform.RotateAround(aiCtr.GetEnemyTransform().position,Vector3.up,mSpeed*Time.deltaTime);
		return TaskStatus.Success;
	}


	public override void OnTriggerEnter(Collider other)
	{
		Debug.Log ("no");
		mSpeed = 0f-mSpeed;
	}

}
