using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class HideRoundBarrier : Action {
    public SharedTransform targetBarrier;
    public AIController aiCtrl;
    private float mSpeed;

    public override void OnStart()
    {
        aiCtrl = GetComponent<AIController>();
        mSpeed = aiCtrl.GetMoveSpeed();
    }
    public override TaskStatus OnUpdate()
    {
        if (!aiCtrl.HasBarrierBetweenEnemy())
        {
            aiCtrl.RotateTurret(aiCtrl.GetEnemyTransform().position);
            transform.RotateAround(targetBarrier.Value.position, Vector3.up, 3*mSpeed * Time.deltaTime);
        }

        if (aiCtrl.HasBarrierBetweenEnemy())
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }
}
