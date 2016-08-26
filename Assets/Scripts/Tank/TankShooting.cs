using UnityEngine;
using UnityEngine.UI;

public class TankShooting : MonoBehaviour
{
    public int m_PlayerNumber = 1;              // Used to identify the different players.
    public Rigidbody m_Shell;                   // Prefab of the shell.
    public Transform m_FireTransform;           // A child of the tank where the shells are spawned.
    //public Slider m_AimSlider;                  // A child of the tank that displays the current launch force.
    public AudioSource m_ShootingAudio;         // Reference to the audio source used to play the shooting audio. NB: different to the movement audio source.
    public AudioClip m_ChargingClip;            // Audio that plays when each shot is charging up.
    public AudioClip m_FireClip;                // Audio that plays when each shot is fired.
	public float m_Damage = 100f;
	public float m_ShellSpeed = 30f;
	[HideInInspector]public float m_MinLaunchForce = 30f;        // The force given to the shell if the fire button is not held.
	[HideInInspector]public float m_MaxLaunchForce = 30f;        // The force given to the shell if the fire button is held for the max charge time.
    public float m_MaxChargeTime = 0.75f;       // How long the shell can charge for before it is fired at max force.

	public float m_MaxRange = 10f;
	public float m_interval = 1f;				//两次开火的时间间隔。
	public float m_ReloadInterval = 3f;
	public int shellCountPerClip = 8;


    private string m_FireButton;                // The input axis that is used for launching shells.
    private float m_CurrentLaunchForce;         // The force that will be given to the shell when the fire button is released.
    //private float m_ChargeSpeed;                // How fast the launch force increases, based on the max charge time.
    //private bool m_Fired;                       // Whether or not the shell has been launched with this button press.
	private float fire_timer = 0.0f;
	private float reload_timer = 0.0f;
	private bool isreloading = false;

	private int m_CurrentShellCount;

    private void OnEnable()
    {
        // When the tank is turned on, reset the launch force and the UI
		//m_AimSlider.minValue = m_MinLaunchForce;
		//m_AimSlider.maxValue = m_MaxLaunchForce;
        m_CurrentLaunchForce = m_MinLaunchForce;
        //m_AimSlider.value = m_MinLaunchForce;
		m_CurrentShellCount = shellCountPerClip;
    }


    private void Start ()
    {
        // The fire axis is based on the player number.
        m_FireButton = "Fire" + m_PlayerNumber;

        // The rate that the launch force charges up is the range of possible forces by the max charge time.
        //m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;

		m_CurrentShellCount = shellCountPerClip;
    }

	/*
    private void Update ()
    {
		fire_timer += Time.deltaTime;
		if(isreloading)
		{
			if(Time.time - reload_timer >= m_ReloadInterval)
			{
				isreloading = false;
				m_CurrentShellCount = shellCountPerClip;
			}
		}


        // The slider should have a default value of the minimum launch force.
        m_AimSlider.value = m_MinLaunchForce;

        // If the max force has been exceeded and the shell hasn't yet been launched...
        if (m_CurrentLaunchForce > m_MaxLaunchForce && !m_Fired)
        {
            // ... use the max force and launch the shell.
            m_CurrentLaunchForce = m_MaxLaunchForce;
            Fire ();
        }
        // Otherwise, if the fire button has just started being pressed...
        else if (Input.GetButtonDown (m_FireButton))
        {
            // ... reset the fired flag and reset the launch force.
            m_Fired = false;
            m_CurrentLaunchForce = m_MinLaunchForce;

            // Change the clip to the charging clip and start it playing.
            m_ShootingAudio.clip = m_ChargingClip;
            m_ShootingAudio.Play ();
        }
        // Otherwise, if the fire button is being held and the shell hasn't been launched yet...
        else if (Input.GetButton (m_FireButton) && !m_Fired)
        {
            // Increment the launch force and update the slider.
            m_CurrentLaunchForce += m_ChargeSpeed * Time.deltaTime;

            m_AimSlider.value = m_CurrentLaunchForce;
        }
        // Otherwise, if the fire button is released and the shell hasn't been launched yet...
        else if (Input.GetButtonUp (m_FireButton) && !m_Fired)
        {
            // ... launch the shell.
            Fire ();
        }
    }
	*/
	private void Update ()
	{
		fire_timer += Time.deltaTime;
		if(isreloading)
		{
			if(Time.time - reload_timer >= m_ReloadInterval)
			{
				isreloading = false;
				m_CurrentShellCount = shellCountPerClip;
			}
		}
		if (Input.GetButtonDown (m_FireButton)) 
		{
			Fire ();	
		}
	}

	public void Fire ()
    {
		if(fire_timer<m_interval || isreloading)
		{
			return;
		}
		fire_timer = 0.0f;
        // Set the fired flag so only Fire is only called once.
        //m_Fired = true;

        // Create an instance of the shell and store a reference to it's rigidbody.
        Rigidbody shellInstance =
            Instantiate (m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

		ShellExplosion shellExplosion = shellInstance.GetComponent<ShellExplosion> ();
		//Debug.Log (m_CurrentLaunchForce);
		shellExplosion.m_MaxLifeTime = m_MaxRange/m_CurrentLaunchForce;
		shellExplosion.m_PlayerNumber = m_PlayerNumber;
		shellExplosion.m_MaxDamage = m_Damage;
        // Set the shell's velocity to the launch force in the fire position's forward direction.
        //shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward; 
		shellInstance.velocity = m_ShellSpeed * m_FireTransform.forward;//炮弹速度
		shellExplosion.startPostion = transform.position;
		shellExplosion.shellRange = m_MaxRange;

        // Change the clip to the firing clip and play it.
        m_ShootingAudio.clip = m_FireClip;
        m_ShootingAudio.Play ();

        // Reset the launch force.  This is a precaution in case of missing button events.
        m_CurrentLaunchForce = m_MinLaunchForce;

		//减少弹药数量
		m_CurrentShellCount --;
		//Debug.Log ("Shell Count is "+m_CurrentShellCount);
		if(m_CurrentShellCount<=0){
			Reload ();
		}

    }

	public int GetCurrentShellCount()
	{
		return m_CurrentShellCount;
	}

	public float GetFireRemaining()
	{
		if(isreloading)
		{
			float reloadremaining = m_ReloadInterval - Time.time + reload_timer;
			return reloadremaining >= 0 ? reloadremaining : 0;
		}
		return m_interval - fire_timer>=0 ? m_interval - fire_timer:0;
	}



	public bool IsReloading()
	{
		return isreloading;
	}

	public void Reload()
	{
		isreloading = true;
		reload_timer = Time.time;
	}

	public int GetShellCountPerClip()
	{
		return shellCountPerClip;
	}
}
