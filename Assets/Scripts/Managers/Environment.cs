using UnityEngine;
using System.Collections;

public class Environment : MonoBehaviour {

	public Transform[] m_PropTransforms;
	public float m_PropInterval;
	public GameObject m_PropPrefab;

	private GameObject[] barriers;

	// Use this for initialization
	void Start () {
		barriers = GameObject.FindGameObjectsWithTag ("Barrier");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public GameObject[] GetBarriers()
	{
		return barriers;
	}

	public Transform[] PropTransforms
	{
		get {return m_PropTransforms;}
	}

	public GameObject GetNearestBarrier(Vector3 position)
	{
		GameObject ret = null;
		float nearest = float.MaxValue;
		for(int i = 0; i<barriers.Length; i++)
		{
			float tmp = Vector3.SqrMagnitude (position - barriers[i].transform.position);
			if (tmp < nearest) {
				nearest = tmp;
				ret = barriers [i];
			}
		}

		return ret;
	}
}
