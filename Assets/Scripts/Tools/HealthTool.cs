using UnityEngine;
using System.Collections;
using System;

public class HealthTool : MonoBehaviour {
	public event EventHandler OnCollected;

	public float m_HealthBag = 100f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void OnTriggerEnter(Collider other)
	{
		TankHealth health = other.GetComponent<TankHealth> ();
		if (health!=null) {
			health.AddHealth (m_HealthBag);
			if(OnCollected!=null)
			{
				OnCollected (this,EventArgs.Empty);
			}
			//Destroy(gameObject);
		}
	}
}
