using UnityEngine;
using System.Collections;
using System;

public class ToolsManager : MonoBehaviour {

	public Transform[] m_ToolsTransform;
	public GameObject m_HealthBag;
	public float m_Interval = 6.0f;
	public float m_HealthBagAmount = 300f;

	//攻击buffer
	public bool m_ShellPropSwitch = false;
	public float m_ShellInterval = 10f;
	public float m_ShellDamageAdded = 150f;
	public float m_ShellLastTime = 4f;
	public GameObject m_ShellProp;

	//攻击buffer相关记录
	private float shellLastTime;
	private bool shellBeenCollected = true;
	private int bufferTransform;

	private float lastTime;
	private bool beenCollected = true;
	private int healthTransform;
	private GameObject healthObject;
	private int index;
	private int times = 0;

	private ArrayList transforms;

	public static ToolsManager instance;

	// Use this for initialization
	void Start () {
		lastTime = Time.time;
		shellLastTime = lastTime;
		transforms = new ArrayList ();
		for(int i = 0; i < m_ToolsTransform.Length; i++)
		{
			transforms.Add (i);
		}
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
			//int i = UnityEngine.Random.Range(0, m_ToolsTransform.Length);
			index = randomPickOne();
			//GameObject health = Instantiate(m_HealthBag, m_ToolsTransform[i].position, m_ToolsTransform[i].rotation) as GameObject;
			//healthTransform = pickOneTransform();
			//血包 血包 攻击buffer
			times++;
			if (times % 3 == 0 && m_ShellPropSwitch) {
				//生成buffer
				GameObject attack = Instantiate(m_ShellProp, m_ToolsTransform[index].position, m_ToolsTransform[index].rotation) as GameObject;
				//设置一些属性
				AttackTool attackTool = attack.GetComponent<AttackTool>();
				if(attackTool!=null)
				{
					attackTool.lastTime = m_ShellLastTime;
					attackTool.damageAdded = m_ShellDamageAdded;
					attackTool.OnCollected += OnCollect;
				}
				healthObject = attack;
			} else {
				GameObject health = Instantiate(m_HealthBag, m_ToolsTransform[index].position, m_ToolsTransform[index].rotation) as GameObject;
				HealthTool healthComponent = health.GetComponent<HealthTool> ();
				if(healthComponent!=null)
				{
					healthComponent.m_HealthBag = m_HealthBagAmount;
					healthComponent.OnCollected += OnCollect;
				}
				healthObject = health;
			}

			beenCollected = false;
		}

		/*
		//生成增强伤害道具，直接硬编码，没有时间看结构了。
		if(m_ShellPropSwitch && shellBeenCollected && (Time.time - shellLastTime) >= m_ShellInterval)
		{
			//生成伤害道具，在随机的地点。
			bufferTransform = pickOneTransform();
			GameObject buffer = Instantiate(m_ShellProp, m_ToolsTransform[bufferTransform].position, m_ToolsTransform[bufferTransform].rotation) as GameObject;
			AttackTool attack = buffer.GetComponent<AttackTool> ();
			if(attack != null)
			{
				attack.damageAdded = m_ShellDamageAdded;
				attack.lastTime = m_ShellLastTime;
			}
			shellBeenCollected = false;
		}
		*/
	}

	private int pickOneTransform()
	{
		int i = UnityEngine.Random.Range (0, transforms.Count);
		int tmp = (int)transforms[i];
		transforms.RemoveAt (i);
		return tmp;
	}

	private int randomPickOne()
	{
		int i = UnityEngine.Random.Range(0, m_ToolsTransform.Length);
		return i;
	}

	public void OnCollect(object sender, EventArgs args)
	{
		Debug.Log ("Tools has been collected");
		beenCollected = true;
		lastTime = Time.time;
		//transforms.Add (healthTransform);
		//healthTransform = -1;
	}

	public void OnShellCollected(object sender, EventArgs args)
	{
		Debug.Log ("Attack Tool has been collected");
		shellBeenCollected = true;
		shellLastTime = Time.time;
		//transforms.Add (bufferTransform);
		//bufferTransform = -1;
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
		return healthObject.transform;
		/*
		if(healthTransform>=0)
		{
			return m_ToolsTransform[healthTransform];
		}
		return null;
		*/
	}

	public Transform GetCurrentAttackTransform()
	{
		if(beenCollected||healthObject==null||times%3!=0)
		{
			return null;
		}
		return healthObject.transform;
	}

	public Transform GetCurrentPropTransform()
	{
		if(beenCollected||healthObject==null)
		{
			return null;
		}
		return healthObject.transform;
	}

	/*
	public Transform GetAttackBufferTransform()
	{
		if(shellBeenCollected||bufferTransform <0)
		{
			return null;
		}
		//return healthObject.transform;
		return m_ToolsTransform[bufferTransform];
	}
	*/

	public bool HasTools()
	{
		return !beenCollected;
	}

	public float GetHealthBagAmount()
	{
		return m_HealthBagAmount;
	}

	public float GetDamageAdded()
	{
		return m_ShellDamageAdded;
	}

	public float GetAttackBufferLastTime()
	{
		return m_ShellLastTime;
	}

	public bool IsPropHealth()
	{
		return !(times % 3 == 0);
	}

}
