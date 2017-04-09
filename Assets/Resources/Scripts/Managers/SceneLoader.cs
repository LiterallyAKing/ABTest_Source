using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

	public void LoadScene(string scenename){
		SceneManager.LoadScene (scenename, LoadSceneMode.Single);
	}

	bool eschit = false;
	float timer = 0;
	private void Update() {
		if (eschit && Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}

		if (!eschit && Input.GetKeyDown(KeyCode.Escape)) {
			eschit = true;
		}
		if (eschit) {
			timer += Time.deltaTime;

		}



		if (eschit && timer > 1.5f) {
			eschit = false;
			timer = 0;
		}


	}

}
