using UnityEngine;
using System.Collections;
using System;

public class HealthTool : MonoBehaviour {
	public event EventHandler OnCollected;
	[HideInInspector]
    public float m_HealthBag = 100f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	private void OnTriggerEnter(Collider other)
	{
		//Debug.Log ("++++++++++++++++++++++++++++++++++++++++");
		TankHealth health = other.gameObject.GetComponent<TankHealth> ();
		if (health != null) {
			//Debug.Log ("------------------------------------");
			health.AddHealth (m_HealthBag);
            //play picking health animation
            OnGetHealthParticleSys(other.gameObject);
			if(OnCollected != null)
			{
				OnCollected (this,EventArgs.Empty);
			}
		}
	}

    private void OnGetHealthParticleSys(GameObject gameObject)
    {
        //play the animation of getting health bag
        Animation cureAnimation = this.gameObject.GetComponent<Animation>();
        Debug.Assert(null != cureAnimation);
        cureAnimation.Play();

        //play the audio of getting health bag
        AudioSource cureAudio = this.gameObject.GetComponent<AudioSource>();
        Debug.Assert(null != cureAudio);
        cureAudio.Play();

        foreach(Transform child in gameObject.transform)
        {
            if(child.name.Equals("GetHealthBagParticle"))
            {
                ParticleSystem particleSystem = child.GetComponent<ParticleSystem>();
                particleSystem.Play();
            }
        }
    }

    public void DestroyHeart()
    {
        Destroy(gameObject);
    }
}
