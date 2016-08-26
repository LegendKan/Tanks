using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class BdHpJugde : Conditional {

	private float deltaHP;
	public AIController aiCtr;

	public override void OnStart(){ 
		aiCtr = this.GetComponent<AIController> ();
	}


	public override TaskStatus OnUpdate()
	{
				
		deltaHP = aiCtr.GetCurrentHealth () - aiCtr.GetEnemyCurrentHealth ();

		if (deltaHP< 0f)	
		       {
					return TaskStatus.Success;
				}

				return TaskStatus.Failure;
	}

			
		

}
