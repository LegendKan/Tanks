using UnityEngine;
using System.Collections;

public class AttackTool : MonoBehaviour {

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

		}
	}
}
