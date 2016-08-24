using UnityEngine;
using System.Collections;

public class TankSensor : MonoBehaviour {

	private float shellRange;
	private int playerNumber;
	private Transform turret;

	// Use this for initialization
	void Start () {
		TankShooting shooting = GetComponent<TankShooting> ();
		shellRange = shooting.m_MaxRange;
		playerNumber = shooting.m_PlayerNumber;
		turret = transform.Find ("TankRenderers/TankTurret");
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.E))
		{
			Debug.Log ("Hit Info: "+RaycastCheck(turret, shellRange));
		}
	}

	private string RaycastCheck(Transform trans, float distance)
	{
		Ray ray = new Ray ();
		ray.origin = trans.position;
		ray.direction = trans.forward;
		RaycastHit hitInfo;
		if(Physics.Raycast(ray, out hitInfo, distance))
		{
			return hitInfo.collider.gameObject.tag;
		}
		return string.Empty;
	}

	public bool CanShootEnemy()
	{
		string hit = RaycastCheck (turret, shellRange);
		if(hit == "Tank"+(3-playerNumber))
		{
			return true;
		}
		return false;
	}


}
