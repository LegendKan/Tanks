using UnityEngine;
using System.Collections;

public class AttackBuffer : MonoBehaviour {
	public float lastTime = 4f;
	public float damageAdded = 150f;

	private float startTime;
	private TankShooting shooting;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
		shooting = GetComponent<TankShooting> ();
		if(shooting!=null)
		{
			
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
