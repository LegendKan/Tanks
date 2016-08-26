using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;

public class BehaviorTreeManage : MonoBehaviour
{

    public AIController aictrl;
    private List<BehaviorTree> behaviourTree = new List<BehaviorTree>();
    private BehaviorTree[] bts = new BehaviorTree[2];
    private BehaviorTree bt, bt1;
    void Start()
    {
        aictrl = this.GetComponent<AIController>();
        bts = this.transform.GetComponents<BehaviorTree>();
        if (bts[0].Group == 1)
        {
            bt = bts[0];
            bt1 = bts[1];
        }
        else
        {
            bt = bts[1];
            bt1 = bts[0];
        }

    }
    void Update()
    {
		if(!BehaviorManager.instance)
		{
			return;
		}
        if (aictrl.IsEnemyAlive())
        {
            if (aictrl.GetCurrentHealth() == aictrl.GetEnemyCurrentHealth() && aictrl.GetShellRange() <= aictrl.GetDistanceWithEnemy())
            {
                if (!BehaviorManager.instance.IsBehaviorEnabled(bt))
                {
                    Debug.Log("dadada",bt);
                    bt.EnableBehavior();
                }
                bt1.DisableBehavior();
            }
            else
            {
                if (!BehaviorManager.instance.IsBehaviorEnabled(bt1))
                {
                    bt1.EnableBehavior();
                }
                bt.DisableBehavior();
            }

        }
    }

}
