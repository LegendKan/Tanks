﻿using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class BdLoadClip : Action {


	public AIController aiCtr;
	private float mSpeed;

	public override void OnStart(){ 
		aiCtr = this.GetComponent<AIController> ();
		mSpeed = aiCtr.GetMoveSpeed ();
	}


	public override TaskStatus OnUpdate()
	{
		//没有弹药且不再换弹药的状态下换弹药
		if (aiCtr.GetCurrentShellCount()==0 && !aiCtr.IsReloading())	
		{
			aiCtr.ReloadClip ();
		
		}
		//换弹药完毕
		if (aiCtr.GetCurrentShellCount()!=0 && !aiCtr.IsReloading()) {
			return TaskStatus.Success;
		}
        //一边换弹药，一边围绕敌人运动运动,一边瞄准
        if (aiCtr.IsAimed(this.transform.position))
        {
            transform.Rotate(Vector3.up * 90);
        }
        this.transform.RotateAround(aiCtr.GetEnemyTransform().position,Vector3.up,mSpeed*Time.deltaTime);
        aiCtr.RotateTurret(aiCtr.GetEnemyTransform().position);
		return TaskStatus.Running;
	}


	public override void OnTriggerEnter(Collider other)
	{
		Debug.Log ("yes");
		mSpeed = 0f-mSpeed;
	}


}
