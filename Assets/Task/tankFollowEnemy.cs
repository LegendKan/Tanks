using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class tankFollowEnemy : Action {
    public AIController enemyCtl;
    private NavMeshAgent agent;
    public override void OnAwake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    public override void OnStart()
    {
        enemyCtl = GetComponent<AIController>();
        agent.stoppingDistance = enemyCtl.GetShellRange();
    }
    public override TaskStatus OnUpdate()
    {
        if (enemyCtl.GetDistanceWithEnemy() > enemyCtl.GetShellRange())
        {
            enemyCtl.RotateTurret(enemyCtl.GetEnemyTransform().position);
            agent.SetDestination(enemyCtl.GetEnemyTransform().position);
            agent.Move(transform.TransformDirection(new Vector3(0, 0, enemyCtl.GetMoveSpeed() * Time.deltaTime)));
        }
        else return TaskStatus.Success;
        if (enemyCtl.GetEnemyTransform() == null)
        return TaskStatus.Failure;

        return TaskStatus.Running;
    }
}
