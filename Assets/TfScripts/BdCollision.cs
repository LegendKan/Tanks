using UnityEngine;
using System.Collections;

public class BdCollision : MonoBehaviour {


	public string collisionTarget;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	void OnTriggrEnter(Collider e)
	{
		collisionTarget = e.gameObject.name;
		Debug.Log (e.gameObject.name);
	}

}
