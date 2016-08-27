using UnityEngine;
using System.Collections;

public class BornShield : MonoBehaviour {
    public float m_fShieldExistTime;

    private bool m_bShieldActive;
    private GameObject m_Shield;
	// Use this for initialization
	void Start () {
        m_Shield = transform.FindChild("shield").gameObject;

    }

    public void OnTankReborn()
    {
        OnShieldEffective();
        
    }

    public void OnGetShieldProp()
    {
        OnShieldEffective();
    }

    private void OnShieldEffective()
    {
        m_Shield.SetActive(true);
        m_bShieldActive = true;
    }
    
	
	// Update is called once per frame
	void Update () {
        m_fShieldExistTime -= Time.deltaTime;
        if (m_fShieldExistTime <= 0.0f && m_bShieldActive)
        {
            DisactiveShield();
        }   
	
	}

    private void DisactiveShield()
    {
        m_Shield.SetActive(false);
        m_bShieldActive = false ;
    }
}
