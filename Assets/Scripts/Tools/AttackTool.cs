using UnityEngine;
using System.Collections;
using System;

public class AttackTool : MonoBehaviour {
	public event EventHandler OnCollected;

	public float lastTime = 4f;
	public float damageAdded = 150f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void OnTriggerEnter(Collider other)
	{
		//Debug.Log ("++++++++++++++++++++++++++++++++++++++++");
		//判断是否是坦克
		string tag = other.gameObject.tag;
		if(tag.StartsWith("Tank"))
		{
			AttackBuffer attackBuffer = other.gameObject.AddComponent <AttackBuffer>();
			attackBuffer.lastTime = lastTime;
			attackBuffer.damageAdded = damageAdded;
			if(OnCollected != null)
			{
				OnCollected (this,EventArgs.Empty);
			}
		}
	}
}
