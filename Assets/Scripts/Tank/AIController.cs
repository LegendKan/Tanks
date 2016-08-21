using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour {

	public GameObject m_Enemy;

	[HideInInspector] public TankHealth health;
	[HideInInspector] public TankMovement movement;
	[HideInInspector] public TankShooting shooting;

	private AIController enemy_ai;

	// Use this for initialization
	void Start () {
		health = GetComponent<TankHealth> ();
		movement = GetComponent<TankMovement> ();
		shooting = GetComponent<TankShooting> ();
		enemy_ai = m_Enemy.GetComponent<AIController> ();
	}

	//获取一些不变的参数

	/// <summary>
	/// Gets the move speed.
	/// </summary>
	/// <returns>The move speed.</returns>
	public float GetMoveSpeed()
	{
		return movement.m_Speed;
	}

	/// <summary>
	/// Gets the shell damage.
	/// </summary>
	/// <returns>The shell damage.</returns>
	public float GetShellDamage()
	{
		return shooting.m_Damage;
	}

	/// <summary>
	/// Gets the starting health.
	/// </summary>
	/// <returns>The starting health.</returns>
	public float GetStartingHealth()
	{
		return health.m_StartingHealth;
	}

	/// <summary>
	/// Gets the body rotate speed.
	/// </summary>
	/// <returns>The body rotate speed.</returns>
	public float GetBodyRotateSpeed()
	{
		return movement.m_TurnSpeed;
	}

	/// <summary>
	/// Gets the turret rotate speed.
	/// </summary>
	/// <returns>The turret rotate speed.</returns>
	public float GetTurretRotateSpeed()
	{
		return movement.m_TurretSpeed;
	}

	/// <summary>
	/// Gets the transform.
	/// </summary>
	/// <returns>The transform.</returns>
	public Transform GetTransform()
	{
		return transform;
	}

	/// <summary>
	/// Gets the distance with enemy.
	/// </summary>
	/// <returns>The distance with enemy.</returns>
	public float GetDistanceWithEnemy()
	{
		return Vector3.Distance (transform.position, GetEnemyTransform().position);
	}

	/// <summary>
	/// Gets the current health.
	/// </summary>
	/// <returns>The current health.</returns>
	public float GetCurrentHealth()
	{
		return health.GetCurrentHealth ();
	}

	/// <summary>
	/// Gets the current health percentage.
	/// </summary>
	/// <returns>The current health percentage.</returns>
	public float GetCurrentHealthPercentage()
	{
		return health.GetCurrentHealth () / health.m_StartingHealth;
	}

	/// <summary>
	/// Gets the current shell count.
	/// </summary>
	/// <returns>The current shell count.</returns>
	public int GetCurrentShellCount()
	{
		return shooting.GetCurrentShellCount();	
	}

	/// <summary>
	/// Rotates the turret. called in OnUpdate.
	/// </summary>
	/// <param name="target">Target.</param>
	public void RotateTurret(Vector3 target)
	{
		movement.RotateTurret (target);
	}

	public bool IsAimed(Vector3 targetPosition)
	{
		return movement.IsAimed (targetPosition);
	}

	/// <summary>
	/// Rotates the tank body.
	/// </summary>
	/// <param name="target">Target.</param>
	public void RotateTankBody(Vector3 target)
	{
		movement.RotateBody (target);
	}

	/// <summary>
	/// Moves the forward.
	/// </summary>
	public void MoveForward()
	{
		movement.Forward ();
	}

	/// <summary>
	/// Moves the back ward.
	/// </summary>
	public void MoveBackWard()
	{
		movement.Backward ();
	}

	/// <summary>
	/// Fire this instance.
	/// </summary>
	public void Fire()
	{
		shooting.Fire ();
	}

	/// <summary>
	/// Gets the fire remaining.
	/// </summary>
	/// <returns>The fire remaining.</returns>
	public float GetFireRemaining()
	{
		return shooting.GetFireRemaining ();
	}

	/// <summary>
	/// Gets the reborn remaining.
	/// </summary>
	/// <returns>The reborn remaining.</returns>
	public float GetRebornRemaining()
	{
		return health.GetRebornRemaining ();
	}

	public bool IsAlive()
	{
		return health.isAlive ();
	}


	//获取敌方的一些参数
	/// <summary>
	/// Determines whether this instance is enemy alive.
	/// </summary>
	/// <returns><c>true</c> if this instance is enemy alive; otherwise, <c>false</c>.</returns>
	public bool IsEnemyAlive()
	{
		return enemy_ai.IsAlive ();
	}

	/// <summary>
	/// Gets the enemy reborn remaining.
	/// </summary>
	/// <returns>The enemy reborn remaining.</returns>
	public float GetEnemyRebornRemaining()
	{
		return enemy_ai.GetRebornRemaining();
	}

	/// <summary>
	/// Gets the enemy fire remaining.
	/// </summary>
	/// <returns>The enemy fire remaining.</returns>
	public float GetEnemyFireRemaining()
	{
		return enemy_ai.GetFireRemaining();
	}

	/// <summary>
	/// Gets the enemy current shell count.
	/// </summary>
	/// <returns>The enemy current shell count.</returns>
	public int GetEnemyCurrentShellCount()
	{
		return enemy_ai.GetCurrentShellCount();
	}

	/// <summary>
	/// Gets the enemy current health percentage.
	/// </summary>
	/// <returns>The enemy current health percentage.</returns>
	public float GetEnemyCurrentHealthPercentage()
	{
		return enemy_ai.GetCurrentHealthPercentage();
	}

	/// <summary>
	/// Gets the enemy current health.
	/// </summary>
	/// <returns>The enemy current health.</returns>
	public float GetEnemyCurrentHealth()
	{
		return enemy_ai.GetCurrentHealth ();
	}

	/// <summary>
	/// Gets the enemy transform.
	/// </summary>
	/// <returns>The enemy transform.</returns>
	public Transform GetEnemyTransform()
	{
		return enemy_ai.GetTransform ();
	}


}
