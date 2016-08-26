<<<<<<< HEAD
﻿using UnityEngine;


public class TankMovement : MonoBehaviour
{
    public int m_PlayerNumber = 1;              // Used to identify which tank belongs to which player.  This is set by this tank's manager.
    public float m_Speed = 12f;                 // How fast the tank moves forward and back.
    public float m_TurnSpeed = 180f;            // How fast the tank turns in degrees per second.
	public float m_TurretSpeed = 180f;			//炮塔旋转速度
    public AudioSource m_MovementAudio;         // Reference to the audio source used to play engine sounds. NB: different to the shooting audio source.
    public AudioClip m_EngineIdling;            // Audio to play when the tank isn't moving.
    public AudioClip m_EngineDriving;           // Audio to play when the tank is moving.
	public float m_PitchRange = 0.2f;           // The amount by which the pitch of the engine noises can vary.


    private string m_MovementAxisName;          // The name of the input axis for moving forward and back.
    private string m_TurnAxisName;              // The name of the input axis for turning.
    private Rigidbody m_Rigidbody;              // Reference used to move the tank.
    private float m_MovementInputValue;         // The current value of the movement input.
    private float m_TurnInputValue;             // The current value of the turn input.
    private float m_OriginalPitch;              // The pitch of the audio source at the start of the scene.
	private float m_MouseInput;

	private Transform turret;


    private void Awake ()
    {
        m_Rigidbody = GetComponent<Rigidbody> ();
    }


    private void OnEnable ()
    {
        // When the tank is turned on, make sure it's not kinematic.
        m_Rigidbody.isKinematic = false;

        // Also reset the input values.
        m_MovementInputValue = 0f;
        m_TurnInputValue = 0f;
    }


    private void OnDisable ()
    {
        // When the tank is turned off, set it to kinematic so it stops moving.
        m_Rigidbody.isKinematic = true;
    }


    private void Start ()
    {
        // The axes names are based on player number.
        m_MovementAxisName = "Vertical" + m_PlayerNumber;
        m_TurnAxisName = "Horizontal" + m_PlayerNumber;

        // Store the original pitch of the audio source.
        m_OriginalPitch = m_MovementAudio.pitch;

		turret = transform.Find ("TankRenderers/TankTurret");

    }


    private void Update ()
    {
        // Store the value of both input axes.
        m_MovementInputValue = Input.GetAxis (m_MovementAxisName);
        m_TurnInputValue = Input.GetAxis (m_TurnAxisName);

		m_MouseInput = Input.GetAxisRaw("Mouse X");

        EngineAudio ();
    }


