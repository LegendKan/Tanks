using UnityEngine;

public class ShellExplosion : MonoBehaviour
{
	public int m_PlayerNumber = 1; 
    public LayerMask m_TankMask;                        // Used to filter what the explosion affects, this should be set to "Players".
    public ParticleSystem m_ExplosionParticles;         // Reference to the particles that will play on explosion.
    public AudioSource m_ExplosionAudio;                // Reference to the audio that will play on explosion.
	[HideInInspector]public float m_MaxDamage = 100f;                    // The amount of damage done if the explosion is centred on a tank.
	[HideInInspector]public Vector3 startPostion;
	[HideInInspector]public float shellRange;
    public float m_ExplosionForce = 0f;              // The amount of force added to a tank at the centre of the explosion.
    public float m_MaxLifeTime = 2f;                    // The time in seconds before the shell is removed.
    public float m_ExplosionRadius = 3f;                // The maximum distance away from the explosion tanks can be and are still affected.


    private void Start ()
    {
        // If it isn't destroyed by then, destroy the shell after it's lifetime.
        //Destroy (gameObject, m_MaxLifeTime);
    }

	private void Update()
	{
		if(Vector3.SqrMagnitude(transform.position - startPostion)>=shellRange)
		{
			Destroy (gameObject);
		}
	}

    private void OnTriggerEnter (Collider other)
    {

		/*
		// Collect all the colliders in a sphere from the shell's current position to a radius of the explosion radius.
        Collider[] colliders = Physics.OverlapSphere (transform.position, m_ExplosionRadius, m_TankMask);

        // Go through all the colliders...
        for (int i = 0; i < colliders.Length; i++)
        {
            // ... and find their rigidbody.
			//flag = false;
            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody> ();

            // If they don't have a rigidbody, go on to the next collider.
            if (!targetRigidbody)
                continue;
            // Add an explosion force.
            //targetRigidbody.AddExplosionForce (m_ExplosionForce, transform.position, m_ExplosionRadius);

            // Find the TankHealth script associated with the rigidbody.
            TankHealth targetHealth = targetRigidbody.GetComponent<TankHealth> ();

            // If there is no TankHealth script attached to the gameobject, go on to the next collider.
			if (!targetHealth || targetHealth.m_PlayerNumber == m_PlayerNumber) {
				continue;
			}     

            // Calculate the amount of damage the target should take based on it's distance from the shell.
            float damage = CalculateDamage (targetRigidbody.position);

            // Deal this damage to the tank.
			targetHealth.TakeDamage (m_PlayerNumber,damage);
        }
		*/

		TankHealth tmp = other.GetComponentInParent<TankHealth> ();
		if( (tmp && tmp.m_PlayerNumber == m_PlayerNumber) || other.GetComponentInParent<GameTools>()!=null || other.gameObject.tag == "Shell")
		{
			return;
		}

		//Debug.Log ("================"+other.gameObject.tag);

		Rigidbody targetRigidbody = other.GetComponent<Rigidbody> ();

		// If they don't have a rigidbody, go on to the next collider.
		if (targetRigidbody != null) {
			// Find the TankHealth script associated with the rigidbody.
			TankHealth targetHealth = targetRigidbody.GetComponent<TankHealth> ();

			// If there is no TankHealth script attached to the gameobject, go on to the next collider.
			if (targetHealth!=null && targetHealth.m_PlayerNumber != m_PlayerNumber) {
				// Calculate the amount of damage the target should take based on it's distance from the shell.
				//float damage = CalculateDamage (targetRigidbody.position);//根据距离计算伤害值

				// Deal this damage to the tank.
				targetHealth.TakeDamage (m_PlayerNumber,m_MaxDamage);
			}
		}
        // Unparent the particles from the shell.
        m_ExplosionParticles.transform.parent = null;

        // Play the particle system.
        m_ExplosionParticles.Play();

        // Play the explosion sound effect.
        m_ExplosionAudio.Play();

        // Once the particles have finished, destroy the gameobject they are on.
        Destroy (m_ExplosionParticles.gameObject, m_ExplosionParticles.duration);

        // Destroy the shell.
        Destroy (gameObject);
    }


    private float CalculateDamage (Vector3 targetPosition)
    {
        // Create a vector from the shell to the target.
        Vector3 explosionToTarget = targetPosition - transform.position;

        // Calculate the distance from the shell to the target.
        float explosionDistance = explosionToTarget.magnitude;

        // Calculate the proportion of the maximum distance (the explosionRadius) the target is away.
        float relativeDistance = (m_ExplosionRadius - explosionDistance) / m_ExplosionRadius;

        // Calculate damage as this proportion of the maximum possible damage.
        float damage = relativeDistance * m_MaxDamage;

        // Make sure that the minimum damage is always 0.
        damage = Mathf.Max (0f, damage);

        return damage;
    }
}
