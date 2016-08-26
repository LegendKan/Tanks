using UnityEngine;
using System.Collections;

public class TankSensor : MonoBehaviour {

	private float shellRange;
	private int playerNumber;
	private Transform turret;
	[HideInInspector] public GameObject[] enemies;
	[HideInInspector] public GameObject[] friends;

	// Use this for initialization
	void Start () {
		TankShooting shooting = GetComponent<TankShooting> ();
		shellRange = shooting.m_MaxRange;
		playerNumber = shooting.m_PlayerNumber;
		turret = transform.Find ("TankRenderers/TankTurret");
		enemies = GameObject.FindGameObjectsWithTag ("Tank"+(3-playerNumber));
		friends = GameObject.FindGameObjectsWithTag ("Tank"+playerNumber);
		ArrayList allFriends = new ArrayList (friends);
		allFriends.Remove (gameObject);
		friends = (GameObject[])allFriends.ToArray (typeof(GameObject));
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.E))
		{
			//AIController aiCtr = GetComponent<AIController> ();
			//Debug.Log ("Has Barrier between enemy "+aiCtr.HasBarrierBetweenEnemy());
		}
	}

	public string RaycastCheck(Transform trans, float distance)
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

	public string RaycastCheck(Vector3 position, Vector3 direction, float distance)
	{
		Ray ray = new Ray ();
		ray.origin = position;
		ray.direction = direction;
		RaycastHit hitInfo;
		if(Physics.Raycast(ray, out hitInfo, distance))
		{
			Debug.Log ("Hit tag: " + hitInfo.collider.gameObject.tag);
			return hitInfo.collider.gameObject.tag;
		}
		Debug.Log ("Hit null");
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

	public GameObject[] GetAllFriends()
	{
		return friends;
	}

	public GameObject[] GetAllEnemies()
	{
		return enemies;
	}

	public GameObject GetNearestFriend()
	{
		if(friends==null || friends.Length==0)
		{
			return null;
		}
		float nearest = float.MaxValue;
		GameObject ret = null;
		for(int i =0;i<friends.Length;i++)
		{
			float tmp = Vector3.SqrMagnitude (friends[i].transform.position - transform.position);
			if(tmp < nearest)
			{
				nearest = tmp;
				ret = friends [i];
			}
		}
		return ret;
	}

	public GameObject GetNearestEnemy()
	{
		if(enemies==null || enemies.Length==0)
		{
			return null;
		}
		float nearest = float.MaxValue;
		GameObject ret = null;
		for(int i =0;i<enemies.Length;i++)
		{
			float tmp = Vector3.SqrMagnitude (enemies[i].transform.position - transform.position);
			if(tmp < nearest)
			{
				nearest = tmp;
				ret = enemies [i];
			}
		}
		return ret;
	}


}
