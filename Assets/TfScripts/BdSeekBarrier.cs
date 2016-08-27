using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class BdSeekBarrier : Action{

	public AIController aiCtrl;
	public GameObject[] barrierObject= new GameObject[10];



	//目标障碍物
	public SharedTransform targetBarrier;
	private float barrierDistance=0.01f;

	//nav
	private NavMeshAgent navMeshAgent;
	public float offsetDistance=0.1f;


	public override void OnAwake(){
		navMeshAgent = this.GetComponent<NavMeshAgent> ();
	}


	public override void OnStart(){ 
		aiCtrl = this.GetComponent<AIController> ();
		barrierObject = aiCtrl.GetAllBarriers ();
		//最远，最安全的障碍物
		foreach (var item in barrierObject) {
			float distanceMath =(aiCtrl.GetEnemyTransform ().position - item.GetComponent<Transform>().position).sqrMagnitude-(aiCtrl.GetTransform ().position - item.GetComponent<Transform>().position).sqrMagnitude;
			if (barrierDistance<distanceMath) {
				barrierDistance = distanceMath;
				targetBarrier.Value = item.GetComponent<Transform>();
			}
		}

		navMeshAgent.speed = aiCtrl.GetMoveSpeed();
		navMeshAgent.angularSpeed = aiCtrl.GetBodyRotateSpeed ();
		navMeshAgent.enabled = true;
		navMeshAgent.destination = targetBarrier.Value.position;
	}


	public override TaskStatus OnUpdate()
	{

	//最远，最安全的障碍物
		barrierDistance=0.01f;
	foreach (var item in barrierObject) {
		float distanceMath =(aiCtrl.GetEnemyTransform ().position - item.GetComponent<Transform>().position).sqrMagnitude-(aiCtrl.GetTransform ().position - item.GetComponent<Transform>().position).sqrMagnitude;
		if (barrierDistance<distanceMath) {
			barrierDistance = distanceMath;
			targetBarrier.Value = item.GetComponent<Transform>();
		}
		}

		if (targetBarrier.Value != null) {
			navMeshAgent.destination=targetBarrier.Value.position;

		}

		if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < offsetDistance) {
			return TaskStatus.Success;
		}

        //不用跑了，可以打了
        if (aiCtrl.GetEnemyCurrentShellCount() * aiCtrl.GetShellDamage() - aiCtrl.GetCurrentHealth() < 0 && aiCtrl.GetCurrentShellCount() * aiCtrl.GetShellDamage() - aiCtrl.GetEnemyCurrentHealth() >= 0)
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
