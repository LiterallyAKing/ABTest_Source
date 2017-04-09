using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour {
	public GameObject launcher;
	public SpriteRenderer launchfire;
	public SpriteRenderer playerplaceholder;
	public InGameManager ingameman;
	public float angleMin, angleMax;
	public float rotateSpeed;

	[HideInInspector] public bool prelaunch = true;
	bool goingup = true;

	Quaternion original_rot;

	private void Start() {
		original_rot = launcher.transform.rotation;
	}

	public void StartPreLaunch() {

		prelaunch = true;
	}

	private void Update() {
		if (prelaunch) {
			if (goingup) {
				Quaternion newrot = launcher.transform.rotation;
				newrot = Quaternion.Euler(new Vector3(0, 0, newrot.eulerAngles.z + (rotateSpeed * Time.deltaTime)));
				launcher.transform.rotation = newrot;
				if (launcher.transform.rotation.eulerAngles.z < 360f - angleMax && launcher.transform.rotation.eulerAngles.z > angleMax) goingup = false;
			} else {
				Quaternion newrot = launcher.transform.rotation;
				newrot = Quaternion.Euler(new Vector3(0, 0, newrot.eulerAngles.z - (rotateSpeed * Time.deltaTime)));
				launcher.transform.rotation = newrot;
				if ((launcher.transform.rotation.eulerAngles.z < angleMax && launcher.transform.rotation.eulerAngles.z < angleMin) || (launcher.transform.rotation.eulerAngles.z > 360f - angleMax && launcher.transform.rotation.eulerAngles.z < 360f + angleMin)) goingup = true;

			}


		}
		if (prelaunch) {
			if (Input.GetKeyDown(KeyCode.Space)) {
				prelaunch = false;
				StartLaunch();
				
			}
		}


	}

	void StartLaunch() {
		playerplaceholder.enabled = false;
		launchfire.enabled = true;
		ingameman.LaunchHit(playerplaceholder.transform.position, launcher.transform.rotation.eulerAngles.z);
		Invoke("FireOut", 1.5f);
	}

	void FireOut() {
		launchfire.enabled = false;
	}

	public void ResetForNextLaunch() {
		if (launchfire.enabled) {
			CancelInvoke("FireOut");
			launchfire.enabled = false;
		}
		playerplaceholder.enabled = true;
		launcher.transform.rotation = original_rot;
	}

}
