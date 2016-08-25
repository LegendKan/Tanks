using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class isEnemyDistanceFromprop : Conditional
{
    public AIController aiCtrl;
    public float tank2distanceFromProp;
    public float tank1distanceFromProp;

    public override void OnStart()
    {
        aiCtrl = GetComponent<AIController>();
    }
    public override TaskStatus OnUpdate()
    {
        tank2distanceFromProp = Vector3.Distance(transform.position, aiCtrl.GetCurrentHealthTransform().position);
        tank1distanceFromProp = Vector3.Distance(aiCtrl.GetEnemyTransform().position, aiCtrl.GetCurrentHealthTransform().position);
        if (tank2distanceFromProp > tank1distanceFromProp)
        {
            return TaskStatus.Success;

        }
        else return TaskStatus.Failure;
    }
}
