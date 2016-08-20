using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FollowCameraControl : MonoBehaviour {

	public Vector3 offsetPosition;

	//[HideInInspector] public Transform targetTransform;
	[HideInInspector] public TankManager tankManager;
	[HideInInspector] public Text m_MessageText;


	private void Start ()
	{
		
	}


	private void Update ()
	{
		transform.position = offsetPosition + tankManager.m_Instance.transform.position;
		if (!tankManager.m_Instance.activeSelf && GameManager.instance.gameState == GameManager.GameState.Playing) {
			//Debug.Log ("Should show canvas");
			m_MessageText.text = DeadMessage ();
			//m_MessageText.text = "Hello";
		} else if(GameManager.instance.gameState == GameManager.GameState.Playing) {
			m_MessageText.text = string.Empty;
		}
	}

	public void Active()
	{
		gameObject.SetActive(true);
	}

	public void Disable()
	{
		gameObject.SetActive(false);
	}

	private string DeadMessage()
	{
		float deadTime = tankManager.GetDeadTime ();
		//Debug.Log ("Dead Time "+deadTime);
		float remaining = tankManager.m_RebornDelay - Time.time + deadTime;
		int final = Mathf.CeilToInt (remaining);
		string message = string.Empty;
		if(remaining>0){
			message = "等待复活\n" + final + "s";
		}
		return message;
	}
}
