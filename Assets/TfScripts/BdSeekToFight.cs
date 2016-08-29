using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class BdSeekToFight : Action{

	public AIController aiCtrl;

	//nav
	private NavMeshAgent navMeshAgent;
	public float offsetDistance;


	public override void OnAwake(){
		navMeshAgent = this.GetComponent<NavMeshAgent> ();

	}


	public override void OnStart(){ 
		aiCtrl = this.GetComponent<AIController> ();

		offsetDistance = aiCtrl.GetShellRange ()+0.5f;

		navMeshAgent.speed = aiCtrl.GetMoveSpeed();
		navMeshAgent.angularSpeed = aiCtrl.GetBodyRotateSpeed ();
		navMeshAgent.enabled = true;
		navMeshAgent.destination = aiCtrl.GetEnemyTransform ().position;

	}


	public override TaskStatus OnUpdate()
	{
		//进入射程
		if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= offsetDistance) {
			return TaskStatus.Success;
		}



        //动态判断局势，跳出
        //劣势
        if (!(aiCtrl.GetEnemyCurrentShellCount() * aiCtrl.GetCurrentDamage() - aiCtrl.GetCurrentHealth() < 0 || aiCtrl.GetCurrentShellCount() * aiCtrl.GetCurrentDamage() - aiCtrl.GetEnemyCurrentHealth() >= 0))
        {
            // if (!aiCtr.IsReloading())
            return TaskStatus.Failure;
        }
        //有道具且近
        if (aiCtrl.GetCurrentHealthTransform() != null && (this.transform.position - aiCtrl.GetCurrentHealthTransform().position).sqrMagnitude <=(aiCtrl.GetEnemyTransform().position - aiCtrl.GetCurrentHealthTransform().position).sqrMagnitude)
        {
            return TaskStatus.Failure;
        }
        if (aiCtrl.GetEnemyTransform() == null)
        {
            return TaskStatus.Failure;
        }


        if (aiCtrl.GetEnemyTransform() != null) {
			navMeshAgent.destination = aiCtrl.GetEnemyTransform ().position;
		}
		return TaskStatus.Running;
	}

	public override void OnEnd()
	{
		navMeshAgent.enabled = false;
	}



}
