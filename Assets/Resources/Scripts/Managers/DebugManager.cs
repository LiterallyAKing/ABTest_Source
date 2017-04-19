using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugManager : MonoBehaviour {

	public SceneLoader sceneload;
	public AnalyticsManager analyticsman;

	public Button startbutton;
	public Toggle trackingtoggle;
	public Toggle experimenttoggle;

	public Text datafound;
	public Text datanotfound;

	public void TrackingToggle() {
		analyticsman.tracking = trackingtoggle.isOn;
		if (trackingtoggle.isOn) {
			experimenttoggle.isOn = false;
			analyticsman.experimentmode = experimenttoggle.isOn;
			experimenttoggle.interactable = false;
		} else {
			experimenttoggle.interactable = true;
		}
	}
	public void ExperimentToggle() {
		analyticsman.experimentmode = experimenttoggle.isOn;
	}


	public void StartGame() {
		//call the appropriate analytics manager routines here!!
		analyticsman.tracking = trackingtoggle.isOn;
		if (trackingtoggle.isOn) {
			analyticsman.experimentmode = experimenttoggle.isOn;
		}
		analyticsman.NewGameBegin();
		sceneload.LoadScene("GameOpening");
	}

	public void DataFileStatus(bool foundornot) {
		if (foundornot) {
			datafound.enabled = true;
		} else {
			datanotfound.enabled = true;
			startbutton.interactable = false;
		}
	}


}
