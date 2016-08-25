using UnityEngine;
using System.Collections;
using System;

public class ToolsManager : MonoBehaviour {

	public Transform[] m_ToolsTransform;
	public GameObject m_HealthBag;
	public float m_Interval = 3.0f;
	public float m_HealthBagAmount = 300f;

	private float lastTime;
	private bool beenCollected = true;
	private GameObject healthObject;
	private int index;

	public static ToolsManager instance;

	// Use this for initialization
	void Start () {
		lastTime = Time.time;
		if(instance==null)
		{
			instance = GetComponent<ToolsManager> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(beenCollected && (Time.time - lastTime) >= m_Interval)
		{
			//生成血包，在随机的地点
			int i = UnityEngine.Random.Range(0, m_ToolsTransform.Length);
			index = i;
			GameObject health = Instantiate(m_HealthBag, m_ToolsTransform[i].position, m_ToolsTransform[i].rotation) as GameObject;
			HealthTool healthComponent = health.GetComponent<HealthTool> ();
			if(healthComponent!=null)
			{
				healthComponent.m_HealthBag = m_HealthBagAmount;
				healthComponent.OnCollected += OnCollect;
			}
			healthObject = health;
			beenCollected = false;
		}
	}

	public void OnCollect(object sender, EventArgs args)
	{
		Debug.Log ("Tools has been collected");
		beenCollected = true;
		lastTime = Time.time;
	}

	public Transform[] GetAllSpawnTransforms()
	{
		return m_ToolsTransform;
	}

	public float GetRemainingTime()
	{
		float remaining = m_Interval -Time.time + lastTime;
		if(remaining<0)
		{
			remaining = 0;
		}
		return remaining;
	}

	public Transform GetCurrentHealthTransform()
	{
		if(beenCollected||healthObject==null)
		{
			return null;
		}
		//return healthObject.transform;
		return m_ToolsTransform[index];
	}

	public bool HasTools()
	{
		return !beenCollected;
	}

	public float GetHealthBagAmount()
	{
		return m_HealthBagAmount;
	}

}
