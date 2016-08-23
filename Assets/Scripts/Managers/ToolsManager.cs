using UnityEngine;
using System.Collections;

public class ToolsManager : MonoBehaviour {

	public Transform[] m_ToolsTransform;
	public GameObject m_HealthBag;
	public float m_Interval;

	private float lastTime = Time.time;
	private bool beenCollected = true;
	private Transform healthTransform;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(beenCollected&&(Time.time - lastTime >= m_Interval))
		{
			//生成血包，在随机的地点
			int i = Random.Range(0, m_ToolsTransform.Length-1);
			GameObject health = Instantiate(m_HealthBag, m_ToolsTransform[i].position, m_ToolsTransform[i].rotation) as GameObject;
			healthTransform = health.transform;
		}
	}


}
