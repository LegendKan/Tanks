using UnityEngine;
using System.Collections;

public class SelfRotate : MonoBehaviour {
	public float m_TotateSpeed=2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.down* m_TotateSpeed,Space.World);
	}
}
