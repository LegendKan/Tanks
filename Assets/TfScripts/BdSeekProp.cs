using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
	
	public class BdSeekProp : Action
	{

		//public SharedFloat speed;		
		//public SharedFloat angularSpeed;
		public float offsetDistance = 0.8f;


		public AIController aictrl;
		private NavMeshAgent navMeshAgent;

		public override void OnAwake()
		{
			navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
		}

		public override void OnStart()
		{
		    aictrl = this.GetComponent<AIController> ();

		    navMeshAgent.speed = aictrl.GetMoveSpeed();
	    	navMeshAgent.angularSpeed = aictrl.GetBodyRotateSpeed ();
			navMeshAgent.enabled = true;
		    navMeshAgent.destination = aictrl.GetCurrentHealthTransform().position;
		    navMeshAgent.stoppingDistance = 0.5f;
		}

		public override TaskStatus OnUpdate()
		{
			if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < offsetDistance) {
            //Debug.Log(navMeshAgent.remainingDistance);
				return TaskStatus.Success;
			}
			
 	 	if (aictrl.GetCurrentHealthTransform() != null) {
			navMeshAgent.destination = aictrl.GetCurrentHealthTransform().position;
			}

        /*
        //强势
        if (aictrl.GetEnemyCurrentShellCount() * aictrl.GetShellDamage() - aictrl.GetCurrentHealth() < 0 && aictrl.GetCurrentShellCount() * aictrl.GetShellDamage() - aictrl.GetEnemyCurrentHealth() >= 0)
        {
            if (!aictrl.IsReloading())
                return TaskStatus.Failure;
        }
        */

        //没道具或者被敌人吃了
        if (aictrl.GetCurrentHealthTransform() == null)
        {
            return TaskStatus.Failure;
        }

        return TaskStatus.Running;
		}



		public override void OnEnd()
		{
			navMeshAgent.enabled = false;
		}


	}
