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
		if (shooting != null) {
			shooting.m_Damage += damageAdded;
			//加特效

		} else {
			Destroy (this);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if((Time.time - startTime)>lastTime)
		{
			shooting.m_Damage -= damageAdded;
			//消除特效

			//删除自己
			Destroy(this);
		}
	}
}
