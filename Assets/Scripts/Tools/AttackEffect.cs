using UnityEngine;
using System.Collections;

public class AttackEffect : MonoBehaviour {
    public float m_fAttackPropEffectiveTime;

    private bool m_bIsAttackPropEffective;
    private GameObject m_AttackProp;
	// Use this for initialization
	void Start () {
        m_AttackProp = transform.FindChild("skl_diancibaopo_01").gameObject;
        m_AttackProp.SetActive(false);
        m_bIsAttackPropEffective = false;
	
	}
	
	// Update is called once per frame
	void Update () {
        m_fAttackPropEffectiveTime -= Time.deltaTime;
        if(m_fAttackPropEffectiveTime <= 0.0f && m_bIsAttackPropEffective)
        {
            OnAttackPropIneffective();
        }
	
	}

    public void OnGetAttackProp()
    {
        m_AttackProp.SetActive(true);
        m_bIsAttackPropEffective = true;
    }

    private void OnAttackPropIneffective()
    {
        m_AttackProp.SetActive(false);
        m_bIsAttackPropEffective = false;
    }
}