    private void EngineAudio ()
    {
        // If there is no input (the tank is stationary)...
        if (Mathf.Abs (m_MovementInputValue) < 0.1f && Mathf.Abs (m_TurnInputValue) < 0.1f)
        {
            // ... and if the audio source is currently playing the driving clip...
            if (m_MovementAudio.clip == m_EngineDriving)
            {
                // ... change the clip to idling and play it.
                m_MovementAudio.clip = m_EngineIdling;
                m_MovementAudio.pitch = Random.Range (m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                m_MovementAudio.Play ();
            }
        }
        else
        {
            // Otherwise if the tank is moving and if the idling clip is currently playing...
            if (m_MovementAudio.clip == m_EngineIdling)
            {
                // ... change the clip to driving and play.
                m_MovementAudio.clip = m_EngineDriving;
                m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                m_MovementAudio.Play();
            }
        }
    }


    private void FixedUpdate ()
    {
        // Adjust the rigidbodies position and orientation in FixedUpdate.
        Move ();
        Turn ();
		TurretTurn ();

		//Debug.Log("Turret Rotation: "+turret.rotation.ToString());
    }


    private void Move ()
    {
        // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
        Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;

        // Apply this movement to the rigidbody's position.
        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
    }

=======
﻿using UnityEngine;


public class TankMovement : MonoBehaviour
{
    public int m_PlayerNumber = 1;              // Used to identify which tank belongs to which player.  This is set by this tank's manager.
    public float m_Speed = 12f;                 // How fast the tank moves forward and back.
    public float m_TurnSpeed = 180f;            // How fast the tank turns in degrees per second.
	public float m_TurretSpeed = 180f;			//炮塔旋转速度
    public AudioSource m_MovementAudio;         // Reference to the audio source used to play engine sounds. NB: different to the shooting audio source.
    public AudioClip m_EngineIdling;            // Audio to play when the tank isn't moving.
    public AudioClip m_EngineDriving;           // Audio to play when the tank is moving.
	public float m_PitchRange = 0.2f;           // The amount by which the pitch of the engine noises can vary.


    private string m_MovementAxisName;          // The name of the input axis for moving forward and back.
    private string m_TurnAxisName;              // The name of the input axis for turning.
    private Rigidbody m_Rigidbody;              // Reference used to move the tank.
    private float m_MovementInputValue;         // The current value of the movement input.
    private float m_TurnInputValue;             // The current value of the turn input.
    private float m_OriginalPitch;              // The pitch of the audio source at the start of the scene.
	private float m_MouseInput;

	private Transform turret;


    private void Awake ()
    {
        m_Rigidbody = GetComponent<Rigidbody> ();
    }


    private void OnEnable ()
    {
        // When the tank is turned on, make sure it's not kinematic.
        m_Rigidbody.isKinematic = false;

        // Also reset the input values.
        m_MovementInputValue = 0f;
        m_TurnInputValue = 0f;
    }


    private void OnDisable ()
    {
        // When the tank is turned off, set it to kinematic so it stops moving.
        m_Rigidbody.isKinematic = true;
    }


    private void Start ()
    {
        // The axes names are based on player number.
        m_MovementAxisName = "Vertical" + m_PlayerNumber;
        m_TurnAxisName = "Horizontal" + m_PlayerNumber;

        // Store the original pitch of the audio source.
        m_OriginalPitch = m_MovementAudio.pitch;

		turret = transform.Find ("TankRenderers/TankTurret");

    }


    private void Update ()
    {
        // Store the value of both input axes.
        m_MovementInputValue = Input.GetAxis (m_MovementAxisName);
        m_TurnInputValue = Input.GetAxis (m_TurnAxisName);

		m_MouseInput = Input.GetAxisRaw("Mouse X");

        EngineAudio ();
    }


    private void EngineAudio ()
    {
        // If there is no input (the tank is stationary)...
        if (Mathf.Abs (m_MovementInputValue) < 0.1f && Mathf.Abs (m_TurnInputValue) < 0.1f)
        {
            // ... and if the audio source is currently playing the driving clip...
            if (m_MovementAudio.clip == m_EngineDriving)
            {
                // ... change the clip to idling and play it.
                m_MovementAudio.clip = m_EngineIdling;
                m_MovementAudio.pitch = Random.Range (m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                m_MovementAudio.Play ();
            }
        }
        else
        {
            // Otherwise if the tank is moving and if the idling clip is currently playing...
            if (m_MovementAudio.clip == m_EngineIdling)
            {
                // ... change the clip to driving and play.
                m_MovementAudio.clip = m_EngineDriving;
                m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                m_MovementAudio.Play();
            }
        }
    }


    private void FixedUpdate ()
    {
        // Adjust the rigidbodies position and orientation in FixedUpdate.
        Move ();
        Turn ();
		//TurretTurn ();

		//Debug.Log("Turret Rotation: "+turret.rotation.ToString());
    }


    private void Move ()
    {
        // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
        Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;

        // Apply this movement to the rigidbody's position.
        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
    }

>>>>>>> a052cf5899697696a3d913d1b7ba41e438c6ac67
    public void MoveToPosition(Vector3 targetPosition)
    {
        Vector3 movementOffset = (targetPosition - m_Rigidbody.position) * m_Speed * Time.deltaTime;
        m_Rigidbody.MovePosition(m_Rigidbody.position + movementOffset);
<<<<<<< HEAD
    }


    private void Turn ()
    {
        // Determine the number of degrees to be turned based on the input, speed and time between frames.
        float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;

        // Make this into a rotation in the y axis.
        Quaternion turnRotation = Quaternion.Euler (0f, turn, 0f);

        // Apply this rotation to the rigidbody's rotation.
        m_Rigidbody.MoveRotation (m_Rigidbody.rotation * turnRotation);
    }

	public void Forward()
	{
		Vector3 movement = transform.forward * m_Speed * Time.deltaTime;
		// Apply this movement to the rigidbody's position.
		m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
	}

	public void Backward()
	{
		Vector3 movement = transform.forward * (-1) * m_Speed * Time.deltaTime;
		// Apply this movement to the rigidbody's position.
		m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
	}

	private void TurretTurn ()
	{
		turret.Rotate (0, m_MouseInput * 2, 0);
	}

	//炮塔旋转
	public void RotateTurret(Vector3 targetPosition)
	{
		Vector3 offset = targetPosition - turret.position;
		offset.y = 0;//turret.position.y
		Quaternion to = Quaternion.LookRotation(offset, Vector3.up);
		turret.rotation = Quaternion.RotateTowards(turret.rotation, to, m_TurretSpeed * Time.deltaTime);
	}

	public bool IsAimed(Vector3 targetPosition)
	{
		Vector3 offset = targetPosition - turret.position;
		offset.y = 0;//
		Quaternion to = Quaternion.LookRotation(offset, Vector3.up);
		return turret.rotation == to;
	}

	public void RotateBody(Vector3 targetPosition)
	{
		Vector3 offset = targetPosition - transform.position;
		offset.y = transform.position.y;
		Quaternion to = Quaternion.LookRotation(offset, Vector3.up);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, to, m_TurnSpeed * Time.deltaTime);
	}

	public Quaternion GetTurretRotation()
	{
		return turret.rotation;
	}
}
=======
    }


