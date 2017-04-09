using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetManager : MonoBehaviour {


	float timer = 1f;
	void Update () {
		timer -= Time.deltaTime;
		if (timer < 0) {
			Destroy(GameObject.Find("ManagerGroup"));
			SceneManager.LoadScene(0);
		}

	}
}
