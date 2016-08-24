using System;
using UnityEngine;
using UnityEngine.UI;
using BehaviorDesigner;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


[Serializable]
public class TankManager
{
    // This class is to manage various settings on a tank.
    // It works with the GameManager class to control how the tanks behave
    // and whether or not players have control of their tank in the 
    // different phases of the game.

    public Color m_PlayerColor;                             // This is the color this tank will be tinted.
    public Transform m_SpawnPoint;                          // The position and direction the tank will have when it spawns.
	public float m_RebornDelay = 3f;
    public int m_PlayerNumber;            // This specifies which player this the manager for.
	public string m_PlayerName;
    [HideInInspector] public string m_ColoredPlayerText;    // A string that represents the player with their number colored to match their tank.
    public GameObject m_Instance;         // A reference to the instance of the tank when it is created.
    [HideInInspector] public int m_Wins;                    // The number of wins this player has so far.

	public Text m_ScoreText;
	public Slider m_HealthSlider;
	public Image m_PlayerImage;
	public Text m_DeadText;
	public BehaviorTree behaviorTree;
    public Slider m_ShellSlider;

    private TankMovement m_Movement;                        // Reference to tank's movement script, used to disable and enable control.
    private TankShooting m_Shooting;                        // Reference to tank's shooting script, used to disable and enable control.
	private TankHealth m_Health;
    private GameObject m_CanvasGameObject;                  // Used to disable the world space UI during the Starting and Ending phases of each round.


    public void Setup ()
    {
        // Get references to the components.
        m_Movement = m_Instance.GetComponent<TankMovement> ();
        m_Shooting = m_Instance.GetComponent<TankShooting> ();
		m_Health = m_Instance.GetComponent<TankHealth> ();
        m_CanvasGameObject = m_Instance.GetComponentInChildren<Canvas> ().gameObject;
		behaviorTree = m_Instance.GetComponent<BehaviorTree> ();

        // Set the player numbers to be consistent across the scripts.
        m_Movement.m_PlayerNumber = m_PlayerNumber;
        m_Shooting.m_PlayerNumber = m_PlayerNumber;
		m_Health.m_PlayerNumber = m_PlayerNumber;
		m_Health.reborn_delay = m_RebornDelay;

        // Create a string using the correct color that says 'PLAYER 1' etc based on the tank's color and the player's number.
		m_ColoredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(m_PlayerColor) + ">" + m_PlayerName + "</color>";

        // Get all of the renderers of the tank.
        MeshRenderer[] renderers = m_Instance.GetComponentsInChildren<MeshRenderer> ();

        // Go through all the renderers...
        for (int i = 0; i < renderers.Length; i++)
        {
            // ... set their material color to the color specific to this tank.
            renderers[i].material.color = m_PlayerColor;
        }

		m_Instance.transform.position = m_SpawnPoint.position;
		m_Instance.transform.rotation = m_SpawnPoint.rotation;
		m_HealthSlider.maxValue = m_Health.m_StartingHealth;
		m_HealthSlider.value = m_Health.m_StartingHealth;

        m_ShellSlider.maxValue = m_Shooting.shellCountPerClip;
        m_ShellSlider.value = m_Shooting.shellCountPerClip;

        

		if(behaviorTree!=null){
			behaviorTree.enabled = false;
		}

		//m_PlayerImage.color = m_PlayerColor;
    }

	//update UI or reborn tank
	public void Update()
	{
		if(!m_Health.isAlive())
		{
			float remaining = m_RebornDelay - Time.time + m_Health.deadTime;
			if (remaining <= 0) {
				Reset ();
				m_DeadText.text = string.Empty;
			} else {
				m_DeadText.text = ""+Mathf.CeilToInt (remaining);
			}
		}
		m_ScoreText.text = m_Wins + "";
		m_HealthSlider.value = m_Health.GetCurrentHealth ();

        if (m_Shooting.GetCurrentShellCount()==0)
        {
            //m_ShellSlider.enabled = false;
            m_ShellSlider.gameObject.SetActive(false);
        }
        else
        {
            //m_ShellSlider.enabled = true;
            m_ShellSlider.gameObject.SetActive(true);
            m_ShellSlider.value = m_Shooting.GetCurrentShellCount();
        }
        
	}


    // Used during the phases of the game where the player shouldn't be able to control their tank.
    public void DisableControl ()
    {
        m_Movement.enabled = false;
        m_Shooting.enabled = false;

		if(behaviorTree!=null){
			behaviorTree.enabled = false;
		}

        m_CanvasGameObject.SetActive (false);
    }


    // Used during the phases of the game where the player should be able to control their tank.
    public void EnableControl ()
    {
        m_Movement.enabled = true;
        m_Shooting.enabled = true;
		if(behaviorTree!=null){
			behaviorTree.enabled = true;
		}

        m_CanvasGameObject.SetActive (true);
    }


    // Used at the start of each round to put the tank into it's default state.
    public void Reset ()
    {
        m_Instance.transform.position = m_SpawnPoint.position;
        m_Instance.transform.rotation = m_SpawnPoint.rotation;

        m_Instance.SetActive (false);
        m_Instance.SetActive (true);
    }

	public float GetDeadTime()
	{
		return m_Health.deadTime;
	}
}
