using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour {

	public GameObject m_Enemy;
	public Environment m_Environment;

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
	/// Gets all barriers.
	/// </summary>
	/// <returns>The all barriers.</returns>
	public GameObject[] GetAllBarriers(){
		return m_Environment.GetBarriers ();
	}

	/// <summary>
	/// Gets the nearest barrier.
	/// </summary>
	/// <returns>The nearest barrier.</returns>
	/// <param name="position">Position.</param>
	public GameObject GetNearestBarrier(Vector3 position)
	{
		return m_Environment.GetNearestBarrier (position);
	}

	/// <summary>
	/// Gets the property transforms.
	/// </summary>
	/// <returns>The property transforms.</returns>
	public Transform[] GetPropTransforms()
	{
		return m_Environment.PropTransforms;
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

	/// <summary>
	/// Gets all tools transforms.返回所有的道具生成点
	/// </summary>
	/// <returns>The all tools transforms.</returns>
	public Transform[] GetAllToolsTransforms()
	{
		return ToolsManager.instance.GetAllSpawnTransforms ();
	}

	/// <summary>
	/// Gets the current health transform.返回血量道具的transform，如果没有血量道具则返回null;
	/// </summary>
	/// <returns>The current health transform.</returns>
	public Transform GetCurrentHealthTransform()
	{
		return ToolsManager.instance.GetCurrentHealthTransform ();
	}

	/// <summary>
	/// Gets the next tools remaining.下次道具生成的剩余时间，返回值>=0.
	/// </summary>
	/// <returns>The next tools remaining.</returns>
	public float GetNextToolsRemaining()
	{
		return ToolsManager.instance.GetRemainingTime ();
	}

	/// <summary>
	/// Determines whether this instance has health bag.是否有血量道具
	/// </summary>
	/// <returns><c>true</c> if this instance has health bag; otherwise, <c>false</c>.</returns>
	public bool HasHealthBag()
	{
		return ToolsManager.instance.HasTools ();
	}

	/// <summary>
	/// Gets the health bag amount.返回血量道具的增加血量的数量。
	/// </summary>
	/// <returns>The health bag amount.</returns>
	public float GetHealthBagAmount()
	{
		return ToolsManager.instance.GetHealthBagAmount();
	}

	/// <summary>
	/// Gets the tools interval.获取道具刷新时间间隔。
	/// </summary>
	/// <returns>The tools interval.</returns>
	public float GetToolsInterval()
	{
		return ToolsManager.instance.m_Interval;
	}

	/// <summary>
	/// Gets the shell range.获取炮弹的攻击范围
	/// </summary>
	/// <returns>The shell range.</returns>
	public float GetShellRange()
	{
		return shooting.m_MaxRange;
	}

	/// <summary>
	/// Gets the shell count per clip.获取每个弹夹的弹夹数量，游戏中不会发生变化。
	/// </summary>
	/// <returns>The shell count per clip.</returns>
	public int GetShellCountPerClip()
	{
		return shooting.GetShellCountPerClip ();
	}

	/// <summary>
	/// Gets the reload interval.获取换弹夹的耗时，游戏过程中不会发生变化。
	/// </summary>
	/// <returns>The reload interval.</returns>
	public float GetReloadInterval()
	{
		return shooting.m_ReloadInterval;
	}

	/// <summary>
	/// Reloads the clip.执行换弹夹动作，该函数立刻返回。
	/// </summary>
	public void ReloadClip()
	{
		shooting.Reload ();
	}

	/// <summary>
	/// Determines whether this instance is reloading.返回是否正在换弹夹，换弹夹是耗时的。
	/// </summary>
	/// <returns><c>true</c> if this instance is reloading; otherwise, <c>false</c>.</returns>
	public bool IsReloading()
	{
		return shooting.IsReloading ();
	}

	/// <summary>
	/// Gets the reborn protect time.获取重生保护的时间,"游戏过程中不会发生变化"
	/// </summary>
	/// <returns>The reborn protect time.</returns>
	public float GetRebornProtectTime()
	{
		return health.m_RebornProtectTime;
	}

	/// <summary>
	/// Determines whether this instance is reborn protected.自己是否在重生保护中。
	/// </summary>
	/// <returns><c>true</c> if this instance is reborn protected; otherwise, <c>false</c>.</returns>
	public bool IsRebornProtected()
	{
		return health.IsRebornProtected();
	}

	/// <summary>
	/// Gets the reborn protect remaining.返回自己重生保护的剩余时间
	/// </summary>
	/// <returns>The reborn protect remaining.</returns>
	public float GetRebornProtectRemaining()
	{
		return health.GetRebornProtectRemaining ();
	}

	/// <summary>
	/// Determines whether this instance is enemy reborn protected.敌人是否在重生保护中。
	/// </summary>
	/// <returns><c>true</c> if this instance is enemy reborn protected; otherwise, <c>false</c>.</returns>
	public bool IsEnemyRebornProtected()
	{
		return enemy_ai.IsRebornProtected();
	}

	/// <summary>
	/// Gets the enemy reborn protect remaining.获取敌方重生保护的剩余时间。
	/// </summary>
	/// <returns>The enemy reborn protect remaining.</returns>
	public float GetEnemyRebornProtectRemaining()
	{
		return enemy_ai.GetRebornProtectRemaining ();
	}


}
