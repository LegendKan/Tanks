using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class SeekBarrier : Action
{

    public AIController aiCtrl;
    public GameObject[] barrierObject = new GameObject[10];
    public SharedTransform targetBarrier;
    private float barrierDistance = 9999.0f;

    private NavMeshAgent navMeshAgent;
    public float offsetDistance = 0.01f;


    public override void OnAwake()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();

    }


    public override void OnStart()
    {
		navMeshAgent = this.GetComponent<NavMeshAgent>();
        aiCtrl = this.GetComponent<AIController>();
		barrierObject = aiCtrl.GetAllBarriers ();
        foreach (var item in barrierObject)
        {
            float distanceMath = (aiCtrl.GetEnemyTransform().position - item.GetComponent<Transform>().position).sqrMagnitude;
            if (barrierDistance > distanceMath)
            {
                barrierDistance = distanceMath;
                targetBarrier.Value = item.GetComponent<Transform>();
            }
        }

        navMeshAgent.speed = aiCtrl.GetMoveSpeed();
        navMeshAgent.angularSpeed = aiCtrl.GetBodyRotateSpeed();
        navMeshAgent.enabled = true;
		if (targetBarrier.Value != null) {
			navMeshAgent.destination = targetBarrier.Value.position;
		} else {
			Debug.Log ("targetBarrier is null");
		}
        

    }


    public override TaskStatus OnUpdate()
    {
        barrierDistance = 9999.0f;
        foreach (var item in barrierObject)
        {
            float distanceMath = (aiCtrl.GetEnemyTransform().position - item.GetComponent<Transform>().position).sqrMagnitude;
            if (barrierDistance > distanceMath)
            {
                barrierDistance = distanceMath;
                targetBarrier.Value = item.GetComponent<Transform>();
            }
        }

        if (targetBarrier.Value != null)
        {
            Debug.Log(targetBarrier.Value.name);
            navMeshAgent.destination = targetBarrier.Value.position;
        }

        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < offsetDistance)
        {
            return TaskStatus.Success;
        }


        return TaskStatus.Running;
    }

    public override void OnEnd()
    {
        navMeshAgent.enabled = false;
    }



}
