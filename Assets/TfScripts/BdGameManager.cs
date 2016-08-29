using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;

public class BdGameManager : MonoBehaviour {

	public AIController aictrl;
	private List<BehaviorTree> behaviourTree = new List<BehaviorTree> ();
	private BehaviorTree[] bts = new BehaviorTree[2];
	private BehaviorTree bt, bt1;

	// Use this for initialization
	void Start () {
		aictrl = this.GetComponent<AIController> ();
		//得到物体中所有的行为树
		bts=this.transform.GetComponents<BehaviorTree>();

		//问号 表达式更好，写的这么low
		if (bts [0].Group == 1) {
			bt = bts [0];
			bt1 = bts [1];
		} else {
			bt = bts [1];
			bt1 = bts [0];
		}
				
	}
	
	// Update is called once per frame
	void Update () {
		if(!BehaviorManager.instance)
		{
			return;
		}
		if (aictrl.IsEnemyAlive ()) {
			if (aictrl.GetDistanceWithEnemy () > aictrl.GetShellRange ()+0.7f) 
			{
				if (!BehaviorManager.instance.IsBehaviorEnabled (bt)) {
					bt.EnableBehavior ();			
				}
				bt1.DisableBehavior ();
			} 
			else 
			{
			     if (!BehaviorManager.instance.IsBehaviorEnabled (bt1))
				  {
				    bt1.EnableBehavior ();
			      }
				bt.DisableBehavior ();
			}
		
		}
	}

}


