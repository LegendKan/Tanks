using UnityEngine;
using System.Collections;

public class BornShield : MonoBehaviour {
    public float m_fShieldExistTime;

    private bool m_bShieldActive;
    private GameObject m_Shield;

	private TankHealth health;

	private void Awake ()
	{
		m_Shield = transform.FindChild("shield").gameObject;
		health = GetComponent<TankHealth> ();
	}

	// Use this for initialization
	void Start () {

    }

	private void OnEnable()
	{
		OnShieldEffective();
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
		//Debug.Log ("HHHHHHHHHHHHHHHHHHHHHHHHHHHHHHh");
        m_Shield.SetActive(true);
        m_bShieldActive = true;
    }
    
	
	// Update is called once per frame
	void Update () {
		if(!health.IsRebornProtected())
		{
			DisactiveShield ();
		}
	
	}

    private void DisactiveShield()
    {
        m_Shield.SetActive(false);
        m_bShieldActive = false ;
    }
}
