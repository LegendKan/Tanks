using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;

public class Tank2AiControl : MonoBehaviour
{

    public AIController aictrl;
    private List<BehaviorTree> behaviourTree = new List<BehaviorTree>();
    private BehaviorTree[] bts = new BehaviorTree[3];
    private BehaviorTree bt, bt1, bt2;

    void Start()
    {
        aictrl = this.GetComponent<AIController>();
        bts = this.transform.GetComponents<BehaviorTree>();
        for (int i = 0; i < 3; i++)
        {
            if (bts[i].Group == 0)
                bt = bts[i];
            else if (bts[i].Group == 1)
                bt1 = bts[i];
            else if (bts[i].Group == 2)
                bt2 = bts[i];
        }

    }
    void Update()
    {
        if (!BehaviorManager.instance)
        {
            return;
        }
        if (aictrl.IsEnemyAlive())
        {
            if (aictrl.HasHealthBag())
            {
                if (Vector3.Distance(aictrl.GetEnemyTransform().position, aictrl.GetCurrentHealthTransform().position) - Vector3.Distance(aictrl.GetTransform().position, aictrl.GetCurrentHealthTransform().position) <= 20)
                {
                    if (!BehaviorManager.instance.IsBehaviorEnabled(bt2))
                    {
                        bt2.EnableBehavior();
                    }
                    bt.DisableBehavior();
                    bt1.DisableBehavior();
                }
            }
            else if (aictrl.GetCurrentHealth() == aictrl.GetEnemyCurrentHealth() && !aictrl.HasHealthBag() && aictrl.GetEnemyCurrentShellCount() == aictrl.GetCurrentShellCount())
            {
                if (!BehaviorManager.instance.IsBehaviorEnabled(bt1))
                {
                    bt1.EnableBehavior();
                }
                bt.DisableBehavior();
                bt2.DisableBehavior();
            }
            else
            {
                if (!BehaviorManager.instance.IsBehaviorEnabled(bt))
                {
                    bt.EnableBehavior();
                }
                bt1.DisableBehavior();
                bt2.DisableBehavior();
            }

        }
    }

}
