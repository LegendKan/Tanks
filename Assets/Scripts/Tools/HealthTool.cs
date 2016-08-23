using UnityEngine;
using System.Collections;
using System;

public class HealthTool : MonoBehaviour {
	public event EventHandler OnCollected;

	[HideInInspector]public float m_HealthBag = 100f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void OnTriggerEnter(Collider other)
	{
		//Debug.Log ("++++++++++++++++++++++++++++++++++++++++");
		TankHealth health = other.gameObject.GetComponent<TankHealth> ();
		if (health!=null) {
			//Debug.Log ("------------------------------------");
			health.AddHealth (m_HealthBag);
			if(OnCollected!=null)
			{
				OnCollected (this,EventArgs.Empty);
			}
			Destroy(gameObject);
		}
	}
}
