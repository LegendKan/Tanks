using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class isBulletNotFull : Conditional
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
        if (aiController.GetCurrentShellCount() != aiController.GetShellCountPerClip())
        {
            return TaskStatus.Success;
        }
        else return TaskStatus.Failure;
    }
}
