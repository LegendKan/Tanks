using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class BdHide : Action
{

    public AIController aiCtrl;
    
    //目标障碍物
    public SharedTransform targetBarrier;
    private float barrierDistance = 0.01f;
    public float mSpeed;
    


    public override void OnStart()
    {
        aiCtrl = this.GetComponent<AIController>();
        mSpeed = aiCtrl.GetMoveSpeed();
    }


    public override TaskStatus OnUpdate()
    {
        if (!aiCtrl.HasBarrierBetweenEnemy())
        {
            transform.RotateAround(targetBarrier.Value.position,Vector3.up,mSpeed * Time.deltaTime);
        }

        if (aiCtrl.HasBarrierBetweenEnemy())
        {
            return TaskStatus.Success;
        }


        //不用跑了，可以打了
        if (aiCtrl.GetEnemyCurrentShellCount() * aiCtrl.GetShellDamage() - aiCtrl.GetCurrentHealth() < 0 && aiCtrl.GetCurrentShellCount() * aiCtrl.GetShellDamage() - aiCtrl.GetEnemyCurrentHealth() >= 0)
        {
            return TaskStatus.Failure;
        }


        return TaskStatus.Running;
    }


    public override void OnTriggerEnter(Collider other)
    {
        Debug.Log("no");
        mSpeed = -mSpeed;
    }

}
