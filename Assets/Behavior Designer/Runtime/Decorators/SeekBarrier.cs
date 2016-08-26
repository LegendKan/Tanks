using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class SeekBarrier : Action
{

    public AIController aiCtrl;
    public GameObject[] barrierObject = new GameObject[10];
    public SharedTransform targetBarrier;
    private float barrierDistance = 0.01f;

    private NavMeshAgent navMeshAgent;
    public float offsetDistance = 0.1f;


    public override void OnAwake()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();

    }


    public override void OnStart()
    {
        aiCtrl = this.GetComponent<AIController>();
        foreach (var item in barrierObject)
        {
            float distanceMath = (aiCtrl.GetEnemyTransform().position - item.GetComponent<Transform>().position).sqrMagnitude - (aiCtrl.GetTransform().position - item.GetComponent<Transform>().position).sqrMagnitude;
            if (barrierDistance < distanceMath)
            {
                barrierDistance = distanceMath;
                targetBarrier.Value = item.GetComponent<Transform>();
            }
        }

        navMeshAgent.speed = aiCtrl.GetMoveSpeed();
        navMeshAgent.angularSpeed = aiCtrl.GetBodyRotateSpeed();
        navMeshAgent.enabled = true;
        navMeshAgent.destination = targetBarrier.Value.position;

    }


    public override TaskStatus OnUpdate()
    {
        foreach (var item in barrierObject)
        {
            float distanceMath = (aiCtrl.GetEnemyTransform().position - item.GetComponent<Transform>().position).sqrMagnitude - (aiCtrl.GetTransform().position - item.GetComponent<Transform>().position).sqrMagnitude;
            if (barrierDistance < distanceMath)
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
