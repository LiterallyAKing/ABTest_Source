using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuManager : MonoBehaviour {


	public void StartGame() {
		GameObject.Find("SceneLoader").GetComponent<SceneLoader>().LoadScene("LaunchScene");
	}
}
