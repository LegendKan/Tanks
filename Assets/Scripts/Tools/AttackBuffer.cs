using UnityEngine;
using System.Collections;

public class AttackBuffer : MonoBehaviour {
	public float lastTime = 4f;
	public float damageAdded = 150f;

	private float startTime;
	private TankShooting shooting;
	private AttackEffect attackEffect;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
		shooting = GetComponent<TankShooting> ();
		if (shooting != null) {
			shooting.m_Damage += damageAdded;
			//加特效
			attackEffect = GetComponent<AttackEffect>();
			if(attackEffect!=null)
			{
				attackEffect.m_fAttackPropEffectiveTime = lastTime;
				attackEffect.OnGetAttackProp ();
			}
		} else {
			Destroy (this);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if((Time.time - startTime)>lastTime)
		{
			shooting.m_Damage -= damageAdded;
			attackEffect.OnAttackPropIneffective ();
			//删除自己
			Destroy(this);
		}
	}

	public float GetBufferRemaining()
	{
		float remaining = lastTime - Time.time + startTime;
		if(remaining<0)
		{
			return 0f;
		}
		return remaining;
	}
}
