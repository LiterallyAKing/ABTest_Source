using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DistanceTracker : MonoBehaviour {

	public Transform gamecamera;
	Vector3 gamecam_initialpos;
	public InGameManager ingameman;
	public PlayerMoveController Player;
	[HideInInspector] public Vector3 initialpos;
	public Text textbox;
	[HideInInspector] public float score = 0;
	[HideInInspector] public bool gameRunning = false;
	public float timeBeforeReset = 2f;
	float resetTimer = 0;

	Vector3 oldpos = new Vector3();

	
	void Start () {
		initialpos = Player.transform.position;
		gamecam_initialpos = gamecamera.position;
	}

	void Update() {
		if (gameRunning) {
			Vector3 dist = Player.transform.position - initialpos;
			score = dist.x;
			textbox.text = Mathf.RoundToInt(score).ToString();


			if (Mathf.RoundToInt(Player.transform.position.x) == Mathf.RoundToInt(oldpos.x)) {
				resetTimer += Time.deltaTime;
				if (resetTimer > timeBeforeReset / 3f) {
					Player.CameToStop();
				}
				if (resetTimer > timeBeforeReset) {
					ingameman.PlayerHasStopped();
					
				}
			} else {
				resetTimer = 0;
			}
			oldpos = Player.transform.position;
		}
	}

	public void Reset (){
		score = 0;
		gameRunning = false;
		resetTimer = 0;
		//Player.transform.position = initialpos;
		gamecamera.position = gamecam_initialpos;
		textbox.text = Mathf.RoundToInt(score).ToString();
	}
}
