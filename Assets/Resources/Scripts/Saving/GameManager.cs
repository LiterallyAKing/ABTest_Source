using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public ResetManager resetbomb;
	public AnalyticsManager analyticsman;

	void Start () {
		
	}
	
	void Update () {
		ListenForDebugReset();


	}




	//Reset System:

	bool[] nums123hit = new bool[3] { false, false, false };
	bool[] nums000hit = new bool[3] { false, false, false };
	float[] hittimers = new float[2] { 0, 0};
	void ListenForDebugReset() {
		ListenFor1230();
		if (nums123hit[0]) {
			hittimers[0] += Time.deltaTime;
		}
		if (nums123hit[0] && nums123hit[1] && nums123hit[2]) {
			hittimers[0] = 0;
			nums123hit[0] = false;
			nums123hit[1] = false;
			nums123hit[2] = false;
			analyticsman.NewGameEnd();
			analyticsman.NewGameBegin();
			analyticsman.sceneload.LoadScene("GameOpening");
			
			//TODO: Reset for new player functionality
		}
		if (hittimers[0] > 1f) {
			hittimers[0] = 0;
			nums123hit[0] = false;
			nums123hit[1] = false;
			nums123hit[2] = false;
		}

		if (nums000hit[0]) {
			hittimers[1] += Time.deltaTime;
		}
		if (nums000hit[0] && nums000hit[1] && nums000hit[2]) {
			hittimers[1] = 0;
			nums000hit[0] = false;
			nums000hit[1] = false;
			nums000hit[2] = false;
			analyticsman.NewGameEnd();
			ResetToDebugMenu();
			return;
		}
		if (hittimers[1] > 1f) {
			hittimers[1] = 0;
			nums000hit[0] = false;
			nums000hit[1] = false;
			nums000hit[2] = false;
		}
	}

	void ListenFor1230() {
		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			nums123hit[0] = true;
		}
		if (Input.GetKeyDown(KeyCode.Alpha2)) {
		if(nums123hit[0]) nums123hit[1] = true;
		}
		if (Input.GetKeyDown(KeyCode.Alpha3)) {
			if (nums123hit[0] && nums123hit[1]) {
				nums123hit[2] = true;
			}
		}
		if (Input.GetKeyDown(KeyCode.Alpha0)) {
			if (!nums000hit[0]) {
				nums000hit[0] = true;
			} else if (!nums000hit[1]) {
				nums000hit[1] = true;
			} else if (!nums000hit[2]) {
				nums000hit[2] = true;
			}
		}
	}
	public void ResetToDebugMenu() {
		ResetManager reset = Instantiate<ResetManager>(resetbomb);
	}

}