    private void Turn ()
    {
        // Determine the number of degrees to be turned based on the input, speed and time between frames.
        float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;

        // Make this into a rotation in the y axis.
        Quaternion turnRotation = Quaternion.Euler (0f, turn, 0f);

        // Apply this rotation to the rigidbody's rotation.
        m_Rigidbody.MoveRotation (m_Rigidbody.rotation * turnRotation);
    }

	public void Forward()
	{
		Vector3 movement = transform.forward * m_Speed * Time.deltaTime;
		// Apply this movement to the rigidbody's position.
		m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
	}

	public void Backward()
	{
		Vector3 movement = transform.forward * (-1) * m_Speed * Time.deltaTime;
		// Apply this movement to the rigidbody's position.
		m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
	}

	private void TurretTurn ()
	{
		turret.Rotate (0, m_MouseInput * 2, 0);
	}

	//炮塔旋转
	public void RotateTurret(Vector3 targetPosition)
	{
		Vector3 offset = targetPosition - turret.position;
		offset.y = 0;//turret.position.y
		Quaternion to = Quaternion.LookRotation(offset, Vector3.up);
		turret.rotation = Quaternion.RotateTowards(turret.rotation, to, m_TurretSpeed * Time.deltaTime);
	}

	public bool IsAimed(Vector3 targetPosition)
	{
		//Debug.Log ("turret position: "+turret.position.ToString());
		//Debug.Log ("IsAimed: "+targetPosition.ToString());
		Vector3 offset = targetPosition - turret.position;
		offset.y = 0;//
		Quaternion to = Quaternion.LookRotation(offset, Vector3.up);
		return turret.rotation == to;
	}

	public void RotateBody(Vector3 targetPosition)
	{
		Vector3 offset = targetPosition - transform.position;
		offset.y = transform.position.y;
		Quaternion to = Quaternion.LookRotation(offset, Vector3.up);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, to, m_TurnSpeed * Time.deltaTime);
	}

	public Quaternion GetTurretRotation()
	{
		return turret.rotation;
	}
}
>>>>>>> a052cf5899697696a3d913d1b7ba41e438c6ac67
