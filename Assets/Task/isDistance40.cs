using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class isDistance40 : Conditional
{
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
        if (Vector3.Distance(aiController.GetEnemyTransform().position,aiController.GetTransform().position) > 40.0f)
        {
            return TaskStatus.Success;
        }
        else return TaskStatus.Failure;
    }
}
