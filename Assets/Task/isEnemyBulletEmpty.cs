using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class isEnemyBulletEmpty : Conditional {
    public AIController aiController;

    // Use this for initialization
    public override void OnStart()
    {
        if (aiController == null)
        {
            aiController = GetComponent<AIController>();
        }
    }

    public override TaskStatus OnUpdate()
    {
        if (aiController.GetEnemyCurrentShellCount() == 0 && aiController.GetCurrentShellCount() * aiController.GetShellDamage() < aiController.GetEnemyCurrentHealth())
        {
            return TaskStatus.Success;
        }
        else return TaskStatus.Failure;
    }

}
