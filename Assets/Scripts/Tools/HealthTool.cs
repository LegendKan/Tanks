using UnityEngine;
using System.Collections;

public class HealthTool : MonoBehaviour {

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
			Destroy(gameObject);
		}
	}
}
