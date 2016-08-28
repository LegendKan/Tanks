using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class ifNotMoveAction : Action {
    private NavMeshAgent agent;
    public AIController aiCtl;
    private Vector3 lastPos;
    private float lastTime;
    private float totalTime = 0f;
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
    public override void OnAwake()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }
    public override void OnStart()
    {
        aiCtl = this.GetComponent<AIController>();
        agent.speed = 8.0f;
        agent.angularSpeed = aiCtl.GetBodyRotateSpeed();
        agent.enabled = true;
        lastPos = aiCtl.GetTransform().position;
        lastTime = 0;
    }
    public override TaskStatus OnUpdate()
    {
        if (lastPos != aiCtl.GetTransform().position)
        {
            lastTime = Time.time;
            lastPos = aiCtl.GetTransform().position;
        }
        if (Time.time - lastTime > 10)
        {
            lastTime = 0;
            Vector3 newPos = RandomNavSphere(aiCtl.GetTransform().position, 100, -1);
            agent.destination = aiCtl.GetEnemyTransform().position;
            Debug.Log("dadadadadadaadada");
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }
}
