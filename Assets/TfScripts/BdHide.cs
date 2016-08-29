using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class BdHide : Action
{

    public AIController aiCtrl;
    
    //目标障碍物
    public SharedTransform targetBarrier;
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

        //动态判断局势，跳出
        //强势
        if (aiCtrl.GetEnemyCurrentShellCount() * aiCtrl.GetShellDamage() - aiCtrl.GetCurrentHealth() < 0 || aiCtrl.GetCurrentShellCount() * aiCtrl.GetShellDamage() - aiCtrl.GetEnemyCurrentHealth() >= 0)
        {
            if (!aiCtrl.IsReloading())
                return TaskStatus.Failure;
        }
        //有道具且近
        if (aiCtrl.GetCurrentHealthTransform() != null && (this.transform.position - aiCtrl.GetCurrentHealthTransform().position).sqrMagnitude < (aiCtrl.GetEnemyTransform().position - aiCtrl.GetCurrentHealthTransform().position).sqrMagnitude)
        {
            return TaskStatus.Failure;
        }


        return TaskStatus.Running;
    }


    public override void OnTriggerEnter(Collider other)
    {
        Debug.Log("hide");
        mSpeed = -mSpeed;
    }

}
